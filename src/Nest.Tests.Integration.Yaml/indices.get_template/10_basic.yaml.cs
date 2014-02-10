using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesGetTemplate
{
	public partial class IndicesGetTemplate10BasicYaml10Tests
	{
		
		public class GetTemplate10Tests : YamlTestsBase
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public GetTemplate10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void GetTemplateTests()
			{

				//do indices.put_template 
				_body = new {
					template= "test-*",
					settings= new {
						number_of_shards= "1",
						number_of_replicas= "0"
					}
				};
				_status = this._client.IndicesPutTemplatePost("test", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.get_template 
				
				_status = this._client.IndicesGetTemplate("test");
				_response = _status.Deserialize<dynamic>();
			}
		}
		
		public class GetAllTemplates10Tests : YamlTestsBase
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public GetAllTemplates10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void GetAllTemplatesTests()
			{

				//do indices.put_template 
				_body = new {
					template= "test-*",
					settings= new {
						number_of_shards= "1"
					}
				};
				_status = this._client.IndicesPutTemplatePost("test", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.put_template 
				_body = new {
					template= "test2-*",
					settings= new {
						number_of_shards= "1"
					}
				};
				_status = this._client.IndicesPutTemplatePost("test2", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.get_template 
				
				_status = this._client.IndicesGetTemplate();
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
