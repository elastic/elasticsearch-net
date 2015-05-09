using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public interface IDispatch
	{
		R Dispatch<D, Q, R>(D descriptor, Func<ElasticsearchPathInfo<Q>, D, ElasticsearchResponse<R>> dispatch)
			where Q : FluentRequestParameters<Q>, new()
			where D : IRequest<Q>
			where R : BaseResponse;

		R Dispatch<D, Q, R>(Func<D, D> selector, Func<ElasticsearchPathInfo<Q>, D, ElasticsearchResponse<R>> dispatch)
			where Q : FluentRequestParameters<Q>, new()
			where D : IRequest<Q>, new()
			where R : BaseResponse;

		Task<I> DispatchAsync<D, Q, R, I>(D descriptor, Func<ElasticsearchPathInfo<Q>, D, Task<ElasticsearchResponse<R>>> dispatch)
			where Q : FluentRequestParameters<Q>, new()
			where D : IRequest<Q>
			where R : BaseResponse, I
			where I : IResponse;

		Task<I> DispatchAsync<D, Q, R, I>(Func<D, D> selector, Func<ElasticsearchPathInfo<Q>, D, Task<ElasticsearchResponse<R>>> dispatch)
			where Q : FluentRequestParameters<Q>, new()
			where D : IRequest<Q>, new()
			where R : BaseResponse, I
			where I : IResponse;
	}
}
