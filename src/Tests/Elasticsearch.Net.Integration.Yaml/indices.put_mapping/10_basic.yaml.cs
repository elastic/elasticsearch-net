using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesPutMapping1
{
	public partial class IndicesPutMapping1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestCreateAndUpdateMapping1Tests : YamlTestsBase
		{
			[Test]
			public void TestCreateAndUpdateMapping1Test()
			{	

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test_index", null));

				//do indices.put_mapping 
				_body = new {
					test_type= new {
						properties= new {
							text1= new {
								type= "string",
								analyzer= "whitespace"
							},
							text2= new {
								type= "string",
								analyzer= "whitespace"
							}
						}
					}
				};
				this.Do(()=> _client.IndicesPutMapping("test_index", "test_type", _body));

				//do indices.get_mapping 
				this.Do(()=> _client.IndicesGetMapping("test_index"));

				//match _response.test_index.mappings.test_type.properties.text1.type: 
				this.IsMatch(_response.test_index.mappings.test_type.properties.text1.type, @"string");

				//match _response.test_index.mappings.test_type.properties.text1.analyzer: 
				this.IsMatch(_response.test_index.mappings.test_type.properties.text1.analyzer, @"whitespace");

				//match _response.test_index.mappings.test_type.properties.text2.type: 
				this.IsMatch(_response.test_index.mappings.test_type.properties.text2.type, @"string");

				//match _response.test_index.mappings.test_type.properties.text2.analyzer: 
				this.IsMatch(_response.test_index.mappings.test_type.properties.text2.analyzer, @"whitespace");

				//do indices.put_mapping 
				_body = new {
					test_type= new {
						properties= new {
							text1= new {
								type= "multi_field",
								fields= new {
									text1= new {
										type= "string",
										analyzer= "whitespace"
									},
									text_raw= new {
										type= "string",
										index= "not_analyzed"
									}
								}
							},
							text2= new {
								type= "string",
								analyzer= "whitespace",
								fields= new {
									text_raw= new {
										type= "string",
										index= "not_analyzed"
									}
								}
							}
						}
					}
				};
				this.Do(()=> _client.IndicesPutMapping("test_index", "test_type", _body));

				//do indices.get_mapping 
				this.Do(()=> _client.IndicesGetMapping("test_index"));

				//match _response.test_index.mappings.test_type.properties.text1.type: 
				this.IsMatch(_response.test_index.mappings.test_type.properties.text1.type, @"string");

				//match _response.test_index.mappings.test_type.properties.text1.fields.text_raw.index: 
				this.IsMatch(_response.test_index.mappings.test_type.properties.text1.fields.text_raw.index, @"not_analyzed");

				//match _response.test_index.mappings.test_type.properties.text2.type: 
				this.IsMatch(_response.test_index.mappings.test_type.properties.text2.type, @"string");

				//match _response.test_index.mappings.test_type.properties.text2.fields.text_raw.index: 
				this.IsMatch(_response.test_index.mappings.test_type.properties.text2.fields.text_raw.index, @"not_analyzed");

			}
		}
	}
}

