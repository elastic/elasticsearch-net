using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Exists
{
	public partial class Exists70DefaultsYaml70Tests
	{
		
		public class ClientSideDefaultType70Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public ClientSideDefaultType70Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void ClientSideDefaultTypeTests()
			{

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//do exists 
				
				_status = this._client.ExistsHead("test_1", "_all", "1");
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
