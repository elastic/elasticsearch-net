using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest;
using Nest.Tests.Integration;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

[SetUpFixture]
public class SetupAndTeardownForIntegrationTests
{
	[SetUp]
	public void Setup()
	{
		var client = new ElasticClient(
			//ElasticsearchConfiguration.Settings(hostOverride: new Uri("http://localhost:9200"))
			ElasticsearchConfiguration.Settings()
		);

		try
		{
			IntegrationSetup.CreateTestIndex(client, ElasticsearchConfiguration.DefaultIndex);
			IntegrationSetup.CreateTestIndex(client, ElasticsearchConfiguration.DefaultIndex + "_clone");

			IntegrationSetup.IndexDemoData(client);
		}
		catch (Exception)
		{

			throw;
		}

	}
	[TearDown]
	public void TearDown()
	{
		var client = ElasticsearchConfiguration.Client.Value;
		client.DeleteIndex(di => di.Indices(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex + "*"));
	}
}

namespace Nest.Tests.Integration
{
	public static class IntegrationSetup
	{
		public static void IndexDemoData(IElasticClient client, string index = null)
		{
			index = index ?? ElasticsearchConfiguration.DefaultIndex;
			var projects = NestTestData.Data;
			var people = NestTestData.People;
			var boolTerms = NestTestData.BoolTerms;
			var bulkResponse = client.Bulk(b => b
				.FixedPath(index)
				.IndexMany(projects)
				.IndexMany(people)
				.IndexMany(boolTerms)
				.Refresh()
			);
		}

		public static string CreateNewIndexWithData(IElasticClient client)
		{
			var newIndex = ElasticsearchConfiguration.NewUniqueIndexName();
			CreateTestIndex(client, newIndex);
			IndexDemoData(client, newIndex);
			return newIndex;
		}


		public static void CreateTestIndex(IElasticClient client, string indexName)
		{
			var createIndexResult = client.CreateIndex(indexName, c => c
				.NumberOfReplicas(ElasticsearchConfiguration.NumberOfReplicas)
				.NumberOfShards(ElasticsearchConfiguration.NumberOfShards)
				.AddMapping<ElasticsearchProject>(m => m
					.MapFromAttributes()
					.Properties(props => props
						.String(s => s
							.Name(p => p.Name)
							.FieldData(fd => fd.Loading(FieldDataLoading.Eager))
							.Fields(fields => fields
								.String(ss => ss
									.Name("sort")
									.Index(FieldIndexOption.NotAnalyzed)
								)
							)
						)
						.String(s => s
							.Name(ep => ep.Content)
							.TermVector(TermVectorOption.WithPositionsOffsetsPayloads)
						)
					)
				)
				.AddAlias(indexName + "-aliased")
				.AddMapping<Person>(m => m.MapFromAttributes())
				.AddMapping<BoolTerm>(m => m.Properties(pp => pp
					.String(sm => sm.Name(p => p.Name1).Index(FieldIndexOption.NotAnalyzed))
					.String(sm => sm.Name(p => p.Name2).Index(FieldIndexOption.NotAnalyzed))
					))
			);
			createIndexResult.IsValid.Should().BeTrue();
		}


	}
}
