using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IDeleteSearchTemplateResponse DeleteSearchTemplate(Id id, Func<DeleteSearchTemplateDescriptor, IDeleteSearchTemplateRequest> selector = null);

		/// <inheritdoc />
		IDeleteSearchTemplateResponse DeleteSearchTemplate(IDeleteSearchTemplateRequest request);

		/// <inheritdoc />
		Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(Id id,
			Func<DeleteSearchTemplateDescriptor, IDeleteSearchTemplateRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(IDeleteSearchTemplateRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		public IDeleteSearchTemplateResponse DeleteSearchTemplate(Id id,
			Func<DeleteSearchTemplateDescriptor, IDeleteSearchTemplateRequest> selector = null
		) =>
			DeleteSearchTemplate(selector.InvokeOrDefault(new DeleteSearchTemplateDescriptor(id)));


		public IDeleteSearchTemplateResponse DeleteSearchTemplate(IDeleteSearchTemplateRequest request) =>
			Dispatcher.Dispatch<IDeleteSearchTemplateRequest, DeleteSearchTemplateRequestParameters, DeleteSearchTemplateResponse>(
				request,
				(p, d) => LowLevelDispatch.DeleteTemplateDispatch<DeleteSearchTemplateResponse>(p)
			);

		public Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(Id id,
			Func<DeleteSearchTemplateDescriptor, IDeleteSearchTemplateRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			DeleteSearchTemplateAsync(selector.InvokeOrDefault(new DeleteSearchTemplateDescriptor(id)), cancellationToken);

		public Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(IDeleteSearchTemplateRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<IDeleteSearchTemplateRequest, DeleteSearchTemplateRequestParameters, DeleteSearchTemplateResponse,
					IDeleteSearchTemplateResponse>(
					request,
					cancellationToken,
					(p, d, c) => LowLevelDispatch.DeleteTemplateDispatchAsync<DeleteSearchTemplateResponse>(p, c)
				);
	}
}
