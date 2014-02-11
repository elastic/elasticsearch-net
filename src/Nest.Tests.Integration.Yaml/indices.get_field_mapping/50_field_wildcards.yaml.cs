using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesGetFieldMapping
{
	public partial class IndicesGetFieldMappingTests
	{	
	
		public class IndicesGetFieldMapping50FieldWildcardsYamlBase : YamlTestsBase
		{
			public IndicesGetFieldMapping50FieldWildcardsYamlBase() : base()
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


		public class GetFieldMappingWithForFieldsTests : IndicesGetFieldMapping50FieldWildcardsYamlBase
		{
			[Test]
			public void GetFieldMappingWithForFieldsTest()
			{	

				//do indices.get_field_mapping 
				_status = this._client.IndicesGetFieldMapping("*");
				_response = _status.Deserialize<dynamic>();

			}
		}

		public class GetFieldMappingWithTForFieldsTests : IndicesGetFieldMapping50FieldWildcardsYamlBase
		{
			[Test]
			public void GetFieldMappingWithTForFieldsTest()
			{	

				//do indices.get_field_mapping 
				_status = this._client.IndicesGetFieldMapping("t*");
				_response = _status.Deserialize<dynamic>();

				//length test_index.test_type: 0; 
				this.IsLength(_response.test_index.test_type, 0);

			}
		}

		public class GetFieldMappingWithT1ForFieldsTests : IndicesGetFieldMapping50FieldWildcardsYamlBase
		{
			[Test]
			public void GetFieldMappingWithT1ForFieldsTest()
			{	

				//do indices.get_field_mapping 
				_status = this._client.IndicesGetFieldMapping("*t1");
				_response = _status.Deserialize<dynamic>();

				//length test_index.test_type: 0; 
				this.IsLength(_response.test_index.test_type, 0);

			}
		}

		public class GetFieldMappingWithWildcardedRelativeNamesTests : IndicesGetFieldMapping50FieldWildcardsYamlBase
		{
			[Test]
			public void GetFieldMappingWithWildcardedRelativeNamesTest()
			{	

				//do indices.get_field_mapping 
				_status = this._client.IndicesGetFieldMapping("i_*");
				_response = _status.Deserialize<dynamic>();

				//length test_index.test_type: 0; 
				this.IsLength(_response.test_index.test_type, 0);

			}
		}
	}
}

