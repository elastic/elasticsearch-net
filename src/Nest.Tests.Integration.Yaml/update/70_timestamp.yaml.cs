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
	public partial class Update70TimestampYaml70Tests
	{
		
		public class Timestamp70Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public Timestamp70Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void TimestampTests()
			{

				//do indices.create 
				_body = new {
					mappings= new {
						test= new {
							_timestamp= new {
								enabled= "1",
								store= "yes"
							}
						}
					}
				};
				this._client.IndicesCreatePost("test_1", _body, nv=>nv);

				//do cluster.health 
				
				this._client.ClusterHealthGet(nv=>nv);

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

				//do get 
				
				this._client.Get("test_1", "test", "1", nv=>nv);

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

				//do get 
				
				this._client.Get("test_1", "test", "1", nv=>nv);

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

				//do get 
				
				this._client.Get("test_1", "test", "1", nv=>nv);
			}
		}
	}
}
