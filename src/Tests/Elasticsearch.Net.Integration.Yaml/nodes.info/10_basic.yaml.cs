using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.NodesInfo1
{
	public partial class NodesInfo1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class NodeInfoTest1Tests : YamlTestsBase
		{
			[Test]
			public void NodeInfoTest1Test()
			{	

				//do nodes.info 
				this.Do(()=> _client.NodesInfoForAll());

				//is_true _response.nodes; 
				this.IsTrue(_response.nodes);

				//is_true _response.cluster_name; 
				this.IsTrue(_response.cluster_name);

			}
		}
	}
}

