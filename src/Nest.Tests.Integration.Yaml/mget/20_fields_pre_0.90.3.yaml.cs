using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Mget
{
	public partial class Mget20FieldsPre0903Yaml20Tests
	{
		
		public class Fields20Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public Fields20Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void FieldsTests()
			{

				//do index 
				_body = new {
					foo= "bar"
				};
				this._client.IndexPost("test_1", "test", "1", _body, nv=>nv);

				//do cluster.health 
				
				this._client.ClusterHealthGet(nv=>nv);

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_id= "1"
						},
						new {
							_id= "1",
							fields= "foo"
						},
						new {
							_id= "1",
							fields= new [] {
								"foo"
							}
						},
						new {
							_id= "1",
							fields= new [] {
								"foo",
								"_source"
							}
						}
					}
				};
				this._client.MgetPost("test_1", "test", _body, nv=>nv);

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_id= "1"
						},
						new {
							_id= "1",
							fields= "foo"
						},
						new {
							_id= "1",
							fields= new [] {
								"foo"
							}
						},
						new {
							_id= "1",
							fields= new [] {
								"foo",
								"_source"
							}
						}
					}
				};
				this._client.MgetPost("test_1", "test", _body, nv=>nv);

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_id= "1"
						},
						new {
							_id= "1",
							fields= "foo"
						},
						new {
							_id= "1",
							fields= new [] {
								"foo"
							}
						},
						new {
							_id= "1",
							fields= new [] {
								"foo",
								"_source"
							}
						}
					}
				};
				this._client.MgetPost("test_1", "test", _body, nv=>nv);

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_id= "1"
						},
						new {
							_id= "1",
							fields= "foo"
						},
						new {
							_id= "1",
							fields= new [] {
								"foo"
							}
						},
						new {
							_id= "1",
							fields= new [] {
								"foo",
								"_source"
							}
						}
					}
				};
				this._client.MgetPost("test_1", "test", _body, nv=>nv);
			}
		}
	}
}
