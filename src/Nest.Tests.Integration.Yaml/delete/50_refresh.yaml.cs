using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Delete
{
	public partial class Delete50RefreshYaml50Tests
	{
		
		public class Refresh50Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public Refresh50Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void RefreshTests()
			{

				//do indices.create 
				_body = new {
					settings= new {
						refresh_interval= "-1",
						number_of_replicas= "0"
					}
				};
				_status = this._client.IndicesCreatePost("test_1", _body);
				_response = _status.Deserialize<dynamic>();

				//do cluster.health 
				
				_status = this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status","green")
				);
				_response = _status.Deserialize<dynamic>();

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body, nv=>nv
					.Add("refresh","1")
				);
				_response = _status.Deserialize<dynamic>();

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_1", "test", "2", _body, nv=>nv
					.Add("refresh","1")
				);
				_response = _status.Deserialize<dynamic>();

				//do search 
				_body = new {
					query= new {
						terms= new {
							_id= new [] {
								"1",
								"2"
							}
						}
					}
				};
				_status = this._client.SearchPost("test_1", "test", _body);
				_response = _status.Deserialize<dynamic>();

				//do delete 
				
				_status = this._client.Delete("test_1", "test", "1");
				_response = _status.Deserialize<dynamic>();

				//do search 
				_body = new {
					query= new {
						terms= new {
							_id= new [] {
								"1",
								"2"
							}
						}
					}
				};
				_status = this._client.SearchPost("test_1", "test", _body);
				_response = _status.Deserialize<dynamic>();

				//do delete 
				
				_status = this._client.Delete("test_1", "test", "2", nv=>nv
					.Add("refresh","1")
				);
				_response = _status.Deserialize<dynamic>();

				//do search 
				_body = new {
					query= new {
						terms= new {
							_id= new [] {
								"1",
								"2"
							}
						}
					}
				};
				_status = this._client.SearchPost("test_1", "test", _body);
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
