using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.ClusterState1
{
	public partial class ClusterState1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ClusterStateTest1Tests : YamlTestsBase
		{
			[Test]
			public void ClusterStateTest1Test()
			{	

				//do cluster.state 
				this.Do(()=> _client.ClusterState());

				//is_true _response.master_node; 
				this.IsTrue(_response.master_node);

			}
		}
	}
}

