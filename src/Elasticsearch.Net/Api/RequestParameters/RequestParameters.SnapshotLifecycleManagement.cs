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
using System.Linq;
using System.Text;
using System.Linq.Expressions;

// ReSharper disable once CheckNamespace
namespace Elasticsearch.Net.Specification.SnapshotLifecycleManagementApi
{
	///<summary>Request options for DeleteSnapshotLifecycle <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-delete-policy.html</para></summary>
	public class DeleteSnapshotLifecycleRequestParameters : RequestParameters<DeleteSnapshotLifecycleRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.DELETE;
		public override bool SupportsBody => false;
	}

	///<summary>Request options for ExecuteSnapshotLifecycle <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-execute-lifecycle.html</para></summary>
	public class ExecuteSnapshotLifecycleRequestParameters : RequestParameters<ExecuteSnapshotLifecycleRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.POST;
		public override bool SupportsBody => false;
	}

	///<summary>Request options for ExecuteRetention <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-execute-retention.html</para></summary>
	public class ExecuteRetentionRequestParameters : RequestParameters<ExecuteRetentionRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.POST;
		public override bool SupportsBody => false;
	}

	///<summary>Request options for GetSnapshotLifecycle <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-get-policy.html</para></summary>
	public class GetSnapshotLifecycleRequestParameters : RequestParameters<GetSnapshotLifecycleRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.GET;
		public override bool SupportsBody => false;
	}

	///<summary>Request options for GetSnapshotLifecycleStats <para>https://www.elastic.co/guide/en/elasticsearch/reference/master/slm-api-get-stats.html</para></summary>
	public class GetSnapshotLifecycleStatsRequestParameters : RequestParameters<GetSnapshotLifecycleStatsRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.GET;
		public override bool SupportsBody => false;
	}

	///<summary>Request options for GetStatus <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-get-status.html</para></summary>
	public class GetSnapshotLifecycleManagementStatusRequestParameters : RequestParameters<GetSnapshotLifecycleManagementStatusRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.GET;
		public override bool SupportsBody => false;
	}

	///<summary>Request options for PutSnapshotLifecycle <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-put-policy.html</para></summary>
	public class PutSnapshotLifecycleRequestParameters : RequestParameters<PutSnapshotLifecycleRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.PUT;
		public override bool SupportsBody => true;
	}

	///<summary>Request options for Start <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-start.html</para></summary>
	public class StartSnapshotLifecycleManagementRequestParameters : RequestParameters<StartSnapshotLifecycleManagementRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.POST;
		public override bool SupportsBody => false;
	}

	///<summary>Request options for Stop <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-stop.html</para></summary>
	public class StopSnapshotLifecycleManagementRequestParameters : RequestParameters<StopSnapshotLifecycleManagementRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.POST;
		public override bool SupportsBody => false;
	}
}