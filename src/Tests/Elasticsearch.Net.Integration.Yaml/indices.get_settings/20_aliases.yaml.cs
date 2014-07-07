using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesGetSettings2
{
	public partial class IndicesGetSettings2YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GettingSettingsForAliasesShouldReturnTheRealIndexAsKey1Tests : YamlTestsBase
		{
			[Test]
			public void GettingSettingsForAliasesShouldReturnTheRealIndexAsKey1Test()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						index= new {
							refresh_interval= "-1",
							number_of_shards= "2",
							number_of_replicas= "3"
						}
					}
				};
				this.Do(()=> _client.IndicesCreate("test-index", _body));

				//do indices.put_alias 
				this.Do(()=> _client.IndicesPutAlias("test-index", "test-alias", null));

				//do indices.get_settings 
				this.Do(()=> _client.IndicesGetSettings("test-alias"));

				//match _response[@"test-index"][@"settings"][@"index"][@"number_of_replicas"]: 
				this.IsMatch(_response[@"test-index"][@"settings"][@"index"][@"number_of_replicas"], 3);

				//match _response[@"test-index"][@"settings"][@"index"][@"number_of_shards"]: 
				this.IsMatch(_response[@"test-index"][@"settings"][@"index"][@"number_of_shards"], 2);

				//match _response[@"test-index"][@"settings"][@"index"][@"refresh_interval"]: 
				this.IsMatch(_response[@"test-index"][@"settings"][@"index"][@"refresh_interval"], -1);

			}
		}
	}
}

