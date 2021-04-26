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
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Elasticsearch.Net.HttpMethod;

namespace Tests.XPack.License.GetLicense
{
	[SkipVersion("<2.3.0", "")]
	public class GetLicenseApiTests
		: ApiIntegrationTestBase<XPackCluster, GetLicenseResponse, IGetLicenseRequest, GetLicenseDescriptor, GetLicenseRequest>
	{
		public GetLicenseApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => GET;

		protected override GetLicenseRequest Initializer => new GetLicenseRequest();

		protected override string UrlPath => $"/_license";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.License.Get(f),
			(client, f) => client.License.GetAsync(f),
			(client, r) => client.License.Get(r),
			(client, r) => client.License.GetAsync(r)
		);

		protected override void ExpectResponse(GetLicenseResponse response)
		{
			var l = response.License;
			l.Should().NotBeNull();
			l.ExpiryDate.Should().BeAfter(DateTime.UtcNow.AddYears(-2));
			l.IssueDate.Should().BeAfter(DateTime.UtcNow.AddYears(-30));
			l.IssueDateInMilliseconds.Should().BeGreaterThan(0);
			l.ExpiryDateInMilliseconds.Should().BeGreaterThan(0);
			l.IssuedTo.Should().NotBeNullOrWhiteSpace();
			l.Issuer.Should().NotBeNullOrWhiteSpace();
			l.MaxNodes.Should().BeGreaterThan(0);
			l.Status.Should().Be(LicenseStatus.Active);
			l.UID.Should().NotBeNullOrWhiteSpace();
		}
	}
}
