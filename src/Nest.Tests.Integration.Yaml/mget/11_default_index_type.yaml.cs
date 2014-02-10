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
	public partial class Mget11DefaultIndexTypeYaml11Tests
	{
		
		public class DefaultIndexType11Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public DefaultIndexType11Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void DefaultIndexTypeTests()
			{

				//do indices.create 
				
				_status = this._client.IndicesCreatePost("test_2", null);
				_response = _status.Deserialize<dynamic>();

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//do cluster.health 
				
				_status = this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status","yellow")
				);
				_response = _status.Deserialize<dynamic>();

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_index= "test_2",
							_id= "1"
						},
						new {
							_type= "none",
							_id= "1"
						},
						new {
							_id= "2"
						},
						new {
							_id= "1"
						}
					}
				};
				_status = this._client.MgetPost("test_1", "test", _body);
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
