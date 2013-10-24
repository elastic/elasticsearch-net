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

			client.CreateIndex(ElasticsearchConfiguration.DefaultIndex, c => c
				.NumberOfReplicas(0)
				.NumberOfShards(1)
				.AddMapping<ElasticSearchProject>(m => m.MapFromAttributes())
				.AddMapping<Person>(m => m.MapFromAttributes())
			);
			client.CreateIndex(ElasticsearchConfiguration.DefaultIndex + "_clone", c => c
				.NumberOfReplicas(0)
				.NumberOfShards(1)
				.AddMapping<ElasticSearchProject>(m => m.MapFromAttributes())
				.AddMapping<Person>(m => m.MapFromAttributes())
			);

			var bulk = new BulkDescriptor();
			foreach (var p in projects)
				bulk.Index<ElasticSearchProject>(i=>i.Object(p));
			foreach (var p in people)
				bulk.Index<Person>(i => i.Object(p));
			client.Bulk(bulk);

			client.Refresh(new[] {ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex + "_clone"});

		}

		[TearDown]
		public static void TearDown()
		{
			var client = ElasticsearchConfiguration.Client;
			client.DeleteIndex(ElasticsearchConfiguration.DefaultIndex + "," + ElasticsearchConfiguration.DefaultIndex + "_*");
		}
	}
}
