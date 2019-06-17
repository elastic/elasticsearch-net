using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Adds or updates application privileges.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PutPrivilegesResponse PutPrivileges(this IElasticClient client, Func<PutPrivilegesDescriptor, IPutPrivilegesRequest> selector)
			=> client.Security.PutPrivileges(selector);

		/// <inheritdoc cref="PutPrivileges(System.Func{Nest.PutPrivilegesDescriptor,Nest.IPutPrivilegesRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PutPrivilegesResponse PutPrivileges(this IElasticClient client, IPutPrivilegesRequest request)
			=> client.Security.PutPrivileges(request);

		/// <inheritdoc cref="PutPrivileges(System.Func{Nest.PutPrivilegesDescriptor,Nest.IPutPrivilegesRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PutPrivilegesResponse> PutPrivilegesAsync(this IElasticClient client,
			Func<PutPrivilegesDescriptor, IPutPrivilegesRequest> selector, CancellationToken ct = default
		)
			=> client.Security.PutPrivilegesAsync(selector, ct);

		/// <inheritdoc cref="PutPrivileges(System.Func{Nest.PutPrivilegesDescriptor,Nest.IPutPrivilegesRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PutPrivilegesResponse> PutPrivilegesAsync(this IElasticClient client, IPutPrivilegesRequest request,
			CancellationToken ct = default
		)
			=> client.Security.PutPrivilegesAsync(request, ct);
	}
}
