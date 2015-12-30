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
		/// <param name="selector">Specify the scroll id as well as request specific configuration</param>
		IClearScrollResponse ClearScroll(ScrollIds scrollIds, Func<ClearScrollDescriptor, IClearScrollRequest> selector = null);

		/// <inheritdoc/>
		IClearScrollResponse ClearScroll(IClearScrollRequest request);

		/// <inheritdoc/>
		Task<IClearScrollResponse> ClearScrollAsync(ScrollIds scrollIds, Func<ClearScrollDescriptor, IClearScrollRequest> selector = null);

		/// <inheritdoc/>
		Task<IClearScrollResponse> ClearScrollAsync(IClearScrollRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IClearScrollResponse ClearScroll(ScrollIds scrollIds, Func<ClearScrollDescriptor, IClearScrollRequest> selector = null) =>
			this.ClearScroll(selector.InvokeOrDefault(new ClearScrollDescriptor().ScrollId(scrollIds)));

		/// <inheritdoc/>
		public IClearScrollResponse ClearScroll(IClearScrollRequest request) => 
			this.Dispatcher.Dispatch<IClearScrollRequest, ClearScrollRequestParameters, ClearScrollResponse>(
				request,
				(p, d) => this.LowLevelDispatch.ClearScrollDispatch<ClearScrollResponse>(p, PatchClearScroll(p))
			);
	

		/// <inheritdoc/>
		public Task<IClearScrollResponse> ClearScrollAsync(ScrollIds scrollIds, Func<ClearScrollDescriptor, IClearScrollRequest> selector = null) => 
			this.ClearScrollAsync(selector.InvokeOrDefault(new ClearScrollDescriptor().ScrollId(scrollIds)));

		/// <inheritdoc/>
		public Task<IClearScrollResponse> ClearScrollAsync(IClearScrollRequest request) => 
			this.Dispatcher.DispatchAsync<IClearScrollRequest, ClearScrollRequestParameters, ClearScrollResponse, IClearScrollResponse>(
				request,
				(p, d) => this.LowLevelDispatch.ClearScrollDispatchAsync<ClearScrollResponse>(p, PatchClearScroll(p))
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