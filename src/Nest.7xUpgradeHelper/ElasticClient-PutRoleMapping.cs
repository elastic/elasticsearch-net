using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static PutRoleMappingResponse PutRoleMapping(this IElasticClient client,Name role, Func<PutRoleMappingDescriptor, IPutRoleMappingRequest> selector = null);

		/// <inheritdoc />
		public static PutRoleMappingResponse PutRoleMapping(this IElasticClient client,IPutRoleMappingRequest request);

		/// <inheritdoc />
		public static Task<PutRoleMappingResponse> PutRoleMappingAsync(this IElasticClient client,Name role, Func<PutRoleMappingDescriptor, IPutRoleMappingRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<PutRoleMappingResponse> PutRoleMappingAsync(this IElasticClient client,IPutRoleMappingRequest request,
			CancellationToken ct = default
		);
	}

}
