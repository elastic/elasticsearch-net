using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		public static ShrinkIndexResponse ShrinkIndex(this IElasticClient client,IndexName source, IndexName target, Func<ShrinkIndexDescriptor, IShrinkIndexRequest> selector = null);

		public static ShrinkIndexResponse ShrinkIndex(this IElasticClient client,IShrinkIndexRequest request);

		public static Task<ShrinkIndexResponse> ShrinkIndexAsync(this IElasticClient client,
			IndexName source,
			IndexName target,
			Func<ShrinkIndexDescriptor, IShrinkIndexRequest> selector = null,
			CancellationToken ct = default
		);

		public static Task<ShrinkIndexResponse> ShrinkIndexAsync(this IElasticClient client,IShrinkIndexRequest request, CancellationToken ct = default);
	}

}
