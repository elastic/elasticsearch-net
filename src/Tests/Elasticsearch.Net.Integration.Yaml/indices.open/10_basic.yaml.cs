using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesOpen1
{
	public partial class IndicesOpen1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicTestForIndexOpenClose1Tests : YamlTestsBase
		{
			[Test]
			public void BasicTestForIndexOpenClose1Test()
			{	

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test_index", null));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"yellow")
				));

				//do indices.close 
				this.Do(()=> _client.IndicesClose("test_index"));

				//do search 
				this.Do(()=> _client.SearchGet("test_index"), shouldCatch: @"forbidden");

				//do indices.open 
				this.Do(()=> _client.IndicesOpen("test_index"));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"yellow")
				));

				//do search 
				this.Do(()=> _client.SearchGet("test_index"));

			}
		}
	}
}

