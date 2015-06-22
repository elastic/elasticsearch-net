using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
    public partial class ElasticClient
    {
        public IPutScriptResponse PutScript(Func<PutScriptDescriptor, PutScriptDescriptor> putScriptDescriptor)
        {
            return this.Dispatcher.Dispatch<PutScriptDescriptor, PutScriptRequestParameters, PutScriptResponse>(
                    putScriptDescriptor,
                    (p, d) => this.RawDispatch.PutScriptDispatch<PutScriptResponse>(p, d)
                );
        }

        public Task<IPutScriptResponse> PutScriptAsync(Func<PutScriptDescriptor, PutScriptDescriptor> putScriptDescriptor)
        {
            return this.Dispatcher.DispatchAsync<PutScriptDescriptor, PutScriptRequestParameters, PutScriptResponse, IPutScriptResponse>(
                    putScriptDescriptor,
                    (p, d) => this.RawDispatch.PutScriptDispatchAsync<PutScriptResponse>(p, d)
                );
        }

    }
}
