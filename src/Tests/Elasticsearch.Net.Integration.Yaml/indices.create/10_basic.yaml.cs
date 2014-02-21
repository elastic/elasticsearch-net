using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesCreate1
{
	public partial class IndicesCreate1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CreateIndexWithMappings1Tests : YamlTestsBase
		{
			[Test]
			public void CreateIndexWithMappings1Test()
			{	

				//do indices.create 
				_body = new {
					mappings= new {
						type_1= new {}
					}
				};
				this.Do(()=> _client.IndicesCreate("test_index", _body));

				//do indices.get_mapping 
				this.Do(()=> _client.IndicesGetMapping("test_index"));

				//match _response.test_index.mappings.type_1.properties: 
				this.IsMatch(_response.test_index.mappings.type_1.properties, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CreateIndexWithSettings2Tests : YamlTestsBase
		{
			[Test]
			public void CreateIndexWithSettings2Test()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						number_of_replicas= "0"
					}
				};
				this.Do(()=> _client.IndicesCreate("test_index", _body));

				//do indices.get_settings 
				this.Do(()=> _client.IndicesGetSettings("test_index"));

				//match _response.test_index.settings.index.number_of_replicas: 
				this.IsMatch(_response.test_index.settings.index.number_of_replicas, 0);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CreateIndexWithWarmers3Tests : YamlTestsBase
		{
			[Test]
			public void CreateIndexWithWarmers3Test()
			{	

				//do indices.create 
				_body = new {
					warmers= new {
						test_warmer= new {
							source= new {
								query= new {
									match_all= new {}
								}
							}
						}
					}
				};
				this.Do(()=> _client.IndicesCreate("test_index", _body));

				//do indices.get_warmer 
				this.Do(()=> _client.IndicesGetWarmer("test_index"));

				//match _response.test_index.warmers.test_warmer.source.query.match_all: 
				this.IsMatch(_response.test_index.warmers.test_warmer.source.query.match_all, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CreateIndexWithMappingsSettingsAndWarmers4Tests : YamlTestsBase
		{
			[Test]
			public void CreateIndexWithMappingsSettingsAndWarmers4Test()
			{	

				//do indices.create 
				_body = new {
					mappings= new {
						type_1= new {}
					},
					settings= new {
						number_of_replicas= "0"
					},
					warmers= new {
						test_warmer= new {
							source= new {
								query= new {
									match_all= new {}
								}
							}
						}
					}
				};
				this.Do(()=> _client.IndicesCreate("test_index", _body));

				//do indices.get_mapping 
				this.Do(()=> _client.IndicesGetMapping("test_index"));

				//match _response.test_index.mappings.type_1.properties: 
				this.IsMatch(_response.test_index.mappings.type_1.properties, new {});

				//do indices.get_settings 
				this.Do(()=> _client.IndicesGetSettings("test_index"));

				//match _response.test_index.settings.index.number_of_replicas: 
				this.IsMatch(_response.test_index.settings.index.number_of_replicas, 0);

				//do indices.get_warmer 
				this.Do(()=> _client.IndicesGetWarmer("test_index"));

				//match _response.test_index.warmers.test_warmer.source.query.match_all: 
				this.IsMatch(_response.test_index.warmers.test_warmer.source.query.match_all, new {});

			}
		}
	}
}

