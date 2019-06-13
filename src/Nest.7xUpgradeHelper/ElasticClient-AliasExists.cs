using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using AliasExistConverter = Func<IApiCallDetails, Stream, ExistsResponse>;

	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Checks if aliases exist for indices
		/// </summary>
		public static ExistsResponse AliasExists(this IElasticClient client,Names name, Func<AliasExistsDescriptor, IAliasExistsRequest> selector = null);

		/// <summary>
		/// Checks if aliases exist for indices
		/// </summary>
		public static ExistsResponse AliasExists(this IElasticClient client,IAliasExistsRequest request);

		/// <summary>
		/// Checks if aliases exist for indices
		/// </summary>
		public static Task<ExistsResponse> AliasExistsAsync(this IElasticClient client,Names name, Func<AliasExistsDescriptor, IAliasExistsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <summary>
		/// Checks if aliases exist for indices
		/// </summary>
		public static Task<ExistsResponse> AliasExistsAsync(this IElasticClient client,IAliasExistsRequest request, CancellationToken ct = default);
	}
}
