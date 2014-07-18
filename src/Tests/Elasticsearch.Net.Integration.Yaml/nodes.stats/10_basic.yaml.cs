using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.NodesStats1
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
				this.Do(()=> _client.NodesStatsForAll("indices,transport"));

				//is_true _response.cluster_name; 
				this.IsTrue(_response.cluster_name);

				//is_true _response.nodes; 
				this.IsTrue(_response.nodes);

			}
		}
	}
}

