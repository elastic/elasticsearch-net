using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Start the index lifecycle management (ILM) plugin.
		/// Starts the ILM plugin if it is currently stopped. ILM is started automatically when the cluster is formed.
		/// Restarting ILM is only necessary if it has been stopped using the Stop ILM API.
		/// </summary>
		StartIlmResponse StartIlm(Func<StartIlmDescriptor, IStartIlmRequest> selector = null);

		/// <inheritdoc cref="StartIlm(System.Func{Nest.StartIlmDescriptor,Nest.IStartIlmRequest})" />
		StartIlmResponse StartIlm(IStartIlmRequest request);

		/// <inheritdoc cref="StartIlm(System.Func{Nest.StartIlmDescriptor,Nest.IStartIlmRequest})" />
		Task<StartIlmResponse> StartIlmAsync(Func<StartIlmDescriptor, IStartIlmRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="StartIlm(System.Func{Nest.StartIlmDescriptor,Nest.IStartIlmRequest})" />
		Task<StartIlmResponse> StartIlmAsync(IStartIlmRequest request,
			CancellationToken cancellationToken = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="StartIlm(System.Func{Nest.StartIlmDescriptor,Nest.IStartIlmRequest})" />
		public StartIlmResponse StartIlm(Func<StartIlmDescriptor, IStartIlmRequest> selector = null) =>
			StartIlm(selector.InvokeOrDefault(new StartIlmDescriptor()));

		/// <inheritdoc cref="StartIlm(System.Func{Nest.StartIlmDescriptor,Nest.IStartIlmRequest})" />
		public StartIlmResponse StartIlm(IStartIlmRequest request) =>
			DoRequest<IStartIlmRequest, StartIlmResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="StartIlm(System.Func{Nest.StartIlmDescriptor,Nest.IStartIlmRequest})" />
		public Task<StartIlmResponse> StartIlmAsync(Func<StartIlmDescriptor, IStartIlmRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			StartIlmAsync(selector.InvokeOrDefault(new StartIlmDescriptor()), cancellationToken);

		/// <inheritdoc cref="StartIlm(System.Func{Nest.StartIlmDescriptor,Nest.IStartIlmRequest})" />
		public Task<StartIlmResponse> StartIlmAsync(IStartIlmRequest request,
			CancellationToken cancellationToken = default
		) =>
			DoRequestAsync<IStartIlmRequest, StartIlmResponse>(request, request.RequestParameters, cancellationToken);

	}
}
