using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesPutSettings1
{
	public partial class IndicesPutSettings1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestIndicesSettings1Tests : YamlTestsBase
		{
			[Test]
			public void TestIndicesSettings1Test()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						index= new {
							number_of_replicas= "0"
						}
					}
				};
				this.Do(()=> _client.IndicesCreate("test-index", _body));

				//do indices.get_settings 
				this.Do(()=> _client.IndicesGetSettings("test-index", nv=>nv
					.AddQueryString("flat_settings", @"true")
				));

				//match _response[@"test-index"][@"settings"][@"index.number_of_replicas"]: 
				this.IsMatch(_response[@"test-index"][@"settings"][@"index.number_of_replicas"], 0);

				//do indices.put_settings 
				_body = new {
					number_of_replicas= "1"
				};
				this.Do(()=> _client.IndicesPutSettingsForAll(_body));

				//do indices.get_settings 
				this.Do(()=> _client.IndicesGetSettingsForAll(nv=>nv
					.AddQueryString("flat_settings", @"false")
				));

				//match _response[@"test-index"][@"settings"][@"index"][@"number_of_replicas"]: 
				this.IsMatch(_response[@"test-index"][@"settings"][@"index"][@"number_of_replicas"], 1);

			}
		}
	}
}

