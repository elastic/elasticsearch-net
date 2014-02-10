using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Mlt
{
	public partial class Mlt10BasicYaml10Tests
	{
		
		public class BasicMlt10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public BasicMlt10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void BasicMltTests()
			{

				//do index 
				_body = new {
					foo= "bar",
					title= "howdy"
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.refresh 
				
				_status = this._client.IndicesRefreshGet();
				_response = _status.Deserialize<dynamic>();

				//do cluster.health 
				
				_status = this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status","green")
					.Add("timeout","1s")
				);
				_response = _status.Deserialize<dynamic>();

				//do mlt 
				
				_status = this._client.MltGet("test_1", "test", "1", nv=>nv
					.Add("mlt_fields","title")
				);
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
