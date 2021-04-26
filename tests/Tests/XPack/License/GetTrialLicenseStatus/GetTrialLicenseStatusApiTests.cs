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
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.Xunit;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.License.GetTrialLicenseStatus
{
	[SkipVersion("<6.1.0", "Only exists in Elasticsearch 6.1.0+")]
	[SkipOnCi]
	public class GetTrialLicenseStatusApiTests
		: ApiIntegrationTestBase<XPackCluster, GetTrialLicenseStatusResponse, IGetTrialLicenseStatusRequest, GetTrialLicenseStatusDescriptor,
			GetTrialLicenseStatusRequest>
	{
		public GetTrialLicenseStatusApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_license/trial_status";

		protected bool BootstrappedWithLicense => !string.IsNullOrEmpty(Cluster.ClusterConfiguration.XPackLicenseJson);

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.License.GetTrialStatus(f),
			(client, f) => client.License.GetTrialStatusAsync(f),
			(client, r) => client.License.GetTrialStatus(r),
			(client, r) => client.License.GetTrialStatusAsync(r)
		);

		protected override void ExpectResponse(GetTrialLicenseStatusResponse response)
		{
			response.ShouldBeValid();
			// returns false if bootstrap already started the trial
			response.EligibleToStartTrial.Should().Be(BootstrappedWithLicense);
		}
	}
}
