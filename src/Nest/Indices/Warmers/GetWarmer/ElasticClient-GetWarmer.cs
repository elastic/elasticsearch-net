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
		/// Getting a warmer for specific index (or alias, or several indices) based on its name. 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-warmers.html#warmer-retrieving
		/// </summary>
		/// <param name="name">The name of the warmer to get</param>
		/// <param name="selector">An optional selector specifying additional parameters for the get warmer operation</param>
		IGetWarmerResponse GetWarmer(Func<GetWarmerDescriptor, IGetWarmerRequest> selector = null);

		/// <inheritdoc/>
		IGetWarmerResponse GetWarmer(IGetWarmerRequest request);

		/// <inheritdoc/>
		Task<IGetWarmerResponse> GetWarmerAsync(Func<GetWarmerDescriptor, IGetWarmerRequest> selector = null);

		/// <inheritdoc/>
		Task<IGetWarmerResponse> GetWarmerAsync(IGetWarmerRequest request);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetWarmerResponse GetWarmer(Func<GetWarmerDescriptor, IGetWarmerRequest> selector = null) =>
			this.GetWarmer(selector.InvokeOrDefault(new GetWarmerDescriptor()));

		/// <inheritdoc/>
		public IGetWarmerResponse GetWarmer(IGetWarmerRequest request) => 
			this.Dispatcher.Dispatch<IGetWarmerRequest, GetWarmerRequestParameters, GetWarmerResponse>(
				request, 
				(p, d) => this.LowLevelDispatch.IndicesGetWarmerDispatch<GetWarmerResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetWarmerResponse> GetWarmerAsync(Func<GetWarmerDescriptor, IGetWarmerRequest> selector = null) => 
			this.GetWarmerAsync(selector.InvokeOrDefault(new GetWarmerDescriptor()));

		/// <inheritdoc/>
		public Task<IGetWarmerResponse> GetWarmerAsync(IGetWarmerRequest request) => 
			this.Dispatcher.DispatchAsync<IGetWarmerRequest, GetWarmerRequestParameters, GetWarmerResponse, IGetWarmerResponse>(
				request, (p, d) => this.LowLevelDispatch.IndicesGetWarmerDispatchAsync<GetWarmerResponse>(p)
			);
	}
}