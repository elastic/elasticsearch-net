using FluentAssertions;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Ingest
{
	public class PipelineCrudTests
		: CrudTestBase<IntrusiveOperationCluster, IPutPipelineResponse, IGetPipelineResponse, IPutPipelineResponse, IDeletePipelineResponse>
	{
		//These calls have low priority and often cause `process_cluster_event_timeout_exception`'s
		public PipelineCrudTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses Create() => Calls<PutPipelineDescriptor, PutPipelineRequest, IPutPipelineRequest, IPutPipelineResponse>(
			CreateInitializer,
			CreateFluent,
			fluent: (s, c, f) => c.PutPipeline(s, f),
			fluentAsync: (s, c, f) => c.PutPipelineAsync(s, f),
			request: (s, c, r) => c.PutPipeline(r),
			requestAsync: (s, c, r) => c.PutPipelineAsync(r)
		);

		protected override void ExpectAfterCreate(IGetPipelineResponse response)
		{
			response.Pipelines.Should().NotBeNull().And.HaveCount(1);

			var kv = response.Pipelines.First();
			kv.Should().NotBeNull();
			kv.Key.Should().NotBeNullOrWhiteSpace();

			var pipeline = kv.Value;
			pipeline.Description.Should().NotBeNull();

			var processors = pipeline.Processors;
			processors.Should().NotBeNull().And.HaveCount(2);

			var uppercase = processors.FirstOrDefault(p => p.Name == "uppercase") as UppercaseProcessor;
			uppercase.Should().NotBeNull();
			uppercase.Field.Should().NotBeNull();

			var set = processors.FirstOrDefault(p => p.Name == "set") as SetProcessor;
			set.Should().NotBeNull();
			set.Field.Should().NotBeNull();
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

		protected override LazyResponses Read() => Calls<GetPipelineDescriptor, GetPipelineRequest, IGetPipelineRequest, IGetPipelineResponse>(
			id => new GetPipelineRequest(id),
			(id, d) => d.Id(id),
			fluent: (s, c, f) => c.GetPipeline(f),
			fluentAsync: (s, c, f) => c.GetPipelineAsync(f),
			request: (s, c, r) => c.GetPipeline(r),
			requestAsync: (s, c, r) => c.GetPipelineAsync(r)
		);

		protected override LazyResponses Update() => Calls<PutPipelineDescriptor, PutPipelineRequest, IPutPipelineRequest, IPutPipelineResponse>(
			UpdateInitializer,
			UpdateFluent,
			fluent: (s, c, f) => c.PutPipeline(s, f),
			fluentAsync: (s, c, f) => c.PutPipelineAsync(s, f),
			request: (s, c, r) => c.PutPipeline(r),
			requestAsync: (s, c, r) => c.PutPipelineAsync(r)
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

		protected override void ExpectAfterUpdate(IGetPipelineResponse response)
		{
			response.Pipelines.Should().NotBeNull().And.HaveCount(1);

			var kv = response.Pipelines.First();
			kv.Should().NotBeNull();
			kv.Key.Should().NotBeNullOrWhiteSpace();

			var pipeline = kv.Value;
			pipeline.Should().NotBeNull();

			var processors = pipeline.Processors;
			processors.Should().NotBeNull().And.HaveCount(3);

			var uppercase = processors.FirstOrDefault(p => p.Name == "uppercase") as UppercaseProcessor;
			uppercase.Should().NotBeNull();
			uppercase.Field.Should().NotBeNull();

			var set = processors.FirstOrDefault(p => p.Name == "set") as SetProcessor;
			set.Should().NotBeNull();
			set.Field.Should().NotBeNull();
			set.Value.Should().NotBeNull();

			var rename = processors.FirstOrDefault(p => p.Name == "rename") as RenameProcessor;
			rename.Should().NotBeNull();
			rename.Field.Should().NotBeNull();
			rename.TargetField.Should().NotBeNull();
		}

		protected override LazyResponses Delete() => Calls<DeletePipelineDescriptor, DeletePipelineRequest, IDeletePipelineRequest, IDeletePipelineResponse>(
			id => new DeletePipelineRequest(id),
			(id, d) => d,
			fluent: (s, c, f) => c.DeletePipeline(s, f),
			fluentAsync: (s, c, f) => c.DeletePipelineAsync(s, f),
			request: (s, c, r) => c.DeletePipeline(r),
			requestAsync: (s, c, r) => c.DeletePipelineAsync(r)
		);

	}
}
