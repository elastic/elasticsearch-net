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
	
		public class IndicesGetFieldMapping10BasicYamlBase : YamlTestsBase
		{
			public IndicesGetFieldMapping10BasicYamlBase() : base()
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


		public class GetFieldMappingWithNoIndexAndTypeTests : IndicesGetFieldMapping10BasicYamlBase
		{
			[Test]
			public void GetFieldMappingWithNoIndexAndTypeTest()
			{	

				//do indices.get_field_mapping 
				_status = this._client.IndicesGetFieldMapping("text");
				_response = _status.Deserialize<dynamic>();

			}
		}

		public class GetFieldMappingByIndexOnlyTests : IndicesGetFieldMapping10BasicYamlBase
		{
			[Test]
			public void GetFieldMappingByIndexOnlyTest()
			{	

				//do indices.get_field_mapping 
				_status = this._client.IndicesGetFieldMapping("test_index", "text");
				_response = _status.Deserialize<dynamic>();

			}
		}

		public class GetFieldMappingByTypeFieldTests : IndicesGetFieldMapping10BasicYamlBase
		{
			[Test]
			public void GetFieldMappingByTypeFieldTest()
			{	

				//do indices.get_field_mapping 
				_status = this._client.IndicesGetFieldMapping("test_index", "test_type", "text");
				_response = _status.Deserialize<dynamic>();

			}
		}

		public class GetFieldMappingByTypeFieldWithAnotherFieldThatDoesntExistTests : IndicesGetFieldMapping10BasicYamlBase
		{
			[Test]
			public void GetFieldMappingByTypeFieldWithAnotherFieldThatDoesntExistTest()
			{	

				//do indices.get_field_mapping 
				_status = this._client.IndicesGetFieldMapping("test_index", "test_type", "System.Collections.Generic.List`1[System.Object]");
				_response = _status.Deserialize<dynamic>();

				//is_false .test_index.test_type.text1; 
				this.IsFalse(_response.test_index.test_type.text1);

			}
		}

		public class GetFieldMappingWithIncludeDefaultsTests : IndicesGetFieldMapping10BasicYamlBase
		{
			[Test]
			public void GetFieldMappingWithIncludeDefaultsTest()
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

