using System;

namespace Evoq.Surfdude
{
    public class ReportLine
    {
        public ReportLine(DateTimeOffset time, Exception exception)
        {
            this.Time = time;
            this.Message = exception.Message;
            this.Exception = exception;
        }

        public ReportLine(DateTimeOffset time, string message)
        {
            this.Time = time;
            this.Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        //

        public DateTimeOffset Time { get; }

        public string Message { get; }

        public Exception Exception { get; }
    }
}