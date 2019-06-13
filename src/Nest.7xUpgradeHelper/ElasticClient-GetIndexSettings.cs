using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The get settings API allows to retrieve settings of index/indices.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-get-settings.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the get index settings operation</param>
		public static GetIndexSettingsResponse GetIndexSettings(this IElasticClient client,Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector);

		/// <inheritdoc />
		public static GetIndexSettingsResponse GetIndexSettings(this IElasticClient client,IGetIndexSettingsRequest request);

		/// <inheritdoc />
		public static Task<GetIndexSettingsResponse> GetIndexSettingsAsync(this IElasticClient client,Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<GetIndexSettingsResponse> GetIndexSettingsAsync(this IElasticClient client,IGetIndexSettingsRequest request,
			CancellationToken ct = default
		);
	}

}
