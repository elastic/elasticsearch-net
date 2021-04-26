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
using static Elasticsearch.Net.HttpMethod;

namespace Tests.XPack.License.StartBasicLicense
{
	[SkipVersion("<6.5.0", "")]
	public class StartBasicLicenseInvalidApiTests
		: ApiIntegrationTestBase<XPackCluster, StartBasicLicenseResponse, IStartBasicLicenseRequest, StartBasicLicenseDescriptor,
			StartBasicLicenseRequest>
	{
		public StartBasicLicenseInvalidApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => POST;

		protected override StartBasicLicenseRequest Initializer => new StartBasicLicenseRequest();

		protected override string UrlPath => $"/_license/start_basic";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.License.StartBasic(f),
			(client, f) => client.License.StartBasicAsync(f),
			(client, r) => client.License.StartBasic(r),
			(client, r) => client.License.StartBasicAsync(r)
		);

		protected override void ExpectResponse(StartBasicLicenseResponse response)
		{
			response.BasicWasStarted.Should().BeFalse();
			response.Acknowledged.Should().BeFalse();
			response.ErrorMessage.Should().NotBeNullOrWhiteSpace();
			var d = response.Acknowledge;
			d.Should().NotBeNull();
			d.Message.Should().NotBeNullOrWhiteSpace();
			d.Should()
				.NotBeEmpty()
				.And.ContainKey("ml")
				.And.ContainKey("security");
		}
	}
}
