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
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-open-close.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the open index operation</param>
		public static OpenIndexResponse OpenIndex(this IElasticClient client,Indices indices, Func<OpenIndexDescriptor, IOpenIndexRequest> selector = null);

		/// <inheritdoc />
		public static OpenIndexResponse OpenIndex(this IElasticClient client,IOpenIndexRequest request);

		/// <inheritdoc />
		public static Task<OpenIndexResponse> OpenIndexAsync(this IElasticClient client,
			Indices indices,
			Func<OpenIndexDescriptor, IOpenIndexRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<OpenIndexResponse> OpenIndexAsync(this IElasticClient client,IOpenIndexRequest request, CancellationToken ct = default);
	}

}
