using System;
using System.Collections.Generic;
using System.Linq;
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
		public class CreateIndexWithAliases4Tests : YamlTestsBase
		{
			[Test]
			public void CreateIndexWithAliases4Test()
			{	

				//do indices.create 
				_body = new {
					aliases= new {
						test_alias= new {},
						test_blias= new {
							routing= "b"
						},
						test_clias= new {
							filter= new {
								term= new {
									field= "value"
								}
							}
						}
					}
				};
				this.Do(()=> _client.IndicesCreate("test_index", _body));

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAlias("test_index"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//match _response.test_index.aliases.test_blias.search_routing: 
				this.IsMatch(_response.test_index.aliases.test_blias.search_routing, @"b");

				//match _response.test_index.aliases.test_blias.index_routing: 
				this.IsMatch(_response.test_index.aliases.test_blias.index_routing, @"b");

				//is_false _response.test_index.aliases.test_blias.filter; 
				this.IsFalse(_response.test_index.aliases.test_blias.filter);

				//match _response.test_index.aliases.test_clias.filter.term.field: 
				this.IsMatch(_response.test_index.aliases.test_clias.filter.term.field, @"value");

				//is_false _response.test_index.aliases.test_clias.index_routing; 
				this.IsFalse(_response.test_index.aliases.test_clias.index_routing);

				//is_false _response.test_index.aliases.test_clias.search_routing; 
				this.IsFalse(_response.test_index.aliases.test_clias.search_routing);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CreateIndexWithMappingsSettingsWarmersAndAliases5Tests : YamlTestsBase
		{
			[Test]
			public void CreateIndexWithMappingsSettingsWarmersAndAliases5Test()
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
					},
					aliases= new {
						test_alias= new {},
						test_blias= new {
							routing= "b"
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

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAlias("test_index"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//match _response.test_index.aliases.test_blias.search_routing: 
				this.IsMatch(_response.test_index.aliases.test_blias.search_routing, @"b");

				//match _response.test_index.aliases.test_blias.index_routing: 
				this.IsMatch(_response.test_index.aliases.test_blias.index_routing, @"b");

			}
		}
	}
}

