using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesPutMapping
{
	public partial class IndicesPutMapping10BasicYaml10Tests
	{
		
		public class TestCreateAndUpdateMapping10Tests
		{
			private readonly RawElasticClient _client;
		
			public TestCreateAndUpdateMapping10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void TestCreateAndUpdateMappingTests()
			{

				//do indices.create 
				this._client.IndicesCreatePost("test_index", null, nv=>nv);

				//do indices.put_mapping 
				this._client.IndicesPutMappingPost("test_index", "test_type", "SERIALIZED BODY HERE", nv=>nv);

				//do indices.get_mapping 
				this._client.IndicesGetMapping("test_index", nv=>nv);

				//do indices.put_mapping 
				this._client.IndicesPutMappingPost("test_index", "test_type", "SERIALIZED BODY HERE", nv=>nv);

				//do indices.get_mapping 
				this._client.IndicesGetMapping("test_index", nv=>nv);
			}
		}
	}
}