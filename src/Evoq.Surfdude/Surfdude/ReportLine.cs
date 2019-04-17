using System;

namespace Evoq.Surfdude
{
    public class ReportLine
    {
        public ReportLine(DateTimeOffset time, StepFailedException stepFailed)
        {
            this.Time = time;
            this.Message = stepFailed.Message;
            this.Exception = stepFailed;
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