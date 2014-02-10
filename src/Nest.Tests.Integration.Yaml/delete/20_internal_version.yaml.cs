using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Delete
{
	public partial class Delete20InternalVersionYaml20Tests
	{
		
		public class InternalVersion20Tests : YamlTestsBase
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public InternalVersion20Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void InternalVersionTests()
			{

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//do delete 
				
				_status = this._client.Delete("test_1", "test", "1", nv=>nv
					.Add("version","2")
				);
				_response = _status.Deserialize<dynamic>();

				//do delete 
				
				_status = this._client.Delete("test_1", "test", "1", nv=>nv
					.Add("version","1")
				);
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
