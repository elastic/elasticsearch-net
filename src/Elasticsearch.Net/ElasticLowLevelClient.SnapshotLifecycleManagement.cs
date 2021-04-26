/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elastic.Transport;
using static Elastic.Transport.HttpMethod;

// ReSharper disable InterpolatedStringExpressionIsNotIFormattable
// ReSharper disable once CheckNamespace
// ReSharper disable InterpolatedStringExpressionIsNotIFormattable
// ReSharper disable RedundantExtendsListEntry
namespace Elasticsearch.Net.Specification.SnapshotLifecycleManagementApi
{
	///<summary>
	/// Snapshot Lifecycle Management APIs.
	/// <para>Not intended to be instantiated directly. Use the <see cref = "IElasticLowLevelClient.SnapshotLifecycleManagement"/> property
	/// on <see cref = "IElasticLowLevelClient"/>.
	///</para>
	///</summary>
	public class LowLevelSnapshotLifecycleManagementNamespace : NamespacedClientProxy
	{
		internal LowLevelSnapshotLifecycleManagementNamespace(ElasticLowLevelClient client): base(client)
		{
		}

		///<summary>DELETE on /_slm/policy/{policy_id} <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-delete-policy.html</para></summary>
		///<param name = "policyId">The id of the snapshot lifecycle policy to remove</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse DeleteSnapshotLifecycle<TResponse>(string policyId, DeleteSnapshotLifecycleRequestParameters requestParameters = null)
			where TResponse : class, ITransportResponse, new() => DoRequest<TResponse>(DELETE, Url($"_slm/policy/{policyId:policyId}"), null, RequestParams(requestParameters));
		///<summary>DELETE on /_slm/policy/{policy_id} <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-delete-policy.html</para></summary>
		///<param name = "policyId">The id of the snapshot lifecycle policy to remove</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("slm.delete_lifecycle", "policy_id")]
		public Task<TResponse> DeleteSnapshotLifecycleAsync<TResponse>(string policyId, DeleteSnapshotLifecycleRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, ITransportResponse, new() => DoRequestAsync<TResponse>(DELETE, Url($"_slm/policy/{policyId:policyId}"), ctx, null, RequestParams(requestParameters));
		///<summary>POST on /_slm/policy/{policy_id}/_execute <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-execute-lifecycle.html</para></summary>
		///<param name = "policyId">The id of the snapshot lifecycle policy to be executed</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse ExecuteSnapshotLifecycle<TResponse>(string policyId, ExecuteSnapshotLifecycleRequestParameters requestParameters = null)
			where TResponse : class, ITransportResponse, new() => DoRequest<TResponse>(POST, Url($"_slm/policy/{policyId:policyId}/_execute"), null, RequestParams(requestParameters));
		///<summary>POST on /_slm/policy/{policy_id}/_execute <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-execute-lifecycle.html</para></summary>
		///<param name = "policyId">The id of the snapshot lifecycle policy to be executed</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("slm.execute_lifecycle", "policy_id")]
		public Task<TResponse> ExecuteSnapshotLifecycleAsync<TResponse>(string policyId, ExecuteSnapshotLifecycleRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, ITransportResponse, new() => DoRequestAsync<TResponse>(POST, Url($"_slm/policy/{policyId:policyId}/_execute"), ctx, null, RequestParams(requestParameters));
		///<summary>POST on /_slm/_execute_retention <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-execute-retention.html</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse ExecuteRetention<TResponse>(ExecuteRetentionRequestParameters requestParameters = null)
			where TResponse : class, ITransportResponse, new() => DoRequest<TResponse>(POST, "_slm/_execute_retention", null, RequestParams(requestParameters));
		///<summary>POST on /_slm/_execute_retention <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-execute-retention.html</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("slm.execute_retention", "")]
		public Task<TResponse> ExecuteRetentionAsync<TResponse>(ExecuteRetentionRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, ITransportResponse, new() => DoRequestAsync<TResponse>(POST, "_slm/_execute_retention", ctx, null, RequestParams(requestParameters));
		///<summary>GET on /_slm/policy/{policy_id} <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-get-policy.html</para></summary>
		///<param name = "policyId">Comma-separated list of snapshot lifecycle policies to retrieve</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse GetSnapshotLifecycle<TResponse>(string policyId, GetSnapshotLifecycleRequestParameters requestParameters = null)
			where TResponse : class, ITransportResponse, new() => DoRequest<TResponse>(GET, Url($"_slm/policy/{policyId:policyId}"), null, RequestParams(requestParameters));
		///<summary>GET on /_slm/policy/{policy_id} <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-get-policy.html</para></summary>
		///<param name = "policyId">Comma-separated list of snapshot lifecycle policies to retrieve</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("slm.get_lifecycle", "policy_id")]
		public Task<TResponse> GetSnapshotLifecycleAsync<TResponse>(string policyId, GetSnapshotLifecycleRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, ITransportResponse, new() => DoRequestAsync<TResponse>(GET, Url($"_slm/policy/{policyId:policyId}"), ctx, null, RequestParams(requestParameters));
		///<summary>GET on /_slm/policy <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-get-policy.html</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse GetSnapshotLifecycle<TResponse>(GetSnapshotLifecycleRequestParameters requestParameters = null)
			where TResponse : class, ITransportResponse, new() => DoRequest<TResponse>(GET, "_slm/policy", null, RequestParams(requestParameters));
		///<summary>GET on /_slm/policy <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-get-policy.html</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("slm.get_lifecycle", "")]
		public Task<TResponse> GetSnapshotLifecycleAsync<TResponse>(GetSnapshotLifecycleRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, ITransportResponse, new() => DoRequestAsync<TResponse>(GET, "_slm/policy", ctx, null, RequestParams(requestParameters));
		///<summary>GET on /_slm/stats <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/slm-api-get-stats.html</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse GetSnapshotLifecycleStats<TResponse>(GetSnapshotLifecycleStatsRequestParameters requestParameters = null)
			where TResponse : class, ITransportResponse, new() => DoRequest<TResponse>(GET, "_slm/stats", null, RequestParams(requestParameters));
		///<summary>GET on /_slm/stats <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/slm-api-get-stats.html</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("slm.get_stats", "")]
		public Task<TResponse> GetSnapshotLifecycleStatsAsync<TResponse>(GetSnapshotLifecycleStatsRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, ITransportResponse, new() => DoRequestAsync<TResponse>(GET, "_slm/stats", ctx, null, RequestParams(requestParameters));
		///<summary>GET on /_slm/status <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-get-status.html</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse GetStatus<TResponse>(GetSnapshotLifecycleManagementStatusRequestParameters requestParameters = null)
			where TResponse : class, ITransportResponse, new() => DoRequest<TResponse>(GET, "_slm/status", null, RequestParams(requestParameters));
		///<summary>GET on /_slm/status <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-get-status.html</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("slm.get_status", "")]
		public Task<TResponse> GetStatusAsync<TResponse>(GetSnapshotLifecycleManagementStatusRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, ITransportResponse, new() => DoRequestAsync<TResponse>(GET, "_slm/status", ctx, null, RequestParams(requestParameters));
		///<summary>PUT on /_slm/policy/{policy_id} <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-put-policy.html</para></summary>
		///<param name = "policyId">The id of the snapshot lifecycle policy</param>
		///<param name = "body">The snapshot lifecycle policy definition to register</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse PutSnapshotLifecycle<TResponse>(string policyId, PostData body, PutSnapshotLifecycleRequestParameters requestParameters = null)
			where TResponse : class, ITransportResponse, new() => DoRequest<TResponse>(PUT, Url($"_slm/policy/{policyId:policyId}"), body, RequestParams(requestParameters));
		///<summary>PUT on /_slm/policy/{policy_id} <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-put-policy.html</para></summary>
		///<param name = "policyId">The id of the snapshot lifecycle policy</param>
		///<param name = "body">The snapshot lifecycle policy definition to register</param>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("slm.put_lifecycle", "policy_id, body")]
		public Task<TResponse> PutSnapshotLifecycleAsync<TResponse>(string policyId, PostData body, PutSnapshotLifecycleRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, ITransportResponse, new() => DoRequestAsync<TResponse>(PUT, Url($"_slm/policy/{policyId:policyId}"), ctx, body, RequestParams(requestParameters));
		///<summary>POST on /_slm/start <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-start.html</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse Start<TResponse>(StartSnapshotLifecycleManagementRequestParameters requestParameters = null)
			where TResponse : class, ITransportResponse, new() => DoRequest<TResponse>(POST, "_slm/start", null, RequestParams(requestParameters));
		///<summary>POST on /_slm/start <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-start.html</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("slm.start", "")]
		public Task<TResponse> StartAsync<TResponse>(StartSnapshotLifecycleManagementRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, ITransportResponse, new() => DoRequestAsync<TResponse>(POST, "_slm/start", ctx, null, RequestParams(requestParameters));
		///<summary>POST on /_slm/stop <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-stop.html</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		public TResponse Stop<TResponse>(StopSnapshotLifecycleManagementRequestParameters requestParameters = null)
			where TResponse : class, ITransportResponse, new() => DoRequest<TResponse>(POST, "_slm/stop", null, RequestParams(requestParameters));
		///<summary>POST on /_slm/stop <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-stop.html</para></summary>
		///<param name = "requestParameters">Request specific configuration such as querystring parameters &amp; request specific connection settings.</param>
		[MapsApi("slm.stop", "")]
		public Task<TResponse> StopAsync<TResponse>(StopSnapshotLifecycleManagementRequestParameters requestParameters = null, CancellationToken ctx = default)
			where TResponse : class, ITransportResponse, new() => DoRequestAsync<TResponse>(POST, "_slm/stop", ctx, null, RequestParams(requestParameters));
	}
}