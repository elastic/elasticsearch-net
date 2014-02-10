using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesPutSettings
{
	public partial class IndicesPutSettings10BasicYaml10Tests
	{
		
		public class TestIndicesSettings10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public TestIndicesSettings10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void TestIndicesSettingsTests()
			{

				//do indices.create 
				_body = new {
					settings= new {
						index= new {
							number_of_replicas= "0"
						}
					}
				};
				this._client.IndicesCreatePost("test-index", _body, nv=>nv);

				//do indices.get_settings 
				
				this._client.IndicesGetSettings("test-index", nv=>nv);

				//do indices.put_settings 
				_body = new {
					number_of_replicas= "1"
				};
				this._client.IndicesPutSettings(_body, nv=>nv);

				//do indices.get_settings 
				
				this._client.IndicesGetSettings(nv=>nv);
			}
		}
	}
}
