using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Update
{
	public partial class Update10DocYaml10Tests
	{
		
		public class PartialDocument10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public PartialDocument10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void PartialDocumentTests()
			{

				//do index 
				_body = new {
					foo= "bar",
					count= "1",
					nested= new {
						one= "1",
						two= "2"
					}
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//do update 
				_body = new {
					doc= new {
						foo= "baz",
						nested= new {
							one= "3"
						}
					}
				};
				_status = this._client.UpdatePost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//do get 
				
				_status = this._client.Get("test_1", "test", "1");
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
