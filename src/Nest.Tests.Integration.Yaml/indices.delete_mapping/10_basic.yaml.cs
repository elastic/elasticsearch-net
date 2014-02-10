using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesDeleteMapping
{
	public partial class IndicesDeleteMapping10BasicYaml10Tests
	{
		
		public class DeleteMappingTests10Tests
		{
			private readonly RawElasticClient _client;
		
			public DeleteMappingTests10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void DeleteMappingTestsTests()
			{

				//do indices.create 
				this._client.IndicesCreatePost("test_index", "SERIALIZED BODY HERE", nv=>nv);

				//do indices.exists_type 
				this._client.IndicesExistsTypeHead("test_index", "test_type", nv=>nv);

				//do indices.delete_mapping 
				this._client.IndicesDeleteMapping("test_index", "test_type", nv=>nv);

				//do indices.exists_type 
				this._client.IndicesExistsTypeHead("test_index", "test_type", nv=>nv);
			}
		}
	}
}