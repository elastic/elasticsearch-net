using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Removes application privileges.
		/// </summary>
		DeletePrivilegesResponse DeletePrivileges(Name application, Name name, Func<DeletePrivilegesDescriptor, IDeletePrivilegesRequest> selector = null);

		/// <inheritdoc cref="DeletePrivileges(Nest.Name,Nest.Name,System.Func{Nest.DeletePrivilegesDescriptor,Nest.IDeletePrivilegesRequest})" />
		DeletePrivilegesResponse DeletePrivileges(IDeletePrivilegesRequest request);

		/// <inheritdoc cref="DeletePrivileges(Nest.Name,Nest.Name,System.Func{Nest.DeletePrivilegesDescriptor,Nest.IDeletePrivilegesRequest})" />
		Task<DeletePrivilegesResponse> DeletePrivilegesAsync(Name application, Name name, Func<DeletePrivilegesDescriptor, IDeletePrivilegesRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="DeletePrivileges(Nest.Name,Nest.Name,System.Func{Nest.DeletePrivilegesDescriptor,Nest.IDeletePrivilegesRequest})" />
		Task<DeletePrivilegesResponse> DeletePrivilegesAsync(IDeletePrivilegesRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="DeletePrivileges(Nest.Name,Nest.Name,System.Func{Nest.DeletePrivilegesDescriptor,Nest.IDeletePrivilegesRequest})" />
		public DeletePrivilegesResponse DeletePrivileges(Name application, Name name, Func<DeletePrivilegesDescriptor, IDeletePrivilegesRequest> selector = null) =>
			DeletePrivileges(selector.InvokeOrDefault(new DeletePrivilegesDescriptor(application, name)));

		/// <inheritdoc cref="DeletePrivileges(Nest.Name,Nest.Name,System.Func{Nest.DeletePrivilegesDescriptor,Nest.IDeletePrivilegesRequest})" />
		public DeletePrivilegesResponse DeletePrivileges(IDeletePrivilegesRequest request) =>
			DoRequest<IDeletePrivilegesRequest, DeletePrivilegesResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="DeletePrivileges(Nest.Name,Nest.Name,System.Func{Nest.DeletePrivilegesDescriptor,Nest.IDeletePrivilegesRequest})" />
		public Task<DeletePrivilegesResponse> DeletePrivilegesAsync(
			Name application,
			Name name,
			Func<DeletePrivilegesDescriptor, IDeletePrivilegesRequest> selector = null,
			CancellationToken ct = default
		) => DeletePrivilegesAsync(selector.InvokeOrDefault(new DeletePrivilegesDescriptor(application, name)), ct);

		/// <inheritdoc cref="DeletePrivileges(Nest.Name,Nest.Name,System.Func{Nest.DeletePrivilegesDescriptor,Nest.IDeletePrivilegesRequest})" />
		public Task<DeletePrivilegesResponse> DeletePrivilegesAsync(IDeletePrivilegesRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeletePrivilegesRequest, DeletePrivilegesResponse>(request, request.RequestParameters, ct);
	}
}
