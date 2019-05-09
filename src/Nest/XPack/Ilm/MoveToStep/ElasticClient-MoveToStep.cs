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
		MoveToStepResponse MoveToStep(IndexName index, Func<MoveToStepDescriptor, IMoveToStepRequest> selector = null);

		/// <inheritdoc cref="MoveToStep(Nest.IndexName,System.Func{Nest.MoveToStepDescriptor,Nest.IMoveToStepRequest})" />
		MoveToStepResponse MoveToStep(IMoveToStepRequest request);

		/// <inheritdoc cref="MoveToStep(Nest.IndexName,System.Func{Nest.MoveToStepDescriptor,Nest.IMoveToStepRequest})" />
		Task<MoveToStepResponse> MoveToStepAsync(IndexName index, Func<MoveToStepDescriptor, IMoveToStepRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="MoveToStep(Nest.IndexName,System.Func{Nest.MoveToStepDescriptor,Nest.IMoveToStepRequest})" />
		Task<MoveToStepResponse> MoveToStepAsync(IMoveToStepRequest request,
			CancellationToken cancellationToken = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="MoveToStep(Nest.IndexName,System.Func{Nest.MoveToStepDescriptor,Nest.IMoveToStepRequest})" />
		public MoveToStepResponse MoveToStep(IndexName index, Func<MoveToStepDescriptor, IMoveToStepRequest> selector = null) =>
			MoveToStep(selector.InvokeOrDefault(new MoveToStepDescriptor(index)));

		/// <inheritdoc cref="MoveToStep(Nest.IndexName,System.Func{Nest.MoveToStepDescriptor,Nest.IMoveToStepRequest})" />
		public MoveToStepResponse MoveToStep(IMoveToStepRequest request) =>
			DoRequest<IMoveToStepRequest, MoveToStepResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="MoveToStep(Nest.IndexName,System.Func{Nest.MoveToStepDescriptor,Nest.IMoveToStepRequest})" />
		public Task<MoveToStepResponse> MoveToStepAsync(IndexName index, Func<MoveToStepDescriptor, IMoveToStepRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			MoveToStepAsync(selector.InvokeOrDefault(new MoveToStepDescriptor(index)), cancellationToken);

		/// <inheritdoc cref="MoveToStep(Nest.IndexName,System.Func{Nest.MoveToStepDescriptor,Nest.IMoveToStepRequest})" />
		public Task<MoveToStepResponse> MoveToStepAsync(IMoveToStepRequest request,
			CancellationToken cancellationToken = default
		) =>
			DoRequestAsync<IMoveToStepRequest, MoveToStepResponse>(request, request.RequestParameters, cancellationToken);
	}
}
