using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest;
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
			var client = ElasticsearchConfiguration.Client;

			var projects = NestTestData.Data;
			var people = NestTestData.People;
			var boolTerms = NestTestData.BoolTerms;

			var createIndexResult = client.CreateIndex(ElasticsearchConfiguration.DefaultIndex, c => c
				.NumberOfReplicas(0)
				.NumberOfShards(1)
				.AddMapping<ElasticsearchProject>(m => m.MapFromAttributes())
				.AddMapping<Person>(m => m.MapFromAttributes())
				.AddMapping<BoolTerm>(m => m.Properties(pp=>pp
					.String(sm => sm.Name(p => p.Name1).Index(FieldIndexOption.not_analyzed))
					.String(sm => sm.Name(p => p.Name2).Index(FieldIndexOption.not_analyzed))	
				))
			);

			var createAntotherIndexResult = client.CreateIndex(ElasticsearchConfiguration.DefaultIndex + "_clone", c => c
				.NumberOfReplicas(0)
				.NumberOfShards(1)
				.AddMapping<ElasticsearchProject>(m => m.MapFromAttributes())
				.AddMapping<Person>(m => m.MapFromAttributes())
				.AddMapping<BoolTerm>(m => m.Properties(pp => pp
					.String(sm => sm.Name(p => p.Name1).Index(FieldIndexOption.not_analyzed))
					.String(sm => sm.Name(p => p.Name2).Index(FieldIndexOption.not_analyzed))
				))
			);

			var bulkResponse = client.Bulk(b=>b
				.IndexMany(projects)
				.IndexMany(people)
				.IndexMany(boolTerms)
				.Refresh()
			);
		}

		[TearDown]
		public static void TearDown()
		{
			var client = ElasticsearchConfiguration.Client;
			client.DeleteIndex(di=>di.Indices(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex + "_*"));
		}
	}
}
