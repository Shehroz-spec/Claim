using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ClaimDigitalize.Services
{
    public static class LoggerService
    {
        public static void LogExceptionsToDebugConsole(InvalidOperationException ex)
        {
            Debug.WriteLine("Error: \"{1}\"", ex.Message);
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }

        public static void LogExceptionsToDebugConsole(SqlException ex)
        {
            Debug.WriteLine("Error: \"{1}\"", ex.Message);
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }

        public static void LogExceptionsToDebugConsole(Exception ex)
        {
            Debug.WriteLine("Error: \"{1}\"", ex.Message);
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }
}