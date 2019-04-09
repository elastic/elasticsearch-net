using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Adds or updates application privileges.
		/// </summary>
		IPutPrivilegesResponse PutPrivileges(Func<PutPrivilegesDescriptor, IPutPrivilegesRequest> selector);

		/// <inheritdoc cref="PutPrivileges(System.Func{Nest.PutPrivilegesDescriptor,Nest.IPutPrivilegesRequest})" />
		IPutPrivilegesResponse PutPrivileges(IPutPrivilegesRequest request);

		/// <inheritdoc cref="PutPrivileges(System.Func{Nest.PutPrivilegesDescriptor,Nest.IPutPrivilegesRequest})" />
		Task<IPutPrivilegesResponse> PutPrivilegesAsync(Func<PutPrivilegesDescriptor, IPutPrivilegesRequest> selector, CancellationToken ct = default);

		/// <inheritdoc cref="PutPrivileges(System.Func{Nest.PutPrivilegesDescriptor,Nest.IPutPrivilegesRequest})" />
		Task<IPutPrivilegesResponse> PutPrivilegesAsync(IPutPrivilegesRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="PutPrivileges(System.Func{Nest.PutPrivilegesDescriptor,Nest.IPutPrivilegesRequest})" />
		public IPutPrivilegesResponse PutPrivileges(Func<PutPrivilegesDescriptor, IPutPrivilegesRequest> selector) =>
			PutPrivileges(selector.InvokeOrDefault(new PutPrivilegesDescriptor()));

		/// <inheritdoc cref="PutPrivileges(System.Func{Nest.PutPrivilegesDescriptor,Nest.IPutPrivilegesRequest})" />
		public IPutPrivilegesResponse PutPrivileges(IPutPrivilegesRequest request) =>
			Dispatch2<IPutPrivilegesRequest, PutPrivilegesResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="PutPrivileges(System.Func{Nest.PutPrivilegesDescriptor,Nest.IPutPrivilegesRequest})" />
		public Task<IPutPrivilegesResponse> PutPrivilegesAsync(
			Func<PutPrivilegesDescriptor, IPutPrivilegesRequest> selector,
			CancellationToken ct = default
		) => PutPrivilegesAsync(selector.InvokeOrDefault(new PutPrivilegesDescriptor()), ct);

		/// <inheritdoc cref="PutPrivileges(System.Func{Nest.PutPrivilegesDescriptor,Nest.IPutPrivilegesRequest})" />
		public Task<IPutPrivilegesResponse> PutPrivilegesAsync(IPutPrivilegesRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IPutPrivilegesRequest, IPutPrivilegesResponse, PutPrivilegesResponse>(request, request.RequestParameters, ct);
	}
}
