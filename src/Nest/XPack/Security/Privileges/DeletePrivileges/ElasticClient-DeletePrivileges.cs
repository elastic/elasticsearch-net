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
		IDeletePrivilegesResponse DeletePrivileges(Name application, Name name, Func<DeletePrivilegesDescriptor, IDeletePrivilegesRequest> selector = null);

		/// <inheritdoc cref="DeletePrivileges(Nest.Name,Nest.Name,System.Func{Nest.DeletePrivilegesDescriptor,Nest.IDeletePrivilegesRequest})" />
		IDeletePrivilegesResponse DeletePrivileges(IDeletePrivilegesRequest request);

		/// <inheritdoc cref="DeletePrivileges(Nest.Name,Nest.Name,System.Func{Nest.DeletePrivilegesDescriptor,Nest.IDeletePrivilegesRequest})" />
		Task<IDeletePrivilegesResponse> DeletePrivilegesAsync(Name application, Name name, Func<DeletePrivilegesDescriptor, IDeletePrivilegesRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="DeletePrivileges(Nest.Name,Nest.Name,System.Func{Nest.DeletePrivilegesDescriptor,Nest.IDeletePrivilegesRequest})" />
		Task<IDeletePrivilegesResponse> DeletePrivilegesAsync(IDeletePrivilegesRequest request, CancellationToken cancellationToken = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="DeletePrivileges(Nest.Name,Nest.Name,System.Func{Nest.DeletePrivilegesDescriptor,Nest.IDeletePrivilegesRequest})" />
		public IDeletePrivilegesResponse DeletePrivileges(Name application, Name name, Func<DeletePrivilegesDescriptor, IDeletePrivilegesRequest> selector = null) =>
			DeletePrivileges(selector.InvokeOrDefault(new DeletePrivilegesDescriptor(application, name)));

		/// <inheritdoc cref="DeletePrivileges(Nest.Name,Nest.Name,System.Func{Nest.DeletePrivilegesDescriptor,Nest.IDeletePrivilegesRequest})" />
		public IDeletePrivilegesResponse DeletePrivileges(IDeletePrivilegesRequest request) =>
			Dispatcher.Dispatch<IDeletePrivilegesRequest, DeletePrivilegesRequestParameters, DeletePrivilegesResponse>(
				request,
				(p, d) => LowLevelDispatch.XpackSecurityDeletePrivilegesDispatch<DeletePrivilegesResponse>(p)
			);

		/// <inheritdoc cref="DeletePrivileges(Nest.Name,Nest.Name,System.Func{Nest.DeletePrivilegesDescriptor,Nest.IDeletePrivilegesRequest})" />
		public Task<IDeletePrivilegesResponse> DeletePrivilegesAsync(Name application, Name name, Func<DeletePrivilegesDescriptor, IDeletePrivilegesRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			DeletePrivilegesAsync(selector.InvokeOrDefault(new DeletePrivilegesDescriptor(application, name)), cancellationToken);

		/// <inheritdoc cref="DeletePrivileges(Nest.Name,Nest.Name,System.Func{Nest.DeletePrivilegesDescriptor,Nest.IDeletePrivilegesRequest})" />
		public Task<IDeletePrivilegesResponse> DeletePrivilegesAsync(IDeletePrivilegesRequest request, CancellationToken cancellationToken = default
		) =>
			Dispatcher.DispatchAsync<IDeletePrivilegesRequest, DeletePrivilegesRequestParameters, DeletePrivilegesResponse, IDeletePrivilegesResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.XpackSecurityDeletePrivilegesDispatchAsync<DeletePrivilegesResponse>(p, c)
			);
	}
}
