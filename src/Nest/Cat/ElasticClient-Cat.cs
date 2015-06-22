using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		private Task<ICatResponse<TCatRecord>> DoCatAsync<TRequest, TParams, TCatRecord>(
			Func<TRequest, TRequest> selector, 
			Func<ElasticsearchPathInfo<TParams>, Task<ElasticsearchResponse<CatResponse<TCatRecord>>>> dispatch
			)
			where TCatRecord : ICatRecord
			where TParams : FluentRequestParameters<TParams>, new()
			where TRequest : class, IRequest<TParams>, new()
		{
			return this.Dispatcher.DispatchAsync<TRequest, TParams, CatResponse<TCatRecord>, ICatResponse<TCatRecord>>(
				this.ForceConfiguration(selector, c => c.ContentType = "application/json"),
				(p, d) => dispatch(p.DeserializationState(
					new Func<IElasticsearchResponse, Stream, CatResponse<TCatRecord>>(this.DeserializeCatResponse<TCatRecord>))
				)
			);
		}

		private Task<ICatResponse<TCatRecord>> DoCatAsync<TRequest, TParams, TCatRecord>(
			TRequest request,
			Func<ElasticsearchPathInfo<TParams>, Task<ElasticsearchResponse<CatResponse<TCatRecord>>>> dispatch
			)
			where TCatRecord : ICatRecord
			where TParams : FluentRequestParameters<TParams>, new()
			where TRequest : IRequest<TParams> 
		{
			return this.Dispatcher.DispatchAsync<TRequest, TParams, CatResponse<TCatRecord>, ICatResponse<TCatRecord>>(
				this.ForceConfiguration(request, c => c.ContentType = "application/json"),
				(p, d) => dispatch(p.DeserializationState(
					new Func<IElasticsearchResponse, Stream, CatResponse<TCatRecord>>(this.DeserializeCatResponse<TCatRecord>))
				)
			);
		}

		private ICatResponse<TCatRecord> DoCat<TRequest, TParams, TCatRecord>(
			Func<TRequest, TRequest> selector, 
			Func<ElasticsearchPathInfo<TParams>, ElasticsearchResponse<CatResponse<TCatRecord>>> dispatch
			)
			where TCatRecord : ICatRecord
			where TParams : FluentRequestParameters<TParams>, new()
			where TRequest : class, IRequest<TParams>, new()
		{
			return this.Dispatcher.Dispatch<TRequest, TParams, CatResponse<TCatRecord>>(
				this.ForceConfiguration(selector, c => c.ContentType = "application/json"),
				(p, d) => dispatch(p.DeserializationState(
					new Func<IElasticsearchResponse, Stream, CatResponse<TCatRecord>>(this.DeserializeCatResponse<TCatRecord>))
				)
			);
		}

		private ICatResponse<TCatRecord> DoCat<TRequest, TParams, TCatRecord>(
			TRequest request,
			Func<ElasticsearchPathInfo<TParams>, ElasticsearchResponse<CatResponse<TCatRecord>>> dispatch
			)
			where TCatRecord : ICatRecord
			where TParams : FluentRequestParameters<TParams>, new()
			where TRequest : IRequest<TParams> 
		{
			return this.Dispatcher.Dispatch<TRequest, TParams, CatResponse<TCatRecord>>(
				this.ForceConfiguration(request, c => c.ContentType = "application/json"),
				(p, d) => dispatch(p.DeserializationState(
					new Func<IElasticsearchResponse, Stream, CatResponse<TCatRecord>>(this.DeserializeCatResponse<TCatRecord>))
				)
			);
		}
	}
}