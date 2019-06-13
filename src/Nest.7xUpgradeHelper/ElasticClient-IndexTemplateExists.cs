using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using IndexTemplateExistConverter = Func<IApiCallDetails, Stream, ExistsResponse>;

	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static ExistsResponse IndexTemplateExists(this IElasticClient client,Name template, Func<IndexTemplateExistsDescriptor, IIndexTemplateExistsRequest> selector = null);

		/// <inheritdoc />
		public static ExistsResponse IndexTemplateExists(this IElasticClient client,IIndexTemplateExistsRequest request);

		/// <inheritdoc />
		public static Task<ExistsResponse> IndexTemplateExistsAsync(this IElasticClient client,Name template,
			Func<IndexTemplateExistsDescriptor, IIndexTemplateExistsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<ExistsResponse> IndexTemplateExistsAsync(this IElasticClient client,IIndexTemplateExistsRequest request,
			CancellationToken ct = default
		);
	}

}
