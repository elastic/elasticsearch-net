using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;
using Tests.Framework.MockData;
using FluentAssertions;
using System.Linq;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Ingest.SimulatePipeline
{
	public class SimulatePipelineApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ISimulatePipelineResponse, ISimulatePipelineRequest, SimulatePipelineDescriptor, SimulatePipelineRequest>
	{
		public SimulatePipelineApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.SimulatePipeline(f),
			fluentAsync: (client, f) => client.SimulatePipelineAsync(f),
			request: (client, r) => client.SimulatePipeline(r),
			requestAsync: (client, r) => client.SimulatePipelineAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/_ingest/pipeline/_simulate";
		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new
		{
			pipeline = new
			{
				description = "pipeline simulation",
				processors = new object []
				{
					new
					{
						set = new
						{
							field = "name",
							value = "buzz"
						}
					},
					new
					{
						append = new
						{
							field = "colors",
							value = new [] { "blue", "black" }
						}
					},
					new
					{
						uppercase = new
						{
							field = "name"
						}
					}
				}
			},
			docs = new object []
			{
				new
				{
					_index = "project",
					_type = "doc",
					_id = Project.Instance.Name,
					_source = Project.InstanceAnonymous
				},
				new
				{
					_index = "otherindex",
					_type = "othertype",
					_id = "otherid",
					_source = Project.InstanceAnonymous
				},
				new
				{
					_index = "otherindex",
					_type = "anotherType",
					_id = "2",
					_source = new { id = "2", colors = new [] { "red" } }
				}
			}
		};

		protected override void ExpectResponse(ISimulatePipelineResponse response)
		{
			response.ShouldBeValid();
			response.Documents.Should().NotBeNull().And.HaveCount(3);

			var simulation = response.Documents.FirstOrDefault(d => d.Document.Id == Project.Instance.Name);
			simulation.Should().NotBeNull();
			simulation.Document.Ingest.Should().NotBeNull();
			simulation.Document.Ingest.Timestamp.Should().NotBe(default(DateTime));
			var project = simulation.Document.Source.As<Project>();
			project.Should().NotBeNull();
			project.Name.Should().Be("BUZZ");
			project.ShouldAdhereToSourceSerializerWhenSet();

			simulation = response.Documents.FirstOrDefault(d => d.Document.Id == "otherid");
			simulation.Should().NotBeNull();
			simulation.Document.Ingest.Should().NotBeNull();
			simulation.Document.Ingest.Timestamp.Should().NotBe(default(DateTime));
			project = simulation.Document.Source.As<Project>();
			project.Name.Should().Be("BUZZ");

			simulation = response.Documents.FirstOrDefault(d => d.Document.Id == "2");
			simulation.Document.Ingest.Should().NotBeNull();
			simulation.Document.Ingest.Timestamp.Should().NotBe(default(DateTime));
			var anotherType = simulation.Document.Source.As<AnotherType>();
			anotherType.Should().NotBeNull();
			anotherType.Colors.Should().BeEquivalentTo(new string[] { "red", "blue", "black" });
		}

		public class AnotherType
		{
			public string Id { get; set; }
			public string[] Colors { get; set; }
		}

		protected override Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> Fluent => d => d
			.Pipeline(pl => pl
				.Description("pipeline simulation")
				.Processors(ps => ps
					.Set<Project>(s => s
						.Field(p => p.Name)
						.Value("buzz")
					)
					.Append<AnotherType>(a => a
						.Field(p => p.Colors)
						.Value("blue", "black")
					)
					.Uppercase<Project>(u => u
						.Field(p => p.Name)
					)
				)
			)
			.Documents(ds => ds
				.Document(doc => doc
					.Source(Project.Instance)
				)
				.Document(doc => doc
					.Source(Project.Instance)
					.Index("otherindex")
					.Type("othertype")
					.Id("otherid")
				)
				.Document(doc => doc
					.Index("otherindex")
					.Type("anotherType")
					.Source(new AnotherType { Id = "2", Colors = new[] { "red" } })
				)
			);

		protected override SimulatePipelineRequest Initializer => new SimulatePipelineRequest
		{
			Pipeline = new Pipeline
			{
				Description = "pipeline simulation",
				Processors = new IProcessor[]
				{
					new SetProcessor
					{
						Field = "name",
						Value = "buzz"
					},
					new AppendProcessor
					{
						Field = "colors",
						Value = new [] { "blue", "black"}
					},
					new UppercaseProcessor
					{
						Field = "name"
					}
				}
			},
			Documents = new[]
			{
				new SimulatePipelineDocument
				{
					Source = Project.Instance
				},
				new SimulatePipelineDocument
				{
					Source = Project.Instance,
					Index = "otherindex",
					Type = "othertype",
					Id = "otherid"
				},
				new SimulatePipelineDocument
				{
					Index = "otherindex",
					Type = "anotherType",
					Source = new AnotherType { Id = "2", Colors = new [] { "red" } }
				}
			}
		};
	}

	public class SimulatePipelineVerboseApiTests : SimulatePipelineApiTests
	{
		public SimulatePipelineVerboseApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override string UrlPath => $"/_ingest/pipeline/_simulate?verbose=true";

		protected override void ExpectResponse(ISimulatePipelineResponse response)
		{
			response.ShouldBeValid();
			response.Documents.Count.Should().Be(3);
			foreach (var doc in response.Documents)
			{
				doc.ProcessorResults.Should().NotBeNull();
				foreach (var result in doc.ProcessorResults)
				{
					result.Document.Should().NotBeNull();
					result.Document.Ingest.Should().NotBeNull();
				}
			}
		}

		protected override Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> Fluent => f => base.Fluent(f.Verbose());

		protected override SimulatePipelineRequest Initializer
		{
			get
			{
				var initializer = base.Initializer;
				initializer.Verbose = true;
				return initializer;
			}
		}
	}
}
