using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.ClusterNodeInfo
{
	public partial class ClusterNodeInfo10BasicYaml10Tests
	{
		
		public class NodeInfoTest10Tests : YamlTestsBase
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
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
				
				_status = this._client.ClusterNodeInfoGet();
				_response = _status.Deserialize<dynamic>();

				//is_true .ok; 
				this.IsTrue(_response.ok);

				//is_true .nodes; 
				this.IsTrue(_response.nodes);

				//is_true .cluster_name; 
				this.IsTrue(_response.cluster_name);
			}
		}
	}
}
