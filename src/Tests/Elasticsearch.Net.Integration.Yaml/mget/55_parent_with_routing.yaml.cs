using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Mget9
{
	public partial class Mget9YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ParentWithRoutingTests : YamlTestsBase
		{
			[Test]
			public void ParentWithRoutingTest()
			{	

				//do indices.create 
				_body = new {
					mappings= new {
						test= new {
							_parent= new {
								type= "foo"
							}
						}
					},
					settings= new {
						number_of_shards= "5",
						number_of_replicas= "0"
					}
				};
				this.Do(()=> _client.IndicesCreate("test_1", _body));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"green")
				));

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("parent", 4)
					.AddQueryString("routing", 5)
				));

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_id= "1"
						},
						new {
							_id= "1",
							parent= "4"
						},
						new {
							_id= "1",
							parent= "4",
							routing= "5"
						}
					}
				};
				this.Do(()=> _client.Mget("test_1", "test", _body, nv=>nv
					.AddQueryString("fields", new [] {
						@"_routing",
						@"_parent"
					})
				));

				//is_false _response.docs[0].found; 
				this.IsFalse(_response.docs[0].found);

				//is_false _response.docs[1].found; 
				this.IsFalse(_response.docs[1].found);

				//is_true _response.docs[2].found; 
				this.IsTrue(_response.docs[2].found);

				//match _response.docs[2]._index: 
				this.IsMatch(_response.docs[2]._index, @"test_1");

				//match _response.docs[2]._type: 
				this.IsMatch(_response.docs[2]._type, @"test");

				//match _response.docs[2]._id: 
				this.IsMatch(_response.docs[2]._id, 1);

				//match _response.docs[2].fields._parent: 
				this.IsMatch(_response.docs[2].fields._parent, 4);

				//match _response.docs[2].fields._routing: 
				this.IsMatch(_response.docs[2].fields._routing, 5);

			}
		}
	}
}

