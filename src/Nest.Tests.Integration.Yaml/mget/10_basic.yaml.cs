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
	public partial class Mget10BasicYaml10Tests
	{
		
		public class BasicMultiGet10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public BasicMultiGet10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void BasicMultiGetTests()
			{

				//do indices.create 
				
				this._client.IndicesCreatePost("test_2", null, nv=>nv);

				//do index 
				_body = new {
					foo= "bar"
				};
				this._client.IndexPost("test_1", "test", "1", _body, nv=>nv);

				//do indices.flush 
				
				this._client.IndicesFlushGet(nv=>nv);

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_index= "test_2",
							_type= "test",
							_id= "1"
						},
						new {
							_index= "test_1",
							_type= "none",
							_id= "1"
						},
						new {
							_index= "test_1",
							_type= "test",
							_id= "2"
						},
						new {
							_index= "test_1",
							_type= "test",
							_id= "1"
						}
					}
				};
				this._client.MgetPost(_body, nv=>nv);
			}
		}
	}
}
