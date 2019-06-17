using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Removes application privileges.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeletePrivilegesResponse DeletePrivileges(this IElasticClient client, Name application, Name name,
			Func<DeletePrivilegesDescriptor, IDeletePrivilegesRequest> selector = null
		)
			=> client.Security.DeletePrivileges(application, name, selector);

		/// <inheritdoc
		///     cref="DeletePrivileges(Nest.Name,Nest.Name,System.Func{Nest.DeletePrivilegesDescriptor,Nest.IDeletePrivilegesRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeletePrivilegesResponse DeletePrivileges(this IElasticClient client, IDeletePrivilegesRequest request)
			=> client.Security.DeletePrivileges(request);

		/// <inheritdoc
		///     cref="DeletePrivileges(Nest.Name,Nest.Name,System.Func{Nest.DeletePrivilegesDescriptor,Nest.IDeletePrivilegesRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeletePrivilegesResponse> DeletePrivilegesAsync(this IElasticClient client, Name application, Name name,
			Func<DeletePrivilegesDescriptor, IDeletePrivilegesRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.DeletePrivilegesAsync(application, name, selector, ct);

		/// <inheritdoc
		///     cref="DeletePrivileges(Nest.Name,Nest.Name,System.Func{Nest.DeletePrivilegesDescriptor,Nest.IDeletePrivilegesRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeletePrivilegesResponse> DeletePrivilegesAsync(this IElasticClient client, IDeletePrivilegesRequest request,
			CancellationToken ct = default
		)
			=> client.Security.DeletePrivilegesAsync(request, ct);
	}
}
