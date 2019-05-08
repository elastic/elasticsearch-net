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
		IPutUserResponse PutUser(Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null);

		/// <inheritdoc cref="PutUser(Nest.Name,System.Func{Nest.PutUserDescriptor,Nest.IPutUserRequest})" />
		IPutUserResponse PutUser(IPutUserRequest request);

		/// <inheritdoc cref="PutUser(Nest.Name,System.Func{Nest.PutUserDescriptor,Nest.IPutUserRequest})" />
		Task<IPutUserResponse> PutUserAsync(Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="PutUser(Nest.Name,System.Func{Nest.PutUserDescriptor,Nest.IPutUserRequest})" />
		Task<IPutUserResponse> PutUserAsync(IPutUserRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="PutUser(Nest.Name,System.Func{Nest.PutUserDescriptor,Nest.IPutUserRequest})" />
		public IPutUserResponse PutUser(Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null) =>
			PutUser(selector.InvokeOrDefault(new PutUserDescriptor(username)));

		/// <inheritdoc cref="PutUser(Nest.Name,System.Func{Nest.PutUserDescriptor,Nest.IPutUserRequest})" />
		public IPutUserResponse PutUser(IPutUserRequest request) =>
			Dispatcher.Dispatch<IPutUserRequest, PutUserRequestParameters, PutUserResponse>(
				request,
				LowLevelDispatch.XpackSecurityPutUserDispatch<PutUserResponse>
			);

		/// <inheritdoc cref="PutUser(Nest.Name,System.Func{Nest.PutUserDescriptor,Nest.IPutUserRequest})" />
		public Task<IPutUserResponse> PutUserAsync(Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			PutUserAsync(selector.InvokeOrDefault(new PutUserDescriptor(username)), cancellationToken);

		/// <inheritdoc cref="PutUser(Nest.Name,System.Func{Nest.PutUserDescriptor,Nest.IPutUserRequest})" />
		public Task<IPutUserResponse> PutUserAsync(IPutUserRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IPutUserRequest, PutUserRequestParameters, PutUserResponse, IPutUserResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.XpackSecurityPutUserDispatchAsync<PutUserResponse>
			);
	}
}
