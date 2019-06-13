using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using GetIndexResponseConverter = Func<IApiCallDetails, Stream, GetIndexResponse>;

	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static GetIndexResponse GetIndex(this IElasticClient client,Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> selector = null);

		/// <inheritdoc />
		public static GetIndexResponse GetIndex(this IElasticClient client,IGetIndexRequest request);

		/// <inheritdoc />
		public static Task<GetIndexResponse> GetIndexAsync(this IElasticClient client,Indices indices, Func<GetIndexDescriptor, IGetIndexRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<GetIndexResponse> GetIndexAsync(this IElasticClient client,IGetIndexRequest request, CancellationToken ct = default);
	}


}
