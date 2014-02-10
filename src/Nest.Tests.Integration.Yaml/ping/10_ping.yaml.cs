using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Ping
{
	public partial class Ping10PingYaml10Tests
	{
		
		public class Ping10Tests : YamlTestsBase
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public Ping10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void PingTests()
			{

				//do ping 
				
				_status = this._client.PingHead();
				_response = _status.Deserialize<dynamic>();

				//is_true ; 
				this.IsTrue(_response);
			}
		}
	}
}
