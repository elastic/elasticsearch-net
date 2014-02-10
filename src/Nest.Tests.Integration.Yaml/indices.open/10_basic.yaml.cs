using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesOpen
{
	public partial class IndicesOpen10BasicYaml10Tests
	{
		
		public class BasicTestForIndexOpenClose10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public BasicTestForIndexOpenClose10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void BasicTestForIndexOpenCloseTests()
			{

				//do indices.create 
				
				_status = this._client.IndicesCreatePost("test_index", null);
				_response = _status.Deserialize<dynamic>();

				//do cluster.health 
				
				_status = this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status","yellow")
				);
				_response = _status.Deserialize<dynamic>();

				//do indices.close 
				
				_status = this._client.IndicesClosePost("test_index");
				_response = _status.Deserialize<dynamic>();

				//do search 
				
				_status = this._client.SearchGet("test_index");
				_response = _status.Deserialize<dynamic>();

				//do indices.open 
				
				_status = this._client.IndicesOpenPost("test_index");
				_response = _status.Deserialize<dynamic>();

				//do cluster.health 
				
				_status = this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status","yellow")
				);
				_response = _status.Deserialize<dynamic>();

				//do search 
				
				_status = this._client.SearchGet("test_index");
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
