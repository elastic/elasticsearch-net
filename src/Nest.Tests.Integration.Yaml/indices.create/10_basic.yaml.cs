using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesCreate
{
	public partial class IndicesCreateTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CreateIndexWithMappingsTests : YamlTestsBase
		{
			[Test]
			public void CreateIndexWithMappingsTest()
			{	

				//do indices.create 
				_body = new {
					mappings= new {
						type_1= new {}
					}
				};
				this.Do(()=> this._client.IndicesCreatePost("test_index", _body));

				//do indices.get_mapping 
				this.Do(()=> this._client.IndicesGetMapping("test_index"));

				//match _response.test_index.type_1.properties: 
				this.IsMatch(_response.test_index.type_1.properties, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CreateIndexWithSettingsTests : YamlTestsBase
		{
			[Test]
			public void CreateIndexWithSettingsTest()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						number_of_replicas= "0"
					}
				};
				this.Do(()=> this._client.IndicesCreatePost("test_index", _body));

				//do indices.get_settings 
				this.Do(()=> this._client.IndicesGetSettings("test_index"));

				//match _responseDictionary[@"test_index"][@"settings"][@"index"][@"number_of_replicas"]: 
				this.IsMatch(_responseDictionary[@"test_index"][@"settings"][@"index"][@"number_of_replicas"], 0);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CreateIndexWithWarmersTests : YamlTestsBase
		{
			[Test]
			public void CreateIndexWithWarmersTest()
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
				this.Do(()=> this._client.IndicesCreatePost("test_index", _body));

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmer("test_index"));

				//match _response.test_index.warmers.test_warmer.source.query.match_all: 
				this.IsMatch(_response.test_index.warmers.test_warmer.source.query.match_all, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CreateIndexWithMappingsSettingsAndWarmersTests : YamlTestsBase
		{
			[Test]
			public void CreateIndexWithMappingsSettingsAndWarmersTest()
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
				this.Do(()=> this._client.IndicesCreatePost("test_index", _body));

				//do indices.get_mapping 
				this.Do(()=> this._client.IndicesGetMapping("test_index"));

				//match _response.test_index.type_1.properties: 
				this.IsMatch(_response.test_index.type_1.properties, new {});

				//do indices.get_settings 
				this.Do(()=> this._client.IndicesGetSettings("test_index"));

				//match _responseDictionary[@"test_index"][@"settings"][@"index"][@"number_of_replicas"]: 
				this.IsMatch(_responseDictionary[@"test_index"][@"settings"][@"index.number_of_replicas"], 0);

				//do indices.get_warmer 
				this.Do(()=> this._client.IndicesGetWarmer("test_index"));

				//match _response.test_index.warmers.test_warmer.source.query.match_all: 
				this.IsMatch(_response.test_index.warmers.test_warmer.source.query.match_all, new {});

			}
		}
	}
}

