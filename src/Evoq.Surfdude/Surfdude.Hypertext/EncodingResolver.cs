using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evoq.Surfdude.Hypertext
{
    public class EncodingResolver
    {
        public virtual Encoding ResolveEncoding(string responseMediaType, Encoding defaultEncoding = null)
        {
            if (string.IsNullOrWhiteSpace(responseMediaType))
            {
                throw new ArgumentNullOrWhitespaceException(nameof(responseMediaType));
            }


            string[] mediaTypeTokens = responseMediaType.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            if (mediaTypeTokens.Contains(Encoding.UTF8.WebName, StringComparer.OrdinalIgnoreCase))
            {
                return Encoding.UTF8;
            }
            else
            {
                IEnumerable<Encoding> encodings = Encoding.GetEncodings().Select(encodingInfo => encodingInfo.GetEncoding());
                Encoding matchingEncoding = encodings.FirstOrDefault(encoding => mediaTypeTokens.Contains(Encoding.UTF8.WebName, StringComparer.OrdinalIgnoreCase));

                return matchingEncoding ?? defaultEncoding ?? throw new UnsupportedMediaTypeException($"The media type '{responseMediaType}' is not supported. No encoding is available.");
            }
        }
    }
}
