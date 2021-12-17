// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.IO;
using Elastic.Clients.Elasticsearch.Ingest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using Tests.Serialization;

namespace Tests.Document.Multiple;

public class BulkApiIndexSerializationTests : SerializerTestBase
{
	// TODO: THESE COULD BE IMPROVED BY PROVIDING A BASE CLASS THAT HANDLES PROPER SERIALISATION CHECKS
	// String verification is brittle if the serialisation approach changes internally and potentially order is different
	// Future improvement is to refactor these to combine with the integration tests as per a "normal" serialisation request
	// We can add methods to the ApiIntegrationTestBase to achieve that cleanly and conditionally verify the collection for ndjson requests

	[U]
	public void IndexFluent()
	{
		var expected = new List<string>()
		{
			{ $@"{{""index"":{{""_id"":""{Project.Instance.Name}"",""routing"":""{Project.Instance.Name}"",""pipeline"":""pipeline""}}}}" }
		};

		var request = new BulkRequestDescriptor().Index(Project.Instance, b => b.Pipeline("pipeline"));

		var stream = new MemoryStream();
		_requestResponseSerializer.Serialize(request, stream);
		stream.Position = 0;

		var reader = new StreamReader(stream);

		foreach (var expectedItem in expected)
		{
			var item = reader.ReadLine();
			item.Should().NotBeNull();
			item.Should().Be(expectedItem);
		}
	}
}

public class BulkApiTests : ApiIntegrationTestBase<WritableCluster, BulkResponse, BulkRequestDescriptor, BulkRequest>
{
	public BulkApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override bool ExpectIsValid => true;

	// We have specific tests for the serialisation behaviour.
	// The roundtrip flow does not work through STJ.
	protected override object ExpectJson => null;

	protected override int ExpectStatusCode => 200;

	protected override Action<BulkRequestDescriptor> Fluent => d => d
		.Index(CallIsolatedValue)
		.Pipeline("default-pipeline")
		.Index(Project.Instance, b => b.Pipeline("pipeline"))
		.Update<Project, object>(b => b.Doc(new { leadDeveloper = new { firstName = "martijn" } }).Id(Project.Instance.Name))
		.Create(Project.Instance, b => b.Id(Project.Instance.Name + "1"))
		.Delete(b => b.Id(Project.Instance.Name + "1").Routing(Project.Instance.Name))
		.Create(Project.Instance, b => b.Id(Project.Instance.Name + "2"))
		.Update<Project>(b => b
			.Id(Project.Instance.Name + "2")
			.Routing(Project.Instance.Name)
			.Script(s => s
				.Source("ctx._source.numberOfCommits = params.commits")
				.Params(p => p.Add("commits", 30))
				.Language("painless")
			)
		);

	protected override HttpMethod HttpMethod => HttpMethod.POST;

	protected override BulkRequest Initializer =>
		new(CallIsolatedValue)
		{
			Pipeline = "default-pipeline",
			Operations = new List<IBulkOperation>
			{
					new BulkIndexOperation<Project>(Project.Instance) { Pipeline = "pipeline" },
					new BulkUpdateOperation<Project, object>(Project.Instance.Name)
					{
						Doc = new { leadDeveloper = new { firstName = "martijn" } }
					},
					new BulkCreateOperation<Project>(Project.Instance)
					{
						Id = Project.Instance.Name + "1",
					},
					new BulkDeleteOperation<Project>(Project.Instance.Name + "1")
					{
						Routing = Project.Instance.Name
					},
					new BulkCreateOperation<Project>(Project.Instance)
					{
						Id = Project.Instance.Name + "2",
					},
					new BulkUpdateOperation<Project, object>(Project.Instance.Name + "2")
					{
						Routing = Project.Instance.Name,
						Script = new InlineScript("ctx._source.numberOfCommits = params.commits")
						{
							Params = new Dictionary<string, object> { { "commits", 30 } },
							Language = "painless"
						}
					}
			}
		};

	protected override bool SupportsDeserialization => false;

	protected override string ExpectedUrlPathAndQuery => $"/{CallIsolatedValue}/_bulk?pipeline=default-pipeline";

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.Bulk(f),
		(client, f) => client.BulkAsync(f),
		(client, r) => client.Bulk(r),
		(client, r) => client.BulkAsync(r)
	);

	protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
	{
		// TODO - REPLACE WITH FLUENT

		var pipelineResponse = client.Ingest.PutPipeline(new IngestPutPipelineRequest("default-pipeline")
		{
			Processors = new ProcessorContainer[]
			{
				new ProcessorContainer(new SetProcessor { Field = "descriptor", Value = "Default" } )
			}
		});

		pipelineResponse.ShouldBeValid("Failed to set up pipeline named 'default-pipeline' required for bulk {p");

		pipelineResponse = client.Ingest.PutPipeline(new IngestPutPipelineRequest("pipeline")
		{
			Processors = new ProcessorContainer[]
			{
				new ProcessorContainer(new SetProcessor { Field = "descriptor", Value = "Overridden" } )
			}
		});

		pipelineResponse.ShouldBeValid($"Failed to set up pipeline named 'pipeline' required for bulk");

		base.IntegrationSetup(client, values);
	}

	protected override void ExpectResponse(BulkResponse response)
	{
		response.Took.Should().BeGreaterThan(0);
		response.Errors.Should().BeFalse();
		response.ItemsWithErrors.Should().NotBeNull().And.BeEmpty();
		response.Items.Should().NotBeEmpty();
		foreach (var item in response.Items)
		{
			item.Index.Should().Be(CallIsolatedValue);
			item.Status.Should().BeGreaterThan(100);
			item.Version.Should().BeGreaterThan(0);
			item.Id.Should().NotBeNullOrWhiteSpace();
			item.IsValid.Should().BeTrue();
			item.Shards.Should().NotBeNull();
			item.Shards.Total.Should().BeGreaterThan(0);
			item.Shards.Successful.Should().BeGreaterThan(0);
			item.SeqNo.Should().BeGreaterOrEqualTo(0);
			item.PrimaryTerm.Should().BeGreaterThan(0);
			item.Result.Should().NotBeNullOrEmpty();
		}

		// TODO - Re-enable once Source endpoint is generated and implemented

		var project1 = Client.GetSource<Project>(CallIsolatedValue, Project.Instance.Name); //.Body;
																							//project1.LeadDeveloper.FirstName.Should().Be("martijn");
																							//project1.Description.Should().Be("Overridden");

		//var project2 = Client.Source<Project>(Project.Instance.Name + "2", p => p
		//	.Index(CallIsolatedValue)
		//	.Routing(Project.Instance.Name)
		//).Body;
		//project2.Description.Should().Be("Default");
		//project2.NumberOfCommits.Should().Be(30);
	}
}
