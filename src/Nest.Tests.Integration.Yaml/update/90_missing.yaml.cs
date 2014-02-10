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
	public partial class Update90MissingYaml90Tests
	{
		
		public class MissingDocumentPartialDoc90Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public MissingDocumentPartialDoc90Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void MissingDocumentPartialDocTests()
			{

				//do update 
				_body = new {
					doc= new {
						foo= "bar"
					}
				};
				this._client.UpdatePost("test_1", "test", "1", _body, nv=>nv);

				//do update 
				_body = new {
					doc= new {
						foo= "bar"
					}
				};
				this._client.UpdatePost("test_1", "test", "1", _body, nv=>nv);
			}
		}
		
		public class MissingDocumentScript90Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public MissingDocumentScript90Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void MissingDocumentScriptTests()
			{

				//do update 
				_body = new {
					script= "ctx._source.foo = bar",
					@params= new {
						bar= "xxx"
					}
				};
				this._client.UpdatePost("test_1", "test", "1", _body, nv=>nv);

				//do update 
				_body = new {
					script= "ctx._source.foo = bar",
					@params= new {
						bar= "xxx"
					}
				};
				this._client.UpdatePost("test_1", "test", "1", _body, nv=>nv);
			}
		}
	}
}
