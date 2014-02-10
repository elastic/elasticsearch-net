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
	public partial class IndicesPutMapping10BasicYaml10Tests
	{
		
		public class TestCreateAndUpdateMapping10Tests : YamlTestsBase
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public TestCreateAndUpdateMapping10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void TestCreateAndUpdateMappingTests()
			{

				//do indices.create 
				
				_status = this._client.IndicesCreatePost("test_index", null);
				_response = _status.Deserialize<dynamic>();

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
				_status = this._client.IndicesPutMappingPost("test_index", "test_type", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.get_mapping 
				
				_status = this._client.IndicesGetMapping("test_index");
				_response = _status.Deserialize<dynamic>();

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
				_status = this._client.IndicesPutMappingPost("test_index", "test_type", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.get_mapping 
				
				_status = this._client.IndicesGetMapping("test_index");
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
