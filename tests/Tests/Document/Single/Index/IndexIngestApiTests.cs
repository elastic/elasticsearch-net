// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elastic.Transport;
using Elasticsearch.Net;
using Nest;
using Tests.Core.Client;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Domain.Extensions;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Tests.Domain.Helpers.TestValueHelper;

namespace Tests.Document.Single.Index
{
	public class IndexIngestApiTests
		: ApiIntegrationTestBase<IntrusiveOperationCluster, IndexResponse, IIndexRequest<Project>, IndexDescriptor<Project>, IndexRequest<Project>>
	{
		public IndexIngestApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson =>
			new
			{
				name = CallIsolatedValue,
				type = Project.TypeName,
				join = Document.Join.ToAnonymousObject(),
				state = "Stable",
				visibility = "Public",
				startedOn = FixedDate,
				lastActivity = FixedDate,
				numberOfContributors = 0,
				sourceOnly = Dependant(null, new { notWrittenByDefaultSerializer = "written" }),
				curatedTags = new[] { new { name = "x", added = FixedDate } },
			};

		protected override int ExpectStatusCode => 201;

		protected override Func<IndexDescriptor<Project>, IIndexRequest<Project>> Fluent => s => s
			.WaitForActiveShards("1")
			.OpType(OpType.Index)
			.Refresh(Refresh.True)
			.Pipeline(PipelineId)
			.Routing("route");

		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool IncludeNullInExpected => false;

		protected override IndexRequest<Project> Initializer =>
			new IndexRequest<Project>(Document)
			{
				Refresh = Refresh.True,
				OpType = OpType.Index,
				WaitForActiveShards = "1",
				Routing = "route",
				Pipeline = PipelineId
			};

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath
			=> $"/project/_doc/{CallIsolatedValue}?wait_for_active_shards=1&op_type=index&refresh=true&routing=route&pipeline={PipelineId}";

		private Project Document => new Project
		{
			State = StateOfBeing.Stable,
			Name = CallIsolatedValue,
			StartedOn = FixedDate,
			LastActivity = FixedDate,
			CuratedTags = new List<Tag> { new Tag { Name = "x", Added = FixedDate } },
			SourceOnly = TestClient.Configuration.Random.SourceSerializer ? new SourceOnlyObject() : null
		};

		private static string PipelineId { get; } = "pipeline-" + Guid.NewGuid().ToString("N").Substring(0, 8);

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values) => client.Ingest.PutPipeline(
			new PutPipelineRequest(PipelineId)
			{
				Description = "Index pipeline test",
				Processors = new List<IProcessor>
				{
					new RenameProcessor
					{
						TargetField = "lastSeen",
						Field = "lastActivity"
					}
				}
			});

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Index(Document, f),
			(client, f) => client.IndexAsync(Document, f),
			(client, r) => client.Index(r),
			(client, r) => client.IndexAsync(r)
		);

		protected override IndexDescriptor<Project> NewDescriptor() => new IndexDescriptor<Project>(Document);
	}
}
