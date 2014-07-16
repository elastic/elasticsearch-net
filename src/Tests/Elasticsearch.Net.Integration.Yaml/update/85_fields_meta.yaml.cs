using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Update15
{
	public partial class Update15YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class MetadataFields1Tests : YamlTestsBase
		{
			[Test]
			public void MetadataFields1Test()
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
				this.Do(()=> _client.IndicesCreate("test_1", _body));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"yellow")
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
					.AddQueryString("fields", new [] {
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
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("parent", 5)
					.AddQueryString("fields", new [] {
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

