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
	public partial class Update20DocUpsertYaml20Tests
	{
		
		public class DocUpsert20Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public DocUpsert20Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void DocUpsertTests()
			{

				//do update 
				_body = new {
					doc= new {
						foo= "bar",
						count= "1"
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
					doc= new {
						foo= "bar",
						count= "1"
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
