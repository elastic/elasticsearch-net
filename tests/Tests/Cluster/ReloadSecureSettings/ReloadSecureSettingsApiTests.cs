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

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cluster.ReloadSecureSettings
{
	[SkipVersion("<6.5.0", "")]
	public class ReloadSecureSettingsApiTests
		: ApiIntegrationTestBase<IntrusiveOperationCluster, ReloadSecureSettingsResponse, IReloadSecureSettingsRequest, ReloadSecureSettingsDescriptor, ReloadSecureSettingsRequest>
	{
		public ReloadSecureSettingsApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/_nodes/reload_secure_settings";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Nodes.ReloadSecureSettings(),
			(client, f) => client.Nodes.ReloadSecureSettingsAsync(),
			(client, r) => client.Nodes.ReloadSecureSettings(r),
			(client, r) => client.Nodes.ReloadSecureSettingsAsync(r)
		);

		protected override void ExpectResponse(ReloadSecureSettingsResponse response)
		{
			response.Nodes.Should().NotBeEmpty();
			response.NodeStatistics.Should().NotBeNull();
			response.NodeStatistics.Total.Should().BeGreaterOrEqualTo(1);
			response.NodeStatistics.Successful.Should().BeGreaterOrEqualTo(1);
			response.ClusterName.Should().NotBeNullOrWhiteSpace();

		}
	}
}
