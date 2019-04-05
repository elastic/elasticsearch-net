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
		IDeleteFilterResponse DeleteFilter(Id filterId, Func<DeleteFilterDescriptor, IDeleteFilterRequest> selector = null);

		/// <inheritdoc cref="DeleteFilter(Nest.Id,System.Func{Nest.DeleteFilterDescriptor,Nest.IDeleteFilterRequest})" />
		IDeleteFilterResponse DeleteFilter(IDeleteFilterRequest request);

		/// <inheritdoc cref="DeleteFilter(Nest.Id,System.Func{Nest.DeleteFilterDescriptor,Nest.IDeleteFilterRequest})" />
		Task<IDeleteFilterResponse> DeleteFilterAsync(Id filterId, Func<DeleteFilterDescriptor, IDeleteFilterRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="DeleteFilter(Nest.Id,System.Func{Nest.DeleteFilterDescriptor,Nest.IDeleteFilterRequest})" />
		Task<IDeleteFilterResponse> DeleteFilterAsync(IDeleteFilterRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteFilterResponse DeleteFilter(Id filterId, Func<DeleteFilterDescriptor, IDeleteFilterRequest> selector = null) =>
			DeleteFilter(selector.InvokeOrDefault(new DeleteFilterDescriptor(filterId)));

		/// <inheritdoc />
		public IDeleteFilterResponse DeleteFilter(IDeleteFilterRequest request) =>
			Dispatcher.Dispatch<IDeleteFilterRequest, DeleteFilterRequestParameters, DeleteFilterResponse>(
				request,
				(p, d) => LowLevelDispatch.XpackMlDeleteFilterDispatch<DeleteFilterResponse>(p)
			);

		/// <inheritdoc />
		public Task<IDeleteFilterResponse> DeleteFilterAsync(Id filterId, Func<DeleteFilterDescriptor, IDeleteFilterRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			DeleteFilterAsync(selector.InvokeOrDefault(new DeleteFilterDescriptor(filterId)), cancellationToken);

		/// <inheritdoc />
		public Task<IDeleteFilterResponse> DeleteFilterAsync(IDeleteFilterRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IDeleteFilterRequest, DeleteFilterRequestParameters, DeleteFilterResponse, IDeleteFilterResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.XpackMlDeleteFilterDispatchAsync<DeleteFilterResponse>(p, c)
			);
	}
}
