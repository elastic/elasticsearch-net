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
	public partial class Mget20FieldsPre0903Yaml20Tests
	{
		
		public class Fields20Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public Fields20Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void FieldsTests()
			{

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//do cluster.health 
				
				_status = this._client.ClusterHealthGet(, nv=>nv
					.Add("wait_for_status","yellow")
				);
				_response = _status.Deserialize<dynamic>();

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_id= "1"
						},
						new {
							_id= "1",
							fields= "foo"
						},
						new {
							_id= "1",
							fields= new [] {
								"foo"
							}
						},
						new {
							_id= "1",
							fields= new [] {
								"foo",
								"_source"
							}
						}
					}
				};
				_status = this._client.MgetPost("test_1", "test", _body);
				_response = _status.Deserialize<dynamic>();

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_id= "1"
						},
						new {
							_id= "1",
							fields= "foo"
						},
						new {
							_id= "1",
							fields= new [] {
								"foo"
							}
						},
						new {
							_id= "1",
							fields= new [] {
								"foo",
								"_source"
							}
						}
					}
				};
				_status = this._client.MgetPost("test_1", "test", _body, nv=>nv
					.Add("fields","foo")
				);
				_response = _status.Deserialize<dynamic>();

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_id= "1"
						},
						new {
							_id= "1",
							fields= "foo"
						},
						new {
							_id= "1",
							fields= new [] {
								"foo"
							}
						},
						new {
							_id= "1",
							fields= new [] {
								"foo",
								"_source"
							}
						}
					}
				};
				_status = this._client.MgetPost("test_1", "test", _body, nv=>nv
					.Add("fields","System.Collections.Generic.List`1[System.Object]")
				);
				_response = _status.Deserialize<dynamic>();

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_id= "1"
						},
						new {
							_id= "1",
							fields= "foo"
						},
						new {
							_id= "1",
							fields= new [] {
								"foo"
							}
						},
						new {
							_id= "1",
							fields= new [] {
								"foo",
								"_source"
							}
						}
					}
				};
				_status = this._client.MgetPost("test_1", "test", _body, nv=>nv
					.Add("fields","System.Collections.Generic.List`1[System.Object]")
				);
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
