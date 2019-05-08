using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Deletes a machine learning filter.
		/// If a machine learning job references the filter, you cannot delete the filter.
		/// You must update or delete the job before you can delete the filter.
		/// </summary>
		DeleteFilterResponse DeleteFilter(Id filterId, Func<DeleteFilterDescriptor, IDeleteFilterRequest> selector = null);

		/// <inheritdoc cref="DeleteFilter(Nest.Id,System.Func{Nest.DeleteFilterDescriptor,Nest.IDeleteFilterRequest})" />
		DeleteFilterResponse DeleteFilter(IDeleteFilterRequest request);

		/// <inheritdoc cref="DeleteFilter(Nest.Id,System.Func{Nest.DeleteFilterDescriptor,Nest.IDeleteFilterRequest})" />
		Task<DeleteFilterResponse> DeleteFilterAsync(Id filterId, Func<DeleteFilterDescriptor, IDeleteFilterRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="DeleteFilter(Nest.Id,System.Func{Nest.DeleteFilterDescriptor,Nest.IDeleteFilterRequest})" />
		Task<DeleteFilterResponse> DeleteFilterAsync(IDeleteFilterRequest request, CancellationToken cancellationToken = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DeleteFilterResponse DeleteFilter(Id filterId, Func<DeleteFilterDescriptor, IDeleteFilterRequest> selector = null) =>
			DeleteFilter(selector.InvokeOrDefault(new DeleteFilterDescriptor(filterId)));

		/// <inheritdoc />
		public DeleteFilterResponse DeleteFilter(IDeleteFilterRequest request) =>
			DoRequest<IDeleteFilterRequest, DeleteFilterResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<DeleteFilterResponse> DeleteFilterAsync(Id filterId, Func<DeleteFilterDescriptor, IDeleteFilterRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			DeleteFilterAsync(selector.InvokeOrDefault(new DeleteFilterDescriptor(filterId)), cancellationToken);

		/// <inheritdoc />
		public Task<DeleteFilterResponse> DeleteFilterAsync(IDeleteFilterRequest request, CancellationToken cancellationToken = default) =>
			DoRequestAsync<IDeleteFilterRequest, DeleteFilterResponse>(request, request.RequestParameters, cancellationToken);
	}
}
