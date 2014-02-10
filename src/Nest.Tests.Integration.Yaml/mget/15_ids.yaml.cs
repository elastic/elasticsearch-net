using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Mget
{
	public partial class Mget15IdsYaml15Tests
	{
		
		public class Ids15Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public Ids15Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void IdsTests()
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
				_status = this._client.IndexPost("test_1", "test_2", "2", _body);
				_response = _status.Deserialize<dynamic>();

				//do cluster.health 
				
				_status = this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status","yellow")
				);
				_response = _status.Deserialize<dynamic>();

				//do mget 
				_body = new {
					ids= new [] {
						"1",
						"2"
					}
				};
				_status = this._client.MgetPost("test_1", "test", _body);
				_response = _status.Deserialize<dynamic>();

				//do mget 
				_body = new {
					ids= new [] {
						"1",
						"2"
					}
				};
				_status = this._client.MgetPost("test_1", _body);
				_response = _status.Deserialize<dynamic>();

				//do mget 
				_body = new {
					ids= new string[] {}
				};
				_status = this._client.MgetPost("test_1", _body);
				_response = _status.Deserialize<dynamic>();

				//do mget 
				_body = new {};
				_status = this._client.MgetPost("test_1", _body);
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
