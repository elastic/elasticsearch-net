using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesStats1
{
	public partial class IndicesStats1YamlTests
	{	
	
		public class IndicesStats110IndexYamlBase : YamlTestsBase
		{
			public IndicesStats110IndexYamlBase() : base()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						number_of_shards= "5",
						number_of_replicas= "1"
					}
				};
				this.Do(()=> _client.IndicesCreate("test1", _body));

				//do indices.create 
				_body = new {
					settings= new {
						number_of_shards= "4",
						number_of_replicas= "1"
					}
				};
				this.Do(()=> _client.IndicesCreate("test2", _body));

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
		public class IndexBlank2Tests : IndicesStats110IndexYamlBase
		{
			[Test]
			public void IndexBlank2Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll());

				//match _response._shards.total: 
				this.IsMatch(_response._shards.total, 18);

				//is_true _response._all; 
				this.IsTrue(_response._all);

				//is_true _response.indices.test1; 
				this.IsTrue(_response.indices.test1);

				//is_true _response.indices.test2; 
				this.IsTrue(_response.indices.test2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class IndexAll3Tests : IndicesStats110IndexYamlBase
		{
			[Test]
			public void IndexAll3Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStats("_all"));

				//match _response._shards.total: 
				this.IsMatch(_response._shards.total, 18);

				//is_true _response._all; 
				this.IsTrue(_response._all);

				//is_true _response.indices.test1; 
				this.IsTrue(_response.indices.test1);

				//is_true _response.indices.test2; 
				this.IsTrue(_response.indices.test2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class IndexStar4Tests : IndicesStats110IndexYamlBase
		{
			[Test]
			public void IndexStar4Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStats("*"));

				//match _response._shards.total: 
				this.IsMatch(_response._shards.total, 18);

				//is_true _response._all; 
				this.IsTrue(_response._all);

				//is_true _response.indices.test1; 
				this.IsTrue(_response.indices.test1);

				//is_true _response.indices.test2; 
				this.IsTrue(_response.indices.test2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class IndexOneIndex5Tests : IndicesStats110IndexYamlBase
		{
			[Test]
			public void IndexOneIndex5Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStats("test1"));

				//match _response._shards.total: 
				this.IsMatch(_response._shards.total, 10);

				//is_true _response._all; 
				this.IsTrue(_response._all);

				//is_true _response.indices.test1; 
				this.IsTrue(_response.indices.test1);

				//is_false _response.indices.test2; 
				this.IsFalse(_response.indices.test2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class IndexMultiIndex6Tests : IndicesStats110IndexYamlBase
		{
			[Test]
			public void IndexMultiIndex6Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStats("test1,test2"));

				//match _response._shards.total: 
				this.IsMatch(_response._shards.total, 18);

				//is_true _response._all; 
				this.IsTrue(_response._all);

				//is_true _response.indices.test1; 
				this.IsTrue(_response.indices.test1);

				//is_true _response.indices.test2; 
				this.IsTrue(_response.indices.test2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class IndexPattern7Tests : IndicesStats110IndexYamlBase
		{
			[Test]
			public void IndexPattern7Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStats("*2"));

				//match _response._shards.total: 
				this.IsMatch(_response._shards.total, 8);

				//is_true _response._all; 
				this.IsTrue(_response._all);

				//is_false _response.indices.test1; 
				this.IsFalse(_response.indices.test1);

				//is_true _response.indices.test2; 
				this.IsTrue(_response.indices.test2);

			}
		}
	}
}

