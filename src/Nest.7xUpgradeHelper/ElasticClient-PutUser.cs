using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static PutUserResponse PutUser(this IElasticClient client,Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null);

		/// <inheritdoc />
		public static PutUserResponse PutUser(this IElasticClient client,IPutUserRequest request);

		/// <inheritdoc />
		public static Task<PutUserResponse> PutUserAsync(this IElasticClient client,Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<PutUserResponse> PutUserAsync(this IElasticClient client,IPutUserRequest request, CancellationToken ct = default);
	}

}
