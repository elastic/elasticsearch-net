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
		R Dispatch<D, Q, R>(D descriptor, Func<D, PostData<object>, ElasticsearchResponse<R>> dispatch)
			where Q : FluentRequestParameters<Q>, new()
			where D : IRequest<Q>
			where R : BaseResponse;

		R Dispatch<D, Q, R>(D descriptor, Func<IApiCallDetails, Stream, R> responseGenerator, Func<D, PostData<object>, ElasticsearchResponse<R>> dispatch)
			where Q : FluentRequestParameters<Q>, new()
			where D : IRequest<Q>
			where R : BaseResponse;

		Task<I> DispatchAsync<D, Q, R, I>(D descriptor, Func<D, PostData<object>, Task<ElasticsearchResponse<R>>> dispatch)
			where Q : FluentRequestParameters<Q>, new()
			where D : IRequest<Q>
			where R : BaseResponse, I
			where I : IResponse;

		Task<I> DispatchAsync<D, Q, R, I>(D descriptor, Func<IApiCallDetails, Stream, R> responseGenerator, Func<D, PostData<object>, Task<ElasticsearchResponse<R>>> dispatch)
			where Q : FluentRequestParameters<Q>, new()
			where D : IRequest<Q>
			where R : BaseResponse, I
			where I : IResponse;
	}
}
