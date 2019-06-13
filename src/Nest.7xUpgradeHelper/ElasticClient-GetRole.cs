using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static GetRoleResponse GetRole(this IElasticClient client,Func<GetRoleDescriptor, IGetRoleRequest> selector = null);

		/// <inheritdoc />
		public static GetRoleResponse GetRole(this IElasticClient client,IGetRoleRequest request);

		/// <inheritdoc />
		public static Task<GetRoleResponse> GetRoleAsync(this IElasticClient client,Func<GetRoleDescriptor, IGetRoleRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<GetRoleResponse> GetRoleAsync(this IElasticClient client,IGetRoleRequest request, CancellationToken ct = default);
	}

}
