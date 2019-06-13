using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static DeleteRoleResponse DeleteRole(this IElasticClient client,Name role, Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector = null);

		/// <inheritdoc />
		public static DeleteRoleResponse DeleteRole(this IElasticClient client,IDeleteRoleRequest request);

		/// <inheritdoc />
		public static Task<DeleteRoleResponse> DeleteRoleAsync(this IElasticClient client,Name role, Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<DeleteRoleResponse> DeleteRoleAsync(this IElasticClient client,IDeleteRoleRequest request, CancellationToken ct = default);
	}

}
