using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.ClusterNodeStats
{
	public partial class ClusterNodeStats10BasicYaml10Tests
	{
		
		public class NodesStats10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public NodesStats10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void NodesStatsTests()
			{

				//do cluster.node_stats 
				
				this._client.ClusterNodeStatsGet(nv=>nv);
			}
		}
	}
}
