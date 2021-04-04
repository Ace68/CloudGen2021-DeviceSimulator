using System;

namespace CloudGenDeviceSimulator.Shared.Services
{
    public class CommonServices
    {
        public static string GetErrorMessage(Exception ex) => CommonServices.GetMessageFromException(ex);

        public static string GetDefaultErrorTrace(Exception ex) => "Source: " + ex.Source + " StackTrace: " +
                                                                   ex.StackTrace + " Message: " +
                                                                   CommonServices.GetMessageFromException(ex);

        private static string GetMessageFromException(Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex.Message;
        }
    }
}