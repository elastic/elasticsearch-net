using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Update10
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
				this.Do(()=> this._client.IndicesCreatePost("test_1", _body));

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status", @"green")
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
				this.Do(()=> this._client.UpdatePost("test_1", "test", "1", _body, nv=>nv
					.Add("parent", 5)
					.Add("routing", 4)
				));

				//do get 
				this.Do(()=> this._client.Get("test_1", "test", "1", nv=>nv
					.Add("routing", 4)
					.Add("parent", 5)
					.Add("fields", new [] {
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
				this.Do(()=> this._client.UpdatePost("test_1", "test", "1", _body, nv=>nv
					.Add("parent", 5)
				), shouldCatch: @"missing");

				//do update 
				_body = new {
					doc= new {
						foo= "baz"
					}
				};
				this.Do(()=> this._client.UpdatePost("test_1", "test", "1", _body, nv=>nv
					.Add("parent", 5)
					.Add("routing", 4)
					.Add("fields", @"foo")
				));

				//match _response.get.fields.foo: 
				this.IsMatch(_response.get.fields.foo, new [] {
					@"baz"
				});

			}
		}
	}
}

