using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Search
{
	public partial class Search20DefaultValuesYaml20Tests
	{
		
		public class DefaultIndex20Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public DefaultIndex20Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void DefaultIndexTests()
			{

				//do indices.create 
				
				_status = this._client.IndicesCreatePost("test_2", null);
				_response = _status.Deserialize<dynamic>();

				//do indices.create 
				
				_status = this._client.IndicesCreatePost("test_1", null);
				_response = _status.Deserialize<dynamic>();

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_2", "test", "42", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.refresh 
				
				_status = this._client.IndicesRefreshGet("System.Collections.Generic.List`1[System.Object]");
				_response = _status.Deserialize<dynamic>();

				//do search 
				_body = new {
					query= new {
						match= new {
							foo= "bar"
						}
					}
				};
				_status = this._client.SearchPost("_all", "test", _body);
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
