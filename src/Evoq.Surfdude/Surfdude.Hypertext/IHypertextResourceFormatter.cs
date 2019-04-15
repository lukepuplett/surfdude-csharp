using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Evoq.Surfdude.Hypertext
{
    public interface IHypertextResourceFormatter
    {
        Task<IHypertextResource> ReadResourceAsync(HttpContent content);
    }
}
