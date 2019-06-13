using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static GetRoleMappingResponse GetRoleMapping(this IElasticClient client,Func<GetRoleMappingDescriptor, IGetRoleMappingRequest> selector = null);

		/// <inheritdoc />
		public static GetRoleMappingResponse GetRoleMapping(this IElasticClient client,IGetRoleMappingRequest request);

		/// <inheritdoc />
		public static Task<GetRoleMappingResponse> GetRoleMappingAsync(this IElasticClient client,Func<GetRoleMappingDescriptor, IGetRoleMappingRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<GetRoleMappingResponse> GetRoleMappingAsync(this IElasticClient client,IGetRoleMappingRequest request,
			CancellationToken ct = default
		);
	}

}
