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
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Indices.AliasManagement.AliasExists
{
	public class AliasExistsApiTests
		: ApiIntegrationTestBase<WritableCluster, ExistsResponse, IAliasExistsRequest, AliasExistsDescriptor, AliasExistsRequest>
	{
		public AliasExistsApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<AliasExistsDescriptor, IAliasExistsRequest> Fluent => d => d;
		protected override HttpMethod HttpMethod => HttpMethod.HEAD;

		protected override AliasExistsRequest Initializer => new AliasExistsRequest(Names(CallIsolatedValue + "-alias"));

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_alias/{CallIsolatedValue}-alias";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values)
				client.Indices.Create(index, c => c
					.Aliases(aa => aa.Alias(index + "-alias"))
				);
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.AliasExists(CallIsolatedValue + "-alias", f),
			(client, f) => client.Indices.AliasExistsAsync(CallIsolatedValue + "-alias", f),
			(client, r) => client.Indices.AliasExists(r),
			(client, r) => client.Indices.AliasExistsAsync(r)
		);

		protected override AliasExistsDescriptor NewDescriptor() => new AliasExistsDescriptor(Names(CallIsolatedValue + "-alias"));

		protected override void ExpectResponse(ExistsResponse response) => response.Exists.Should().BeTrue();
	}

	public class AliasExistsNotFoundApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ExistsResponse, IAliasExistsRequest, AliasExistsDescriptor, AliasExistsRequest>

	{
		public AliasExistsNotFoundApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 404;

		protected override Func<AliasExistsDescriptor, IAliasExistsRequest> Fluent => d => d;
		protected override HttpMethod HttpMethod => HttpMethod.HEAD;

		protected override AliasExistsRequest Initializer => new AliasExistsRequest(Names("unknown-alias"));

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_alias/unknown-alias";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.AliasExists("unknown-alias", f),
			(client, f) => client.Indices.AliasExistsAsync("unknown-alias", f),
			(client, r) => client.Indices.AliasExists(r),
			(client, r) => client.Indices.AliasExistsAsync(r)
		);

		protected override AliasExistsDescriptor NewDescriptor() => new AliasExistsDescriptor(Names("unknown-alias"));

		protected override void ExpectResponse(ExistsResponse response)
		{
			response.ServerError.Should().BeNull();
			response.Exists.Should().BeFalse();
		}
	}
}
