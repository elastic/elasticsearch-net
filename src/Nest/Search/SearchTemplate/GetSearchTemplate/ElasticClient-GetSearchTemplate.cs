using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IGetSearchTemplateResponse GetSearchTemplate(Id id, Func<GetSearchTemplateDescriptor, IGetSearchTemplateRequest> selector = null);

		/// <inheritdoc />
		IGetSearchTemplateResponse GetSearchTemplate(IGetSearchTemplateRequest request);

		/// <inheritdoc />
		Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(Id id, Func<GetSearchTemplateDescriptor, IGetSearchTemplateRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(IGetSearchTemplateRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		public IGetSearchTemplateResponse GetSearchTemplate(Id id, Func<GetSearchTemplateDescriptor, IGetSearchTemplateRequest> selector = null) =>
			GetSearchTemplate(selector.InvokeOrDefault(new GetSearchTemplateDescriptor(id)));

		public IGetSearchTemplateResponse GetSearchTemplate(IGetSearchTemplateRequest request) =>
			Dispatcher.Dispatch<IGetSearchTemplateRequest, GetSearchTemplateRequestParameters, GetSearchTemplateResponse>(
				request,
				(p, d) => LowLevelDispatch.GetTemplateDispatch<GetSearchTemplateResponse>(p)
			);

		public Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(Id id,
			Func<GetSearchTemplateDescriptor, IGetSearchTemplateRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GetSearchTemplateAsync(selector.InvokeOrDefault(new GetSearchTemplateDescriptor(id)), cancellationToken);

		public Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(IGetSearchTemplateRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<IGetSearchTemplateRequest, GetSearchTemplateRequestParameters, GetSearchTemplateResponse, IGetSearchTemplateResponse>(
					request,
					cancellationToken,
					(p, d, c) => LowLevelDispatch.GetTemplateDispatchAsync<GetSearchTemplateResponse>(p, c)
				);
	}
}
