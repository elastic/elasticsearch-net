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
	public partial class Update25ScriptUpsertYaml25Tests
	{
		
		public class ScriptUpsert25Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public ScriptUpsert25Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void ScriptUpsertTests()
			{

				//do update 
				_body = new {
					script= "ctx._source.foo = bar",
					@params= new {
						bar= "xxx"
					},
					upsert= new {
						foo= "baz"
					}
				};
				this._client.UpdatePost("test_1", "test", "1", _body, nv=>nv);

				//do get 
				
				this._client.Get("test_1", "test", "1", nv=>nv);

				//do update 
				_body = new {
					script= "ctx._source.foo = bar",
					@params= new {
						bar= "xxx"
					},
					upsert= new {
						foo= "baz"
					}
				};
				this._client.UpdatePost("test_1", "test", "1", _body, nv=>nv);

				//do get 
				
				this._client.Get("test_1", "test", "1", nv=>nv);
			}
		}
	}
}
