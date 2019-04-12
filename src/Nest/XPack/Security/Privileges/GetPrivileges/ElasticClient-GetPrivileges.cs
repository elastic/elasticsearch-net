using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieves application privileges.
		/// </summary>
		IGetPrivilegesResponse GetPrivileges(Func<GetPrivilegesDescriptor, IGetPrivilegesRequest> selector = null);

		/// <inheritdoc cref="GetPrivileges(System.Func{Nest.GetPrivilegesDescriptor,Nest.IGetPrivilegesRequest})" />
		IGetPrivilegesResponse GetPrivileges(IGetPrivilegesRequest request);

		/// <inheritdoc cref="GetPrivileges(System.Func{Nest.GetPrivilegesDescriptor,Nest.IGetPrivilegesRequest})" />
		Task<IGetPrivilegesResponse> GetPrivilegesAsync(Func<GetPrivilegesDescriptor, IGetPrivilegesRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="GetPrivileges(System.Func{Nest.GetPrivilegesDescriptor,Nest.IGetPrivilegesRequest})" />
		Task<IGetPrivilegesResponse> GetPrivilegesAsync(IGetPrivilegesRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="GetPrivileges(System.Func{Nest.GetPrivilegesDescriptor,Nest.IGetPrivilegesRequest})" />
		public IGetPrivilegesResponse GetPrivileges(Func<GetPrivilegesDescriptor, IGetPrivilegesRequest> selector = null) =>
			GetPrivileges(selector.InvokeOrDefault(new GetPrivilegesDescriptor()));

		/// <inheritdoc cref="GetPrivileges(System.Func{Nest.GetPrivilegesDescriptor,Nest.IGetPrivilegesRequest})" />
		public IGetPrivilegesResponse GetPrivileges(IGetPrivilegesRequest request) =>
			DoRequest<IGetPrivilegesRequest, GetPrivilegesResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="GetPrivileges(System.Func{Nest.GetPrivilegesDescriptor,Nest.IGetPrivilegesRequest})" />
		public Task<IGetPrivilegesResponse> GetPrivilegesAsync(
			Func<GetPrivilegesDescriptor, IGetPrivilegesRequest> selector = null,
			CancellationToken ct = default
		) => GetPrivilegesAsync(selector.InvokeOrDefault(new GetPrivilegesDescriptor()), ct);

		/// <inheritdoc cref="GetPrivileges(System.Func{Nest.GetPrivilegesDescriptor,Nest.IGetPrivilegesRequest})" />
		public Task<IGetPrivilegesResponse> GetPrivilegesAsync(IGetPrivilegesRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetPrivilegesRequest, IGetPrivilegesResponse, GetPrivilegesResponse>(request, request.RequestParameters, ct);
	}
}
