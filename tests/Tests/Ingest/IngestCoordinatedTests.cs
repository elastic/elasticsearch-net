// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Linq;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Ingest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Ingest;

public class IngestCoordinatedTests : CoordinatedIntegrationTestBase<ReadOnlyCluster>
{
	private const string CreatePipelineStep = nameof(CreatePipelineStep);
	private const string GetPipelineStep = nameof(GetPipelineStep);
	private const string UpdatePipelineStep = nameof(UpdatePipelineStep);
	private const string DeletePipelineStep = nameof(DeletePipelineStep);

	// These calls have low priority and may cause a process_cluster_event_timeout_exception.
	// In v7 we used the intrusive operation cluster and we may need to switch back if the above
	// is still an issue in CI builds.
	public IngestCoordinatedTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(
		new CoordinatedUsage(cluster, usage)
		{
			{
				CreatePipelineStep, u =>
					u.Calls<PutPipelineRequestDescriptor<Project>, PutPipelineRequest, PutPipelineResponse>(
						v => new PutPipelineRequest($"test-pipeline-{v}")
						{
							Description = "Project Pipeline",
							Processors = new Processor[]
							{
								new UppercaseProcessor
								{
									Field = Infer.Field<Project>(p => p.State)
								},
								new SetProcessor
								{
									Field = Infer.Field<Project>(p => p.NumberOfCommits),
									Value = 0
								}
							},
							Version = 1
						},
						(v, d) => d
							.Description("Project Pipeline")
							.Processors(
								p => p.Uppercase(u => u.Field(f => f.State)),
								p => p.Set(u => u.Field(f => f.NumberOfCommits).Value(0)))
							.Version(1),
						(v, c, f) => c.Ingest.PutPipeline($"test-pipeline-{v}", f),
						(v, c, f) => c.Ingest.PutPipelineAsync($"test-pipeline-{v}", f),
						(_, c, r) => c.Ingest.PutPipeline(r),
						(_, c, r) => c.Ingest.PutPipelineAsync(r)
					)
			},
			{
				GetPipelineStep, u =>
					u.Calls<GetPipelineRequestDescriptor<Project>, GetPipelineRequest, GetPipelineResponse>(
						v => new GetPipelineRequest($"test-pipeline-{v}"),
						(v, d) => d.Id($"test-pipeline-{v}"),
						(v, c, f) => c.Ingest.GetPipeline(f),
						(v, c, f) => c.Ingest.GetPipelineAsync(f),
						(_, c, r) => c.Ingest.GetPipeline(r),
						(_, c, r) => c.Ingest.GetPipelineAsync(r)
					)
			},
			{
				UpdatePipelineStep, u =>
					u.Calls<PutPipelineRequestDescriptor<Project>, PutPipelineRequest, PutPipelineResponse>(
						v => new PutPipelineRequest($"test-pipeline-{v}")
						{
							Description = "Project Pipeline (updated)",
							Processors = new Processor[]
							{
								new UppercaseProcessor
								{
									Field = Infer.Field<Project>(p => p.State)
								},
								new SetProcessor
								{
									Field = Infer.Field<Project>(p => p.NumberOfCommits),
									Value = 0
								},
								new RenameProcessor
								{
									Field = Infer.Field<Project>(p => p.LeadDeveloper),
									TargetField = "techLead"
								}
							}
						},
						(v, d) => d
							.Description("Project Pipeline (updated)")
							.Processors(
								p => p.Uppercase(u => u.Field(f => f.State)),
								p => p.Set(u => u.Field(f => f.NumberOfCommits).Value(0)),
								p => p.Rename(u => u.Field(f => f.LeadDeveloper).TargetField("techLead"))),
						(v, c, f) => c.Ingest.PutPipeline($"test-pipeline-{v}", f),
						(v, c, f) => c.Ingest.PutPipelineAsync($"test-pipeline-{v}", f),
						(_, c, r) => c.Ingest.PutPipeline(r),
						(_, c, r) => c.Ingest.PutPipelineAsync(r)
					)
			},
			{
				DeletePipelineStep, u =>
					u.Calls<DeletePipelineRequestDescriptor, DeletePipelineRequest, DeletePipelineResponse>(
						v => new DeletePipelineRequest($"test-pipeline-{v}"),
						(v, d) => d,
						(v, c, f) => c.Ingest.DeletePipeline($"test-pipeline-{v}", f),
						(v, c, f) => c.Ingest.DeletePipelineAsync($"test-pipeline-{v}", f),
						(_, c, r) => c.Ingest.DeletePipeline(r),
						(_, c, r) => c.Ingest.DeletePipelineAsync(r)
					)
			},
		})
	{
	}

	[I]
	public async Task CreateResponse() => await Assert<PutPipelineResponse>(CreatePipelineStep, (v, r) =>
	{
		r.IsValidResponse.Should().BeTrue();
		r.Acknowledged.Should().BeTrue();
	});

	[I]
	public async Task GetResponse() => await Assert<GetPipelineResponse>(GetPipelineStep, (v, r) =>
	{
		// TODO: The code to access the variant in this test should be updated
		// after https://github.com/elastic/elasticsearch-net/issues/7400 is merged.

		r.IsValidResponse.Should().BeTrue();

		r.Pipelines.Should().NotBeNull().And.HaveCount(1);

		var kv = r.Pipelines.First();
		kv.Should().NotBeNull();
		kv.Key.Should().NotBeNullOrWhiteSpace();

		var pipeline = kv.Value;
		pipeline.Description.Should().Be("Project Pipeline");
		pipeline.Version.Should().Be(1);

		var processors = pipeline.Processors;
		processors.Should().NotBeNull().And.HaveCount(2);

		UppercaseProcessor uppercase = null;
		processors.FirstOrDefault(p => p.TryGet(out uppercase));
		uppercase.Should().NotBeNull();
		uppercase.Field.Should().Be("state");

		SetProcessor set = null;
		processors.FirstOrDefault(p => p.TryGet(out set));
		set.Should().NotBeNull();
		set.Field.Should().Be("numberOfCommits");
		set.Value.Should().Be(0);
	});

	[I]
	public async Task UpdateResponse() => await Assert<PutPipelineResponse>(UpdatePipelineStep, (v, r) =>
	{
		r.IsValidResponse.Should().BeTrue();
		r.Acknowledged.Should().BeTrue();
	});

	[I]
	public async Task DeleteResponse() => await Assert<DeletePipelineResponse>(DeletePipelineStep, (v, r) =>
	{
		r.IsValidResponse.Should().BeTrue();
		r.Acknowledged.Should().BeTrue();
	});
}
