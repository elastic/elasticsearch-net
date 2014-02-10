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
	public partial class Mget13MissingMetadataYaml13Tests
	{
		
		public class MissingMetadata13Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public MissingMetadata13Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void MissingMetadataTests()
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
							_index= "test_1",
							_type= "test"
						}
					}
				};
				this._client.MgetPost(_body, nv=>nv);

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_type= "test",
							_id= "1"
						}
					}
				};
				this._client.MgetPost(_body, nv=>nv);

				//do mget 
				_body = new {
					docs= new dynamic[] {}
				};
				this._client.MgetPost(_body, nv=>nv);

				//do mget 
				_body = new {};
				this._client.MgetPost(_body, nv=>nv);

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_index= "test_1",
							_id= "1"
						}
					}
				};
				this._client.MgetPost(_body, nv=>nv);
			}
		}
	}
}
