namespace Evoq.Surfdude
{
    using System.Threading;

    public class JourneyContext
    {
        public JourneyContext(string rootUri, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(rootUri))
            {
                throw new ArgumentNullOrWhitespaceException(nameof(rootUri));
            }
            
            this.RootUri = rootUri;
            this.CancellationToken = cancellationToken;
        }
        
        //

        public string RootUri { get; }

        public bool IgnoreBadResults { get; set; } = false;

        public CancellationToken CancellationToken { get; }
    }
}