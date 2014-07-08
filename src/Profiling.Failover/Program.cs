using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.ConnectionPool;
using Nest;

namespace Profiling.Failover
{
	class Program
	{
		static void Main(string[] args)
		{
			var seeds = new[]
			{
				new Uri("http://localhost:9200"),
				new Uri("http://localhost:9201"),
				new Uri("http://localhost:9202"),
			};
			var sniffingConnectionPool = new SniffingConnectionPool(seeds);
			var connectionSettings = new ConnectionSettings(sniffingConnectionPool);
			var client = new ElasticClient(connectionSettings);
			var rootNode = client.RootNodeInfo();
			rootNode = client.RootNodeInfo();
			rootNode = client.RootNodeInfo();
			rootNode = client.RootNodeInfo();
			rootNode = client.RootNodeInfo();
		}
	}
}
