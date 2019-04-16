namespace Evoq.Surfdude
{
    using System;
    using Evoq.Surfdude.Hypertext.SimpleJson;

    public sealed class Journey : JourneyBuilder
    {
        public Journey(JourneyContext journeyContext)
            : base(journeyContext, new SimpleJsonResourceReaderWriter(journeyContext))
        {

        }

        public static IJourneyStart Start(string rootUri)
        {
            if (string.IsNullOrWhiteSpace(rootUri))
            {
                throw new ArgumentNullOrWhitespaceException(nameof(rootUri));
            }

            return Start(new JourneyContext(rootUri));
        }

        public static IJourneyStart Start(JourneyContext journeyContext)
        {
            if (journeyContext == null)
            {
                throw new ArgumentNullException(nameof(journeyContext));
            }

            return new Journey(journeyContext);
        }
    }
}
