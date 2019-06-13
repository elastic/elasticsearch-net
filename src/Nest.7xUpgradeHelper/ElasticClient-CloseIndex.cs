using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The open and close index APIs allow to close an index, and later on opening it.
		/// A closed index has almost no overhead on the cluster (except for maintaining its metadata), and is blocked
		/// for read/write operations.
		/// A closed index can be opened which will then go through the normal recovery process.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-open-close.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-open-close.html</a>
		/// </summary>
		/// <param name="selector">A descriptor thata describes the close index operation</param>
		public static CloseIndexResponse CloseIndex(this IElasticClient client,Indices indices, Func<CloseIndexDescriptor, ICloseIndexRequest> selector = null);

		/// <inheritdoc />
		public static CloseIndexResponse CloseIndex(this IElasticClient client,ICloseIndexRequest request);

		/// <inheritdoc />
		public static Task<CloseIndexResponse> CloseIndexAsync(this IElasticClient client,
			Indices indices,
			Func<CloseIndexDescriptor, ICloseIndexRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<CloseIndexResponse> CloseIndexAsync(this IElasticClient client,ICloseIndexRequest request, CancellationToken ct = default);
	}

}
