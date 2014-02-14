using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.NodesInfo1
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
				this.Do(()=> this._client.NodesInfoGetForAll());

				//is_true _response.nodes; 
				this.IsTrue(_response.nodes);

				//is_true _response.cluster_name; 
				this.IsTrue(_response.cluster_name);

			}
		}
	}
}

