using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesStats3
{
	public partial class IndicesStats3YamlTests
	{	
	
		public class IndicesStats312LevelYamlBase : YamlTestsBase
		{
			public IndicesStats312LevelYamlBase() : base()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test1", "bar", "1", _body));

				//do index 
				_body = new {
					foo= "baz"
				};
				this.Do(()=> _client.Index("test2", "baz", "1", _body));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class LevelBlank2Tests : IndicesStats312LevelYamlBase
		{
			[Test]
			public void LevelBlank2Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll());

				//is_true _response._all.total.docs; 
				this.IsTrue(_response._all.total.docs);

				//is_true _response._all.total.docs; 
				this.IsTrue(_response._all.total.docs);

				//is_true _response.indices.test1.total.docs; 
				this.IsTrue(_response.indices.test1.total.docs);

				//is_true _response.indices.test1.total.docs; 
				this.IsTrue(_response.indices.test1.total.docs);

				//is_false _response.indices.test1.shards; 
				this.IsFalse(_response.indices.test1.shards);

				//is_true _response.indices.test2.total.docs; 
				this.IsTrue(_response.indices.test2.total.docs);

				//is_true _response.indices.test2.total.docs; 
				this.IsTrue(_response.indices.test2.total.docs);

				//is_false _response.indices.test2.shards; 
				this.IsFalse(_response.indices.test2.shards);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class LevelIndices3Tests : IndicesStats312LevelYamlBase
		{
			[Test]
			public void LevelIndices3Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll(nv=>nv
					.AddQueryString("level", @"indices")
				));

				//is_true _response._all.total.docs; 
				this.IsTrue(_response._all.total.docs);

				//is_true _response._all.total.docs; 
				this.IsTrue(_response._all.total.docs);

				//is_true _response.indices.test1.total.docs; 
				this.IsTrue(_response.indices.test1.total.docs);

				//is_true _response.indices.test1.total.docs; 
				this.IsTrue(_response.indices.test1.total.docs);

				//is_false _response.indices.test1.shards; 
				this.IsFalse(_response.indices.test1.shards);

				//is_true _response.indices.test2.total.docs; 
				this.IsTrue(_response.indices.test2.total.docs);

				//is_true _response.indices.test2.total.docs; 
				this.IsTrue(_response.indices.test2.total.docs);

				//is_false _response.indices.test2.shards; 
				this.IsFalse(_response.indices.test2.shards);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class LevelCluster4Tests : IndicesStats312LevelYamlBase
		{
			[Test]
			public void LevelCluster4Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll(nv=>nv
					.AddQueryString("level", @"cluster")
				));

				//is_true _response._all.total.docs; 
				this.IsTrue(_response._all.total.docs);

				//is_true _response._all.total.docs; 
				this.IsTrue(_response._all.total.docs);

				//is_false _response.indices; 
				this.IsFalse(_response.indices);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class LevelShards5Tests : IndicesStats312LevelYamlBase
		{
			[Test]
			public void LevelShards5Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll(nv=>nv
					.AddQueryString("level", @"shards")
				));

				//is_true _response._all.total.docs; 
				this.IsTrue(_response._all.total.docs);

				//is_true _response._all.total.docs; 
				this.IsTrue(_response._all.total.docs);

				//is_true _response.indices.test1.total.docs; 
				this.IsTrue(_response.indices.test1.total.docs);

				//is_true _response.indices.test1.total.docs; 
				this.IsTrue(_response.indices.test1.total.docs);

				//is_true _response.indices.test1.shards; 
				this.IsTrue(_response.indices.test1.shards);

				//is_true _response.indices.test2.total.docs; 
				this.IsTrue(_response.indices.test2.total.docs);

				//is_true _response.indices.test2.total.docs; 
				this.IsTrue(_response.indices.test2.total.docs);

				//is_true _response.indices.test2.shards; 
				this.IsTrue(_response.indices.test2.shards);

			}
		}
	}
}

