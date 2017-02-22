using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest_5_2_0
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IDeleteSearchTemplateResponse DeleteSearchTemplate(Id id, Func<DeleteSearchTemplateDescriptor, IDeleteSearchTemplateRequest> selector = null);

		/// <inheritdoc/>
		IDeleteSearchTemplateResponse DeleteSearchTemplate(IDeleteSearchTemplateRequest request);

		/// <inheritdoc/>
		Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(Id id, Func<DeleteSearchTemplateDescriptor, IDeleteSearchTemplateRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(IDeleteSearchTemplateRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		public IDeleteSearchTemplateResponse DeleteSearchTemplate(Id id, Func<DeleteSearchTemplateDescriptor, IDeleteSearchTemplateRequest> selector = null) =>
			this.DeleteSearchTemplate(selector.InvokeOrDefault(new DeleteSearchTemplateDescriptor(id)));


		public IDeleteSearchTemplateResponse DeleteSearchTemplate(IDeleteSearchTemplateRequest request) =>
			this.Dispatcher.Dispatch<IDeleteSearchTemplateRequest, DeleteSearchTemplateRequestParameters, DeleteSearchTemplateResponse>(
				request,
				(p, d) => this.LowLevelDispatch.DeleteTemplateDispatch<DeleteSearchTemplateResponse>(p)
			);

		public Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(Id id, Func<DeleteSearchTemplateDescriptor, IDeleteSearchTemplateRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.DeleteSearchTemplateAsync(selector.InvokeOrDefault(new DeleteSearchTemplateDescriptor(id)), cancellationToken);

		public Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(IDeleteSearchTemplateRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IDeleteSearchTemplateRequest, DeleteSearchTemplateRequestParameters, DeleteSearchTemplateResponse, IDeleteSearchTemplateResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.DeleteTemplateDispatchAsync<DeleteSearchTemplateResponse>(p, c)
			);
	}
}
