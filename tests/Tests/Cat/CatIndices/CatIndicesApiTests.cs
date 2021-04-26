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
 using Elastic.Elasticsearch.Ephemeral;
 using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cat.CatIndices
{
	public class CatIndicesApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatIndicesRecord>, ICatIndicesRequest, CatIndicesDescriptor, CatIndicesRequest>
	{
		public CatIndicesApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/indices";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Indices(f),
			(client, f) => client.Cat.IndicesAsync(f),
			(client, r) => client.Cat.Indices(r),
			(client, r) => client.Cat.IndicesAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatIndicesRecord> response) =>
			response.Records.Should().NotBeEmpty().And.Contain(a => !string.IsNullOrEmpty(a.Status));
	}

	public class CatIndicesApiNotFoundWithSecurityTests
		: ApiIntegrationTestBase<XPackCluster, CatResponse<CatIndicesRecord>, ICatIndicesRequest, CatIndicesDescriptor, CatIndicesRequest>
	{
		public CatIndicesApiNotFoundWithSecurityTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 403;

		protected override Func<CatIndicesDescriptor, ICatIndicesRequest> Fluent => f => f
			.Index("doesnot-exist-*")
			.RequestConfiguration(r => r.BasicAuthentication(ClusterAuthentication.User.Username, ClusterAuthentication.User.Password));

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override CatIndicesRequest Initializer => new CatIndicesRequest("doesnot-exist-*")
		{
			RequestConfiguration = new RequestConfiguration
			{
				BasicAuthenticationCredentials = new BasicAuthenticationCredentials(
					ClusterAuthentication.User.Username,
					ClusterAuthentication.User.Password)
			}
		};

		protected override string UrlPath => "/_cat/indices/doesnot-exist-%2A";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Indices(f),
			(client, f) => client.Cat.IndicesAsync(f),
			(client, r) => client.Cat.Indices(r),
			(client, r) => client.Cat.IndicesAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatIndicesRecord> response)
		{
			response.Records.Should().BeEmpty();
			response.ApiCall.Should().NotBeNull();
		}
	}
}
