using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static DeleteRoleMappingResponse DeleteRoleMapping(this IElasticClient client,Name role, Func<DeleteRoleMappingDescriptor, IDeleteRoleMappingRequest> selector = null);

		/// <inheritdoc />
		public static DeleteRoleMappingResponse DeleteRoleMapping(this IElasticClient client,IDeleteRoleMappingRequest request);

		/// <inheritdoc />
		public static Task<DeleteRoleMappingResponse> DeleteRoleMappingAsync(this IElasticClient client,Name role,
			Func<DeleteRoleMappingDescriptor, IDeleteRoleMappingRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<DeleteRoleMappingResponse> DeleteRoleMappingAsync(this IElasticClient client,IDeleteRoleMappingRequest request,
			CancellationToken ct = default
		);
	}

}
