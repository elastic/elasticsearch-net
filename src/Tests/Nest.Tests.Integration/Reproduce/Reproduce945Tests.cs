using System.Text;
using Elasticsearch.Net.ConnectionPool;
using FluentAssertions;
using Nest.Tests.Integration;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest.Tests.Integration.Reproduce
{
	[TestFixture]
	public class Reproduce945Tests 
	{
		[Test]
		[Ignore("Depends on setting max request size on server")]
		public void WhenPostExceedsHttpLimit_DoNotRetry_UsingConnectionPooling()
		{
			var pool = new StaticConnectionPool(new []
			{
				new Uri("http://localhost:9200"),
				new Uri("http://127.0.0.1:9200"),
			});
			var settings = new ConnectionSettings(pool);
			var client = new ElasticClient(settings);


			var index = ElasticsearchConfiguration.NewUniqueIndexName();
			var projects = NestTestData.Data;
			var people = NestTestData.People;
			var boolTerms = NestTestData.BoolTerms;
			var bulk = client.Bulk(b => b
				.FixedPath(index)
				.IndexMany(projects)
				.IndexMany(people)
				.IndexMany(boolTerms)
			);

			bulk.IsValid.Should().BeFalse();
			bulk.ConnectionStatus.NumberOfRetries.Should().Be(0);
		}
		
		[Test]
		[Ignore("Depends on setting max request size on server")]
		public void WhenPostExceedsHttpLimit_DoNotRetry_UsingPlainRetry()
		{
			var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
				.MaximumRetries(2);
			var client = new ElasticClient(settings);

			var index = ElasticsearchConfiguration.NewUniqueIndexName();
			var projects = NestTestData.Data;
			var people = NestTestData.People;
			var boolTerms = NestTestData.BoolTerms;
			var bulk = client.Bulk(b => b
				.FixedPath(index)
				.IndexMany(projects)
				.IndexMany(people)
				.IndexMany(boolTerms)
			);

			bulk.IsValid.Should().BeFalse();
			bulk.ConnectionStatus.NumberOfRetries.Should().Be(0);
		}
	}
}
