using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
    public partial class ElasticClient
    {
        public IDeleteScriptResponse DeleteScript(Func<DeleteScriptDescriptor, DeleteScriptDescriptor> deleteScriptDescriptor)
        {
            return this.Dispatcher.Dispatch<DeleteScriptDescriptor, DeleteScriptRequestParameters, DeleteScriptResponse>(
                    deleteScriptDescriptor,
                    (p, d) => this.LowLevelDispatch.DeleteScriptDispatch<DeleteScriptResponse>(p)
                );
        }

        public Task<IDeleteScriptResponse> DeleteScriptAsync(Func<DeleteScriptDescriptor, DeleteScriptDescriptor> deleteScriptDescriptor)
        {
            return this.Dispatcher.DispatchAsync<DeleteScriptDescriptor, DeleteScriptRequestParameters, DeleteScriptResponse, IDeleteScriptResponse>(
                    deleteScriptDescriptor,
                    (p, d) => this.LowLevelDispatch.DeleteScriptDispatchAsync<DeleteScriptResponse>(p)
                );
        }
    }
}
