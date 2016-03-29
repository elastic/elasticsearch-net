using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Add a single index alias
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-aliases.html#alias-adding
		/// </summary>
		/// <param name="request">A descriptor that describes the put alias request</param>
		IPutAliasResponse PutAlias(IPutAliasRequest request);

		/// <inheritdoc/>
		Task<IPutAliasResponse> PutAliasAsync(IPutAliasRequest request);

		/// <inheritdoc/>
		IPutAliasResponse PutAlias(Indices indices, Name alias, Func<PutAliasDescriptor, IPutAliasRequest> selector = null); 

		/// <inheritdoc/>
		Task<IPutAliasResponse> PutAliasAsync(Indices indices, Name alias, Func<PutAliasDescriptor, IPutAliasRequest> selector = null);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IPutAliasResponse PutAlias(IPutAliasRequest request) => 
			this.Dispatcher.Dispatch<IPutAliasRequest, PutAliasRequestParameters, PutAliasResponse>(
				request,
				this.LowLevelDispatch.IndicesPutAliasDispatch<PutAliasResponse>
			);

		/// <inheritdoc/>
		public Task<IPutAliasResponse> PutAliasAsync(IPutAliasRequest request) => 
			this.Dispatcher.DispatchAsync<IPutAliasRequest, PutAliasRequestParameters, PutAliasResponse, IPutAliasResponse>(
				request,
				this.LowLevelDispatch.IndicesPutAliasDispatchAsync<PutAliasResponse>
			);

		/// <inheritdoc/>
		public IPutAliasResponse PutAlias(Indices indices, Name alias, Func<PutAliasDescriptor, IPutAliasRequest> selector = null) =>
			this.PutAlias(selector.InvokeOrDefault(new PutAliasDescriptor(indices, alias)));

		/// <inheritdoc/>
		public Task<IPutAliasResponse> PutAliasAsync(Indices indices, Name alias, Func<PutAliasDescriptor, IPutAliasRequest> selector = null) =>
			this.PutAliasAsync(selector.InvokeOrDefault(new PutAliasDescriptor(indices, alias)));
	}
}