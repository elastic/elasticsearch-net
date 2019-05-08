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
		IMoveToStepResponse MoveToStep(IndexName index, Func<MoveToStepDescriptor, IMoveToStepRequest> selector = null);

		/// <inheritdoc cref="MoveToStep(Nest.IndexName,System.Func{Nest.MoveToStepDescriptor,Nest.IMoveToStepRequest})" />
		IMoveToStepResponse MoveToStep(IMoveToStepRequest request);

		/// <inheritdoc cref="MoveToStep(Nest.IndexName,System.Func{Nest.MoveToStepDescriptor,Nest.IMoveToStepRequest})" />
		Task<IMoveToStepResponse> MoveToStepAsync(IndexName index, Func<MoveToStepDescriptor, IMoveToStepRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="MoveToStep(Nest.IndexName,System.Func{Nest.MoveToStepDescriptor,Nest.IMoveToStepRequest})" />
		Task<IMoveToStepResponse> MoveToStepAsync(IMoveToStepRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="MoveToStep(Nest.IndexName,System.Func{Nest.MoveToStepDescriptor,Nest.IMoveToStepRequest})" />
		public IMoveToStepResponse MoveToStep(IndexName index, Func<MoveToStepDescriptor, IMoveToStepRequest> selector = null) =>
			MoveToStep(selector.InvokeOrDefault(new MoveToStepDescriptor(index)));

		/// <inheritdoc cref="MoveToStep(Nest.IndexName,System.Func{Nest.MoveToStepDescriptor,Nest.IMoveToStepRequest})" />
		public IMoveToStepResponse MoveToStep(IMoveToStepRequest request) =>
			Dispatcher.Dispatch<IMoveToStepRequest, MoveToStepRequestParameters, MoveToStepResponse>(
				request,
				LowLevelDispatch.XpackIlmMoveToStepDispatch<MoveToStepResponse>
			);

		/// <inheritdoc cref="MoveToStep(Nest.IndexName,System.Func{Nest.MoveToStepDescriptor,Nest.IMoveToStepRequest})" />
		public Task<IMoveToStepResponse> MoveToStepAsync(IndexName index, Func<MoveToStepDescriptor, IMoveToStepRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			MoveToStepAsync(selector.InvokeOrDefault(new MoveToStepDescriptor(index)), cancellationToken);

		/// <inheritdoc cref="MoveToStep(Nest.IndexName,System.Func{Nest.MoveToStepDescriptor,Nest.IMoveToStepRequest})" />
		public Task<IMoveToStepResponse> MoveToStepAsync(IMoveToStepRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IMoveToStepRequest, MoveToStepRequestParameters, MoveToStepResponse, IMoveToStepResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.XpackIlmMoveToStepDispatchAsync<MoveToStepResponse>
			);
	}
}
