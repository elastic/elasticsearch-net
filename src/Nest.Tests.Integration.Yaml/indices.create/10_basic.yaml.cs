using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesCreate
{
	public partial class IndicesCreateTests
	{	


		public class CreateIndexWithMappingsTests : YamlTestsBase
		{
			[Test]
			public void CreateIndexWithMappingsTest()
			{	

				//do indices.create 
				_body = new {
					mappings= new {
						type_1= new {}
					}
				};
				_status = this._client.IndicesCreatePost("test_index", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.get_mapping 
				_status = this._client.IndicesGetMapping("test_index");
				_response = _status.Deserialize<dynamic>();

			}
		}

		public class CreateIndexWithSettingsTests : YamlTestsBase
		{
			[Test]
			public void CreateIndexWithSettingsTest()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						number_of_replicas= "0"
					}
				};
				_status = this._client.IndicesCreatePost("test_index", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.get_settings 
				_status = this._client.IndicesGetSettings("test_index");
				_response = _status.Deserialize<dynamic>();

			}
		}

		public class CreateIndexWithWarmersTests : YamlTestsBase
		{
			[Test]
			public void CreateIndexWithWarmersTest()
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
				_status = this._client.IndicesCreatePost("test_index", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.get_warmer 
				_status = this._client.IndicesGetWarmer("test_index");
				_response = _status.Deserialize<dynamic>();

			}
		}

		public class CreateIndexWithMappingsSettingsAndWarmersTests : YamlTestsBase
		{
			[Test]
			public void CreateIndexWithMappingsSettingsAndWarmersTest()
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
				_status = this._client.IndicesCreatePost("test_index", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.get_mapping 
				_status = this._client.IndicesGetMapping("test_index");
				_response = _status.Deserialize<dynamic>();

				//do indices.get_settings 
				_status = this._client.IndicesGetSettings("test_index");
				_response = _status.Deserialize<dynamic>();

				//do indices.get_warmer 
				_status = this._client.IndicesGetWarmer("test_index");
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

