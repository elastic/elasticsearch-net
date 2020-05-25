// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.Xunit;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Security.ApiKey
{
	/*
	 *   On the server internally create api key does a search. on CI this search often fails and the PUT for create api key returns with:
	 *
	 *  - [1] BadResponse: Node: https://localhost:9200/ Took: 00:00:00.9531746
# OriginalException: Elasticsearch.Net.ElasticsearchClientException: The remote server returned an error: (503) Server Unavailable.. Call: Status code 503 from: PUT /_security/api_key?pretty=true&error_trace=true. ServerError: Type: search_phase_execution_exception Reason: "all shards failed" ---> System.Net.WebException: The remote server returned an error: (503) Server Unavailable.
   at System.Net.HttpWebRequest.GetResponse()
   at Elasticsearch.Net.HttpWebRequestConnection.Request[TResponse](RequestData requestData) in D:\a\1\s\src\Elasticsearch.Net\Connection\HttpWebRequestConnection.cs:line 59
   --- End of inner exception stack trace ---
# Request:
{"name":"nest-initializer-c241e819","role_descriptors":{}}
# Response:
{
  "error" : {
    "root_cause" : [ ],
    "type" : "search_phase_execution_exception",
    "reason" : "all shards failed",
    "phase" : "query",
    "grouped" : true,
    "failed_shards" : [ ],
    "stack_trace" : "Failed to execute phase [query], all shards failed\r\n\tat ....."
  },
  "status" : 503
}

	 *
	 */

	[SkipVersion("<7.0.0", "Implemented in version 7.0.0")]
	[SkipOnCi] //TODO flakey: investigate see above for more information
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

				return d => d.RequestConfiguration(r => r.ApiKeyAuthentication(response.Id, response.ApiKey));
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
						ApiKeyAuthenticationCredentials = new ApiKeyAuthenticationCredentials(response.Id, response.ApiKey)
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
