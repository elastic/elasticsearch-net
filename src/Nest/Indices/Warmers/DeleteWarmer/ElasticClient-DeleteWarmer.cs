using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetWarmerConverter = Func<IApiCallDetails, Stream, GetWarmerResponse>;
	using CrazyWarmerResponse = Dictionary<string, Dictionary<string, IWarmers>>;
	
	public partial interface IElasticClient
	{
		/// <summary>
		/// Deletes a warmer
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-warmers.html#removing
		/// </summary>
		/// <param name="name">The name of the warmer to delete</param>
		/// <param name="selector">An optional selector specifying additional parameters for the delete warmer operation</param>
		IDeleteWarmerResponse DeleteWarmer(Indices indices, Names names, Func<DeleteWarmerDescriptor, IDeleteWarmerRequest> selector = null);

		/// <inheritdoc/>
		IDeleteWarmerResponse DeleteWarmer(IDeleteWarmerRequest request);

		/// <inheritdoc/>
		Task<IDeleteWarmerResponse> DeleteWarmerAsync(Indices indices, Names names, Func<DeleteWarmerDescriptor, IDeleteWarmerRequest> selector = null);

		/// <inheritdoc/>
		Task<IDeleteWarmerResponse> DeleteWarmerAsync(IDeleteWarmerRequest request);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IDeleteWarmerResponse DeleteWarmer(Indices indices, Names names, Func<DeleteWarmerDescriptor, IDeleteWarmerRequest> selector = null) =>
			this.DeleteWarmer(selector.InvokeOrDefault(new DeleteWarmerDescriptor(indices, names)));

		/// <inheritdoc/>
		public IDeleteWarmerResponse DeleteWarmer(IDeleteWarmerRequest request) => 
			this.Dispatcher.Dispatch<IDeleteWarmerRequest, DeleteWarmerRequestParameters, DeleteWarmerResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesDeleteWarmerDispatch<DeleteWarmerResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IDeleteWarmerResponse> DeleteWarmerAsync(Indices indices, Names names, Func<DeleteWarmerDescriptor, IDeleteWarmerRequest> selector = null) => 
			this.DeleteWarmerAsync(selector.InvokeOrDefault(new DeleteWarmerDescriptor(indices, names)));

		/// <inheritdoc/>
		public Task<IDeleteWarmerResponse> DeleteWarmerAsync(IDeleteWarmerRequest request) => 
			this.Dispatcher.DispatchAsync<IDeleteWarmerRequest, DeleteWarmerRequestParameters, DeleteWarmerResponse, IDeleteWarmerResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesDeleteWarmerDispatchAsync<DeleteWarmerResponse>(p)
			);
	}
}