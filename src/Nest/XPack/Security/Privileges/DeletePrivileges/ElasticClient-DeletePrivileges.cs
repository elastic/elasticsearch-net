using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IDeletePrivilegesResponse DeletePrivileges(ApplicationName application, Name name, Func<DeletePrivilegesDescriptor, IDeletePrivilegesRequest> selector = null);

		/// <inheritdoc />
		IDeletePrivilegesResponse DeletePrivileges(IDeletePrivilegesRequest request);

		/// <inheritdoc />
		Task<IDeletePrivilegesResponse> DeletePrivilegesAsync(ApplicationName application, Name name, Func<DeletePrivilegesDescriptor, IDeletePrivilegesRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<IDeletePrivilegesResponse> DeletePrivilegesAsync(IDeletePrivilegesRequest request, CancellationToken cancellationToken = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeletePrivilegesResponse DeletePrivileges(ApplicationName application, Name name, Func<DeletePrivilegesDescriptor, IDeletePrivilegesRequest> selector = null) =>
			DeletePrivileges(selector.InvokeOrDefault(new DeletePrivilegesDescriptor(application, name)));

		/// <inheritdoc />
		public IDeletePrivilegesResponse DeletePrivileges(IDeletePrivilegesRequest request) =>
			Dispatcher.Dispatch<IDeletePrivilegesRequest, DeletePrivilegesRequestParameters, DeletePrivilegesResponse>(
				request,
				(p, d) => LowLevelDispatch.XpackSecurityDeletePrivilegesDispatch<DeletePrivilegesResponse>(p)
			);

		/// <inheritdoc />
		public Task<IDeletePrivilegesResponse> DeletePrivilegesAsync(ApplicationName application, Name name, Func<DeletePrivilegesDescriptor, IDeletePrivilegesRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			DeletePrivilegesAsync(selector.InvokeOrDefault(new DeletePrivilegesDescriptor(application, name)), cancellationToken);

		/// <inheritdoc />
		public Task<IDeletePrivilegesResponse> DeletePrivilegesAsync(IDeletePrivilegesRequest request, CancellationToken cancellationToken = default
		) =>
			Dispatcher.DispatchAsync<IDeletePrivilegesRequest, DeletePrivilegesRequestParameters, DeletePrivilegesResponse, IDeletePrivilegesResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.XpackSecurityDeletePrivilegesDispatchAsync<DeletePrivilegesResponse>(p, c)
			);
	}
}
