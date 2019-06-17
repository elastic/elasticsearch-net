using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.Create(), please update this usage.")]
		public static CreateIndexResponse CreateIndex(this IElasticClient client, IndexName index,
			Func<CreateIndexDescriptor, ICreateIndexRequest> selector = null
		)
			=> client.Indices.Create(index, selector);

		[Obsolete("Moved to client.Indices.Create(), please update this usage.")]
		public static CreateIndexResponse CreateIndex(this IElasticClient client, ICreateIndexRequest request)
			=> client.Indices.Create(request);

		[Obsolete("Moved to client.Indices.CreateAsync(), please update this usage.")]
		public static Task<CreateIndexResponse> CreateIndexAsync(this IElasticClient client,
			IndexName index,
			Func<CreateIndexDescriptor, ICreateIndexRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.CreateAsync(index, selector, ct);

		[Obsolete("Moved to client.Indices.CreateAsync(), please update this usage.")]
		public static Task<CreateIndexResponse> CreateIndexAsync(this IElasticClient client, ICreateIndexRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.CreateAsync(request, ct);
	}
}
