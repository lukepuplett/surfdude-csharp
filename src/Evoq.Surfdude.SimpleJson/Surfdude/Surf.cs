namespace Evoq.Surfdude
{
    using System;
    using System.Linq;
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

            return Wave(CreateContext(rootUri));
        }

        public static ISurfStart Wave(RideContext journeyContext)
        {
            if (journeyContext == null)
            {
                throw new ArgumentNullException(nameof(journeyContext));
            }

            return new Surf(journeyContext);
        }

        private static RideContext CreateContext(string rootUri)
        {
            int[] goodCodes = Enumerable.Range(200, 200).ToArray();

            return new RideContext(rootUri)
            {
                ExpectedStatusCodes = goodCodes
            };
        }
    }
}
