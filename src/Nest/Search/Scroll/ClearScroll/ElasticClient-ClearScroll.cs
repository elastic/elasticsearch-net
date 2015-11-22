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
		IEmptyResponse ClearScroll(ScrollIds scrollIds, Func<ClearScrollDescriptor, IClearScrollRequest> clearScrollSelector = null);

		/// <inheritdoc/>
		IEmptyResponse ClearScroll(IClearScrollRequest clearScrollRequest);

		/// <inheritdoc/>
		Task<IEmptyResponse> ClearScrollAsync(ScrollIds scrollIds, Func<ClearScrollDescriptor, IClearScrollRequest> clearScrollSelector = null);

		/// <inheritdoc/>
		Task<IEmptyResponse> ClearScrollAsync(IClearScrollRequest clearScrollRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IEmptyResponse ClearScroll(ScrollIds scrollIds, Func<ClearScrollDescriptor, IClearScrollRequest> clearScrollSelector = null) =>
			this.ClearScroll(clearScrollSelector.InvokeOrDefault(new ClearScrollDescriptor().ScrollId(scrollIds)));

		/// <inheritdoc/>
		public IEmptyResponse ClearScroll(IClearScrollRequest clearScrollRequest) => 
			this.Dispatcher.Dispatch<IClearScrollRequest, ClearScrollRequestParameters, EmptyResponse>(
				clearScrollRequest,
				(p, d) => this.LowLevelDispatch.ClearScrollDispatch<EmptyResponse>(p, PatchClearScroll(p))
			);
	

		/// <inheritdoc/>
		public Task<IEmptyResponse> ClearScrollAsync(ScrollIds scrollIds, Func<ClearScrollDescriptor, IClearScrollRequest> clearScrollSelector = null) => 
			this.ClearScrollAsync(clearScrollSelector.InvokeOrDefault(new ClearScrollDescriptor().ScrollId(scrollIds)));

		/// <inheritdoc/>
		public Task<IEmptyResponse> ClearScrollAsync(IClearScrollRequest clearScrollRequest) => 
			this.Dispatcher.DispatchAsync<IClearScrollRequest, ClearScrollRequestParameters, EmptyResponse, IEmptyResponse>(
				clearScrollRequest,
				(p, d) => this.LowLevelDispatch.ClearScrollDispatchAsync<EmptyResponse>(p, PatchClearScroll(p))
			);

		private static string PatchClearScroll(IRequest<ClearScrollRequestParameters> p)
		{
			string body = null;
			var scrollId = p.RouteValues.ScrollId;
			if (scrollId != null && scrollId != "_all")
			{
				p.RouteValues.Remove("scroll_id");
				body = scrollId;
			}
			return body;
		}
	}
}