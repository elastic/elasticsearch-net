using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesOpen2
{
	public partial class IndicesOpen2YamlTests
	{	
	
		public class IndicesOpen220MultipleIndicesYamlBase : YamlTestsBase
		{
			public IndicesOpen220MultipleIndicesYamlBase() : base()
			{	

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test_index1", null));

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test_index2", null));

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test_index3", null));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.Add("wait_for_status", @"yellow")
				));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class AllIndices2Tests : IndicesOpen220MultipleIndicesYamlBase
		{
			[Test]
			public void AllIndices2Test()
			{	

				//do indices.close 
				this.Do(()=> _client.IndicesClose("_all"));

				//do search 
				this.Do(()=> _client.SearchGet("test_index2"), shouldCatch: @"forbidden");

				//do indices.open 
				this.Do(()=> _client.IndicesOpen("_all"));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.Add("wait_for_status", @"yellow")
				));

				//do search 
				this.Do(()=> _client.SearchGet("test_index2"));

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TrailingWildcard3Tests : IndicesOpen220MultipleIndicesYamlBase
		{
			[Test]
			public void TrailingWildcard3Test()
			{	

				//do indices.close 
				this.Do(()=> _client.IndicesClose("test_*"));

				//do search 
				this.Do(()=> _client.SearchGet("test_index2"), shouldCatch: @"forbidden");

				//do indices.open 
				this.Do(()=> _client.IndicesOpen("test_*"));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.Add("wait_for_status", @"yellow")
				));

				//do search 
				this.Do(()=> _client.SearchGet("test_index2"));

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class OnlyWildcard4Tests : IndicesOpen220MultipleIndicesYamlBase
		{
			[Test]
			public void OnlyWildcard4Test()
			{	

				//do indices.close 
				this.Do(()=> _client.IndicesClose("*"));

				//do search 
				this.Do(()=> _client.SearchGet("test_index3"), shouldCatch: @"forbidden");

				//do indices.open 
				this.Do(()=> _client.IndicesOpen("*"));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.Add("wait_for_status", @"yellow")
				));

				//do search 
				this.Do(()=> _client.SearchGet("test_index3"));

			}
		}
	}
}

