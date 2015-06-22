using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
    public partial class ElasticClient
    {
        public IGetScriptResponse GetScript(Func<GetScriptDescriptor, GetScriptDescriptor> getScriptDescriptor)
        {
            return this.Dispatcher.Dispatch<GetScriptDescriptor, GetScriptRequestParameters, GetScriptResponse>(
                    getScriptDescriptor,
                    (p, d) => this.RawDispatch.GetScriptDispatch<GetScriptResponse>(p)
                );
        }

        public Task<IGetScriptResponse> GetScriptAsync(Func<GetScriptDescriptor, GetScriptDescriptor> getScriptDescriptor)
        {
            return this.Dispatcher.DispatchAsync<GetScriptDescriptor, GetScriptRequestParameters, GetScriptResponse, IGetScriptResponse>(
                    getScriptDescriptor,
                    (p, d) => this.RawDispatch.GetScriptDispatchAsync<GetScriptResponse>(p)
                );
        }

    }
}
