using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Debug
{
	[TestFixture]
	public class MemoryUsageTests : IntegrationSetup
	{
		[Test]
		public void DeserializeOfStreamDoesNotHoldACopyOfTheResponse()
		{
			var uri = ElasticsearchConfiguration.CreateBaseUri();
			var settings = new ConnectionSettings(uri, ElasticsearchConfiguration.DefaultIndex);
			IElasticClient client = new ElasticClient(settings);
			
			var results = client.Search<ElasticsearchProject>(s => s.MatchAll());


		}

	}
}
