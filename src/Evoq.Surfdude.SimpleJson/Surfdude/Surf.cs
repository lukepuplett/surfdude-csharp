namespace Evoq.Surfdude
{
    using System;
    using Evoq.Surfdude.Hypertext.SimpleJson;

    public sealed class Surf : SurfBuilder
    {
        public Surf(RideContext journeyContext)
            : base(journeyContext, new SimpleJsonResourceReaderWriter(journeyContext))
        {

        }

        public static ISurfStart Wave(string rootUri)
        {
            if (string.IsNullOrWhiteSpace(rootUri))
            {
                throw new ArgumentNullOrWhitespaceException(nameof(rootUri));
            }

            return Wave(new RideContext(rootUri));
        }

        public static ISurfStart Wave(RideContext journeyContext)
        {
            if (journeyContext == null)
            {
                throw new ArgumentNullException(nameof(journeyContext));
            }

            return new Surf(journeyContext);
        }
    }
}
