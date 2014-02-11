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
				this.Do(()=> this._client.IndicesCreatePost("test_index", _body));

			}
		}


		public class GetFieldMappingWithNoIndexAndTypeTests : IndicesGetFieldMapping10BasicYamlBase
		{
			[Test]
			public void GetFieldMappingWithNoIndexAndTypeTest()
			{	

				//do indices.get_field_mapping 
				this.Do(()=> this._client.IndicesGetFieldMapping("text"));

				//match _response.test_index.test_type.text.mapping.text.type: 
				this.IsMatch(_response.test_index.test_type.text.mapping.text.type, @"string");

			}
		}

		public class GetFieldMappingByIndexOnlyTests : IndicesGetFieldMapping10BasicYamlBase
		{
			[Test]
			public void GetFieldMappingByIndexOnlyTest()
			{	

				//do indices.get_field_mapping 
				this.Do(()=> this._client.IndicesGetFieldMapping("test_index", "text"));

				//match _response.test_index.test_type.text.mapping.text.type: 
				this.IsMatch(_response.test_index.test_type.text.mapping.text.type, @"string");

			}
		}

		public class GetFieldMappingByTypeFieldTests : IndicesGetFieldMapping10BasicYamlBase
		{
			[Test]
			public void GetFieldMappingByTypeFieldTest()
			{	

				//do indices.get_field_mapping 
				this.Do(()=> this._client.IndicesGetFieldMapping("test_index", "test_type", "text"));

				//match _response.test_index.test_type.text.mapping.text.type: 
				this.IsMatch(_response.test_index.test_type.text.mapping.text.type, @"string");

			}
		}

		public class GetFieldMappingByTypeFieldWithAnotherFieldThatDoesntExistTests : IndicesGetFieldMapping10BasicYamlBase
		{
			[Test]
			public void GetFieldMappingByTypeFieldWithAnotherFieldThatDoesntExistTest()
			{	

				//do indices.get_field_mapping 
				this.Do(()=> this._client.IndicesGetFieldMapping("test_index", "test_type", "System.Collections.Generic.List`1[System.Object]"));

				//match _response.test_index.test_type.text.mapping.text.type: 
				this.IsMatch(_response.test_index.test_type.text.mapping.text.type, @"string");

				//is_false _response.test_index.test_type.text1; 
				this.IsFalse(_response.test_index.test_type.text1);

			}
		}

		public class GetFieldMappingWithIncludeDefaultsTests : IndicesGetFieldMapping10BasicYamlBase
		{
			[Test]
			public void GetFieldMappingWithIncludeDefaultsTest()
			{	

				//do indices.get_field_mapping 
				this.Do(()=> this._client.IndicesGetFieldMapping("test_index", "test_type", "text", nv=>nv
					.Add("include_defaults", @"true")
				));

				//match _response.test_index.test_type.text.mapping.text.type: 
				this.IsMatch(_response.test_index.test_type.text.mapping.text.type, @"string");

				//match _response.test_index.test_type.text.mapping.text.analyzer: 
				this.IsMatch(_response.test_index.test_type.text.mapping.text.analyzer, @"default");

			}
		}
	}
}

