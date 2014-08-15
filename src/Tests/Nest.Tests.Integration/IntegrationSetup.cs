using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration
{
	[SetUpFixture]
	public class IntegrationSetup
	{
		[SetUp]
		public static void Setup()
		{
			var client = new ElasticClient(
				//ElasticsearchConfiguration.Settings(hostOverride: new Uri("http://localhost:9200"))
				ElasticsearchConfiguration.Settings()
			);

			//uncomment the next line if you want to see the setup in fiddler too
			//var client = ElasticsearchConfiguration.Client;

			var projects = NestTestData.Data;
			var people = NestTestData.People;
			var boolTerms = NestTestData.BoolTerms;

			try
			{
				CreateTestIndex(client, ElasticsearchConfiguration.DefaultIndex);
				CreateTestIndex(client, ElasticsearchConfiguration.DefaultIndex + "_clone");

				var bulkResponse = client.Bulk(b => b
					.IndexMany(projects)
					.IndexMany(people)
					.IndexMany(boolTerms)
					.Refresh()
				);
			}
			catch (Exception)
			{

				throw;
			}

		}

		public static void CreateTestIndex(IElasticClient client, string indexName)
		{
			var createIndexResult = client.CreateIndex(indexName, c => c
				.NumberOfReplicas(0)
				.NumberOfShards(1)
				.AddMapping<ElasticsearchProject>(m => m
					.MapFromAttributes()
					.Properties(p => p
						.String(s => s.Name(ep => ep.Content).TermVector(TermVectorOption.WithPositionsOffsetsPayloads))
					)
				)
				.AddMapping<Person>(m => m.MapFromAttributes())
				.AddMapping<BoolTerm>(m => m.Properties(pp => pp
					.String(sm => sm.Name(p => p.Name1).Index(FieldIndexOption.NotAnalyzed))
					.String(sm => sm.Name(p => p.Name2).Index(FieldIndexOption.NotAnalyzed))
					))
			);
			createIndexResult.IsValid.Should().BeTrue();
		}

		[TearDown]
		public static void TearDown()
		{
			var client = ElasticsearchConfiguration.Client.Value;
			client.DeleteIndex(di => di.Indices(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex + "*"));
		}
	}
}
