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
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
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
				_body = new {
					mappings= new {
						test_type= new {
							properties= new {
								t1= new {
									type= "string"
								},
								t2= new {
									type= "string"
								},
								obj= new {
									path= "just_name",
									properties= new {
										t1= new {
											type= "string"
										},
										i_t1= new {
											type= "string",
											index_name= "t1"
										},
										i_t3= new {
											type= "string",
											index_name= "t3"
										}
									}
								}
							}
						}
					}
				};
				_status = this._client.IndicesCreatePost("test_index", _body);
				_response = _status.Deserialize<dynamic>();
			}
		}
		
		public class GetFieldMappingWithForFields50Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
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
				
				_status = this._client.IndicesGetFieldMapping("*");
				_response = _status.Deserialize<dynamic>();
			}
		}
		
		public class GetFieldMappingWithTForFields50Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
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
				
				_status = this._client.IndicesGetFieldMapping("t*");
				_response = _status.Deserialize<dynamic>();
			}
		}
		
		public class GetFieldMappingWithT1ForFields50Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
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
				
				_status = this._client.IndicesGetFieldMapping("*t1");
				_response = _status.Deserialize<dynamic>();
			}
		}
		
		public class GetFieldMappingWithWildcardedRelativeNames50Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
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
				
				_status = this._client.IndicesGetFieldMapping("i_*");
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
