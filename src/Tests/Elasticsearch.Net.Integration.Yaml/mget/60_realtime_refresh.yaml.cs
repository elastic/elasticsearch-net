using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Mget10
{
	public partial class Mget10YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class RealtimeRefresh1Tests : YamlTestsBase
		{
			[Test]
			public void RealtimeRefresh1Test()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						index= new {
							refresh_interval= "-1",
							number_of_replicas= "0"
						}
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
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//do mget 
				_body = new {
					ids= new [] {
						"1"
					}
				};
				this.Do(()=> _client.Mget("test_1", "test", _body, nv=>nv
					.AddQueryString("realtime", 0)
				));

				//is_false _response.docs[0].found; 
				this.IsFalse(_response.docs[0].found);

				//do mget 
				_body = new {
					ids= new [] {
						"1"
					}
				};
				this.Do(()=> _client.Mget("test_1", "test", _body, nv=>nv
					.AddQueryString("realtime", 1)
				));

				//is_true _response.docs[0].found; 
				this.IsTrue(_response.docs[0].found);

				//do mget 
				_body = new {
					ids= new [] {
						"1"
					}
				};
				this.Do(()=> _client.Mget("test_1", "test", _body, nv=>nv
					.AddQueryString("realtime", 0)
					.AddQueryString("refresh", 1)
				));

				//is_true _response.docs[0].found; 
				this.IsTrue(_response.docs[0].found);

			}
		}
	}
}

