using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest;
using Nest.Tests.Integration;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration
{
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

	public static class IntegrationSetup
	{
		public static void IndexDemoData(IElasticClient client, string index = null)
		{
			index = index ?? ElasticsearchConfiguration.DefaultIndex;
			var projects = NestTestData.Data;
			var people = NestTestData.People;
			var boolTerms = NestTestData.BoolTerms;
			var parents = NestTestData.Parents;
			var children = NestTestData.Children;

			var bulkResponse = client.Bulk(b => {
				b.FixedPath(index);

				b.IndexMany(projects);
				b.IndexMany(people);
				b.IndexMany(boolTerms);

				var rand = new Random();
				foreach (var parent in parents)
					b.Index<Parent>(i => i.Document(parent));
				foreach (var child in children)
					b.Index<Child>(i => i
						.Document(child)
						.Parent(parents[rand.Next(parents.Count)].Id)
					);
				
				b.Refresh();
				
				return b;
			});
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
				.AddAlias(indexName + "-aliased")
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
				.AddMapping<Person>(m => m
					.MapFromAttributes()
				)
				.AddMapping<BoolTerm>(m => m
					.Properties(pp => pp
						.String(sm => sm.Name(p => p.Name1).Index(FieldIndexOption.NotAnalyzed))
						.String(sm => sm.Name(p => p.Name2).Index(FieldIndexOption.NotAnalyzed))
					)
				)
				.AddMapping<Parent>(m => m
					.MapFromAttributes()
				)
				.AddMapping<Child>(m => m
					.SetParent<Parent>()
					.MapFromAttributes()
				)
			);
			createIndexResult.IsValid.Should().BeTrue();
		}


	}
}
