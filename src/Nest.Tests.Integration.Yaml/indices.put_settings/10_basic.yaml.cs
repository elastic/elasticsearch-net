using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesPutSettings
{
	public partial class IndicesPutSettings10BasicYaml10Tests
	{
		
		public class TestIndicesSettings10Tests : YamlTestsBase
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public TestIndicesSettings10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void TestIndicesSettingsTests()
			{

				//do indices.create 
				_body = new {
					settings= new {
						index= new {
							number_of_replicas= "0"
						}
					}
				};
				_status = this._client.IndicesCreatePost("test-index", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.get_settings 
				
				_status = this._client.IndicesGetSettings("test-index");
				_response = _status.Deserialize<dynamic>();

				//do indices.put_settings 
				_body = new {
					number_of_replicas= "1"
				};
				_status = this._client.IndicesPutSettings(_body);
				_response = _status.Deserialize<dynamic>();

				//do indices.get_settings 
				
				_status = this._client.IndicesGetSettings();
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
