using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using FluentAssertions;
using Nest;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Source
{
	[TestFixture]
	public class SourceRequestTests : BaseJsonTests
	{
		private IElasticsearchResponse _status;

		public SourceRequestTests()
		{
			
			var settings = new ConnectionSettings(UnitTestDefaults.Uri, UnitTestDefaults.DefaultIndex)
				.SetConnectionStatusHandler(r=> this._status = r)
				.ExposeRawResponse();
			var connection = new InMemoryConnection(settings);
			var client = new ElasticClient(settings, connection);

			var request = new SourceRequest<ElasticsearchProject>(2)
			{
				Preference = "local",
				Routing = "2",
			};
			var response = client.Source<ElasticsearchProject>(request);
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/nest_test_data/elasticsearchprojects/2/_source?preference=local&routing=2");
			this._status.RequestMethod.Should().Be("GET");
		}
	}

}
