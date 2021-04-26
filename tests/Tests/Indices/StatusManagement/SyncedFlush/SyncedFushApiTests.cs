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

using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.StatusManagement.SyncedFlush
{
	public class SyncedFlushApiTests
		: ApiIntegrationAgainstNewIndexTestBase
			<IntrusiveOperationCluster, SyncedFlushResponse, ISyncedFlushRequest, SyncedFlushDescriptor, SyncedFlushRequest>
	{
		public SyncedFlushApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<SyncedFlushDescriptor, ISyncedFlushRequest> Fluent => d => d.AllowNoIndices();
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override SyncedFlushRequest Initializer => new SyncedFlushRequest(CallIsolatedValue) { AllowNoIndices = true };
		protected override string UrlPath => $"/{CallIsolatedValue}/_flush/synced?allow_no_indices=true";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.SyncedFlush(CallIsolatedValue, f),
			(client, f) => client.Indices.SyncedFlushAsync(CallIsolatedValue, f),
			(client, r) => client.Indices.SyncedFlush(r),
			(client, r) => client.Indices.SyncedFlushAsync(r)
		);
	}
}
