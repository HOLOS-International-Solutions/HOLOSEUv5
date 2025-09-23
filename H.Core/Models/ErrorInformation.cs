using System.Windows;
using H.Infrastructure;

namespace H.Core.Models
{
    public class ErrorInformation : MessageBase
    {
        #region Constructors

        public ErrorInformation()
        {
        }

        public ErrorInformation(string msg) : this()
        {
            base.Message = msg;
        }

        public ErrorInformation(string message, Exception exception, bool isCritical)
        {
            base.Message = message;
            Exception = exception;
            IsCritical = isCritical;
        }


        #endregion

        #region Properties
        public Exception Exception { get; }
        public bool IsCritical { get; }
        #endregion 
    }
}