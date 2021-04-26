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

using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.IndexManagement.UnfreezeIndex
{
	public class UnfreezeIndexApiTests
		: ApiIntegrationAgainstNewIndexTestBase
			<WritableCluster, UnfreezeIndexResponse, IUnfreezeIndexRequest, UnfreezeIndexDescriptor, UnfreezeIndexRequest>
	{
		public UnfreezeIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override UnfreezeIndexRequest Initializer => new UnfreezeIndexRequest(CallIsolatedValue);
		protected override string UrlPath => $"/{CallIsolatedValue}/_unfreeze";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.Unfreeze(CallIsolatedValue),
			(client, f) => client.Indices.UnfreezeAsync(CallIsolatedValue),
			(client, r) => client.Indices.Unfreeze(r),
			(client, r) => client.Indices.UnfreezeAsync(r)
		);

		protected override void OnBeforeCall(IElasticClient client)
		{
			var freeze = client.Indices.Freeze(CallIsolatedValue);
			freeze.IsValid.Should().BeTrue();
		}

		protected override void ExpectResponse(UnfreezeIndexResponse response) => response.Acknowledged.Should().BeTrue();
	}
}
