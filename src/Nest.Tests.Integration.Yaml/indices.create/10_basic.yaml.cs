using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesCreate
{
	public partial class IndicesCreate10BasicYaml10Tests
	{
		
		public class CreateIndexWithMappings10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public CreateIndexWithMappings10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void CreateIndexWithMappingsTests()
			{

				//do indices.create 
				_body = new {
					mappings= new {
						type_1= new {}
					}
				};
				this._client.IndicesCreatePost("test_index", _body, nv=>nv);

				//do indices.get_mapping 
				
				this._client.IndicesGetMapping("test_index", nv=>nv);
			}
		}
		
		public class CreateIndexWithSettings10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public CreateIndexWithSettings10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void CreateIndexWithSettingsTests()
			{

				//do indices.create 
				_body = new {
					settings= new {
						number_of_replicas= "0"
					}
				};
				this._client.IndicesCreatePost("test_index", _body, nv=>nv);

				//do indices.get_settings 
				
				this._client.IndicesGetSettings("test_index", nv=>nv);
			}
		}
		
		public class CreateIndexWithWarmers10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public CreateIndexWithWarmers10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void CreateIndexWithWarmersTests()
			{

				//do indices.create 
				_body = new {
					warmers= new {
						test_warmer= new {
							source= new {
								query= new {
									match_all= new {}
								}
							}
						}
					}
				};
				this._client.IndicesCreatePost("test_index", _body, nv=>nv);

				//do indices.get_warmer 
				
				this._client.IndicesGetWarmer("test_index", nv=>nv);
			}
		}
		
		public class CreateIndexWithMappingsSettingsAndWarmers10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public CreateIndexWithMappingsSettingsAndWarmers10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void CreateIndexWithMappingsSettingsAndWarmersTests()
			{

				//do indices.create 
				_body = new {
					mappings= new {
						type_1= new {}
					},
					settings= new {
						number_of_replicas= "0"
					},
					warmers= new {
						test_warmer= new {
							source= new {
								query= new {
									match_all= new {}
								}
							}
						}
					}
				};
				this._client.IndicesCreatePost("test_index", _body, nv=>nv);

				//do indices.get_mapping 
				
				this._client.IndicesGetMapping("test_index", nv=>nv);

				//do indices.get_settings 
				
				this._client.IndicesGetSettings("test_index", nv=>nv);

				//do indices.get_warmer 
				
				this._client.IndicesGetWarmer("test_index", nv=>nv);
			}
		}
	}
}
