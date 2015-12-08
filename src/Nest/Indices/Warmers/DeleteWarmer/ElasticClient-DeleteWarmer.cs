using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
		IAcknowledgedResponse DeleteWarmer(Indices indices, Names names, Func<DeleteWarmerDescriptor, IDeleteWarmerRequest> selector = null);

		/// <inheritdoc/>
		IAcknowledgedResponse DeleteWarmer(IDeleteWarmerRequest request);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> DeleteWarmerAsync(Indices indices, Names names, Func<DeleteWarmerDescriptor, IDeleteWarmerRequest> selector = null);

		/// <inheritdoc/>
		Task<IAcknowledgedResponse> DeleteWarmerAsync(IDeleteWarmerRequest request);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IAcknowledgedResponse DeleteWarmer(Indices indices, Names names, Func<DeleteWarmerDescriptor, IDeleteWarmerRequest> selector = null) =>
			this.DeleteWarmer(selector.InvokeOrDefault(new DeleteWarmerDescriptor(indices, names)));

		/// <inheritdoc/>
		public IAcknowledgedResponse DeleteWarmer(IDeleteWarmerRequest request) => 
			this.Dispatcher.Dispatch<IDeleteWarmerRequest, DeleteWarmerRequestParameters, AcknowledgedResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesDeleteWarmerDispatch<AcknowledgedResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> DeleteWarmerAsync(Indices indices, Names names, Func<DeleteWarmerDescriptor, IDeleteWarmerRequest> selector = null) => 
			this.DeleteWarmerAsync(selector.InvokeOrDefault(new DeleteWarmerDescriptor(indices, names)));

		/// <inheritdoc/>
		public Task<IAcknowledgedResponse> DeleteWarmerAsync(IDeleteWarmerRequest request) => 
			this.Dispatcher.DispatchAsync<IDeleteWarmerRequest, DeleteWarmerRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesDeleteWarmerDispatchAsync<AcknowledgedResponse>(p)
			);
	}
}