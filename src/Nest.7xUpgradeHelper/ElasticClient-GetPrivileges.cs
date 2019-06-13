using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieves application privileges.
		/// </summary>
		public static GetPrivilegesResponse GetPrivileges(this IElasticClient client,Func<GetPrivilegesDescriptor, IGetPrivilegesRequest> selector = null);

		/// <inheritdoc cref="GetPrivileges(System.Func{Nest.GetPrivilegesDescriptor,Nest.IGetPrivilegesRequest})" />
		public static GetPrivilegesResponse GetPrivileges(this IElasticClient client,IGetPrivilegesRequest request);

		/// <inheritdoc cref="GetPrivileges(System.Func{Nest.GetPrivilegesDescriptor,Nest.IGetPrivilegesRequest})" />
		public static Task<GetPrivilegesResponse> GetPrivilegesAsync(this IElasticClient client,Func<GetPrivilegesDescriptor, IGetPrivilegesRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="GetPrivileges(System.Func{Nest.GetPrivilegesDescriptor,Nest.IGetPrivilegesRequest})" />
		public static Task<GetPrivilegesResponse> GetPrivilegesAsync(this IElasticClient client,IGetPrivilegesRequest request, CancellationToken ct = default);
	}

}
