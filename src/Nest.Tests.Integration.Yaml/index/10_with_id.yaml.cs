using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Index
{
	public partial class Index10WithIdYaml10Tests
	{
		
		public class IndexWithId10Tests : YamlTestsBase
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public IndexWithId10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void IndexWithIdTests()
			{

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test-weird-index-Ã¤Â¸Â­Ã¦â€“â€¡", "weird.type", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//is_true .ok; 
				this.IsTrue(_response.ok);

				//do get 
				
				_status = this._client.Get("test-weird-index-Ã¤Â¸Â­Ã¦â€“â€¡", "weird.type", "1");
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
