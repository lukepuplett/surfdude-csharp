namespace Evoq.Surfdude
{
    using System;
    using System.Linq;
    using Evoq.Surfdude.Hypertext.SimpleJson;

    public sealed class Surf : WaveBuilder
    {
        public Surf(SurfContext journeyContext)
            : base(journeyContext, new SimpleJsonResourceReaderWriter(journeyContext))
        {

        }

        public static IWaveStart Wave(string rootUri)
        {
            if (string.IsNullOrWhiteSpace(rootUri))
            {
                throw new ArgumentNullOrWhitespaceException(nameof(rootUri));
            }

            return Wave(CreateContext(rootUri));
        }

        public static IWaveStart Wave(SurfContext journeyContext)
        {
            if (journeyContext == null)
            {
                throw new ArgumentNullException(nameof(journeyContext));
            }

            return new Surf(journeyContext);
        }

        private static SurfContext CreateContext(string rootUri)
        {
            return new SurfContext(rootUri);
        }
    }
}
