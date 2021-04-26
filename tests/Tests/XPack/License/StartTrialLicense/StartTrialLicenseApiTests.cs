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
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.License.StartTrialLicense
{
	public class TrialLicenseCluster : ClientTestClusterBase { }

	[SkipVersion("<6.4.0", "Only exists in Elasticsearch 6.1.0+, expect x-pack to ship in default distribution")]
	public class StartTrialLicenseApiTests
		: ApiIntegrationTestBase<TrialLicenseCluster, StartTrialLicenseResponse, IStartTrialLicenseRequest, StartTrialLicenseDescriptor,
			StartTrialLicenseRequest>
	{
		public StartTrialLicenseApiTests(TrialLicenseCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected bool BootstrappedWithLicense => !string.IsNullOrEmpty(Cluster.ClusterConfiguration.XPackLicenseJson);

		protected override bool ExpectIsValid => true;
		[I] public override async Task ReturnsExpectedIsValid() =>
			await AssertOnAllResponses(r => r.ShouldHaveExpectedIsValid(r.TrialWasStarted));

		protected override int ExpectStatusCode => 200;
		[I] public override async Task ReturnsExpectedStatusCode() =>
			await AssertOnAllResponses(r => r.ApiCall.HttpStatusCode.Should().Be(r.TrialWasStarted ? 200 : 403));

		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_license/start_trial?acknowledge=true";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.License.StartTrial(f),
			(client, f) => client.License.StartTrialAsync(f),
			(client, r) => client.License.StartTrial(r),
			(client, r) => client.License.StartTrialAsync(r)
		);

		protected override StartTrialLicenseRequest Initializer => new StartTrialLicenseRequest { Acknowledge = true };
		protected override Func<StartTrialLicenseDescriptor, IStartTrialLicenseRequest> Fluent => s => s.Acknowledge();

		protected override void ExpectResponse(StartTrialLicenseResponse response)
		{
			response.Acknowledged.Should().BeTrue();
			if (!response.TrialWasStarted)
				response.ErrorMessage.Should().NotBeNullOrWhiteSpace().And.Contain("Trial was already activated");
		}
	}
}
