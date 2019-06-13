using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Provide low level segments information that a Lucene index (shard level) is built with.
		/// Allows to be used to provide more information on the state of a shard and an index, possibly optimization information,
		/// data "wasted" on deletes, and so on.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-segments.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the segments operation</param>
		public static SegmentsResponse Segments(this IElasticClient client,Indices indices, Func<SegmentsDescriptor, ISegmentsRequest> selector = null);

		/// <inheritdoc />
		public static SegmentsResponse Segments(this IElasticClient client,ISegmentsRequest request);

		/// <inheritdoc />
		public static Task<SegmentsResponse> SegmentsAsync(this IElasticClient client,
			Indices indices,
			Func<SegmentsDescriptor, ISegmentsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<SegmentsResponse> SegmentsAsync(this IElasticClient client,ISegmentsRequest request, CancellationToken ct = default);
	}

}
