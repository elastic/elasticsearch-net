using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
		/// <param name="putAliasRequest">A descriptor that describes the put alias request</param>
		IPutAliasResponse PutAlias(IPutAliasRequest putAliasRequest);

		/// <inheritdoc/>
		Task<IPutAliasResponse> PutAliasAsync(IPutAliasRequest putAliasRequest);

		/// <inheritdoc/>
		IPutAliasResponse PutAlias(Indices indices, Name alias, Func<PutAliasDescriptor, IPutAliasRequest> putAliasSelector = null); 

		/// <inheritdoc/>
		Task<IPutAliasResponse> PutAliasAsync(Indices indices, Name alias, Func<PutAliasDescriptor, IPutAliasRequest> putAliasSelector = null);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IPutAliasResponse PutAlias(IPutAliasRequest putAliasRequest) => 
			this.Dispatcher.Dispatch<IPutAliasRequest, PutAliasRequestParameters, PutAliasResponse>(
				putAliasRequest,
				this.LowLevelDispatch.IndicesPutAliasDispatch<PutAliasResponse>
			);

		/// <inheritdoc/>
		public Task<IPutAliasResponse> PutAliasAsync(IPutAliasRequest putAliasRequest) => 
			this.Dispatcher.DispatchAsync<IPutAliasRequest, PutAliasRequestParameters, PutAliasResponse, IPutAliasResponse>(
				putAliasRequest,
				this.LowLevelDispatch.IndicesPutAliasDispatchAsync<PutAliasResponse>
			);

		/// <inheritdoc/>
		public IPutAliasResponse PutAlias(Indices indices, Name alias, Func<PutAliasDescriptor, IPutAliasRequest> putAliasSelector = null) =>
			this.PutAlias(putAliasSelector.InvokeOrDefault(new PutAliasDescriptor(indices, alias)));

		/// <inheritdoc/>
		public Task<IPutAliasResponse> PutAliasAsync(Indices indices, Name alias, Func<PutAliasDescriptor, IPutAliasRequest> putAliasSelector = null) =>
			this.PutAliasAsync(putAliasSelector.InvokeOrDefault(new PutAliasDescriptor(indices, alias)));
	}
}