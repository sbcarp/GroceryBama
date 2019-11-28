using MySql.Data.MySqlClient;
using System;

namespace GroceryBama.Controllers
{
    public class ErrorHandler
    {
        private Exception ErrorContext;
        public ErrorHandler(Exception ex)
        {
            ErrorContext = ex;
        }
        public BasePacket ToBasePacket()
        {
            if (ErrorContext is MySqlException)
            {
                return new BasePacket(false, ((MySqlException)ErrorContext).Number, ErrorContext.Message);
            }
            else
            {
                Console.WriteLine("========================================================");
                Console.WriteLine(ErrorContext.Message);
                Console.WriteLine(ErrorContext.StackTrace);
                return new BasePacket(false, -1, ErrorContext.Message);
            }
        }
    }
}
