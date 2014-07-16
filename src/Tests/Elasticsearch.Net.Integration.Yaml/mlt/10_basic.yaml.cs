using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Mlt1
{
	public partial class Mlt1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicMlt1Tests : YamlTestsBase
		{
			[Test]
			public void BasicMlt1Test()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						index= new {
							number_of_replicas= "0"
						}
					},
					mappings= new {
						test= new {
							properties= new {
								foo= new {
									type= "string"
								},
								title= new {
									type= "string"
								}
							}
						}
					}
				};
				this.Do(()=> _client.IndicesCreate("test_1", _body));

				//do index 
				_body = new {
					foo= "bar",
					title= "howdy"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//do indices.refresh 
				this.Do(()=> _client.IndicesRefreshForAll());

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"green")
				));

				//do mlt 
				this.Do(()=> _client.MltGet("test_1", "test", "1", nv=>nv
					.AddQueryString("mlt_fields", @"title")
				));

				//match _response.hits.total: 
				this.IsMatch(_response.hits.total, 0);

			}
		}
	}
}

