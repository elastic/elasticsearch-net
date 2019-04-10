using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Triggers execution of a specific step in the lifecycle policy.
		/// Manually moves an index into the specified step and executes that step.
		/// You must specify both the current step and the step to be executed in the body of the request.
		/// The request will fail if the current step does not match the step currently being executed for the index.
		/// This is to prevent the index from being moved from an unexpected step into the next step.
		/// </summary>
		IIlmMoveToStepResponse IlmMoveToStep(Func<IlmMoveToStepDescriptor, IIlmMoveToStepRequest> selector = null);

		/// <inheritdoc cref="IlmMoveToStep(System.Func{Nest.IlmMoveToStepDescriptor,Nest.IIlmMoveToStepRequest})" />
		IIlmMoveToStepResponse IlmMoveToStep(IIlmMoveToStepRequest request);

		/// <inheritdoc cref="IlmMoveToStep(System.Func{Nest.IlmMoveToStepDescriptor,Nest.IIlmMoveToStepRequest})" />
		Task<IIlmMoveToStepResponse> IlmMoveToStepAsync(Func<IlmMoveToStepDescriptor, IIlmMoveToStepRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="IlmMoveToStep(System.Func{Nest.IlmMoveToStepDescriptor,Nest.IIlmMoveToStepRequest})" />
		Task<IIlmMoveToStepResponse> IlmMoveToStepAsync(IIlmMoveToStepRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="IlmMoveToStep(System.Func{Nest.IlmMoveToStepDescriptor,Nest.IIlmMoveToStepRequest})" />
		public IIlmMoveToStepResponse IlmMoveToStep(Func<IlmMoveToStepDescriptor, IIlmMoveToStepRequest> selector = null) =>
			IlmMoveToStep(selector.InvokeOrDefault(new IlmMoveToStepDescriptor()));

		/// <inheritdoc cref="IlmMoveToStep(System.Func{Nest.IlmMoveToStepDescriptor,Nest.IIlmMoveToStepRequest})" />
		public IIlmMoveToStepResponse IlmMoveToStep(IIlmMoveToStepRequest request) =>
			Dispatcher.Dispatch<IIlmMoveToStepRequest, IlmMoveToStepRequestParameters, IlmMoveToStepResponse>(
				request,
				LowLevelDispatch.IlmMoveToStepDispatch<IlmMoveToStepResponse>
			);

		/// <inheritdoc cref="IlmMoveToStep(System.Func{Nest.IlmMoveToStepDescriptor,Nest.IIlmMoveToStepRequest})" />
		public Task<IIlmMoveToStepResponse> IlmMoveToStepAsync(Func<IlmMoveToStepDescriptor, IIlmMoveToStepRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			IlmMoveToStepAsync(selector.InvokeOrDefault(new IlmMoveToStepDescriptor()), cancellationToken);

		/// <inheritdoc cref="IlmMoveToStep(System.Func{Nest.IlmMoveToStepDescriptor,Nest.IIlmMoveToStepRequest})" />
		public Task<IIlmMoveToStepResponse> IlmMoveToStepAsync(IIlmMoveToStepRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IIlmMoveToStepRequest, IlmMoveToStepRequestParameters, IlmMoveToStepResponse, IIlmMoveToStepResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.IlmMoveToStepDispatchAsync<IlmMoveToStepResponse>
			);
	}
}
