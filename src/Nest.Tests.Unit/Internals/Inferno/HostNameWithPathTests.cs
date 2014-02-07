using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using Nest.Resolvers;
using System;
using FluentAssertions;

namespace Nest.Tests.Unit.Internals.Inferno
{
	[TestFixture]
	public class HostNameWithPathTests
	{
		private static Uri HostName = new Uri(Test.Default.Uri, "/my-api-token");
		private static ConnectionSettings Settings = new ConnectionSettings(HostName,Test.Default.DefaultIndex);

		private static ElasticClient Client = new ElasticClient(Settings, new InMemoryConnection(Settings));

		[Test]
		public void BasePathMakesItIntoReuqest()
		{
			var result = Client.Get<ElasticsearchProject>(g=>g.Id(1));
			var url = result.ConnectionStatus.RequestUrl;
			url.Should().Be("http://localhost:9200/my-api-token/nest_test_data/elasticsearchprojects/1");
		}
	}
}
