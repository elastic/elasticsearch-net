using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Update
{
	public partial class UpdateTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class MetadataFieldsTests : YamlTestsBase
		{
			[Test]
			public void MetadataFieldsTest()
			{	

				//skip 0 - 999; 
				this.Skip("0 - 999", "Update doesn't return metadata fields, waiting for #3259");

				//do indices.create 
				_body = new {
					mappings= new {
						test= new {
							_parent= new {
								type= "foo"
							},
							_timestamp= new {
								enabled= "1",
								store= "yes"
							},
							_ttl= new {
								enabled= "1",
								store= "yes",
								@default= "10s"
							}
						}
					}
				};
				this.Do(()=> this._client.IndicesCreatePost("test_1", _body));

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status", @"yellow")
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
					.Add("fields", new [] {
						@"_parent",
						@"_routing",
						@"_timestamp",
						@"_ttl"
					})
				));

				//match _response.get.fields._parent: 
				this.IsMatch(_response.get.fields._parent, 5);

				//match _response.get.fields._routing: 
				this.IsMatch(_response.get.fields._routing, 5);

				//is_true _response.get.fields._timestamp; 
				this.IsTrue(_response.get.fields._timestamp);

				//is_true _response.get.fields._ttl; 
				this.IsTrue(_response.get.fields._ttl);

				//do get 
				this.Do(()=> this._client.Get("test_1", "test", "1", nv=>nv
					.Add("parent", 5)
					.Add("fields", new [] {
						@"_parent",
						@"_routing",
						@"_timestamp",
						@"_ttl"
					})
				));

			}
		}
	}
}

