using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieves application privileges for authenticated user.
		/// </summary>
		public static GetUserPrivilegesResponse GetUserPrivileges(this IElasticClient client,Func<GetUserPrivilegesDescriptor, IGetUserPrivilegesRequest> selector = null);

		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		public static GetUserPrivilegesResponse GetUserPrivileges(this IElasticClient client,IGetUserPrivilegesRequest request);

		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		public static Task<GetUserPrivilegesResponse> GetUserPrivilegesAsync(this IElasticClient client,Func<GetUserPrivilegesDescriptor, IGetUserPrivilegesRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		public static Task<GetUserPrivilegesResponse> GetUserPrivilegesAsync(this IElasticClient client,IGetUserPrivilegesRequest request, CancellationToken ct = default);
	}

}
