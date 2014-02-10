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
	public partial class Delete25ExternalVersionYaml25Tests
	{
		
		public class ExternalVersion25Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public ExternalVersion25Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void ExternalVersionTests()
			{

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body, nv=>nv
					.Add("version_type","external")
					.Add("version","5")
				);
				_response = _status.Deserialize<dynamic>();

				//do delete 
				
				_status = this._client.Delete("test_1", "test", "1", nv=>nv
					.Add("version_type","external")
					.Add("version","4")
				);
				_response = _status.Deserialize<dynamic>();

				//do delete 
				
				_status = this._client.Delete("test_1", "test", "1", nv=>nv
					.Add("version_type","external")
					.Add("version","6")
				);
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
