using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Determine whether the authenticated user has a specified list of privileges.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static HasPrivilegesResponse HasPrivileges(this IElasticClient client,
			Func<HasPrivilegesDescriptor, IHasPrivilegesRequest> selector = null
		)
			=> client.Security.HasPrivileges(selector);

		/// <inheritdoc cref="HasPrivileges(System.Func{Nest.HasPrivilegesDescriptor,Nest.IHasPrivilegesRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static HasPrivilegesResponse HasPrivileges(this IElasticClient client, IHasPrivilegesRequest request)
			=> client.Security.HasPrivileges(request);

		/// <inheritdoc cref="HasPrivileges(System.Func{Nest.HasPrivilegesDescriptor,Nest.IHasPrivilegesRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<HasPrivilegesResponse> HasPrivilegesAsync(this IElasticClient client,
			Func<HasPrivilegesDescriptor, IHasPrivilegesRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.HasPrivilegesAsync(selector, ct);

		/// <inheritdoc cref="HasPrivileges(System.Func{Nest.HasPrivilegesDescriptor,Nest.IHasPrivilegesRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<HasPrivilegesResponse> HasPrivilegesAsync(this IElasticClient client, IHasPrivilegesRequest request,
			CancellationToken ct = default
		)
			=> client.Security.HasPrivilegesAsync(request, ct);
	}
}
