using GroceryBama.Entities;
using GroceryBama.MySqlScripts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GroceryBama.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StoresController : Controller
    {
        private StoresScript storesScript;

        public StoresController()
        {
            storesScript = new StoresScript();
        }
        [AllowAnonymous]
        [HttpGet("GetStores")]
        public ActionResult GetStores()
        {
            try
            {
                return Json(new BasePacket(true, storesScript.GetStores()));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }

        [AllowAnonymous]
        [HttpPost("SwitchStore")]
        public ActionResult SwitchStore([FromBody]int groceryId)
        {
            try
            {

                if (User.Identity.IsAuthenticated) storesScript.SwitchStore(User.Identity.Name, groceryId);
                return Json(new BasePacket(true, null));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }

        [AllowAnonymous]
        [HttpGet("GetItems")]
        public ActionResult GetItems(int groceryId, int startIndex, int endIndex, string foodGroup)
        {
            try
            {
                return Json(new BasePacket(true, storesScript.GetItems(groceryId, startIndex, endIndex, foodGroup)));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }

        [Authorize(Roles = "buyer")]
        [HttpGet("GetCartItems")]
        public JsonResult GetCartItems(int groceryId)
        {
            try
            {
                return Json(new BasePacket(true, storesScript.GetCartItems(User.Identity.Name, groceryId).Items));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }

        [Authorize(Roles = "buyer")]
        [HttpPost("AddToCart")]
        public JsonResult AddToCart([FromBody] CartParams Params)
        {
            try
            {
                int cartQuantity = storesScript.AddItemToCart(User.Identity.Name, Params.groceryId, Params.itemId, Params.quantity);
                return Json(new BasePacket(true, new { cartQuantity = cartQuantity }));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }

        [Authorize(Roles = "buyer")]
        [HttpGet("GetCartQuantity")]
        public JsonResult GetCartQuantity(int groceryId)
        {
            try
            {
                int quantity = storesScript.GetCartQuantity(User.Identity.Name, groceryId);
                return Json(new BasePacket(true, new { CartQuantity = quantity }));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }

        [Authorize(Roles = "buyer")]
        [HttpPost("RemoveItemFromCart")]
        public ActionResult RemoveItemFromCart([FromBody]CartParams Params)
        {
            try
            {
                int groceryId = Params.groceryId;
                int itemId = Params.itemId;
                return Json(new BasePacket(true, storesScript.RemoveItemFromCart(User.Identity.Name, groceryId, itemId)));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }

        [Authorize(Roles = "buyer")]
        [HttpPost("UpdateCartItemQuantity")]
        public JsonResult UpdateCartItemQuantity([FromBody] CartParams Params)
        {
            try
            {
                return Json(new BasePacket(true, storesScript.UpdateCartItemQuantity(User.Identity.Name, Params.groceryId, Params.itemId, Params.quantity)));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }

        [Authorize(Roles = "buyer")]
        [HttpPost("Checkout")]
        public JsonResult Checkout([FromBody] Order order)
        {
            try
            {
                return Json(new BasePacket(true, storesScript.Checkout(User.Identity.Name, order.GroceryId, order.RequestDeliveryTime, order.DeliveryInstructions, order.PaymentMethodId)));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }

        [Authorize(Roles = "buyer,deliverer")]
        [HttpGet("GetOrderDetail/{orderId}")]
        public JsonResult GetOrderDetail(int orderId)
        {
            return Json(new BasePacket(true, storesScript.GetOrderDetail(User.Identity.Name, orderId)));
        }

        [Authorize(Roles = "buyer,deliverer")]
        [HttpGet("GetOrders")]
        public ActionResult GetOrders(int startIndex, int endIndex)
        {
            try
            {
                return Json(new BasePacket(true, storesScript.GetOrders(User.Identity.Name, startIndex, endIndex)));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }

        [Authorize(Roles = "buyer,deliverer")]
        [HttpPost("UpdateOrderStatus")]
        public ActionResult UpdateOrderStatus([FromBody]Order order)
        {
            try
            {
                int orderId = order.OrderId, orderStatus = order.Status;
                JsonResult permissionDeniedPacket = Json(new BasePacket(false, 7000, "Permission denied"));
                // Out of range
                if (orderStatus <= 0 || orderStatus > 3) return permissionDeniedPacket;
                // When a buyer try to update order to driver on the way or deliveried
                if (orderStatus > 0 && orderStatus < 3 && !User.IsInRole("deliverer")) return permissionDeniedPacket;
                // When a deliverer try to cancel an order
                if (orderStatus == 3 && !User.IsInRole("buyer")) return permissionDeniedPacket;
                // When a user try to cancel an order that not in waiting status
                // TODO
                storesScript.UpdateOrderStatus(User.Identity.Name, orderId, orderStatus);
                return Json(new BasePacket(true, null));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }

        [Authorize(Roles = "manager")]
        [HttpGet("GetOutstandingOrders")]
        public ActionResult GetOutstandingOrders(int groceryId, int startIndex, int endIndex)
        {
            try
            {
                return Json(new BasePacket(true, storesScript.GetOutstandingOrders(groceryId, startIndex, endIndex)));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }

        [Authorize(Roles = "manager")]
        [HttpGet("GetStatistic")]
        public ActionResult GetStatistic(int groceryId)
        {
            try
            {
                return Json(new BasePacket(true, storesScript.GetStatistic(groceryId)));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }
        [Authorize(Roles = "manager")]
        [HttpPost("UpdateInventoryItemQuantity")]
        public ActionResult UpdateInventoryItemQuantity([FromBody] ItemParams itemParams)
        {
            try
            {
                storesScript.UpdateInventoryItemQuantity(itemParams.ItemId, itemParams.Quantity);
                return Json(new BasePacket(true, null));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }
        [Authorize(Roles = "manager")]
        [HttpPost("DeleteItemFromInventory")]
        public ActionResult DeleteItemFromInventory([FromBody] ItemParams itemParams)
        {
            try
            {
                storesScript.DeleteItemFromInventory(itemParams.GroceryId, itemParams.ItemId);
                return Json(new BasePacket(true, null));
            }
            catch (Exception ex)
            {
                return Json(new ErrorHandler(ex).ToBasePacket());
            }
        }
        protected override void Dispose(bool disposing)
        {
            storesScript.Dispose();
            base.Dispose(disposing);
        }
    }
}