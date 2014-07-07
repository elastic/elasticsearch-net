using Elasticsearch.Net.Connection.Thrift;
using Nest;

namespace Profiling.Indexing
{
	public class ThriftTester : Tester
	{
		public override IElasticClient CreateClient(string indexName)
		{
			var settings = this.CreateSettings(indexName, 9500);
			settings.SetMaximumAsyncConnections(25);
			var client = new ElasticClient(settings, new ThriftConnection(settings));
			return client;
		}
	}
}
