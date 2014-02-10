using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesGetMapping
{
	public partial class IndicesGetMapping20MissingTypeYaml20Tests
	{
		
		public class Raise404WhenTypeDoesntExist20Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public Raise404WhenTypeDoesntExist20Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void Raise404WhenTypeDoesntExistTests()
			{

				//do indices.create 
				_body = new {
					mappings= new {
						test_type= new {
							properties= new {
								text= new {
									type= "string",
									analyzer= "whitespace"
								}
							}
						}
					}
				};
				_status = this._client.IndicesCreatePost("test_index", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.get_mapping 
				
				_status = this._client.IndicesGetMapping("test_index", "not_test_type");
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
