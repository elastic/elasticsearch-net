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
	public partial class Update80FieldsYaml80Tests
	{
		
		public class Fields80Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public Fields80Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void FieldsTests()
			{

				//do update 
				_body = new {
					doc= new {
						foo= "baz"
					},
					upsert= new {
						foo= "bar"
					}
				};
				this._client.UpdatePost("test_1", "test", "1", _body, nv=>nv);
			}
		}
	}
}
