// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Ingest
{
	public class PipelineCrudTests
		: CrudTestBase<IntrusiveOperationCluster, PutPipelineResponse, GetPipelineResponse, PutPipelineResponse, DeletePipelineResponse>
	{
		//These calls have low priority and often cause `process_cluster_event_timeout_exception`'s
		public PipelineCrudTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses Create() => Calls<PutPipelineDescriptor, PutPipelineRequest, IPutPipelineRequest, PutPipelineResponse>(
			CreateInitializer,
			CreateFluent,
			(s, c, f) => c.Ingest.PutPipeline(s, f),
			(s, c, f) => c.Ingest.PutPipelineAsync(s, f),
			(s, c, r) => c.Ingest.PutPipeline(r),
			(s, c, r) => c.Ingest.PutPipelineAsync(r)
		);

		protected override void ExpectAfterCreate(GetPipelineResponse response)
		{
			response.Pipelines.Should().NotBeNull().And.HaveCount(1);

			var kv = response.Pipelines.First();
			kv.Should().NotBeNull();
			kv.Key.Should().NotBeNullOrWhiteSpace();

			var pipeline = kv.Value;
			pipeline.Description.Should().NotBeNull();

			var processors = pipeline.Processors.ToArray();
			processors.Should().NotBeNull().And.HaveCount(2);

			var uppercase = processors.FirstOrDefault(p => p.Name == "uppercase") as UppercaseProcessor;
			uppercase.Should().NotBeNull();
			uppercase!.Field.Should().NotBeNull();

			var set = processors.FirstOrDefault(p => p.Name == "set") as SetProcessor;
			set.Should().NotBeNull();
			set!.Field.Should().NotBeNull();
			set.Value.Should().NotBeNull();
		}

		private PutPipelineRequest CreateInitializer(string pipelineId) => new PutPipelineRequest(pipelineId)
		{
			Description = "Project Pipeline",
			Processors = new IProcessor[]
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
			}
		};

		private IPutPipelineRequest CreateFluent(string pipelineId, PutPipelineDescriptor d) => d
			.Description("Project Pipeline")
			.Processors(ps => ps
				.Uppercase<Project>(u => u
					.Field(p => p.State)
				)
				.Set<Project>(s => s
					.Field(p => p.NumberOfCommits)
					.Value(0)
				)
			);

		protected override LazyResponses Read() => Calls<GetPipelineDescriptor, GetPipelineRequest, IGetPipelineRequest, GetPipelineResponse>(
			id => new GetPipelineRequest(id),
			(id, d) => d.Id(id),
			(s, c, f) => c.Ingest.GetPipeline(f),
			(s, c, f) => c.Ingest.GetPipelineAsync(f),
			(s, c, r) => c.Ingest.GetPipeline(r),
			(s, c, r) => c.Ingest.GetPipelineAsync(r)
		);

		protected override LazyResponses Update() => Calls<PutPipelineDescriptor, PutPipelineRequest, IPutPipelineRequest, PutPipelineResponse>(
			UpdateInitializer,
			UpdateFluent,
			(s, c, f) => c.Ingest.PutPipeline(s, f),
			(s, c, f) => c.Ingest.PutPipelineAsync(s, f),
			(s, c, r) => c.Ingest.PutPipeline(r),
			(s, c, r) => c.Ingest.PutPipelineAsync(r)
		);

		private PutPipelineRequest UpdateInitializer(string pipelineId) => new PutPipelineRequest(pipelineId)
		{
			Description = "Project Pipeline (updated)",
			Processors = new IProcessor[]
			{
				new UppercaseProcessor
				{
					Field = Infer.Field<Project>(p => p.State)
				},
				new SetProcessor
				{
					Field = Infer.Field<Project>(p => p.NumberOfCommits),
					Value = 500
				},
				new RenameProcessor
				{
					Field = Infer.Field<Project>(p => p.LeadDeveloper),
					TargetField = "techLead"
				}
			}
		};

		private IPutPipelineRequest UpdateFluent(string pipelineId, PutPipelineDescriptor d) => d
			.Description("Project Pipeline (updated)")
			.Processors(ps => ps
				.Uppercase<Project>(u => u
					.Field(p => p.State)
				)
				.Set<Project>(s => s
					.Field(p => p.NumberOfCommits)
					.Value(500)
				)
				.Rename<Project>(s => s
					.Field(p => p.LeadDeveloper)
					.TargetField("techLead")
				)
			);

		protected override void ExpectAfterUpdate(GetPipelineResponse response)
		{
			response.Pipelines.Should().NotBeNull().And.HaveCount(1);

			var kv = response.Pipelines.First();
			kv.Should().NotBeNull();
			kv.Key.Should().NotBeNullOrWhiteSpace();

			var pipeline = kv.Value;
			pipeline.Should().NotBeNull();

			var processors = pipeline.Processors.ToArray();
			processors.Should().NotBeNull().And.HaveCount(3);

			var uppercase = processors.FirstOrDefault(p => p.Name == "uppercase") as UppercaseProcessor;
			uppercase!.Should().NotBeNull();
			uppercase.Field.Should().NotBeNull();

			var set = processors.FirstOrDefault(p => p.Name == "set") as SetProcessor;
			set!.Should().NotBeNull();
			set.Field.Should().NotBeNull();
			set.Value.Should().NotBeNull();

			var rename = processors.FirstOrDefault(p => p.Name == "rename") as RenameProcessor;
			rename!.Should().NotBeNull();
			rename.Field.Should().NotBeNull();
			rename.TargetField.Should().NotBeNull();
		}

		protected override LazyResponses Delete() =>
			Calls<DeletePipelineDescriptor, DeletePipelineRequest, IDeletePipelineRequest, DeletePipelineResponse>(
				id => new DeletePipelineRequest(id),
				(id, d) => d,
				(s, c, f) => c.Ingest.DeletePipeline(s, f),
				(s, c, f) => c.Ingest.DeletePipelineAsync(s, f),
				(s, c, r) => c.Ingest.DeletePipeline(r),
				(s, c, r) => c.Ingest.DeletePipelineAsync(r)
			);
	}
}
