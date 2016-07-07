using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IGetSearchTemplateResponse GetSearchTemplate(Id id, Func<GetSearchTemplateDescriptor, IGetSearchTemplateRequest> selector = null);

		/// <inheritdoc/>
		IGetSearchTemplateResponse GetSearchTemplate(IGetSearchTemplateRequest request);

		/// <inheritdoc/>
		Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(Id id, Func<GetSearchTemplateDescriptor, IGetSearchTemplateRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(IGetSearchTemplateRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}
	public partial class ElasticClient
	{
		public IGetSearchTemplateResponse GetSearchTemplate(Id id, Func<GetSearchTemplateDescriptor, IGetSearchTemplateRequest> selector = null) =>
			this.GetSearchTemplate(selector.InvokeOrDefault(new GetSearchTemplateDescriptor(id)));

		public IGetSearchTemplateResponse GetSearchTemplate(IGetSearchTemplateRequest request) =>
			this.Dispatcher.Dispatch<IGetSearchTemplateRequest, GetSearchTemplateRequestParameters, GetSearchTemplateResponse>(
				request,
				(p, d) => this.LowLevelDispatch.GetTemplateDispatch<GetSearchTemplateResponse>(p)
			);

		public Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(Id id, Func<GetSearchTemplateDescriptor, IGetSearchTemplateRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.GetSearchTemplateAsync(selector.InvokeOrDefault(new GetSearchTemplateDescriptor(id)), cancellationToken);

		public Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(IGetSearchTemplateRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGetSearchTemplateRequest, GetSearchTemplateRequestParameters, GetSearchTemplateResponse, IGetSearchTemplateResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.GetTemplateDispatchAsync<GetSearchTemplateResponse>(p, c)
			);
	}
}
