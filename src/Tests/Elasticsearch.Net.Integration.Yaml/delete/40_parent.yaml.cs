using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Delete7
{
	public partial class Delete7YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Parent1Tests : YamlTestsBase
		{
			[Test]
			public void Parent1Test()
			{	

				//do indices.create 
				_body = new {
					mappings= new {
						test= new {
							_parent= new {
								type= "foo"
							}
						}
					}
				};
				this.Do(()=> _client.IndicesCreate("test_1", _body));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"yellow")
				));

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("parent", 5)
				));

				//do delete 
				this.Do(()=> _client.Delete("test_1", "test", "1", nv=>nv
					.AddQueryString("parent", 1)
				), shouldCatch: @"missing");

				//do delete 
				this.Do(()=> _client.Delete("test_1", "test", "1", nv=>nv
					.AddQueryString("parent", 5)
				));

			}
		}
	}
}

