using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IPutUserResponse PutUser(Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null);

		/// <inheritdoc />
		IPutUserResponse PutUser(IPutUserRequest request);

		/// <inheritdoc />
		Task<IPutUserResponse> PutUserAsync(Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IPutUserResponse> PutUserAsync(IPutUserRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IPutUserResponse PutUser(Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null) =>
			PutUser(selector.InvokeOrDefault(new PutUserDescriptor(username)));

		/// <inheritdoc />
		public IPutUserResponse PutUser(IPutUserRequest request) =>
			Dispatcher.Dispatch<IPutUserRequest, PutUserRequestParameters, PutUserResponse>(
				request,
				LowLevelDispatch.SecurityPutUserDispatch<PutUserResponse>
			);

		/// <inheritdoc />
		public Task<IPutUserResponse> PutUserAsync(Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			PutUserAsync(selector.InvokeOrDefault(new PutUserDescriptor(username)), cancellationToken);

		/// <inheritdoc />
		public Task<IPutUserResponse> PutUserAsync(IPutUserRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IPutUserRequest, PutUserRequestParameters, PutUserResponse, IPutUserResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.SecurityPutUserDispatchAsync<PutUserResponse>
			);
	}
}
