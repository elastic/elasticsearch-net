using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Create
{
	public partial class Create35ExternalVersionYaml35Tests
	{
		
		public class ExternalVersion35Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public ExternalVersion35Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void ExternalVersionTests()
			{

				//do create 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body, nv=>nv
					.Add("version_type","external")
					.Add("version","5")
					.Add("op_type","create")
				);
				_response = _status.Deserialize<dynamic>();

				//do create 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body, nv=>nv
					.Add("version_type","external")
					.Add("version","5")
					.Add("op_type","create")
				);
				_response = _status.Deserialize<dynamic>();

				//do create 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body, nv=>nv
					.Add("version_type","external")
					.Add("version","6")
					.Add("op_type","create")
				);
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
