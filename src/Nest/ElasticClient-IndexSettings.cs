using System;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IIndexSettingsResponse GetIndexSettings(Func<GetIndexSettingsDescriptor, GetIndexSettingsDescriptor> selector)
		{
			return this.Dispatch<GetIndexSettingsDescriptor, GetIndexSettingsRequestParameters, IndexSettingsResponse>(
				selector,
				(p, d) => this.RawDispatch.IndicesGetSettingsDispatch<IndexSettingsResponse>(p)
			);
		}

		/// <inheritdoc />
		public IIndexSettingsResponse GetIndexSettings(IGetIndexSettingsRequest getIndexSettingsRequest)
		{
			return this.Dispatch<IGetIndexSettingsRequest, GetIndexSettingsRequestParameters, IndexSettingsResponse>(
				getIndexSettingsRequest,
				(p, d) => this.RawDispatch.IndicesGetSettingsDispatch<IndexSettingsResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IIndexSettingsResponse> GetIndexSettingsAsync(Func<GetIndexSettingsDescriptor, GetIndexSettingsDescriptor> selector)
		{
			return this.DispatchAsync
				<GetIndexSettingsDescriptor, GetIndexSettingsRequestParameters, IndexSettingsResponse, IIndexSettingsResponse>(
					selector,
					(p, d) => this.RawDispatch.IndicesGetSettingsDispatchAsync<IndexSettingsResponse>(p)
				);
		}

		/// <inheritdoc />
		public Task<IIndexSettingsResponse> GetIndexSettingsAsync(IGetIndexSettingsRequest getIndexSettingsRequest)
		{
			return this.DispatchAsync<IGetIndexSettingsRequest, GetIndexSettingsRequestParameters, IndexSettingsResponse, IIndexSettingsResponse>(
				getIndexSettingsRequest,
				(p, d) => this.RawDispatch.IndicesGetSettingsDispatchAsync<IndexSettingsResponse>(p)
			);
		}

	}
}