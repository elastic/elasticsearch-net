using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesPutMapping
{
	public partial class IndicesPutMapping10BasicYaml10Tests
	{
		
		public class TestCreateAndUpdateMapping10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
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
				
				this._client.IndicesCreatePost("test_index", null, nv=>nv);

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
				this._client.IndicesPutMappingPost("test_index", "test_type", _body, nv=>nv);

				//do indices.get_mapping 
				
				this._client.IndicesGetMapping("test_index", nv=>nv);

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
				this._client.IndicesPutMappingPost("test_index", "test_type", _body, nv=>nv);

				//do indices.get_mapping 
				
				this._client.IndicesGetMapping("test_index", nv=>nv);
			}
		}
	}
}
