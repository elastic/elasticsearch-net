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
				this._client.IndicesPutTemplatePost("test", "SERIALIZED BODY HERE", nv=>nv);

				//do indices.get_template 
				this._client.IndicesGetTemplate("test", nv=>nv);
			}
		}
		
		public class GetAllTemplates10Tests
		{
			private readonly RawElasticClient _client;
		
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
				this._client.IndicesPutTemplatePost("test", "SERIALIZED BODY HERE", nv=>nv);

				//do indices.put_template 
				this._client.IndicesPutTemplatePost("test2", "SERIALIZED BODY HERE", nv=>nv);

				//do indices.get_template 
				this._client.IndicesGetTemplate(nv=>nv);
			}
		}
	}
}