namespace Evoq.Surfdude
{
    using Evoq.Surfdude.Hypertext.Http;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SurfReport
    {
        private readonly IWallClock wallClock;
        private readonly List<ReportLine> reportLines = new List<ReportLine>(20);

        //

        public SurfReport()
            : this(new WallClock(() => DateTime.Now))
        { }

        public SurfReport(IWallClock wallClock)
        {
            this.wallClock = wallClock ?? throw new ArgumentNullException(nameof(wallClock));
        }

        //

        public bool HasException { get; private set; }

        public IEnumerable<ReportLine> Lines => this.reportLines.ToArray();

        private Exception FirstException => this.reportLines.FirstOrDefault(line => line.Exception != null).Exception;

        //

        internal void AppendStopped()
        {
            this.reportLines.Add(new ReportLine(this.wallClock.Now(), "Stopped"));
        }

        internal void AppendException(UnexpectedHttpResponseException stepFailed)
        {
            this.reportLines.Add(new ReportLine(this.wallClock.Now(), stepFailed));
            this.HasException = true;
        }

        internal void AppendCancelled()
        {
            this.reportLines.Add(new ReportLine(this.wallClock.Now(), "Cancelled"));
        }

        internal void AppendStarted()
        {
            this.reportLines.Add(new ReportLine(this.wallClock.Now(), "Started"));
        }

        internal void AppendResourceAction(string verb, string relation, string resourceId)
        {
            if (string.IsNullOrWhiteSpace(verb))
            {
                throw new ArgumentNullOrWhitespaceException(nameof(verb));
            }

            if (string.IsNullOrWhiteSpace(relation))
            {
                throw new ArgumentNullOrWhitespaceException(nameof(relation));
            }

            if (string.IsNullOrWhiteSpace(resourceId))
            {
                throw new ArgumentNullOrWhitespaceException(nameof(resourceId));
            }

            string message = $"{verb.ToUpper()} {relation}: {resourceId}";

            this.reportLines.Add(new ReportLine(this.wallClock.Now(), message));
        }

        internal void AppendStep(string name)
        {
            this.reportLines.Add(new ReportLine(this.wallClock.Now(), $"Running step '{name}'."));
        }

        public void EnsureSuccess()
        {
            if (this.HasException)
            {
                throw this.FirstException;
            }
        }
    }
}