using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Create10
{
	public partial class Create10YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Refresh1Tests : YamlTestsBase
		{
			[Test]
			public void Refresh1Test()
			{	

				//do indices.create 
				_body = new {
					settings= new Dictionary<string, object> {
						 { "index.refresh_interval",  "-1" }
					}
				};
				this.Do(()=> _client.IndicesCreate("test_1", _body));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"yellow")
				));

				//do create 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("op_type", @"create")
				));

				//do search 
				_body = new {
					query= new {
						term= new {
							_id= "1"
						}
					}
				};
				this.Do(()=> _client.Search("test_1", "test", _body));

				//match _response.hits.total: 
				this.IsMatch(_response.hits.total, 0);

				//do create 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "2", _body, nv=>nv
					.AddQueryString("refresh", 1)
					.AddQueryString("op_type", @"create")
				));

				//do search 
				_body = new {
					query= new {
						term= new {
							_id= "2"
						}
					}
				};
				this.Do(()=> _client.Search("test_1", "test", _body));

				//match _response.hits.total: 
				this.IsMatch(_response.hits.total, 1);

			}
		}
	}
}

