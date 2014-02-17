using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesOpen2
{
	public partial class IndicesOpen2YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Setup1Tests : YamlTestsBase
		{
			[Test]
			public void Setup1Test()
			{	

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePut("test_index1", null));

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePut("test_index2", null));

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePut("test_index3", null));

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status", @"yellow")
				));

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class AllIndices2Tests : YamlTestsBase
		{
			[Test]
			public void AllIndices2Test()
			{	

				//do indices.close 
				this.Do(()=> this._client.IndicesClosePost("_all"));

				//do search 
				this.Do(()=> this._client.SearchGet("test_index2"), shouldCatch: @"forbidden");

				//do indices.open 
				this.Do(()=> this._client.IndicesOpenPost("_all"));

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status", @"yellow")
				));

				//do search 
				this.Do(()=> this._client.SearchGet("test_index2"));

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TrailingWildcard3Tests : YamlTestsBase
		{
			[Test]
			public void TrailingWildcard3Test()
			{	

				//do indices.close 
				this.Do(()=> this._client.IndicesClosePost("test_*"));

				//do search 
				this.Do(()=> this._client.SearchGet("test_index2"), shouldCatch: @"forbidden");

				//do indices.open 
				this.Do(()=> this._client.IndicesOpenPost("test_*"));

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status", @"yellow")
				));

				//do search 
				this.Do(()=> this._client.SearchGet("test_index2"));

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class OnlyWildcard4Tests : YamlTestsBase
		{
			[Test]
			public void OnlyWildcard4Test()
			{	

				//do indices.close 
				this.Do(()=> this._client.IndicesClosePost("*"));

				//do search 
				this.Do(()=> this._client.SearchGet("test_index3"), shouldCatch: @"forbidden");

				//do indices.open 
				this.Do(()=> this._client.IndicesOpenPost("*"));

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status", @"yellow")
				));

				//do search 
				this.Do(()=> this._client.SearchGet("test_index3"));

			}
		}
	}
}

