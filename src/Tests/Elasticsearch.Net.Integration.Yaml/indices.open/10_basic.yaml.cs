using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesOpen1
{
	public partial class IndicesOpen1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicTestForIndexOpenClose1Tests : YamlTestsBase
		{
			[Test]
			public void BasicTestForIndexOpenClose1Test()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						number_of_replicas= "0"
					}
				};
				this.Do(()=> _client.IndicesCreate("test_index", _body));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"green")
				));

				//do indices.close 
				this.Do(()=> _client.IndicesClose("test_index"));

				//do search 
				this.Do(()=> _client.SearchGet("test_index"), shouldCatch: @"forbidden");

				//do indices.open 
				this.Do(()=> _client.IndicesOpen("test_index"));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"green")
				));

				//do search 
				this.Do(()=> _client.SearchGet("test_index"));

			}
		}
	}
}

