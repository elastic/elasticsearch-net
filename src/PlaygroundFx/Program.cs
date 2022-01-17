using System;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

namespace PlaygroundFx
{
    internal class Program
    {
		private static async Task Main(string[] args)
        {
			var client = new ElasticClient(new ElasticsearchClientSettings()
				.CertificateFingerprint("028567742bb754e19ddc8eab752b70d6534f98dccdb681863f57f9b0564170c0")
				.Authentication(new BasicAuthentication("elastic", "HSPvzLR7cSt8PwXJRWjl")));

			var result = await client.PingAsync();

			var health = await client.IndexManagement.ExistsAsync("not-exists");

			//var client = new ElasticClient(new ElasticsearchClientSettings(new Uri("https://azure.es.eastus.azure.elastic-cloud.com:9243/"))
			//	.CertificateFingerprint("1E69964DFF1259B9ADE47556144E501F381A84B07E5EEC84B81ECF7D4B850C1D")
			//	.Authentication(new BasicAuthentication("elastic", "Z9vNfZN86RxHJ97Poi1BYhC6")));

			// client.ElasticsearchClientSettings.ResponseHeadersToParse

			//var searchAgain = new SearchRequest()
			//{
			//	Query = new Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer(new Elastic.Clients.Elasticsearch.QueryDsl.BoolQuery { Boost = 1.2F }),
			//	MinScore = 10.0,
			//	Profile = true,
			//	RequestConfiguration = new RequestConfiguration() { ResponseHeadersToParse = new HeadersList("made-up") }
			//};

			//var response = client.Search<Person>(searchAgain);
		}
    }
}
