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
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Indices.StatusManagement.ClearCache
{
	public class ClearCacheApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ClearCacheResponse, IClearCacheRequest, ClearCacheDescriptor, ClearCacheRequest>
	{
		public ClearCacheApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<ClearCacheDescriptor, IClearCacheRequest> Fluent => d => d.Request();
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ClearCacheRequest Initializer => new ClearCacheRequest(AllIndices) { Request = true };
		protected override string UrlPath => "/_all/_cache/clear?request=true";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.ClearCache(AllIndices, f),
			(client, f) => client.Indices.ClearCacheAsync(AllIndices, f),
			(client, r) => client.Indices.ClearCache(r),
			(client, r) => client.Indices.ClearCacheAsync(r)
		);
	}
}
