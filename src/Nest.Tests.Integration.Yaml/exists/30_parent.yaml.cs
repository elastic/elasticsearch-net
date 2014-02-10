using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Exists
{
	public partial class Exists30ParentYaml30Tests
	{
		
		public class Parent30Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public Parent30Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void ParentTests()
			{

				//do indices.create 
				_body = new {
					mappings= new {
						test= new {
							_parent= new {
								type= "foo"
							}
						}
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

				//do exists 
				
				this._client.ExistsHead("test_1", "test", "1", nv=>nv);

				//do exists 
				
				this._client.ExistsHead("test_1", "test", "1", nv=>nv);
			}
		}
	}
}
