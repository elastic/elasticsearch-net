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
	public partial class Create15WithoutIdYaml15Tests
	{
		
		public class CreateWithoutId15Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public CreateWithoutId15Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void CreateWithoutIdTests()
			{

				//do create 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_1", "test", _body, nv=>nv
					.Add("op_type","create")
				);
				_response = _status.Deserialize<dynamic>();

				//do get 
				
				_status = this._client.Get("test_1", "test", "$id");
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
