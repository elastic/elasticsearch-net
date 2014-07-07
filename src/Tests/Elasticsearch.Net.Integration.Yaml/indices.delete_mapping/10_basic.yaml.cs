using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesDeleteMapping1
{
	public partial class IndicesDeleteMapping1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class DeleteMappingTests1Tests : YamlTestsBase
		{
			[Test]
			public void DeleteMappingTests1Test()
			{	

				//do indices.create 
				_body = new {
					mappings= new {
						test_type= new {
							properties= new {
								text= new {
									type= "string",
									analyzer= "whitespace"
								}
							}
						}
					}
				};
				this.Do(()=> _client.IndicesCreate("test_index", _body));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"yellow")
				));

				//do indices.exists_type 
				this.Do(()=> _client.IndicesExistsType("test_index", "test_type"));

				//is_true this._status; 
				this.IsTrue(this._status);

				//do indices.delete_mapping 
				this.Do(()=> _client.IndicesDeleteMapping("test_index", "test_type"));

				//do indices.exists_type 
				this.Do(()=> _client.IndicesExistsType("test_index", "test_type"));

				//is_false this._status; 
				this.IsFalse(this._status);

			}
		}
	}
}

