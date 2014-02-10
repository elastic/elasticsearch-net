using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesGetMapping
{
	public partial class IndicesGetMapping10BasicYaml10Tests
	{
		
		public class Setup10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public Setup10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void SetupTests()
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
				_status = this._client.IndicesCreatePost("test_index", _body);
				_response = _status.Deserialize<dynamic>();
			}
		}
		
		public class GetIndexMapping10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public GetIndexMapping10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void GetIndexMappingTests()
			{

				//do indices.get_mapping 
				
				_status = this._client.IndicesGetMapping("test_index");
				_response = _status.Deserialize<dynamic>();
			}
		}
		
		public class GetTypeMapping10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public GetTypeMapping10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void GetTypeMappingTests()
			{

				//do indices.get_mapping 
				
				_status = this._client.IndicesGetMapping("test_index", "test_type");
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
