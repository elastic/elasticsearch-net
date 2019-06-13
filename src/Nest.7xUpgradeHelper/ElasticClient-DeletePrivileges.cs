using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Removes application privileges.
		/// </summary>
		public static DeletePrivilegesResponse DeletePrivileges(this IElasticClient client,Name application, Name name, Func<DeletePrivilegesDescriptor, IDeletePrivilegesRequest> selector = null);

		/// <inheritdoc cref="DeletePrivileges(Nest.Name,Nest.Name,System.Func{Nest.DeletePrivilegesDescriptor,Nest.IDeletePrivilegesRequest})" />
		public static DeletePrivilegesResponse DeletePrivileges(this IElasticClient client,IDeletePrivilegesRequest request);

		/// <inheritdoc cref="DeletePrivileges(Nest.Name,Nest.Name,System.Func{Nest.DeletePrivilegesDescriptor,Nest.IDeletePrivilegesRequest})" />
		public static Task<DeletePrivilegesResponse> DeletePrivilegesAsync(this IElasticClient client,Name application, Name name, Func<DeletePrivilegesDescriptor, IDeletePrivilegesRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="DeletePrivileges(Nest.Name,Nest.Name,System.Func{Nest.DeletePrivilegesDescriptor,Nest.IDeletePrivilegesRequest})" />
		public static Task<DeletePrivilegesResponse> DeletePrivilegesAsync(this IElasticClient client,IDeletePrivilegesRequest request, CancellationToken ct = default);
	}

}
