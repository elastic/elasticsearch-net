using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieves application privileges for authenticated user.
		/// </summary>
		IGetUserPrivilegesResponse GetUserPrivileges(Func<GetUserPrivilegesDescriptor, IGetUserPrivilegesRequest> selector = null);

		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		IGetUserPrivilegesResponse GetUserPrivileges(IGetUserPrivilegesRequest request);

		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		Task<IGetUserPrivilegesResponse> GetUserPrivilegesAsync(Func<GetUserPrivilegesDescriptor, IGetUserPrivilegesRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		Task<IGetUserPrivilegesResponse> GetUserPrivilegesAsync(IGetUserPrivilegesRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		public IGetUserPrivilegesResponse GetUserPrivileges(Func<GetUserPrivilegesDescriptor, IGetUserPrivilegesRequest> selector = null) =>
			GetUserPrivileges(selector.InvokeOrDefault(new GetUserPrivilegesDescriptor()));

		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		public IGetUserPrivilegesResponse GetUserPrivileges(IGetUserPrivilegesRequest request) =>
			Dispatch2<IGetUserPrivilegesRequest, GetUserPrivilegesResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		public Task<IGetUserPrivilegesResponse> GetUserPrivilegesAsync(
			Func<GetUserPrivilegesDescriptor, IGetUserPrivilegesRequest> selector = null,
			CancellationToken ct = default
		) => GetUserPrivilegesAsync(selector.InvokeOrDefault(new GetUserPrivilegesDescriptor()), ct);

		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		public Task<IGetUserPrivilegesResponse> GetUserPrivilegesAsync(IGetUserPrivilegesRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IGetUserPrivilegesRequest, IGetUserPrivilegesResponse, GetUserPrivilegesResponse>
				(request, request.RequestParameters, ct);
	}
}
