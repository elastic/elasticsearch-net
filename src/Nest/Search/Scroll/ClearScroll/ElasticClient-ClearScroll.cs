using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Deletes a registered scroll request on the cluster 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-request-scroll.html
		/// </summary>
		/// <param name="clearScrollSelector">Specify the scroll id as well as request specific configuration</param>
		IEmptyResponse ClearScroll(string scrollId, Func<ClearScrollDescriptor, IClearScrollRequest> clearScrollSelector = null);

		/// <inheritdoc/>
		IEmptyResponse ClearScroll(IClearScrollRequest clearScrollRequest);

		/// <inheritdoc/>
		Task<IEmptyResponse> ClearScrollAsync(string scrollId, Func<ClearScrollDescriptor, IClearScrollRequest> clearScrollSelector = null);

		/// <inheritdoc/>
		Task<IEmptyResponse> ClearScrollAsync(IClearScrollRequest clearScrollRequest);
	}

	public partial class ElasticClient
	{

		/// <inheritdoc/>
		public IEmptyResponse ClearScroll(string scrollId, Func<ClearScrollDescriptor, IClearScrollRequest> clearScrollSelector = null) => 
			this.Dispatcher.Dispatch<IClearScrollRequest, ClearScrollRequestParameters, EmptyResponse>(
				clearScrollSelector.InvokeOrDefault(new ClearScrollDescriptor().ScrollId(scrollId)),
				(p, d) =>
				{
					var body = PatchClearScroll(p);
					return this.LowLevelDispatch.ClearScrollDispatch<EmptyResponse>(p, body);
				}
				);

		/// <inheritdoc/>
		public IEmptyResponse ClearScroll(IClearScrollRequest clearScrollRequest) => 
			this.Dispatcher.Dispatch<IClearScrollRequest, ClearScrollRequestParameters, EmptyResponse>(
				clearScrollRequest,
				(p, d) =>
				{
					var body = PatchClearScroll(p);
					return this.LowLevelDispatch.ClearScrollDispatch<EmptyResponse>(p, body);
				}
				);

		/// <inheritdoc/>
		public Task<IEmptyResponse> ClearScrollAsync(string scrollId, Func<ClearScrollDescriptor, IClearScrollRequest> clearScrollSelector = null) => 
			this.Dispatcher.DispatchAsync<IClearScrollRequest, ClearScrollRequestParameters, EmptyResponse, IEmptyResponse>(
				clearScrollSelector.InvokeOrDefault(new ClearScrollDescriptor().ScrollId(scrollId)),
				(p, d) =>
				{
					var body = PatchClearScroll(p);
					return this.LowLevelDispatch.ClearScrollDispatchAsync<EmptyResponse>(p, body);
				}
			);

		/// <inheritdoc/>
		public Task<IEmptyResponse> ClearScrollAsync(IClearScrollRequest clearScrollRequest) => 
			this.Dispatcher.DispatchAsync<IClearScrollRequest, ClearScrollRequestParameters, EmptyResponse, IEmptyResponse>(
				clearScrollRequest,
				(p, d) =>
				{
					var body = PatchClearScroll(p);
					return this.LowLevelDispatch.ClearScrollDispatchAsync<EmptyResponse>(p, body);
				}
			);

		private static string PatchClearScroll(ElasticsearchPathInfo<ClearScrollRequestParameters> p)
		{
			string body = null;
			var scrollId = p.ScrollId;
			if (scrollId != null && scrollId != "_all")
			{
				p.ScrollId = null;
				body = scrollId;
			}
			return body;
		}
	}
}