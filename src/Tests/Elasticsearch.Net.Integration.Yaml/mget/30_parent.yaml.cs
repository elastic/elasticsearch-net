using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Mget7
{
	public partial class Mget7YamlTests
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
					},
					settings= new {
						number_of_shards= "5"
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
					.AddQueryString("parent", 4)
				));

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "2", _body, nv=>nv
					.AddQueryString("parent", 5)
				));

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_id= "1"
						},
						new {
							_id= "1",
							parent= "5",
							fields= new [] {
								"_parent",
								"_routing"
							}
						},
						new {
							_id= "1",
							parent= "4",
							fields= new [] {
								"_parent",
								"_routing"
							}
						},
						new {
							_id= "2",
							parent= "5",
							fields= new [] {
								"_parent",
								"_routing"
							}
						}
					}
				};
				this.Do(()=> _client.Mget("test_1", "test", _body));

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
				this.IsMatch(_response.docs[2].fields._routing, 4);

				//is_true _response.docs[3].found; 
				this.IsTrue(_response.docs[3].found);

				//match _response.docs[3]._index: 
				this.IsMatch(_response.docs[3]._index, @"test_1");

				//match _response.docs[3]._type: 
				this.IsMatch(_response.docs[3]._type, @"test");

				//match _response.docs[3]._id: 
				this.IsMatch(_response.docs[3]._id, 2);

				//match _response.docs[3].fields._parent: 
				this.IsMatch(_response.docs[3].fields._parent, 5);

				//match _response.docs[3].fields._routing: 
				this.IsMatch(_response.docs[3].fields._routing, 5);

			}
		}
	}
}

