using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Update9
{
	public partial class Update9YamlTests
	{	
	
		public class Update950ParentYamlBase : YamlTestsBase
		{
			public Update950ParentYamlBase() : base()
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
					.Add("wait_for_status", @"yellow")
				));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Parent2Tests : Update950ParentYamlBase
		{
			[Test]
			public void Parent2Test()
			{	

				//do update 
				_body = new {
					doc= new {
						foo= "baz"
					},
					upsert= new {
						foo= "bar"
					}
				};
				this.Do(()=> _client.Update("test_1", "test", "1", _body), shouldCatch: @"/RoutingMissingException/");

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
					.Add("parent", 5)
				));

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.Add("parent", 5)
					.Add("fields", new [] {
						@"_parent",
						@"_routing"
					})
				));

				//match _response.fields._parent: 
				this.IsMatch(_response.fields._parent, 5);

				//match _response.fields._routing: 
				this.IsMatch(_response.fields._routing, 5);

				//do update 
				_body = new {
					doc= new {
						foo= "baz"
					}
				};
				this.Do(()=> _client.Update("test_1", "test", "1", _body, nv=>nv
					.Add("parent", 5)
					.Add("fields", @"foo")
				));

				//match _response.get.fields.foo: 
				this.IsMatch(_response.get.fields.foo, new [] {
					@"baz"
				});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ParentOmitted3Tests : Update950ParentYamlBase
		{
			[Test]
			public void ParentOmitted3Test()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.Add("parent", 5)
				));

				//do update 
				_body = new {
					doc= new {
						foo= "baz"
					}
				};
				this.Do(()=> _client.Update("test_1", "test", "1", _body), shouldCatch: @"request");

			}
		}
	}
}

