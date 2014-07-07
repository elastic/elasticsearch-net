using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Delete9
{
	public partial class Delete9YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Refresh1Tests : YamlTestsBase
		{
			[Test]
			public void Refresh1Test()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						refresh_interval= "-1",
						number_of_shards= "5",
						number_of_replicas= "0"
					}
				};
				this.Do(()=> _client.IndicesCreate("test_1", _body));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"green")
				));

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("refresh", 1)
				));

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "2", _body, nv=>nv
					.AddQueryString("refresh", 1)
				));

				//do search 
				_body = new {
					query= new {
						terms= new {
							_id= new [] {
								"1",
								"2"
							}
						}
					}
				};
				this.Do(()=> _client.Search("test_1", "test", _body));

				//match _response.hits.total: 
				this.IsMatch(_response.hits.total, 2);

				//do delete 
				this.Do(()=> _client.Delete("test_1", "test", "1"));

				//do search 
				_body = new {
					query= new {
						terms= new {
							_id= new [] {
								"1",
								"2"
							}
						}
					}
				};
				this.Do(()=> _client.Search("test_1", "test", _body));

				//match _response.hits.total: 
				this.IsMatch(_response.hits.total, 2);

				//do delete 
				this.Do(()=> _client.Delete("test_1", "test", "2", nv=>nv
					.AddQueryString("refresh", 1)
				));

				//do search 
				_body = new {
					query= new {
						terms= new {
							_id= new [] {
								"1",
								"2"
							}
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

