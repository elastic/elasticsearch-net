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
using Tests.Core.Xunit;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Elasticsearch.Net.HttpMethod;

namespace Tests.XPack.License.GetBasicLicenseStatus
{
	[SkipVersion("<6.5.0", "")]
	[SkipOnCi]
	public class GetBasicLicenseStatusApiTests
		: ApiIntegrationTestBase<XPackCluster, GetBasicLicenseStatusResponse, IGetBasicLicenseStatusRequest, GetBasicLicenseStatusDescriptor, GetBasicLicenseStatusRequest>
	{
		public GetBasicLicenseStatusApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => GET;

		protected override GetBasicLicenseStatusRequest Initializer => new GetBasicLicenseStatusRequest();

		protected override string UrlPath => $"/_license/basic_status";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.License.GetBasicStatus(f),
			(client, f) => client.License.GetBasicStatusAsync(f),
			(client, r) => client.License.GetBasicStatus(r),
			(client, r) => client.License.GetBasicStatusAsync(r)
		);

		protected override void ExpectResponse(GetBasicLicenseStatusResponse response) =>
			response.EligableToStartBasic.Should().BeTrue();
	}
}
