using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesGetFieldMapping
{
	public partial class IndicesGetFieldMapping50FieldWildcardsYaml50Tests
	{
		
		public class Setup50Tests
		{
			private readonly RawElasticClient _client;
		
			public Setup50Tests()
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
		
		public class GetFieldMappingWithForFields50Tests
		{
			private readonly RawElasticClient _client;
		
			public GetFieldMappingWithForFields50Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void GetFieldMappingWithForFieldsTests()
			{

				//do indices.get_field_mapping 
				this._client.IndicesGetFieldMapping("*", nv=>nv);
			}
		}
		
		public class GetFieldMappingWithTForFields50Tests
		{
			private readonly RawElasticClient _client;
		
			public GetFieldMappingWithTForFields50Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void GetFieldMappingWithTForFieldsTests()
			{

				//do indices.get_field_mapping 
				this._client.IndicesGetFieldMapping("t*", nv=>nv);
			}
		}
		
		public class GetFieldMappingWithT1ForFields50Tests
		{
			private readonly RawElasticClient _client;
		
			public GetFieldMappingWithT1ForFields50Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void GetFieldMappingWithT1ForFieldsTests()
			{

				//do indices.get_field_mapping 
				this._client.IndicesGetFieldMapping("*t1", nv=>nv);
			}
		}
		
		public class GetFieldMappingWithWildcardedRelativeNames50Tests
		{
			private readonly RawElasticClient _client;
		
			public GetFieldMappingWithWildcardedRelativeNames50Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void GetFieldMappingWithWildcardedRelativeNamesTests()
			{

				//do indices.get_field_mapping 
				this._client.IndicesGetFieldMapping("i_*", nv=>nv);
			}
		}
	}
}