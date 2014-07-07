using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Delete6
{
	public partial class Delete6YamlTests
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
						number_of_shards= "5"
					}
				};
				this.Do(()=> _client.IndicesCreate("test_1", _body));

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("routing", 5)
				));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"yellow")
				));

				//do delete 
				this.Do(()=> _client.Delete("test_1", "test", "1", nv=>nv
					.AddQueryString("routing", 4)
				), shouldCatch: @"missing");

				//do delete 
				this.Do(()=> _client.Delete("test_1", "test", "1", nv=>nv
					.AddQueryString("routing", 5)
				));

			}
		}
	}
}

