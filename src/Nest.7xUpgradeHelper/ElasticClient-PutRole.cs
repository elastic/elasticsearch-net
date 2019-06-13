using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static PutRoleResponse PutRole(this IElasticClient client,Name role, Func<PutRoleDescriptor, IPutRoleRequest> selector = null);

		/// <inheritdoc />
		public static PutRoleResponse PutRole(this IElasticClient client,IPutRoleRequest request);

		/// <inheritdoc />
		public static Task<PutRoleResponse> PutRoleAsync(this IElasticClient client,Name role, Func<PutRoleDescriptor, IPutRoleRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<PutRoleResponse> PutRoleAsync(this IElasticClient client,IPutRoleRequest request, CancellationToken ct = default);
	}

}
