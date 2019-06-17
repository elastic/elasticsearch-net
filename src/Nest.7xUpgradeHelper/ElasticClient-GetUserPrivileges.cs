using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieves application privileges for authenticated user.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetUserPrivilegesResponse GetUserPrivileges(this IElasticClient client,
			Func<GetUserPrivilegesDescriptor, IGetUserPrivilegesRequest> selector = null
		)
			=> client.Security.GetUserPrivileges(selector);

		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetUserPrivilegesResponse GetUserPrivileges(this IElasticClient client, IGetUserPrivilegesRequest request)
			=> client.Security.GetUserPrivileges(request);

		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetUserPrivilegesResponse> GetUserPrivilegesAsync(this IElasticClient client,
			Func<GetUserPrivilegesDescriptor, IGetUserPrivilegesRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.GetUserPrivilegesAsync(selector, ct);

		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetUserPrivilegesResponse> GetUserPrivilegesAsync(this IElasticClient client, IGetUserPrivilegesRequest request,
			CancellationToken ct = default
		)
			=> client.Security.GetUserPrivilegesAsync(request, ct);
	}
}
