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
	public partial class Update22DocAsUpsertYaml22Tests
	{
		
		public class DocAsUpsert22Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public DocAsUpsert22Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void DocAsUpsertTests()
			{

				//do update 
				_body = new {
					doc= new {
						foo= "bar",
						count= "1"
					},
					doc_as_upsert= "1"
				};
				this._client.UpdatePost("test_1", "test", "1", _body, nv=>nv);

				//do get 
				
				this._client.Get("test_1", "test", "1", nv=>nv);

				//do update 
				_body = new {
					doc= new {
						count= "2"
					},
					doc_as_upsert= "1"
				};
				this._client.UpdatePost("test_1", "test", "1", _body, nv=>nv);

				//do get 
				
				this._client.Get("test_1", "test", "1", nv=>nv);
			}
		}
	}
}
