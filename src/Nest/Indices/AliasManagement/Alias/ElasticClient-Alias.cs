using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// APIs in elasticsearch accept an index name when working against a specific index, and several indices when applicable. 
		/// <para>The index aliases API allow to alias an index with a name, with all APIs automatically converting the alias name to the 
		/// actual index name.</para><para> An alias can also be mapped to more than one index, and when specifying it, the alias 
		/// will automatically expand to the aliases indices.i</para><para> An alias can also be associated with a filter that will 
		/// automatically be applied when searching, and routing values.</para>
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-aliases.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the alias operation</param>
		IAcknowledgedResponse Alias(Func<BulkAliasDescriptor, IBulkAliasRequest> selector);

		/// <inheritdoc/>
		IAcknowledgedResponse Alias(IBulkAliasRequest request);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> AliasAsync(Func<BulkAliasDescriptor, IBulkAliasRequest> selector);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> AliasAsync(IBulkAliasRequest request);
	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IAcknowledgedResponse Alias(IBulkAliasRequest request) => 
			this.Dispatcher.Dispatch<IBulkAliasRequest, BulkAliasRequestParameters, AcknowledgedResponse>(
				request,
				this.LowLevelDispatch.IndicesUpdateAliasesDispatch<AcknowledgedResponse>
			);

		/// <inheritdoc/>
		public IAcknowledgedResponse Alias(Func<BulkAliasDescriptor, IBulkAliasRequest> selector) =>
			this.Alias(selector?.Invoke(new BulkAliasDescriptor()));

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> AliasAsync(IBulkAliasRequest request) => 
			this.Dispatcher.DispatchAsync<IBulkAliasRequest, BulkAliasRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				request,
				this.LowLevelDispatch.IndicesUpdateAliasesDispatchAsync<AcknowledgedResponse>
			);

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> AliasAsync(Func<BulkAliasDescriptor, IBulkAliasRequest> selector)=>
			this.AliasAsync(selector?.Invoke(new BulkAliasDescriptor()));

	}
}