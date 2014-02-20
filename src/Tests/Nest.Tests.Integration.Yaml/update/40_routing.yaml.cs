using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Update8
{
	public partial class Update8YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Routing1Tests : YamlTestsBase
		{
			[Test]
			public void Routing1Test()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						index= new {
							number_of_replicas= "0"
						}
					}
				};
				this.Do(()=> _client.IndicesCreate("test_1", _body));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
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
				this.Do(()=> _client.Update("test_1", "test", "1", _body, nv=>nv
					.Add("routing", 5)
				));

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.Add("routing", 5)
					.Add("fields", @"_routing")
				));

				//match _response.fields._routing: 
				this.IsMatch(_response.fields._routing, 5);

				//do update 
				_body = new {
					doc= new {
						foo= "baz"
					}
				};
				this.Do(()=> _client.Update("test_1", "test", "1", _body), shouldCatch: @"missing");

				//do update 
				_body = new {
					doc= new {
						foo= "baz"
					}
				};
				this.Do(()=> _client.Update("test_1", "test", "1", _body, nv=>nv
					.Add("routing", 5)
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

