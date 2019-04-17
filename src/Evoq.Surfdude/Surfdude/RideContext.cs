namespace Evoq.Surfdude
{
    using System.Threading;

    public class RideContext
    {
        public RideContext(string rootUri)
        {
            if (string.IsNullOrWhiteSpace(rootUri))
            {
                throw new ArgumentNullOrWhitespaceException(nameof(rootUri));
            }
            
            this.RootUri = rootUri;
        }
        
        //

        public string RootUri { get; }

        public bool IgnoreBadResults { get; set; } = false;
    }
}