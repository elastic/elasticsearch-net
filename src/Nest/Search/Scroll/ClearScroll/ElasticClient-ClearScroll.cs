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
		IEmptyResponse ClearScroll(ScrollIds scrollIds, Func<ClearScrollDescriptor, IClearScrollRequest> selector = null);

		/// <inheritdoc/>
		IEmptyResponse ClearScroll(IClearScrollRequest request);

		/// <inheritdoc/>
		Task<IEmptyResponse> ClearScrollAsync(ScrollIds scrollIds, Func<ClearScrollDescriptor, IClearScrollRequest> selector = null);

		/// <inheritdoc/>
		Task<IEmptyResponse> ClearScrollAsync(IClearScrollRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IEmptyResponse ClearScroll(ScrollIds scrollIds, Func<ClearScrollDescriptor, IClearScrollRequest> selector = null) =>
			this.ClearScroll(selector.InvokeOrDefault(new ClearScrollDescriptor().ScrollId(scrollIds)));

		/// <inheritdoc/>
		public IEmptyResponse ClearScroll(IClearScrollRequest request) => 
			this.Dispatcher.Dispatch<IClearScrollRequest, ClearScrollRequestParameters, EmptyResponse>(
				request,
				(p, d) => this.LowLevelDispatch.ClearScrollDispatch<EmptyResponse>(p, PatchClearScroll(p))
			);
	

		/// <inheritdoc/>
		public Task<IEmptyResponse> ClearScrollAsync(ScrollIds scrollIds, Func<ClearScrollDescriptor, IClearScrollRequest> selector = null) => 
			this.ClearScrollAsync(selector.InvokeOrDefault(new ClearScrollDescriptor().ScrollId(scrollIds)));

		/// <inheritdoc/>
		public Task<IEmptyResponse> ClearScrollAsync(IClearScrollRequest request) => 
			this.Dispatcher.DispatchAsync<IClearScrollRequest, ClearScrollRequestParameters, EmptyResponse, IEmptyResponse>(
				request,
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