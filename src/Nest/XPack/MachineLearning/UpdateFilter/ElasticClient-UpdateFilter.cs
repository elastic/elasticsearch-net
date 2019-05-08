using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Updates the description of a machine learning filter, adds items, or removes items.
		/// </summary>
		UpdateFilterResponse UpdateFilter(Id filterId, Func<UpdateFilterDescriptor, IUpdateFilterRequest> selector = null);

		/// <inheritdoc cref="UpdateFilter(Nest.Id,System.Func{Nest.UpdateFilterDescriptor,Nest.IUpdateFilterRequest})" />
		UpdateFilterResponse UpdateFilter(IUpdateFilterRequest request);

		/// <inheritdoc cref="UpdateFilter(Nest.Id,System.Func{Nest.UpdateFilterDescriptor,Nest.IUpdateFilterRequest})" />
		Task<UpdateFilterResponse> UpdateFilterAsync(Id filterId, Func<UpdateFilterDescriptor, IUpdateFilterRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="UpdateFilter(Nest.Id,System.Func{Nest.UpdateFilterDescriptor,Nest.IUpdateFilterRequest})" />
		Task<UpdateFilterResponse> UpdateFilterAsync(IUpdateFilterRequest request, CancellationToken cancellationToken = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public UpdateFilterResponse UpdateFilter(Id filterId, Func<UpdateFilterDescriptor, IUpdateFilterRequest> selector = null) =>
			UpdateFilter(selector.InvokeOrDefault(new UpdateFilterDescriptor(filterId)));

		/// <inheritdoc />
		public UpdateFilterResponse UpdateFilter(IUpdateFilterRequest request) =>
			DoRequest<IUpdateFilterRequest, UpdateFilterResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<UpdateFilterResponse> UpdateFilterAsync(Id filterId, Func<UpdateFilterDescriptor, IUpdateFilterRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			UpdateFilterAsync(selector.InvokeOrDefault(new UpdateFilterDescriptor(filterId)), cancellationToken);

		/// <inheritdoc />
		public Task<UpdateFilterResponse> UpdateFilterAsync(IUpdateFilterRequest request, CancellationToken cancellationToken = default) =>
			DoRequestAsync<IUpdateFilterRequest, UpdateFilterResponse>(request, request.RequestParameters, cancellationToken);
	}
}
