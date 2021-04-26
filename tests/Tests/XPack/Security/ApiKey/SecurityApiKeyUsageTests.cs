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
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Security.ApiKey
{
	[SkipVersion("<7.0.0", "Implemented in version 7.0.0")]
	public class SecurityApiKeyUsageTests
		: ApiIntegrationTestBase<XPackCluster, NodesInfoResponse, INodesInfoRequest,
			NodesInfoDescriptor, NodesInfoRequest>
	{
		public SecurityApiKeyUsageTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void OnBeforeCall(IElasticClient client)
		{
			var r = client.Security.CreateApiKey(o => o.Name(CallIsolatedValue));
			r.ShouldBeValid();
			ExtendedValue("response", r);
		}

		protected override bool ExpectIsValid => true;

		protected override int ExpectStatusCode => 200;

		protected override Func<NodesInfoDescriptor, INodesInfoRequest> Fluent
		{
			get
			{
				TryGetExtendedValue<CreateApiKeyResponse>("response", out var response);

				// Unit tests for HitsTheCorrectUrl will have a null response object.
				if (response == null)
					return d => d;

				return d => d.RequestConfiguration(r => r.Authentication(new Base64ApiKey(response.Id, response.ApiKey)));
			}
		}

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override NodesInfoDescriptor NewDescriptor() => new NodesInfoDescriptor();

		protected override NodesInfoRequest Initializer
		{
			get
			{
				TryGetExtendedValue<CreateApiKeyResponse>("response", out var response);

				// Unit tests for HitsTheCorrectUrl will have a null response object.
				if (response == null)
					return new NodesInfoRequest();

				return new NodesInfoRequest
				{
					RequestConfiguration = new RequestConfiguration
					{
						AuthenticationHeader = new Base64ApiKey(response.Id, response.ApiKey)
					}
				};
			}
		}

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => "/_nodes";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Nodes.Info(f),
			(client, f) => client.Nodes.InfoAsync(f),
			(client, r) => client.Nodes.Info(r),
			(client, r) => client.Nodes.InfoAsync(r)
		);

		protected override void ExpectResponse(NodesInfoResponse response)
		{
			response.IsValid.Should().BeTrue();
			response.Nodes.Should().NotBeEmpty();
		}
	}
}
