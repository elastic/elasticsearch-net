using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesGetFieldMapping
{
	public partial class IndicesGetFieldMapping30MissingTypeYaml30Tests
	{
		
		public class Raise404WhenTypeDoesntExist30Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public Raise404WhenTypeDoesntExist30Tests()
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
				this._client.IndicesCreatePost("test_index", _body, nv=>nv);

				//do indices.get_field_mapping 
				
				this._client.IndicesGetFieldMapping("test_index", "not_test_type", "text", nv=>nv);
			}
		}
	}
}
