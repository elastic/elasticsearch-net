using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Scroll
{
	public partial class Scroll10BasicYaml10Tests
	{
		
		public class BasicScroll10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public BasicScroll10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void BasicScrollTests()
			{

				//do indices.create 
				
				_status = this._client.IndicesCreatePost("test_scroll", null);
				_response = _status.Deserialize<dynamic>();

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_scroll", "test", "42", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.refresh 
				
				_status = this._client.IndicesRefreshGet();
				_response = _status.Deserialize<dynamic>();

				//do search 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				_status = this._client.SearchPost("test_scroll", _body, nv=>nv
					.Add("search_type","scan")
					.Add("scroll","1m")
				);
				_response = _status.Deserialize<dynamic>();

				//do scroll 
				
				_status = this._client.ScrollGet("$scroll_id");
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
