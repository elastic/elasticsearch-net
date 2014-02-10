using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Msearch
{
	public partial class Msearch10BasicYaml10Tests
	{
		
		public class BasicMultiSearch10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public BasicMultiSearch10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void BasicMultiSearchTests()
			{

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//do index 
				_body = new {
					foo= "baz"
				};
				_status = this._client.IndexPost("test_1", "test", "2", _body);
				_response = _status.Deserialize<dynamic>();

				//do index 
				_body = new {
					foo= "foo"
				};
				_status = this._client.IndexPost("test_1", "test", "3", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.refresh 
				
				_status = this._client.IndicesRefreshGet();
				_response = _status.Deserialize<dynamic>();

				//do msearch 
				_body = @"{""index"":""test_1""}
{""query"":{""match_all"":{}}}
{""index"":""test_2""}
{""query"":{""match_all"":{}}}
{""search_type"":""count"",""index"":""test_1""}
{""query"":{""match"":{""foo"":""bar""}}}";
				_status = this._client.MsearchPost(_body);
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
