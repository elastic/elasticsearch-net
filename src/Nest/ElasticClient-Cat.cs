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

		/// <inheritdoc />
		public ICatResponse<CatAllocationRecord> CatAllocation(Func<CatAllocationDescriptor, CatAllocationDescriptor> selector = null)
		{
			return this.DoCat<CatAllocationDescriptor, CatAllocationRequestParameters, CatAllocationRecord>(selector, this.RawDispatch.CatAllocationDispatch<CatResponse<CatAllocationRecord>>);
		}

		public ICatResponse<CatAllocationRecord> CatAllocation(ICatAllocationRequest request)
		{
			return this.DoCat<ICatAllocationRequest, CatAllocationRequestParameters, CatAllocationRecord>(request, this.RawDispatch.CatAllocationDispatch<CatResponse<CatAllocationRecord>>);
		}

		public Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(Func<CatAllocationDescriptor, CatAllocationDescriptor> selector = null)
		{
			return this.DoCatAsync<CatAllocationDescriptor, CatAllocationRequestParameters, CatAllocationRecord>(selector, this.RawDispatch.CatAllocationDispatchAsync<CatResponse<CatAllocationRecord>>);
		}

		public Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(ICatAllocationRequest request)
		{
			return this.DoCatAsync<ICatAllocationRequest, CatAllocationRequestParameters, CatAllocationRecord>(request, this.RawDispatch.CatAllocationDispatchAsync<CatResponse<CatAllocationRecord>>);
		}

		/// <inheritdoc />
		public ICatResponse<CatCountRecord> CatCount(Func<CatCountDescriptor, CatCountDescriptor> selector = null)
		{
			return this.DoCat<CatCountDescriptor, CatCountRequestParameters, CatCountRecord>(selector, this.RawDispatch.CatCountDispatch<CatResponse<CatCountRecord>>);
		}

		public ICatResponse<CatCountRecord> CatCount(ICatCountRequest request)
		{
			return this.DoCat<ICatCountRequest, CatCountRequestParameters, CatCountRecord>(request, this.RawDispatch.CatCountDispatch<CatResponse<CatCountRecord>>);
		}

		public Task<ICatResponse<CatCountRecord>> CatCountAsync(Func<CatCountDescriptor, CatCountDescriptor> selector = null)
		{
			return this.DoCatAsync<CatCountDescriptor, CatCountRequestParameters, CatCountRecord>(selector, this.RawDispatch.CatCountDispatchAsync<CatResponse<CatCountRecord>>);
		}

		public Task<ICatResponse<CatCountRecord>> CatCountAsync(ICatCountRequest request)
		{
			return this.DoCatAsync<ICatCountRequest, CatCountRequestParameters, CatCountRecord>(request, this.RawDispatch.CatCountDispatchAsync<CatResponse<CatCountRecord>>);
		}


		/// <inheritdoc />
		public ICatResponse<CatHealthRecord> CatHealth(Func<CatHealthDescriptor, CatHealthDescriptor> selector = null)
		{
			return this.DoCat<CatHealthDescriptor, CatHealthRequestParameters, CatHealthRecord>(selector, this.RawDispatch.CatHealthDispatch<CatResponse<CatHealthRecord>>);
		}

		public ICatResponse<CatHealthRecord> CatHealth(ICatHealthRequest request)
		{
			return this.DoCat<ICatHealthRequest, CatHealthRequestParameters, CatHealthRecord>(request, this.RawDispatch.CatHealthDispatch<CatResponse<CatHealthRecord>>);
		}

		public Task<ICatResponse<CatHealthRecord>> CatHealthAsync(Func<CatHealthDescriptor, CatHealthDescriptor> selector = null)
		{
			return this.DoCatAsync<CatHealthDescriptor, CatHealthRequestParameters, CatHealthRecord>(selector, this.RawDispatch.CatHealthDispatchAsync<CatResponse<CatHealthRecord>>);
		}

		public Task<ICatResponse<CatHealthRecord>> CatHealthAsync(ICatHealthRequest request)
		{
			return this.DoCatAsync<ICatHealthRequest, CatHealthRequestParameters, CatHealthRecord>(request, this.RawDispatch.CatHealthDispatchAsync<CatResponse<CatHealthRecord>>);
		}

		/// <inheritdoc />
		public ICatResponse<CatIndicesRecord> CatIndices(Func<CatIndicesDescriptor, CatIndicesDescriptor> selector = null)
		{
			return this.DoCat<CatIndicesDescriptor, CatIndicesRequestParameters, CatIndicesRecord>(selector, this.RawDispatch.CatIndicesDispatch<CatResponse<CatIndicesRecord>>);
		}

		public ICatResponse<CatIndicesRecord> CatIndices(ICatIndicesRequest request)
		{
			return this.DoCat<ICatIndicesRequest, CatIndicesRequestParameters, CatIndicesRecord>(request, this.RawDispatch.CatIndicesDispatch<CatResponse<CatIndicesRecord>>);
		}

		public Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(Func<CatIndicesDescriptor, CatIndicesDescriptor> selector = null)
		{
			return this.DoCatAsync<CatIndicesDescriptor, CatIndicesRequestParameters, CatIndicesRecord>(selector, this.RawDispatch.CatIndicesDispatchAsync<CatResponse<CatIndicesRecord>>);
		}

		public Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(ICatIndicesRequest request)
		{
			return this.DoCatAsync<ICatIndicesRequest, CatIndicesRequestParameters, CatIndicesRecord>(request, this.RawDispatch.CatIndicesDispatchAsync<CatResponse<CatIndicesRecord>>);
		}

		/// <inheritdoc />
		public ICatResponse<CatMasterRecord> CatMaster(Func<CatMasterDescriptor, CatMasterDescriptor> selector = null)
		{
			return this.DoCat<CatMasterDescriptor, CatMasterRequestParameters, CatMasterRecord>(selector, this.RawDispatch.CatMasterDispatch<CatResponse<CatMasterRecord>>);
		}

		public ICatResponse<CatMasterRecord> CatMaster(ICatMasterRequest request)
		{
			return this.DoCat<ICatMasterRequest, CatMasterRequestParameters, CatMasterRecord>(request, this.RawDispatch.CatMasterDispatch<CatResponse<CatMasterRecord>>);
		}

		public Task<ICatResponse<CatMasterRecord>> CatMasterAsync(Func<CatMasterDescriptor, CatMasterDescriptor> selector = null)
		{
			return this.DoCatAsync<CatMasterDescriptor, CatMasterRequestParameters, CatMasterRecord>(selector, this.RawDispatch.CatMasterDispatchAsync<CatResponse<CatMasterRecord>>);
		}

		public Task<ICatResponse<CatMasterRecord>> CatMasterAsync(ICatMasterRequest request)
		{
			return this.DoCatAsync<ICatMasterRequest, CatMasterRequestParameters, CatMasterRecord>(request, this.RawDispatch.CatMasterDispatchAsync<CatResponse<CatMasterRecord>>);
		}

		/// <inheritdoc />
		public ICatResponse<CatNodesRecord> CatNodes(Func<CatNodesDescriptor, CatNodesDescriptor> selector = null)
		{
			return this.DoCat<CatNodesDescriptor, CatNodesRequestParameters, CatNodesRecord>(selector, this.RawDispatch.CatNodesDispatch<CatResponse<CatNodesRecord>>);
		}

		public ICatResponse<CatNodesRecord> CatNodes(ICatNodesRequest request)
		{
			return this.DoCat<ICatNodesRequest, CatNodesRequestParameters, CatNodesRecord>(request, this.RawDispatch.CatNodesDispatch<CatResponse<CatNodesRecord>>);
		}

		public Task<ICatResponse<CatNodesRecord>> CatNodesAsync(Func<CatNodesDescriptor, CatNodesDescriptor> selector = null)
		{
			return this.DoCatAsync<CatNodesDescriptor, CatNodesRequestParameters, CatNodesRecord>(selector, this.RawDispatch.CatNodesDispatchAsync<CatResponse<CatNodesRecord>>);
		}

		public Task<ICatResponse<CatNodesRecord>> CatNodesAsync(ICatNodesRequest request)
		{
			return this.DoCatAsync<ICatNodesRequest, CatNodesRequestParameters, CatNodesRecord>(request, this.RawDispatch.CatNodesDispatchAsync<CatResponse<CatNodesRecord>>);
		}

		/// <inheritdoc />
		public ICatResponse<CatPendingTasksRecord> CatPendingTasks(Func<CatPendingTasksDescriptor, CatPendingTasksDescriptor> selector = null)
		{
			return this.DoCat<CatPendingTasksDescriptor, CatPendingTasksRequestParameters, CatPendingTasksRecord>(selector, this.RawDispatch.CatPendingTasksDispatch<CatResponse<CatPendingTasksRecord>>);
		}

		public ICatResponse<CatPendingTasksRecord> CatPendingTasks(ICatPendingTasksRequest request)
		{
			return this.DoCat<ICatPendingTasksRequest, CatPendingTasksRequestParameters, CatPendingTasksRecord>(request, this.RawDispatch.CatPendingTasksDispatch<CatResponse<CatPendingTasksRecord>>);
		}

		public Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(Func<CatPendingTasksDescriptor, CatPendingTasksDescriptor> selector = null)
		{
			return this.DoCatAsync<CatPendingTasksDescriptor, CatPendingTasksRequestParameters, CatPendingTasksRecord>(selector, this.RawDispatch.CatPendingTasksDispatchAsync<CatResponse<CatPendingTasksRecord>>);
		}

		public Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(ICatPendingTasksRequest request)
		{
			return this.DoCatAsync<ICatPendingTasksRequest, CatPendingTasksRequestParameters, CatPendingTasksRecord>(request, this.RawDispatch.CatPendingTasksDispatchAsync<CatResponse<CatPendingTasksRecord>>);
		}

		/// <inheritdoc />
		public ICatResponse<CatPluginsRecord> CatPlugins(Func<CatPluginsDescriptor, CatPluginsDescriptor> selector = null)
		{
			return this.DoCat<CatPluginsDescriptor, CatPluginsRequestParameters, CatPluginsRecord>(selector, this.RawDispatch.CatPluginsDispatch<CatResponse<CatPluginsRecord>>);
		}

		public ICatResponse<CatPluginsRecord> CatPlugins(ICatPluginsRequest request)
		{
			return this.DoCat<ICatPluginsRequest, CatPluginsRequestParameters, CatPluginsRecord>(request, this.RawDispatch.CatPluginsDispatch<CatResponse<CatPluginsRecord>>);
		}

		public Task<ICatResponse<CatPluginsRecord>> CatPluginsAsync(Func<CatPluginsDescriptor, CatPluginsDescriptor> selector = null)
		{
			return this.DoCatAsync<CatPluginsDescriptor, CatPluginsRequestParameters, CatPluginsRecord>(selector, this.RawDispatch.CatPluginsDispatchAsync<CatResponse<CatPluginsRecord>>);
		}

		public Task<ICatResponse<CatPluginsRecord>> CatPluginsAsync(ICatPluginsRequest request)
		{
			return this.DoCatAsync<ICatPluginsRequest, CatPluginsRequestParameters, CatPluginsRecord>(request, this.RawDispatch.CatPluginsDispatchAsync<CatResponse<CatPluginsRecord>>);
		}

		/// <inheritdoc />
		public ICatResponse<CatRecoveryRecord> CatRecovery(Func<CatRecoveryDescriptor, CatRecoveryDescriptor> selector = null)
		{
			return this.DoCat<CatRecoveryDescriptor, CatRecoveryRequestParameters, CatRecoveryRecord>(selector, this.RawDispatch.CatRecoveryDispatch<CatResponse<CatRecoveryRecord>>);
		}

		public ICatResponse<CatRecoveryRecord> CatRecovery(ICatRecoveryRequest request)
		{
			return this.DoCat<ICatRecoveryRequest, CatRecoveryRequestParameters, CatRecoveryRecord>(request, this.RawDispatch.CatRecoveryDispatch<CatResponse<CatRecoveryRecord>>);
		}

		public Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(Func<CatRecoveryDescriptor, CatRecoveryDescriptor> selector = null)
		{
			return this.DoCatAsync<CatRecoveryDescriptor, CatRecoveryRequestParameters, CatRecoveryRecord>(selector, this.RawDispatch.CatRecoveryDispatchAsync<CatResponse<CatRecoveryRecord>>);
		}

		public Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(ICatRecoveryRequest request)
		{
			return this.DoCatAsync<ICatRecoveryRequest, CatRecoveryRequestParameters, CatRecoveryRecord>(request, this.RawDispatch.CatRecoveryDispatchAsync<CatResponse<CatRecoveryRecord>>);
		}

		/// <inheritdoc />
		public ICatResponse<CatThreadPoolRecord> CatThreadPool(Func<CatThreadPoolDescriptor, CatThreadPoolDescriptor> selector = null)
		{
			return this.DoCat<CatThreadPoolDescriptor, CatThreadPoolRequestParameters, CatThreadPoolRecord>(selector, this.RawDispatch.CatThreadPoolDispatch<CatResponse<CatThreadPoolRecord>>);
		}

		public ICatResponse<CatThreadPoolRecord> CatThreadPool(ICatThreadPoolRequest request)
		{
			return this.DoCat<ICatThreadPoolRequest, CatThreadPoolRequestParameters, CatThreadPoolRecord>(request, this.RawDispatch.CatThreadPoolDispatch<CatResponse<CatThreadPoolRecord>>);
		}

		public Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(Func<CatThreadPoolDescriptor, CatThreadPoolDescriptor> selector = null)
		{
			return this.DoCatAsync<CatThreadPoolDescriptor, CatThreadPoolRequestParameters, CatThreadPoolRecord>(selector, this.RawDispatch.CatThreadPoolDispatchAsync<CatResponse<CatThreadPoolRecord>>);
		}

		public Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(ICatThreadPoolRequest request)
		{
			return this.DoCatAsync<ICatThreadPoolRequest, CatThreadPoolRequestParameters, CatThreadPoolRecord>(request, this.RawDispatch.CatThreadPoolDispatchAsync<CatResponse<CatThreadPoolRecord>>);
		}

		/// <inheritdoc />
		public ICatResponse<CatShardsRecord> CatShards(Func<CatShardsDescriptor, CatShardsDescriptor> selector = null)
		{
			return this.DoCat<CatShardsDescriptor, CatShardsRequestParameters, CatShardsRecord>(selector, this.RawDispatch.CatShardsDispatch<CatResponse<CatShardsRecord>>);
		}

		public ICatResponse<CatShardsRecord> CatShards(ICatShardsRequest request)
		{
			return this.DoCat<ICatShardsRequest, CatShardsRequestParameters, CatShardsRecord>(request, this.RawDispatch.CatShardsDispatch<CatResponse<CatShardsRecord>>);
		}

		public Task<ICatResponse<CatShardsRecord>> CatShardsAsync(Func<CatShardsDescriptor, CatShardsDescriptor> selector = null)
		{
			return this.DoCatAsync<CatShardsDescriptor, CatShardsRequestParameters, CatShardsRecord>(selector, this.RawDispatch.CatShardsDispatchAsync<CatResponse<CatShardsRecord>>);
		}

		public Task<ICatResponse<CatShardsRecord>> CatShardsAsync(ICatShardsRequest request)
		{
			return this.DoCatAsync<ICatShardsRequest, CatShardsRequestParameters, CatShardsRecord>(request, this.RawDispatch.CatShardsDispatchAsync<CatResponse<CatShardsRecord>>);
		}

		/// <inheritdoc />
		public ICatResponse<CatFielddataRecord> CatFielddata(Func<CatFielddataDescriptor, CatFielddataDescriptor> selector = null)
		{
			return this.DoCat<CatFielddataDescriptor, CatFielddataRequestParameters, CatFielddataRecord>(selector, this.RawDispatch.CatFielddataDispatch<CatResponse<CatFielddataRecord>>);
		}

		public ICatResponse<CatFielddataRecord> CatFielddata(ICatFielddataRequest request)
		{
			return this.DoCat<ICatFielddataRequest, CatFielddataRequestParameters, CatFielddataRecord>(request, this.RawDispatch.CatFielddataDispatch<CatResponse<CatFielddataRecord>>);
		}

		public Task<ICatResponse<CatFielddataRecord>> CatFielddataAsync(Func<CatFielddataDescriptor, CatFielddataDescriptor> selector = null)
		{
			return this.DoCatAsync<CatFielddataDescriptor, CatFielddataRequestParameters, CatFielddataRecord>(selector, this.RawDispatch.CatFielddataDispatchAsync<CatResponse<CatFielddataRecord>>);
		}

		public Task<ICatResponse<CatFielddataRecord>> CatFielddataAsync(ICatFielddataRequest request)
		{
			return this.DoCatAsync<ICatFielddataRequest, CatFielddataRequestParameters, CatFielddataRecord>(request, this.RawDispatch.CatFielddataDispatchAsync<CatResponse<CatFielddataRecord>>);
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