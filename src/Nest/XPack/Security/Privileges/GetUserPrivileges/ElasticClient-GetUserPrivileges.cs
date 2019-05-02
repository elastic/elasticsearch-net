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
		GetUserPrivilegesResponse GetUserPrivileges(Func<GetUserPrivilegesDescriptor, IGetUserPrivilegesRequest> selector = null);

		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		GetUserPrivilegesResponse GetUserPrivileges(IGetUserPrivilegesRequest request);

		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		Task<GetUserPrivilegesResponse> GetUserPrivilegesAsync(Func<GetUserPrivilegesDescriptor, IGetUserPrivilegesRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		Task<GetUserPrivilegesResponse> GetUserPrivilegesAsync(IGetUserPrivilegesRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		public GetUserPrivilegesResponse GetUserPrivileges(Func<GetUserPrivilegesDescriptor, IGetUserPrivilegesRequest> selector = null) =>
			GetUserPrivileges(selector.InvokeOrDefault(new GetUserPrivilegesDescriptor()));

		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		public GetUserPrivilegesResponse GetUserPrivileges(IGetUserPrivilegesRequest request) =>
			DoRequest<IGetUserPrivilegesRequest, GetUserPrivilegesResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		public Task<GetUserPrivilegesResponse> GetUserPrivilegesAsync(
			Func<GetUserPrivilegesDescriptor, IGetUserPrivilegesRequest> selector = null,
			CancellationToken ct = default
		) => GetUserPrivilegesAsync(selector.InvokeOrDefault(new GetUserPrivilegesDescriptor()), ct);

		/// <inheritdoc cref="GetUserPrivileges(System.Func{Nest.GetUserPrivilegesDescriptor,Nest.IGetUserPrivilegesRequest})" />
		public Task<GetUserPrivilegesResponse> GetUserPrivilegesAsync(IGetUserPrivilegesRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetUserPrivilegesRequest, GetUserPrivilegesResponse>
				(request, request.RequestParameters, ct);
	}
}
