﻿using System.Net.Http;
using System.Threading.Tasks;

namespace Evoq.Surfdude
{
    internal class FinalStep : HttpRequestStep
    {
        public FinalStep(HttpClient httpClient, JourneyContext journeyContext) : base(httpClient, journeyContext)
        {
        }

        internal override Task<object> RunInternalAsync(IStep previous)
        {
            throw new System.NotImplementedException();
        }
    }
}