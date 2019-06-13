using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		public static RolloverIndexResponse RolloverIndex(this IElasticClient client,Name alias, Func<RolloverIndexDescriptor, IRolloverIndexRequest> selector = null);

		public static RolloverIndexResponse RolloverIndex(this IElasticClient client,IRolloverIndexRequest request);

		public static Task<RolloverIndexResponse> RolloverIndexAsync(this IElasticClient client,
			Name alias,
			Func<RolloverIndexDescriptor, IRolloverIndexRequest> selector = null,
			CancellationToken ct = default
		);

		public static Task<RolloverIndexResponse> RolloverIndexAsync(this IElasticClient client,IRolloverIndexRequest request,
			CancellationToken ct = default
		);
	}

}
