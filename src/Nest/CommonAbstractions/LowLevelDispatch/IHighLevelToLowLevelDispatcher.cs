using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public interface IHighLevelToLowLevelDispatcher
	{
		TResponse Dispatch<TRequest, TQueryString, TResponse>(
			TRequest descriptor,
			Func<TRequest, PostData<object>, ElasticsearchResponse<TResponse>> dispatch
		)
			where TQueryString : FluentRequestParameters<TQueryString>, new()
			where TRequest : IRequest<TQueryString>
			where TResponse : ResponseBase;

		TResponse Dispatch<TRequest, TQueryString, TResponse>(
			TRequest descriptor,
			Func<IApiCallDetails, Stream, TResponse> responseGenerator,
			Func<TRequest, PostData<object>, ElasticsearchResponse<TResponse>> dispatch
		)
			where TQueryString : FluentRequestParameters<TQueryString>, new()
			where TRequest : IRequest<TQueryString>
			where TResponse : ResponseBase;

		Task<TResponseInterface> DispatchAsync<TRequest, TQueryString, TResponse, TResponseInterface>(
			TRequest descriptor,
			CancellationToken cancellationToken,
			Func<TRequest, PostData<object>, CancellationToken, Task<ElasticsearchResponse<TResponse>>> dispatch
		)
			where TQueryString : FluentRequestParameters<TQueryString>, new()
			where TRequest : IRequest<TQueryString>
			where TResponse : ResponseBase, TResponseInterface
			where TResponseInterface : IResponse;

		Task<TResponseInterface> DispatchAsync<TRequest, TQueryString, TResponse, TResponseInterface>(
			TRequest descriptor,
			CancellationToken cancellationToken,
			Func<IApiCallDetails, Stream, TResponse> responseGenerator,
			Func<TRequest, PostData<object>, CancellationToken, Task<ElasticsearchResponse<TResponse>>> dispatch
		)
			where TQueryString : FluentRequestParameters<TQueryString>, new()
			where TRequest : IRequest<TQueryString>
			where TResponse : ResponseBase, TResponseInterface
			where TResponseInterface : IResponse;
	}
}
