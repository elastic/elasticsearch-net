using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{

		/// <inheritdoc />
		public ICatResponse<CatAliasesRecord> CatAliases(Func<CatAliasesDescriptor, CatAliasesDescriptor> selector = null)
		{
			return this.DoCat<CatAliasesDescriptor, CatAliasesRequestParameters, CatAliasesRecord>(selector, this.RawDispatch.CatAliasesDispatch<CatResponse<CatAliasesRecord>>);
		}

		public ICatResponse<CatAliasesRecord> CatAliases(ICatAliasesRequest request)
		{
			return this.DoCat<ICatAliasesRequest, CatAliasesRequestParameters, CatAliasesRecord>(request, this.RawDispatch.CatAliasesDispatch<CatResponse<CatAliasesRecord>>);
		}

		public Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(Func<CatAliasesDescriptor, CatAliasesDescriptor> selector = null)
		{
			return this.DoCatAsync<CatAliasesDescriptor, CatAliasesRequestParameters, CatAliasesRecord>(selector, this.RawDispatch.CatAliasesDispatchAsync<CatResponse<CatAliasesRecord>>);
		}

		public Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(ICatAliasesRequest request)
		{
			return this.DoCatAsync<ICatAliasesRequest, CatAliasesRequestParameters, CatAliasesRecord>(request, this.RawDispatch.CatAliasesDispatchAsync<CatResponse<CatAliasesRecord>>);
		}









		private CatResponse<TCatRecord> DeserializeCatResponse<TCatRecord>(IElasticsearchResponse response, Stream stream)
			where TCatRecord : ICatRecord
		{
			var records = this.Serializer.Deserialize<IEnumerable<TCatRecord>>(stream);
			return new CatResponse<TCatRecord>(response) { Records = records };
		}


		private Task<ICatResponse<TCatRecord>> DoCatAsync<TRequest, TParams, TCatRecord>(
			Func<TRequest, TRequest> selector, 
			Func<ElasticsearchPathInfo<TParams>, Task<ElasticsearchResponse<CatResponse<TCatRecord>>>> dispatch
			)
			where TCatRecord : ICatRecord
			where TParams : FluentRequestParameters<TParams>, new()
			where TRequest : class, IRequest<TParams>, new()
		{
			return this.DispatchAsync<TRequest, TParams, CatResponse<TCatRecord>, ICatResponse<TCatRecord>>(
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
			return this.DispatchAsync<TRequest, TParams, CatResponse<TCatRecord>, ICatResponse<TCatRecord>>(
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
			return this.Dispatch<TRequest, TParams, CatResponse<TCatRecord>>(
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
			return this.Dispatch<TRequest, TParams, CatResponse<TCatRecord>>(
				this.ForceConfiguration(request, c => c.ContentType = "application/json"),
				(p, d) => dispatch(p.DeserializationState(
					new Func<IElasticsearchResponse, Stream, CatResponse<TCatRecord>>(this.DeserializeCatResponse<TCatRecord>))
				)
			);
		}
	}
}