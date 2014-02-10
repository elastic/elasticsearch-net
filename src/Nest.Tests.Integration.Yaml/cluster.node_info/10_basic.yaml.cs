using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.ClusterNodeInfo
{
	public partial class ClusterNodeInfo10BasicYaml10Tests
	{
		
		public class NodeInfoTest10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public NodeInfoTest10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void NodeInfoTestTests()
			{

				//do cluster.node_info 
				
				this._client.ClusterNodeInfoGet(nv=>nv);
			}
		}
	}
}
