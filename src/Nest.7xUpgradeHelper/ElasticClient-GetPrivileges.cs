using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieves application privileges.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetPrivilegesResponse GetPrivileges(this IElasticClient client,
			Func<GetPrivilegesDescriptor, IGetPrivilegesRequest> selector = null
		)
			=> client.Security.GetPrivileges(null, selector);

		/// <inheritdoc cref="GetPrivileges(System.Func{Nest.GetPrivilegesDescriptor,Nest.IGetPrivilegesRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetPrivilegesResponse GetPrivileges(this IElasticClient client, IGetPrivilegesRequest request)
			=> client.Security.GetPrivileges(request);

		/// <inheritdoc cref="GetPrivileges(System.Func{Nest.GetPrivilegesDescriptor,Nest.IGetPrivilegesRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetPrivilegesResponse> GetPrivilegesAsync(this IElasticClient client,
			Func<GetPrivilegesDescriptor, IGetPrivilegesRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.GetPrivilegesAsync(null, selector, ct);

		/// <inheritdoc cref="GetPrivileges(System.Func{Nest.GetPrivilegesDescriptor,Nest.IGetPrivilegesRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetPrivilegesResponse> GetPrivilegesAsync(this IElasticClient client, IGetPrivilegesRequest request,
			CancellationToken ct = default
		)
			=> client.Security.GetPrivilegesAsync(request, ct);
	}
}
