namespace Shop.Service
{
    using log4net;
    using System;
    using System.Diagnostics;
    using Library;

    /// <sURMmary>
    /// The Provider exception.
    /// </sURMmary>
    [DebuggerNonUserCode]
    [Serializable]
    public class BusinessException : ApplicationException
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(BusinessException));
        //private string nat = "\\192.168.10.9\\ehd";
        /// <sURMmary>
        /// Initializes a new instance of the <see cref="BusinessLogicLayerException"/> class.
        /// </sURMmary>
        [Obsolete("This constructor need for generic usages, use constructor with parameters instead.", true)]
        public BusinessException()
        {
        }

        /// <sURMmary>
        /// Initializes a new instance of the <see cref="BusinessLogicLayerException"/> class.
        /// </sURMmary>
        /// <param name="message">
        /// The message.
        /// </param>
        public BusinessException(string message)
            : base(message)
        {
            log.Warn(message);
            //CGlobal.writelog(nat + "/logfile/", message);
        }

        public BusinessException(string message, params object[] args)
            : base(message)
        {
            log.Warn(string.Format(message, args));
        }

        /// <sURMmary>
        /// Initializes a new instance of the <see cref="BusinessLogicLayerException"/> class.
        /// </sURMmary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="ex">
        /// The ex.
        /// </param>
        public BusinessException(string message, Exception ex)
            : base(message, ex)
        {
            log.Error(ex);
        }
    }
}