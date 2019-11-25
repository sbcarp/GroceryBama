using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GroceryBama.Controllers
{
    [DataContract]
    public class BasePacket
    {
        public bool Success { get; set; }
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }

        public BasePacket(bool isSuccess, int errorCode)
        {
            Success = isSuccess;
            ErrorCode = errorCode;
        }
        public BasePacket(bool isSuccess, string message)
        {
            Success = isSuccess;
            Message = message;
        }
        public BasePacket(bool isSuccess, dynamic data)
        {
            Success = isSuccess;
            Data = data;
        }
        
        public BasePacket(bool isSuccess, int errorCode, string message)
        {
            Success = isSuccess;
            ErrorCode = errorCode;
            Message = message;
        }
    }
}
