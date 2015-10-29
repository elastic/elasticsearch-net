using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetWarmerConverter = Func<IApiCallDetails, Stream, WarmerResponse>;
	using CrazyWarmerResponse = Dictionary<string, Dictionary<string, IWarmers>>;
	
	public partial interface IElasticClient
	{
		/// <summary>
		/// Deletes a warmer
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-warmers.html#removing
		/// </summary>
		/// <param name="name">The name of the warmer to delete</param>
		/// <param name="selector">An optional selector specifying additional parameters for the delete warmer operation</param>
		IIndicesOperationResponse DeleteWarmer(Indices indices, Names names, Func<DeleteWarmerDescriptor, IDeleteWarmerRequest> selector = null);

		/// <inheritdoc/>
		IIndicesOperationResponse DeleteWarmer(IDeleteWarmerRequest deleteWarmerRequest);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> DeleteWarmerAsync(Indices indices, Names names, Func<DeleteWarmerDescriptor, IDeleteWarmerRequest> selector = null);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> DeleteWarmerAsync(IDeleteWarmerRequest deleteWarmerRequest);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IIndicesOperationResponse DeleteWarmer(Indices indices, Names names, Func<DeleteWarmerDescriptor, IDeleteWarmerRequest> selector = null) =>
			this.DeleteWarmer(selector.InvokeOrDefault(new DeleteWarmerDescriptor(indices, names)));

		/// <inheritdoc/>
		public IIndicesOperationResponse DeleteWarmer(IDeleteWarmerRequest deleteWarmerRequest) => 
			this.Dispatcher.Dispatch<IDeleteWarmerRequest, DeleteWarmerRequestParameters, IndicesOperationResponse>(
				deleteWarmerRequest,
				(p, d) => this.LowLevelDispatch.IndicesDeleteWarmerDispatch<IndicesOperationResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> DeleteWarmerAsync(Indices indices, Names names, Func<DeleteWarmerDescriptor, IDeleteWarmerRequest> selector = null) => 
			this.DeleteWarmerAsync(selector.InvokeOrDefault(new DeleteWarmerDescriptor(indices, names)));

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> DeleteWarmerAsync(IDeleteWarmerRequest deleteWarmerRequest) => 
			this.Dispatcher.DispatchAsync<IDeleteWarmerRequest, DeleteWarmerRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				deleteWarmerRequest,
				(p, d) => this.LowLevelDispatch.IndicesDeleteWarmerDispatchAsync<IndicesOperationResponse>(p)
			);
	}
}