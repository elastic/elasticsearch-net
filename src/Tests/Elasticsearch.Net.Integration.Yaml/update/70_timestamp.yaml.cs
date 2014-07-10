using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Update12
{
	public partial class Update12YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Timestamp1Tests : YamlTestsBase
		{
			[Test]
			public void Timestamp1Test()
			{	

				//do indices.create 
				_body = new {
					mappings= new {
						test= new {
							_timestamp= new {
								enabled= "1",
								store= "yes"
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
				this.Do(()=> _client.Update("test_1", "test", "1", _body));

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("fields", @"_timestamp")
				));

				//is_true _response.fields._timestamp; 
				this.IsTrue(_response.fields._timestamp);

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
					.AddQueryString("timestamp", @"1372011280000")
				));

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("fields", @"_timestamp")
				));

				//match _response.fields._timestamp: 
				this.IsMatch(_response.fields._timestamp, @"1372011280000");

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
					.AddQueryString("timestamp", @"2013-06-23T18:14:40")
				));

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("fields", @"_timestamp")
				));

				//match _response.fields._timestamp: 
				this.IsMatch(_response.fields._timestamp, @"1372011280000");

			}
		}
	}
}

