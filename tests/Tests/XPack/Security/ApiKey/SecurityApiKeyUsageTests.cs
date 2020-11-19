// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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

				return d => d.RequestConfiguration(r => r.Authentication(new BasicAuthentication(response.Id, response.ApiKey)));
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
						AuthenticationHeader = new BasicAuthentication(response.Id, response.ApiKey)
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
