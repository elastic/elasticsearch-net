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
	public partial class Mget55ParentWithRoutingYaml55Tests
	{
		
		public class Parent55Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public Parent55Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void ParentTests()
			{

				//do indices.create 
				_body = new {
					mappings= new {
						test= new {
							_parent= new {
								type= "foo"
							}
						}
					},
					settings= new {
						number_of_replicas= "0"
					}
				};
				_status = this._client.IndicesCreatePost("test_1", _body);
				_response = _status.Deserialize<dynamic>();

				//do cluster.health 
				
				_status = this._client.ClusterHealthGet(, nv=>nv
					.Add("wait_for_status","green")
				);
				_response = _status.Deserialize<dynamic>();

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body, nv=>nv
					.Add("parent","4")
					.Add("routing","5")
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
							parent= "4"
						},
						new {
							_id= "1",
							parent= "4",
							routing= "5"
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
