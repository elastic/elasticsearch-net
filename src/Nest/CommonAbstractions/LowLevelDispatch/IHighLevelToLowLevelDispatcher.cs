using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Elasticsearch.Net.Connection;

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
			where TResponse : BaseResponse;

		TResponse Dispatch<TRequest, TQueryString, TResponse>(
			TRequest descriptor, 
			Func<IApiCallDetails, Stream, TResponse> responseGenerator, 
			Func<TRequest, PostData<object>, ElasticsearchResponse<TResponse>> dispatch
			)
			where TQueryString : FluentRequestParameters<TQueryString>, new()
			where TRequest : IRequest<TQueryString>
			where TResponse : BaseResponse;

		Task<TResponseInterface> DispatchAsync<TRequest, TQueryString, TResponse, TResponseInterface>(
			TRequest descriptor, 
			Func<TRequest, PostData<object>, Task<ElasticsearchResponse<TResponse>>> dispatch
			)
			where TQueryString : FluentRequestParameters<TQueryString>, new()
			where TRequest : IRequest<TQueryString>
			where TResponse : BaseResponse, TResponseInterface
			where TResponseInterface : IResponse;

		Task<TResponseInterface> DispatchAsync<TRequest, TQueryString, TResponse, TResponseInterface>(
			TRequest descriptor, 
			Func<IApiCallDetails, Stream, TResponse> responseGenerator, 
			Func<TRequest, PostData<object>, Task<ElasticsearchResponse<TResponse>>> dispatch
		)
			where TQueryString : FluentRequestParameters<TQueryString>, new()
			where TRequest : IRequest<TQueryString>
			where TResponse : BaseResponse, TResponseInterface
			where TResponseInterface : IResponse;
	}
}
