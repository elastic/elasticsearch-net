using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		
		/// <inheritdoc />
		public IEmptyResponse ClearScroll(Func<ClearScrollDescriptor, ClearScrollDescriptor> clearScrollSelector)
		{
			return this.Dispatcher.Dispatch<ClearScrollDescriptor, ClearScrollRequestParameters, EmptyResponse>(
				clearScrollSelector,
				(p, d) =>
				{
					var body = PatchClearScroll(p);
					return this.LowLevelDispatch.ClearScrollDispatch<EmptyResponse>(p, body);
				}
				);
		}

		/// <inheritdoc />
		public IEmptyResponse ClearScroll(IClearScrollRequest clearScrollRequest)
		{
			return this.Dispatcher.Dispatch<IClearScrollRequest, ClearScrollRequestParameters, EmptyResponse>(
				clearScrollRequest,
				(p, d) =>
				{
					var body = PatchClearScroll(p);
					return this.LowLevelDispatch.ClearScrollDispatch<EmptyResponse>(p, body);
				}
				);
		}

		/// <inheritdoc />
		public Task<IEmptyResponse> ClearScrollAsync(Func<ClearScrollDescriptor, ClearScrollDescriptor> clearScrollSelector)
		{
			return this.Dispatcher.DispatchAsync<ClearScrollDescriptor, ClearScrollRequestParameters, EmptyResponse, IEmptyResponse>(
				clearScrollSelector,
				(p, d) =>
				{
					var body = PatchClearScroll(p);
					return this.LowLevelDispatch.ClearScrollDispatchAsync<EmptyResponse>(p, body);
				}
			);
		}

		/// <inheritdoc />
		public Task<IEmptyResponse> ClearScrollAsync(IClearScrollRequest clearScrollRequest)
		{
			return this.Dispatcher.DispatchAsync<IClearScrollRequest, ClearScrollRequestParameters, EmptyResponse, IEmptyResponse>(
				clearScrollRequest,
				(p, d) =>
				{
					var body = PatchClearScroll(p);
					return this.LowLevelDispatch.ClearScrollDispatchAsync<EmptyResponse>(p, body);
				}
			);
		}

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