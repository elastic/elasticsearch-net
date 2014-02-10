using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesGetTemplate
{
	public partial class IndicesGetTemplate10BasicYaml10Tests
	{
		
		public class GetTemplate10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public GetTemplate10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void GetTemplateTests()
			{

				//do indices.put_template 
				_body = new {
					template= "test-*",
					settings= new {
						number_of_shards= "1",
						number_of_replicas= "0"
					}
				};
				this._client.IndicesPutTemplatePost("test", _body, nv=>nv);

				//do indices.get_template 
				
				this._client.IndicesGetTemplate("test", nv=>nv);
			}
		}
		
		public class GetAllTemplates10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public GetAllTemplates10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void GetAllTemplatesTests()
			{

				//do indices.put_template 
				_body = new {
					template= "test-*",
					settings= new {
						number_of_shards= "1"
					}
				};
				this._client.IndicesPutTemplatePost("test", _body, nv=>nv);

				//do indices.put_template 
				_body = new {
					template= "test2-*",
					settings= new {
						number_of_shards= "1"
					}
				};
				this._client.IndicesPutTemplatePost("test2", _body, nv=>nv);

				//do indices.get_template 
				
				this._client.IndicesGetTemplate(nv=>nv);
			}
		}
	}
}
