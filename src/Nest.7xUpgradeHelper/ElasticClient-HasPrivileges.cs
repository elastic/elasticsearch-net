using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Determine whether the authenticated user has a specified list of privileges.
		/// </summary>
		public static HasPrivilegesResponse HasPrivileges(this IElasticClient client,Func<HasPrivilegesDescriptor, IHasPrivilegesRequest> selector = null);

		/// <inheritdoc cref="HasPrivileges(System.Func{Nest.HasPrivilegesDescriptor,Nest.IHasPrivilegesRequest})" />
		public static HasPrivilegesResponse HasPrivileges(this IElasticClient client,IHasPrivilegesRequest request);

		/// <inheritdoc cref="HasPrivileges(System.Func{Nest.HasPrivilegesDescriptor,Nest.IHasPrivilegesRequest})" />
		public static Task<HasPrivilegesResponse> HasPrivilegesAsync(this IElasticClient client,Func<HasPrivilegesDescriptor, IHasPrivilegesRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="HasPrivileges(System.Func{Nest.HasPrivilegesDescriptor,Nest.IHasPrivilegesRequest})" />
		public static Task<HasPrivilegesResponse> HasPrivilegesAsync(this IElasticClient client,IHasPrivilegesRequest request, CancellationToken ct = default);
	}

}
