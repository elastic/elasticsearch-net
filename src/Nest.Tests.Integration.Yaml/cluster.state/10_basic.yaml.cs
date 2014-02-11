using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.ClusterState
{
	public partial class ClusterStateTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ClusterStateTestTests : YamlTestsBase
		{
			[Test]
			public void ClusterStateTestTest()
			{	

				//do cluster.state 
				this.Do(()=> this._client.ClusterStateGet());

				//is_true _response.master_node; 
				this.IsTrue(_response.master_node);

			}
		}
	}
}

