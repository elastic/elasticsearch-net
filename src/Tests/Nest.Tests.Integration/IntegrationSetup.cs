using System;
using System.Collections.Generic;
using System.Linq;
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
			var client = new ElasticClient(ElasticsearchConfiguration.Settings(hostOverride: new Uri("http://localhost:9200")));

			//uncomment the next line if you want to see the setup in fiddler too
			//var client = ElasticsearchConfiguration.Client;

			var projects = NestTestData.Data;
			var people = NestTestData.People;
			var boolTerms = NestTestData.BoolTerms;

			try
			{
				var createIndexResult = client.CreateIndex(ElasticsearchConfiguration.DefaultIndex, c => c
								.NumberOfReplicas(0)
								.NumberOfShards(1)
								.AddMapping<ElasticsearchProject>(m => m
								.MapFromAttributes()
								.Properties(p => p
								.String(s => s.Name(ep => ep.Content).TermVector(TermVectorOption.WithPositionsOffsetsPayloads))))
								.AddMapping<Person>(m => m.MapFromAttributes())
								.AddMapping<BoolTerm>(m => m.Properties(pp => pp
									.String(sm => sm.Name(p => p.Name1).Index(FieldIndexOption.NotAnalyzed))
									.String(sm => sm.Name(p => p.Name2).Index(FieldIndexOption.NotAnalyzed))
								))
							);

				var createAntotherIndexResult = client.CreateIndex(ElasticsearchConfiguration.DefaultIndex + "_clone", c => c
					.NumberOfReplicas(0)
					.NumberOfShards(1)
					.AddMapping<ElasticsearchProject>(m => m
					.MapFromAttributes()
					.Properties(p => p
					.String(s => s.Name(ep => ep.Content).TermVector(TermVectorOption.WithPositionsOffsetsPayloads))))
					.AddMapping<Person>(m => m.MapFromAttributes())
					.AddMapping<BoolTerm>(m => m.Properties(pp => pp
						.String(sm => sm.Name(p => p.Name1).Index(FieldIndexOption.NotAnalyzed))
						.String(sm => sm.Name(p => p.Name2).Index(FieldIndexOption.NotAnalyzed))
					))
				);

				var bulkResponse = client.Bulk(b => b
					.IndexMany(projects)
					.IndexMany(people)
					.IndexMany(boolTerms)
					.Refresh()
				);
			}
			catch (Exception e)
			{

				throw;
			}

		}

		[TearDown]
		public static void TearDown()
		{
			var client = ElasticsearchConfiguration.Client.Value;
			client.DeleteIndex(di => di.Indices(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndexPrefix + "*"));
		}
	}
}
