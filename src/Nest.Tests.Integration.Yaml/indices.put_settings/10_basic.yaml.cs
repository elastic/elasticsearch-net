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
				this._client.IndicesCreatePost("test-index", "SERIALIZED BODY HERE", nv=>nv);

				//do indices.get_settings 
				this._client.IndicesGetSettings("test-index", nv=>nv);

				//do indices.put_settings 
				this._client.IndicesPutSettings("SERIALIZED BODY HERE", nv=>nv);

				//do indices.get_settings 
				this._client.IndicesGetSettings(nv=>nv);
			}
		}
	}
}