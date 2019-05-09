using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Adds and updates users in the native realm. These users are commonly referred to as native users.
		/// </summary>
		PutUserResponse PutUser(Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null);

		/// <inheritdoc cref="PutUser(Nest.Name,System.Func{Nest.PutUserDescriptor,Nest.IPutUserRequest})" />
		PutUserResponse PutUser(IPutUserRequest request);

		/// <inheritdoc cref="PutUser(Nest.Name,System.Func{Nest.PutUserDescriptor,Nest.IPutUserRequest})" />
		Task<PutUserResponse> PutUserAsync(Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="PutUser(Nest.Name,System.Func{Nest.PutUserDescriptor,Nest.IPutUserRequest})" />
		Task<PutUserResponse> PutUserAsync(IPutUserRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="PutUser(Nest.Name,System.Func{Nest.PutUserDescriptor,Nest.IPutUserRequest})" />
		public PutUserResponse PutUser(Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null) =>
			PutUser(selector.InvokeOrDefault(new PutUserDescriptor(username)));

		/// <inheritdoc cref="PutUser(Nest.Name,System.Func{Nest.PutUserDescriptor,Nest.IPutUserRequest})" />
		public PutUserResponse PutUser(IPutUserRequest request) =>
			DoRequest<IPutUserRequest, PutUserResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="PutUser(Nest.Name,System.Func{Nest.PutUserDescriptor,Nest.IPutUserRequest})" />
		public Task<PutUserResponse> PutUserAsync(
			Name username,
			Func<PutUserDescriptor, IPutUserRequest> selector = null,
			CancellationToken ct = default
		) => PutUserAsync(selector.InvokeOrDefault(new PutUserDescriptor(username)), ct);

		/// <inheritdoc cref="PutUser(Nest.Name,System.Func{Nest.PutUserDescriptor,Nest.IPutUserRequest})" />
		public Task<PutUserResponse> PutUserAsync(IPutUserRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IPutUserRequest, PutUserResponse>
				(request, request.RequestParameters, ct);
	}
}
