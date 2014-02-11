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
	public partial class ClusterNodeInfoTests
	{	


		public class NodeInfoTestTests : YamlTestsBase
		{
			[Test]
			public void NodeInfoTestTest()
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

