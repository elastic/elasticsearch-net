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

        public IDeleteScriptResponse DeleteScript(Func<DeleteScriptDescriptor, DeleteScriptDescriptor> deleteScriptDescriptor)
        {
            return this.Dispatcher.Dispatch<DeleteScriptDescriptor, DeleteScriptRequestParameters, DeleteScriptResponse>(
                    deleteScriptDescriptor,
                    (p, d) => this.RawDispatch.DeleteScriptDispatch<DeleteScriptResponse>(p)
                );
        }

        public Task<IDeleteScriptResponse> DeleteScriptAsync(Func<DeleteScriptDescriptor, DeleteScriptDescriptor> deleteScriptDescriptor)
        {
            return this.Dispatcher.DispatchAsync<DeleteScriptDescriptor, DeleteScriptRequestParameters, DeleteScriptResponse, IDeleteScriptResponse>(
                    deleteScriptDescriptor,
                    (p, d) => this.RawDispatch.DeleteScriptDispatchAsync<DeleteScriptResponse>(p)
                );
        }
    }
}
