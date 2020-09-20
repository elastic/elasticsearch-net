// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Ingest.SimulatePipeline
{
	public class SimulatePipelineApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, SimulatePipelineResponse, ISimulatePipelineRequest, SimulatePipelineDescriptor,
			SimulatePipelineRequest>
	{
		public SimulatePipelineApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new
		{
			pipeline = new
			{
				description = "pipeline simulation",
				processors = new object[]
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
							value = new[] { "blue", "black" }
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
			docs = new object[]
			{
				new
				{
					_index = "project",
					_id = Project.Instance.Name,
					_source = Project.InstanceAnonymous
				},
				new
				{
					_index = "otherindex",
					_id = "otherid",
					_source = Project.InstanceAnonymous
				},
				new
				{
					_index = "otherindex",
					_id = "2",
					_source = new { id = "2", colors = new[] { "red" } }
				}
			}
		};

		protected override int ExpectStatusCode => 200;

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
					.Id("otherid")
				)
				.Document(doc => doc
					.Index("otherindex")
					.Source(new AnotherType { Id = "2", Colors = new[] { "red" } })
				)
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

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
						Value = new[] { "blue", "black" }
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
					Id = "otherid"
				},
				new SimulatePipelineDocument
				{
					Index = "otherindex",
					Source = new AnotherType { Id = "2", Colors = new[] { "red" } }
				}
			}
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_ingest/pipeline/_simulate";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Ingest.SimulatePipeline(f),
			(client, f) => client.Ingest.SimulatePipelineAsync(f),
			(client, r) => client.Ingest.SimulatePipeline(r),
			(client, r) => client.Ingest.SimulatePipelineAsync(r)
		);

		protected override void ExpectResponse(SimulatePipelineResponse response)
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
			public string[] Colors { get; set; }
			public string Id { get; set; }
		}
	}

	public class SimulatePipelineVerboseApiTests : SimulatePipelineApiTests
	{
		public SimulatePipelineVerboseApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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

		protected override string UrlPath => $"/_ingest/pipeline/_simulate?verbose=true";

		protected override void ExpectResponse(SimulatePipelineResponse response)
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
	}
}
