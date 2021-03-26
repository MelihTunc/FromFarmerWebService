using System;

namespace FromFarmer.Utilities.ExceptionHandling
{
    public static class ExceptionHelper
    {
        public static string GetExceptionInnerMessage(this Exception ex)
        {
            Exception exception = ex;
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }
            return String.IsNullOrEmpty(exception.Message)
                ? ex.Message
                : exception.Message;
        }
    }
}
