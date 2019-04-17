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
		PutPrivilegesResponse PutPrivileges(Func<PutPrivilegesDescriptor, IPutPrivilegesRequest> selector);

		/// <inheritdoc cref="PutPrivileges(System.Func{Nest.PutPrivilegesDescriptor,Nest.IPutPrivilegesRequest})" />
		PutPrivilegesResponse PutPrivileges(IPutPrivilegesRequest request);

		/// <inheritdoc cref="PutPrivileges(System.Func{Nest.PutPrivilegesDescriptor,Nest.IPutPrivilegesRequest})" />
		Task<PutPrivilegesResponse> PutPrivilegesAsync(Func<PutPrivilegesDescriptor, IPutPrivilegesRequest> selector, CancellationToken ct = default);

		/// <inheritdoc cref="PutPrivileges(System.Func{Nest.PutPrivilegesDescriptor,Nest.IPutPrivilegesRequest})" />
		Task<PutPrivilegesResponse> PutPrivilegesAsync(IPutPrivilegesRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="PutPrivileges(System.Func{Nest.PutPrivilegesDescriptor,Nest.IPutPrivilegesRequest})" />
		public PutPrivilegesResponse PutPrivileges(Func<PutPrivilegesDescriptor, IPutPrivilegesRequest> selector) =>
			PutPrivileges(selector.InvokeOrDefault(new PutPrivilegesDescriptor()));

		/// <inheritdoc cref="PutPrivileges(System.Func{Nest.PutPrivilegesDescriptor,Nest.IPutPrivilegesRequest})" />
		public PutPrivilegesResponse PutPrivileges(IPutPrivilegesRequest request) =>
			DoRequest<IPutPrivilegesRequest, PutPrivilegesResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="PutPrivileges(System.Func{Nest.PutPrivilegesDescriptor,Nest.IPutPrivilegesRequest})" />
		public Task<PutPrivilegesResponse> PutPrivilegesAsync(
			Func<PutPrivilegesDescriptor, IPutPrivilegesRequest> selector,
			CancellationToken ct = default
		) => PutPrivilegesAsync(selector.InvokeOrDefault(new PutPrivilegesDescriptor()), ct);

		/// <inheritdoc cref="PutPrivileges(System.Func{Nest.PutPrivilegesDescriptor,Nest.IPutPrivilegesRequest})" />
		public Task<PutPrivilegesResponse> PutPrivilegesAsync(IPutPrivilegesRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IPutPrivilegesRequest, PutPrivilegesResponse>(request, request.RequestParameters, ct);
	}
}
