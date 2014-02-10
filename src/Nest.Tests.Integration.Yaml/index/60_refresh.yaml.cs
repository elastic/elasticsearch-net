using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Index
{
	public partial class Index60RefreshYaml60Tests
	{
		
		public class Refresh60Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public Refresh60Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void RefreshTests()
			{

				//do indices.create 
				_body = new {
					settings= new {
						index= new { refresh_interval= "-1" }
					}
				};
				this._client.IndicesCreatePost("test_1", _body, nv=>nv);

				//do cluster.health 
				
				this._client.ClusterHealthGet(nv=>nv);

				//do index 
				_body = new {
					foo= "bar"
				};
				this._client.IndexPost("test_1", "test", "1", _body, nv=>nv);

				//do search 
				_body = new {
					query= new {
						term= new {
							_id= "1"
						}
					}
				};
				this._client.SearchPost("test_1", "test", _body, nv=>nv);

				//do index 
				_body = new {
					foo= "bar"
				};
				this._client.IndexPost("test_1", "test", "2", _body, nv=>nv);

				//do search 
				_body = new {
					query= new {
						term= new {
							_id= "2"
						}
					}
				};
				this._client.SearchPost("test_1", "test", _body, nv=>nv);
			}
		}
	}
}
