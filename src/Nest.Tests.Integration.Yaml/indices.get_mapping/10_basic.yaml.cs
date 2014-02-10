using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesGetMapping
{
	public partial class IndicesGetMapping10BasicYaml10Tests
	{
		
		public class Setup10Tests
		{
			private readonly RawElasticClient _client;
		
			public Setup10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void SetupTests()
			{

				//do indices.create 
				this._client.IndicesCreatePost("test_index", "SERIALIZED BODY HERE", nv=>nv);
			}
		}
		
		public class GetIndexMapping10Tests
		{
			private readonly RawElasticClient _client;
		
			public GetIndexMapping10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void GetIndexMappingTests()
			{

				//do indices.get_mapping 
				this._client.IndicesGetMapping("test_index", nv=>nv);
			}
		}
		
		public class GetTypeMapping10Tests
		{
			private readonly RawElasticClient _client;
		
			public GetTypeMapping10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void GetTypeMappingTests()
			{

				//do indices.get_mapping 
				this._client.IndicesGetMapping("test_index", "test_type", nv=>nv);
			}
		}
	}
}