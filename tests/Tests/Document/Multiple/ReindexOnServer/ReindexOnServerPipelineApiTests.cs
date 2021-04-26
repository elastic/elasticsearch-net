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
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Document.Multiple.ReindexOnServer
{
	[SkipVersion("<2.3.0", "")]
	public class ReindexOnServerPipelineApiTests
		: ApiIntegrationTestBase<IntrusiveOperationCluster, ReindexOnServerResponse, IReindexOnServerRequest, ReindexOnServerDescriptor,
			ReindexOnServerRequest>
	{
		public ReindexOnServerPipelineApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson =>
			new
			{
				dest = new
				{
					index = $"{CallIsolatedValue}-clone",
					pipeline = $"{Pipeline}"
				},
				source = new
				{
					index = CallIsolatedValue
				},
				conflicts = "proceed"
			};

		protected override int ExpectStatusCode => 200;

		protected override Func<ReindexOnServerDescriptor, IReindexOnServerRequest> Fluent => d => d
			.Source(s => s
				.Index(CallIsolatedValue)
			)
			.Destination(s => s
				.Index(CallIsolatedValue + "-clone")
				.Pipeline($"{Pipeline}")
			)
			.Conflicts(Conflicts.Proceed)
			.Refresh();

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ReindexOnServerRequest Initializer => new ReindexOnServerRequest()
		{
			Source = new ReindexSource
			{
				Index = CallIsolatedValue
			},
			Destination = new ReindexDestination
			{
				Index = CallIsolatedValue + "-clone",
				Pipeline = $"{Pipeline}"
			},
			Conflicts = Conflicts.Proceed,
			Refresh = true,
		};

		protected virtual string Pipeline { get; } = "pipeline-id";

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => $"/_reindex?refresh=true";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			var pipelineResponse = client.Ingest.PutPipeline(Pipeline, p => p
				.Processors(pr => pr
					.Set<Test>(t => t.Field(f => f.Flag).Value("Overridden"))
				)
			);
			pipelineResponse.ShouldBeValid($"Failed to set up pipeline named '{Pipeline}' required for bulk");

			foreach (var index in values.Values)
			{
				Client.Index(new Test { Id = 1, Flag = "bar" }, i => i.Index(index).Refresh(Refresh.True));
				Client.Index(new Test { Id = 2, Flag = "bar" }, i => i.Index(index).Refresh(Refresh.True));
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.ReindexOnServer(f),
			(client, f) => client.ReindexOnServerAsync(f),
			(client, r) => client.ReindexOnServer(r),
			(client, r) => client.ReindexOnServerAsync(r)
		);

		protected override void OnAfterCall(IElasticClient client) => client.Indices.Refresh(CallIsolatedValue);

		protected override void ExpectResponse(ReindexOnServerResponse response)
		{
			response.Task.Should().BeNull();
			response.Took.Should().BeGreaterThan(TimeSpan.FromMilliseconds(0));
			response.Total.Should().Be(2);
			response.Updated.Should().Be(0);
			response.Created.Should().Be(2);
			response.Batches.Should().Be(1);

			var search = Client.Search<Test>(s => s
				.Index(CallIsolatedValue + "-clone")
			);
			search.Total.Should().BeGreaterThan(0);
			search.Documents.Should().OnlyContain(t => t.Flag == "Overridden");
		}

		public class Test
		{
			public string Flag { get; set; }
			public long Id { get; set; }
		}
	}
}
