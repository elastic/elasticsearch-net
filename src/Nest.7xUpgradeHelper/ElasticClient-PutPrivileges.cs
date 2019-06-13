using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Adds or updates application privileges.
		/// </summary>
		public static PutPrivilegesResponse PutPrivileges(this IElasticClient client,Func<PutPrivilegesDescriptor, IPutPrivilegesRequest> selector);

		/// <inheritdoc cref="PutPrivileges(System.Func{Nest.PutPrivilegesDescriptor,Nest.IPutPrivilegesRequest})" />
		public static PutPrivilegesResponse PutPrivileges(this IElasticClient client,IPutPrivilegesRequest request);

		/// <inheritdoc cref="PutPrivileges(System.Func{Nest.PutPrivilegesDescriptor,Nest.IPutPrivilegesRequest})" />
		public static Task<PutPrivilegesResponse> PutPrivilegesAsync(this IElasticClient client,Func<PutPrivilegesDescriptor, IPutPrivilegesRequest> selector, CancellationToken ct = default);

		/// <inheritdoc cref="PutPrivileges(System.Func{Nest.PutPrivilegesDescriptor,Nest.IPutPrivilegesRequest})" />
		public static Task<PutPrivilegesResponse> PutPrivilegesAsync(this IElasticClient client,IPutPrivilegesRequest request, CancellationToken ct = default);
	}

}
