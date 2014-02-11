using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.ClusterNodeStats
{
	public partial class ClusterNodeStatsTests
	{	


		public class NodesStatsTests : YamlTestsBase
		{
			[Test]
			public void NodesStatsTest()
			{	

				//do cluster.node_stats 
				_status = this._client.ClusterNodeStatsGet(nv=>nv
					.Add("indices","true")
					.Add("transport","true")
				);
				_response = _status.Deserialize<dynamic>();

				//is_true .cluster_name; 
				this.IsTrue(_response.cluster_name);

				//is_true .nodes; 
				this.IsTrue(_response.nodes);

			}
		}
	}
}

