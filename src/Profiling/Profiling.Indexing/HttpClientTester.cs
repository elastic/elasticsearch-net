using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection.HttpClient;
using Nest;

namespace Profiling.Indexing
{
	public class HttpClientTester : Tester
	{
		public override IElasticClient CreateClient(string indexName)
		{
			var settings = this.CreateSettings(indexName, 9200);
			var client = new ElasticClient(settings, new ElasticsearchHttpClient(settings));
			return client;
		}
		

	}
}
