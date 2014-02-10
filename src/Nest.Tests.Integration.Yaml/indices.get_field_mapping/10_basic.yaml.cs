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
	public partial class IndicesGetFieldMapping10BasicYaml10Tests
	{
		
		public class Setup10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
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
				_body = new {
					mappings= new {
						test_type= new {
							properties= new {
								text= new {
									type= "string"
								}
							}
						}
					}
				};
				_status = this._client.IndicesCreatePost("test_index", _body);
				_response = _status.Deserialize<dynamic>();
			}
		}
		
		public class GetFieldMappingWithNoIndexAndType10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public GetFieldMappingWithNoIndexAndType10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void GetFieldMappingWithNoIndexAndTypeTests()
			{

				//do indices.get_field_mapping 
				
				_status = this._client.IndicesGetFieldMapping("text");
				_response = _status.Deserialize<dynamic>();
			}
		}
		
		public class GetFieldMappingByIndexOnly10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public GetFieldMappingByIndexOnly10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void GetFieldMappingByIndexOnlyTests()
			{

				//do indices.get_field_mapping 
				
				_status = this._client.IndicesGetFieldMapping("test_index", "text");
				_response = _status.Deserialize<dynamic>();
			}
		}
		
		public class GetFieldMappingByTypeField10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public GetFieldMappingByTypeField10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void GetFieldMappingByTypeFieldTests()
			{

				//do indices.get_field_mapping 
				
				_status = this._client.IndicesGetFieldMapping("test_index", "test_type", "text");
				_response = _status.Deserialize<dynamic>();
			}
		}
		
		public class GetFieldMappingByTypeFieldWithAnotherFieldThatDoesntExist10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public GetFieldMappingByTypeFieldWithAnotherFieldThatDoesntExist10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void GetFieldMappingByTypeFieldWithAnotherFieldThatDoesntExistTests()
			{

				//do indices.get_field_mapping 
				
				_status = this._client.IndicesGetFieldMapping("test_index", "test_type", "System.Collections.Generic.List`1[System.Object]");
				_response = _status.Deserialize<dynamic>();
			}
		}
		
		public class GetFieldMappingWithIncludeDefaults10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
			public GetFieldMappingWithIncludeDefaults10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void GetFieldMappingWithIncludeDefaultsTests()
			{

				//do indices.get_field_mapping 
				
				_status = this._client.IndicesGetFieldMapping("test_index", "test_type", "text", nv=>nv
					.Add("include_defaults","true")
				);
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
