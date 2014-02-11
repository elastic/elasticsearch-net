using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesPutMapping
{
	public partial class IndicesPutMappingTests
	{	


		public class TestCreateAndUpdateMappingTests : YamlTestsBase
		{
			[Test]
			public void TestCreateAndUpdateMappingTest()
			{	

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePost("test_index", null));

				//do indices.put_mapping 
				_body = new {
					test_type= new {
						properties= new {
							text= new {
								type= "string",
								analyzer= "whitespace"
							}
						}
					}
				};
				this.Do(()=> this._client.IndicesPutMappingPost("test_index", "test_type", _body));

				//do indices.get_mapping 
				this.Do(()=> this._client.IndicesGetMapping("test_index"));

				//match _response.test_index.test_type.properties.text.type: 
				this.IsMatch(_response.test_index.test_type.properties.text.type, @"string");

				//match _response.test_index.test_type.properties.text.analyzer: 
				this.IsMatch(_response.test_index.test_type.properties.text.analyzer, @"whitespace");

				//do indices.put_mapping 
				_body = new {
					test_type= new {
						properties= new {
							text= new {
								type= "multi_field",
								fields= new {
									text= new {
										type= "string",
										analyzer= "whitespace"
									},
									text_raw= new {
										type= "string",
										index= "not_analyzed"
									}
								}
							}
						}
					}
				};
				this.Do(()=> this._client.IndicesPutMappingPost("test_index", "test_type", _body));

				//do indices.get_mapping 
				this.Do(()=> this._client.IndicesGetMapping("test_index"));

				//match _response.test_index.test_type.properties.text.type: 
				this.IsMatch(_response.test_index.test_type.properties.text.type, @"multi_field");

				//match _response.test_index.test_type.properties.text.fields.text_raw.index: 
				this.IsMatch(_response.test_index.test_type.properties.text.fields.text_raw.index, @"not_analyzed");

			}
		}
	}
}

