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
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.ReloadSearchAnalyzers
{
	[SkipVersion("<7.7.0", "Introduced in 7.7.0")]
	public class ReloadSearchAnalyzersApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ReloadSearchAnalyzersResponse, IReloadSearchAnalyzersRequest, ReloadSearchAnalyzersDescriptor, ReloadSearchAnalyzersRequest>
	{
		public ReloadSearchAnalyzersApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override void OnBeforeCall(IElasticClient client)
		{
			var create = client.Indices.Create(CallIsolatedValue, c => c
				.Settings(s => s
					.NumberOfShards(4)
					.NumberOfRoutingShards(8)
					.NumberOfReplicas(0)
				)
			);
			create.ShouldBeValid();
		}

		protected override ReloadSearchAnalyzersDescriptor NewDescriptor() => new ReloadSearchAnalyzersDescriptor(CallIsolatedValue);

		protected override Func<ReloadSearchAnalyzersDescriptor, IReloadSearchAnalyzersRequest> Fluent => d => d.Index(CallIsolatedValue);
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ReloadSearchAnalyzersRequest Initializer => new ReloadSearchAnalyzersRequest(CallIsolatedValue);
		protected override string UrlPath => $"/{CallIsolatedValue}/_reload_search_analyzers";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.ReloadSearchAnalyzers(CallIsolatedValue, f),
			(client, f) => client.Indices.ReloadSearchAnalyzersAsync(CallIsolatedValue, f),
			(client, r) => client.Indices.ReloadSearchAnalyzers(r),
			(client, r) => client.Indices.ReloadSearchAnalyzersAsync(r)
		);

		protected override void ExpectResponse(ReloadSearchAnalyzersResponse response) => response.ShouldBeValid();
	}
}
