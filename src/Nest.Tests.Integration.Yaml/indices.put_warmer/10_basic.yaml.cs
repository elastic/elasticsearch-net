using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesPutWarmer
{
	public partial class IndicesPutWarmer10BasicYaml10Tests
	{
		
		public class BasicTestForWarmers10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public BasicTestForWarmers10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void BasicTestForWarmersTests()
			{

				//do indices.create 
				
				_status = this._client.IndicesCreatePost("test_index", null);
				_response = _status.Deserialize<dynamic>();

				//do cluster.health 
				
				_status = this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status","yellow")
				);
				_response = _status.Deserialize<dynamic>();

				//do indices.get_warmer 
				
				_status = this._client.IndicesGetWarmer("test_index", "test_warmer");
				_response = _status.Deserialize<dynamic>();

				//do indices.put_warmer 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				_status = this._client.IndicesPutWarmer("test_index", "test_warmer", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.get_warmer 
				
				_status = this._client.IndicesGetWarmer("test_index", "test_warmer");
				_response = _status.Deserialize<dynamic>();

				//do indices.delete_warmer 
				
				_status = this._client.IndicesDeleteWarmer("test_index");
				_response = _status.Deserialize<dynamic>();

				//do indices.get_warmer 
				
				_status = this._client.IndicesGetWarmer("test_index", "test_warmer");
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
