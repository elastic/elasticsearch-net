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
	public partial class Delete30RoutingYaml30Tests
	{
		
		public class Routing30Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public Routing30Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void RoutingTests()
			{

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body, nv=>nv
					.Add("routing","5")
				);
				_response = _status.Deserialize<dynamic>();

				//do cluster.health 
				
				_status = this._client.ClusterHealthGet(, nv=>nv
					.Add("wait_for_status","yellow")
				);
				_response = _status.Deserialize<dynamic>();

				//do delete 
				
				_status = this._client.Delete("test_1", "test", "1", nv=>nv
					.Add("routing","4")
				);
				_response = _status.Deserialize<dynamic>();

				//do delete 
				
				_status = this._client.Delete("test_1", "test", "1", nv=>nv
					.Add("routing","5")
				);
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
