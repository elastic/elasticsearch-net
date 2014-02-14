using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.NodesStats1
{
	public partial class NodesStats1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class NodesStats1Tests : YamlTestsBase
		{
			[Test]
			public void NodesStats1Test()
			{	

				//do nodes.stats 
				this.Do(()=> this._client.NodesStatsGetForAll("indices,transport"));

				//is_true _response.cluster_name; 
				this.IsTrue(_response.cluster_name);

				//is_true _response.nodes; 
				this.IsTrue(_response.nodes);

			}
		}
	}
}

