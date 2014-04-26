using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Update10
{
	public partial class Update10YamlTests
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
						number_of_replicas= "0"
					}
				};
				this.Do(()=> _client.IndicesCreate("test_1", _body));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"green")
				));

				//do update 
				_body = new {
					doc= new {
						foo= "baz"
					},
					upsert= new {
						foo= "bar"
					}
				};
				this.Do(()=> _client.Update("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("parent", 5)
					.AddQueryString("routing", 4)
				));

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("routing", 4)
					.AddQueryString("parent", 5)
					.AddQueryString("fields", new [] {
						@"_parent",
						@"_routing"
					})
				));

				//match _response.fields._parent: 
				this.IsMatch(_response.fields._parent, 5);

				//match _response.fields._routing: 
				this.IsMatch(_response.fields._routing, 4);

				//do update 
				_body = new {
					doc= new {
						foo= "baz"
					}
				};
				this.Do(()=> _client.Update("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("parent", 5)
				), shouldCatch: @"missing");

				//do update 
				_body = new {
					doc= new {
						foo= "baz"
					}
				};
				this.Do(()=> _client.Update("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("parent", 5)
					.AddQueryString("routing", 4)
					.AddQueryString("fields", @"foo")
				));

				//match _response.get.fields.foo: 
				this.IsMatch(_response.get.fields.foo, new [] {
					@"baz"
				});

			}
		}
	}
}

