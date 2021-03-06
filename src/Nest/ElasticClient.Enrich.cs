// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗  
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝  
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// -----------------------------------------------
//  
// This file is automatically generated 
// Please do not edit these files manually
// Run the following in the root of the repos:
//
// 		*NIX 		:	./build.sh codegen
// 		Windows 	:	build.bat codegen
//
// -----------------------------------------------
// ReSharper disable RedundantUsingDirective
using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net.Specification.EnrichApi;

// ReSharper disable once CheckNamespace
// ReSharper disable RedundantTypeArgumentsOfMethod
namespace Nest.Specification.EnrichApi
{
	///<summary>
	/// Enrich APIs.
	/// <para>Not intended to be instantiated directly. Use the <see cref = "IElasticClient.Enrich"/> property
	/// on <see cref = "IElasticClient"/>.
	///</para>
	///</summary>
	public class EnrichNamespace : NamespacedClientProxy
	{
		internal EnrichNamespace(ElasticClient client): base(client)
		{
		}

		/// <summary>
		/// <c>DELETE</c> request to the <c>enrich.delete_policy</c> API, read more about this API online:
		/// <para></para>
		/// <a href = "https://www.elastic.co/guide/en/elasticsearch/reference/current/delete-enrich-policy-api.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/delete-enrich-policy-api.html</a>
		/// </summary>
		public DeleteEnrichPolicyResponse DeletePolicy(Name name, Func<DeleteEnrichPolicyDescriptor, IDeleteEnrichPolicyRequest> selector = null) => DeletePolicy(selector.InvokeOrDefault(new DeleteEnrichPolicyDescriptor(name: name)));
		/// <summary>
		/// <c>DELETE</c> request to the <c>enrich.delete_policy</c> API, read more about this API online:
		/// <para></para>
		/// <a href = "https://www.elastic.co/guide/en/elasticsearch/reference/current/delete-enrich-policy-api.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/delete-enrich-policy-api.html</a>
		/// </summary>
		public Task<DeleteEnrichPolicyResponse> DeletePolicyAsync(Name name, Func<DeleteEnrichPolicyDescriptor, IDeleteEnrichPolicyRequest> selector = null, CancellationToken ct = default) => DeletePolicyAsync(selector.InvokeOrDefault(new DeleteEnrichPolicyDescriptor(name: name)), ct);
		/// <summary>
		/// <c>DELETE</c> request to the <c>enrich.delete_policy</c> API, read more about this API online:
		/// <para></para>
		/// <a href = "https://www.elastic.co/guide/en/elasticsearch/reference/current/delete-enrich-policy-api.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/delete-enrich-policy-api.html</a>
		/// </summary>
		public DeleteEnrichPolicyResponse DeletePolicy(IDeleteEnrichPolicyRequest request) => DoRequest<IDeleteEnrichPolicyRequest, DeleteEnrichPolicyResponse>(request, request.RequestParameters);
		/// <summary>
		/// <c>DELETE</c> request to the <c>enrich.delete_policy</c> API, read more about this API online:
		/// <para></para>
		/// <a href = "https://www.elastic.co/guide/en/elasticsearch/reference/current/delete-enrich-policy-api.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/delete-enrich-policy-api.html</a>
		/// </summary>
		public Task<DeleteEnrichPolicyResponse> DeletePolicyAsync(IDeleteEnrichPolicyRequest request, CancellationToken ct = default) => DoRequestAsync<IDeleteEnrichPolicyRequest, DeleteEnrichPolicyResponse>(request, request.RequestParameters, ct);
		/// <summary>
		/// <c>PUT</c> request to the <c>enrich.execute_policy</c> API, read more about this API online:
		/// <para></para>
		/// <a href = "https://www.elastic.co/guide/en/elasticsearch/reference/current/execute-enrich-policy-api.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/execute-enrich-policy-api.html</a>
		/// </summary>
		public ExecuteEnrichPolicyResponse ExecutePolicy(Name name, Func<ExecuteEnrichPolicyDescriptor, IExecuteEnrichPolicyRequest> selector = null) => ExecutePolicy(selector.InvokeOrDefault(new ExecuteEnrichPolicyDescriptor(name: name)));
		/// <summary>
		/// <c>PUT</c> request to the <c>enrich.execute_policy</c> API, read more about this API online:
		/// <para></para>
		/// <a href = "https://www.elastic.co/guide/en/elasticsearch/reference/current/execute-enrich-policy-api.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/execute-enrich-policy-api.html</a>
		/// </summary>
		public Task<ExecuteEnrichPolicyResponse> ExecutePolicyAsync(Name name, Func<ExecuteEnrichPolicyDescriptor, IExecuteEnrichPolicyRequest> selector = null, CancellationToken ct = default) => ExecutePolicyAsync(selector.InvokeOrDefault(new ExecuteEnrichPolicyDescriptor(name: name)), ct);
		/// <summary>
		/// <c>PUT</c> request to the <c>enrich.execute_policy</c> API, read more about this API online:
		/// <para></para>
		/// <a href = "https://www.elastic.co/guide/en/elasticsearch/reference/current/execute-enrich-policy-api.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/execute-enrich-policy-api.html</a>
		/// </summary>
		public ExecuteEnrichPolicyResponse ExecutePolicy(IExecuteEnrichPolicyRequest request) => DoRequest<IExecuteEnrichPolicyRequest, ExecuteEnrichPolicyResponse>(request, request.RequestParameters);
		/// <summary>
		/// <c>PUT</c> request to the <c>enrich.execute_policy</c> API, read more about this API online:
		/// <para></para>
		/// <a href = "https://www.elastic.co/guide/en/elasticsearch/reference/current/execute-enrich-policy-api.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/execute-enrich-policy-api.html</a>
		/// </summary>
		public Task<ExecuteEnrichPolicyResponse> ExecutePolicyAsync(IExecuteEnrichPolicyRequest request, CancellationToken ct = default) => DoRequestAsync<IExecuteEnrichPolicyRequest, ExecuteEnrichPolicyResponse>(request, request.RequestParameters, ct);
		/// <summary>
		/// <c>GET</c> request to the <c>enrich.get_policy</c> API, read more about this API online:
		/// <para></para>
		/// <a href = "https://www.elastic.co/guide/en/elasticsearch/reference/current/enrich-get-policy.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/enrich-get-policy.html</a>
		/// </summary>
		public GetEnrichPolicyResponse GetPolicy(Names name = null, Func<GetEnrichPolicyDescriptor, IGetEnrichPolicyRequest> selector = null) => GetPolicy(selector.InvokeOrDefault(new GetEnrichPolicyDescriptor().Name(name: name)));
		/// <summary>
		/// <c>GET</c> request to the <c>enrich.get_policy</c> API, read more about this API online:
		/// <para></para>
		/// <a href = "https://www.elastic.co/guide/en/elasticsearch/reference/current/enrich-get-policy.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/enrich-get-policy.html</a>
		/// </summary>
		public Task<GetEnrichPolicyResponse> GetPolicyAsync(Names name = null, Func<GetEnrichPolicyDescriptor, IGetEnrichPolicyRequest> selector = null, CancellationToken ct = default) => GetPolicyAsync(selector.InvokeOrDefault(new GetEnrichPolicyDescriptor().Name(name: name)), ct);
		/// <summary>
		/// <c>GET</c> request to the <c>enrich.get_policy</c> API, read more about this API online:
		/// <para></para>
		/// <a href = "https://www.elastic.co/guide/en/elasticsearch/reference/current/enrich-get-policy.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/enrich-get-policy.html</a>
		/// </summary>
		public GetEnrichPolicyResponse GetPolicy(IGetEnrichPolicyRequest request) => DoRequest<IGetEnrichPolicyRequest, GetEnrichPolicyResponse>(request, request.RequestParameters);
		/// <summary>
		/// <c>GET</c> request to the <c>enrich.get_policy</c> API, read more about this API online:
		/// <para></para>
		/// <a href = "https://www.elastic.co/guide/en/elasticsearch/reference/current/enrich-get-policy.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/enrich-get-policy.html</a>
		/// </summary>
		public Task<GetEnrichPolicyResponse> GetPolicyAsync(IGetEnrichPolicyRequest request, CancellationToken ct = default) => DoRequestAsync<IGetEnrichPolicyRequest, GetEnrichPolicyResponse>(request, request.RequestParameters, ct);
		/// <summary>
		/// <c>PUT</c> request to the <c>enrich.put_policy</c> API, read more about this API online:
		/// <para></para>
		/// <a href = "https://www.elastic.co/guide/en/elasticsearch/reference/current/put-enrich-policy-api.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/put-enrich-policy-api.html</a>
		/// </summary>
		public PutEnrichPolicyResponse PutPolicy<TDocument>(Name name, Func<PutEnrichPolicyDescriptor<TDocument>, IPutEnrichPolicyRequest> selector)
			where TDocument : class => PutPolicy(selector.InvokeOrDefault(new PutEnrichPolicyDescriptor<TDocument>(name: name)));
		/// <summary>
		/// <c>PUT</c> request to the <c>enrich.put_policy</c> API, read more about this API online:
		/// <para></para>
		/// <a href = "https://www.elastic.co/guide/en/elasticsearch/reference/current/put-enrich-policy-api.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/put-enrich-policy-api.html</a>
		/// </summary>
		public Task<PutEnrichPolicyResponse> PutPolicyAsync<TDocument>(Name name, Func<PutEnrichPolicyDescriptor<TDocument>, IPutEnrichPolicyRequest> selector, CancellationToken ct = default)
			where TDocument : class => PutPolicyAsync(selector.InvokeOrDefault(new PutEnrichPolicyDescriptor<TDocument>(name: name)), ct);
		/// <summary>
		/// <c>PUT</c> request to the <c>enrich.put_policy</c> API, read more about this API online:
		/// <para></para>
		/// <a href = "https://www.elastic.co/guide/en/elasticsearch/reference/current/put-enrich-policy-api.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/put-enrich-policy-api.html</a>
		/// </summary>
		public PutEnrichPolicyResponse PutPolicy(IPutEnrichPolicyRequest request) => DoRequest<IPutEnrichPolicyRequest, PutEnrichPolicyResponse>(request, request.RequestParameters);
		/// <summary>
		/// <c>PUT</c> request to the <c>enrich.put_policy</c> API, read more about this API online:
		/// <para></para>
		/// <a href = "https://www.elastic.co/guide/en/elasticsearch/reference/current/put-enrich-policy-api.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/put-enrich-policy-api.html</a>
		/// </summary>
		public Task<PutEnrichPolicyResponse> PutPolicyAsync(IPutEnrichPolicyRequest request, CancellationToken ct = default) => DoRequestAsync<IPutEnrichPolicyRequest, PutEnrichPolicyResponse>(request, request.RequestParameters, ct);
		/// <summary>
		/// <c>GET</c> request to the <c>enrich.stats</c> API, read more about this API online:
		/// <para></para>
		/// <a href = "https://www.elastic.co/guide/en/elasticsearch/reference/current/enrich-stats-api.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/enrich-stats-api.html</a>
		/// </summary>
		public EnrichStatsResponse Stats(Func<EnrichStatsDescriptor, IEnrichStatsRequest> selector = null) => Stats(selector.InvokeOrDefault(new EnrichStatsDescriptor()));
		/// <summary>
		/// <c>GET</c> request to the <c>enrich.stats</c> API, read more about this API online:
		/// <para></para>
		/// <a href = "https://www.elastic.co/guide/en/elasticsearch/reference/current/enrich-stats-api.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/enrich-stats-api.html</a>
		/// </summary>
		public Task<EnrichStatsResponse> StatsAsync(Func<EnrichStatsDescriptor, IEnrichStatsRequest> selector = null, CancellationToken ct = default) => StatsAsync(selector.InvokeOrDefault(new EnrichStatsDescriptor()), ct);
		/// <summary>
		/// <c>GET</c> request to the <c>enrich.stats</c> API, read more about this API online:
		/// <para></para>
		/// <a href = "https://www.elastic.co/guide/en/elasticsearch/reference/current/enrich-stats-api.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/enrich-stats-api.html</a>
		/// </summary>
		public EnrichStatsResponse Stats(IEnrichStatsRequest request) => DoRequest<IEnrichStatsRequest, EnrichStatsResponse>(request, request.RequestParameters);
		/// <summary>
		/// <c>GET</c> request to the <c>enrich.stats</c> API, read more about this API online:
		/// <para></para>
		/// <a href = "https://www.elastic.co/guide/en/elasticsearch/reference/current/enrich-stats-api.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/enrich-stats-api.html</a>
		/// </summary>
		public Task<EnrichStatsResponse> StatsAsync(IEnrichStatsRequest request, CancellationToken ct = default) => DoRequestAsync<IEnrichStatsRequest, EnrichStatsResponse>(request, request.RequestParameters, ct);
	}
}