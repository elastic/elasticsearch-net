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
	public partial class Update15ScriptYaml15Tests
	{
		
		public class Script15Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public Script15Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void ScriptTests()
			{

				//do index 
				_body = new {
					foo= "bar",
					count= "1"
				};
				this._client.IndexPost("test_1", "test", "1", _body, nv=>nv);

				//do update 
				_body = new {
					lang= "mvel",
					script= "ctx._source.foo = bar",
					@params= new {
						bar= "xxx"
					}
				};
				this._client.UpdatePost("test_1", "test", "1", _body, nv=>nv);

				//do get 
				
				this._client.Get("test_1", "test", "1", nv=>nv);

				//do update 
				
				this._client.UpdatePost("test_1", "test", "1", null, nv=>nv);

				//do get 
				
				this._client.Get("test_1", "test", "1", nv=>nv);

				//do update 
				_body = new {
					script= "1",
					lang= "doesnotexist",
					@params= new {
						bar= "xxx"
					}
				};
				this._client.UpdatePost("test_1", "test", "1", _body, nv=>nv);

				//do update 
				
				this._client.UpdatePost("test_1", "test", "1", null, nv=>nv);
			}
		}
	}
}
