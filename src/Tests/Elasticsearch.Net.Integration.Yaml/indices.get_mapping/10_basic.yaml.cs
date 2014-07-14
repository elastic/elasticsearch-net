using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesGetMapping1
{
	public partial class IndicesGetMapping1YamlTests
	{	
	
		public class IndicesGetMapping110BasicYamlBase : YamlTestsBase
		{
			public IndicesGetMapping110BasicYamlBase() : base()
			{	

				//do indices.create 
				_body = new {
					mappings= new {
						type_1= new {},
						type_2= new {}
					}
				};
				this.Do(()=> _client.IndicesCreate("test_1", _body));

				//do indices.create 
				_body = new {
					mappings= new {
						type_2= new {},
						type_3= new {}
					}
				};
				this.Do(()=> _client.IndicesCreate("test_2", _body));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetMapping2Tests : IndicesGetMapping110BasicYamlBase
		{
			[Test]
			public void GetMapping2Test()
			{	

				//do indices.get_mapping 
				this.Do(()=> _client.IndicesGetMappingForAll());

				//match _response.test_1.mappings.type_1.properties: 
				this.IsMatch(_response.test_1.mappings.type_1.properties, new {});

				//match _response.test_1.mappings.type_2.properties: 
				this.IsMatch(_response.test_1.mappings.type_2.properties, new {});

				//match _response.test_2.mappings.type_2.properties: 
				this.IsMatch(_response.test_2.mappings.type_2.properties, new {});

				//match _response.test_2.mappings.type_3.properties: 
				this.IsMatch(_response.test_2.mappings.type_3.properties, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexMapping3Tests : IndicesGetMapping110BasicYamlBase
		{
			[Test]
			public void GetIndexMapping3Test()
			{	

				//do indices.get_mapping 
				this.Do(()=> _client.IndicesGetMapping("test_1"));

				//match _response.test_1.mappings.type_1.properties: 
				this.IsMatch(_response.test_1.mappings.type_1.properties, new {});

				//match _response.test_1.mappings.type_2.properties: 
				this.IsMatch(_response.test_1.mappings.type_2.properties, new {});

				//is_false _response.test_2; 
				this.IsFalse(_response.test_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexMappingAll4Tests : IndicesGetMapping110BasicYamlBase
		{
			[Test]
			public void GetIndexMappingAll4Test()
			{	

				//do indices.get_mapping 
				this.Do(()=> _client.IndicesGetMapping("test_1", "_all"));

				//match _response.test_1.mappings.type_1.properties: 
				this.IsMatch(_response.test_1.mappings.type_1.properties, new {});

				//match _response.test_1.mappings.type_2.properties: 
				this.IsMatch(_response.test_1.mappings.type_2.properties, new {});

				//is_false _response.test_2; 
				this.IsFalse(_response.test_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexMapping5Tests : IndicesGetMapping110BasicYamlBase
		{
			[Test]
			public void GetIndexMapping5Test()
			{	

				//do indices.get_mapping 
				this.Do(()=> _client.IndicesGetMapping("test_1", "*"));

				//match _response.test_1.mappings.type_1.properties: 
				this.IsMatch(_response.test_1.mappings.type_1.properties, new {});

				//match _response.test_1.mappings.type_2.properties: 
				this.IsMatch(_response.test_1.mappings.type_2.properties, new {});

				//is_false _response.test_2; 
				this.IsFalse(_response.test_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexMappingType6Tests : IndicesGetMapping110BasicYamlBase
		{
			[Test]
			public void GetIndexMappingType6Test()
			{	

				//do indices.get_mapping 
				this.Do(()=> _client.IndicesGetMapping("test_1", "type_1"));

				//match _response.test_1.mappings.type_1.properties: 
				this.IsMatch(_response.test_1.mappings.type_1.properties, new {});

				//is_false _response.test_1.mappings.type_2; 
				this.IsFalse(_response.test_1.mappings.type_2);

				//is_false _response.test_2; 
				this.IsFalse(_response.test_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexMappingTypeType7Tests : IndicesGetMapping110BasicYamlBase
		{
			[Test]
			public void GetIndexMappingTypeType7Test()
			{	

				//do indices.get_mapping 
				this.Do(()=> _client.IndicesGetMapping("test_1", "type_1,type_2"));

				//match _response.test_1.mappings.type_1.properties: 
				this.IsMatch(_response.test_1.mappings.type_1.properties, new {});

				//match _response.test_1.mappings.type_2.properties: 
				this.IsMatch(_response.test_1.mappings.type_2.properties, new {});

				//is_false _response.test_2; 
				this.IsFalse(_response.test_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexMappingType8Tests : IndicesGetMapping110BasicYamlBase
		{
			[Test]
			public void GetIndexMappingType8Test()
			{	

				//do indices.get_mapping 
				this.Do(()=> _client.IndicesGetMapping("test_1", "*2"));

				//match _response.test_1.mappings.type_2.properties: 
				this.IsMatch(_response.test_1.mappings.type_2.properties, new {});

				//is_false _response.test_1.mappings.type_1; 
				this.IsFalse(_response.test_1.mappings.type_1);

				//is_false _response.test_2; 
				this.IsFalse(_response.test_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetMappingType9Tests : IndicesGetMapping110BasicYamlBase
		{
			[Test]
			public void GetMappingType9Test()
			{	

				//do indices.get_mapping 
				this.Do(()=> _client.IndicesGetMappingForAll("type_2"));

				//match _response.test_1.mappings.type_2.properties: 
				this.IsMatch(_response.test_1.mappings.type_2.properties, new {});

				//match _response.test_2.mappings.type_2.properties: 
				this.IsMatch(_response.test_2.mappings.type_2.properties, new {});

				//is_false _response.test_1.mappings.type_1; 
				this.IsFalse(_response.test_1.mappings.type_1);

				//is_false _response.test_2.mappings.type_3; 
				this.IsFalse(_response.test_2.mappings.type_3);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAllMappingType10Tests : IndicesGetMapping110BasicYamlBase
		{
			[Test]
			public void GetAllMappingType10Test()
			{	

				//do indices.get_mapping 
				this.Do(()=> _client.IndicesGetMapping("_all", "type_2"));

				//match _response.test_1.mappings.type_2.properties: 
				this.IsMatch(_response.test_1.mappings.type_2.properties, new {});

				//match _response.test_2.mappings.type_2.properties: 
				this.IsMatch(_response.test_2.mappings.type_2.properties, new {});

				//is_false _response.test_1.mappings.type_1; 
				this.IsFalse(_response.test_1.mappings.type_1);

				//is_false _response.test_2.mappings.type_3; 
				this.IsFalse(_response.test_2.mappings.type_3);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetMappingType11Tests : IndicesGetMapping110BasicYamlBase
		{
			[Test]
			public void GetMappingType11Test()
			{	

				//do indices.get_mapping 
				this.Do(()=> _client.IndicesGetMapping("*", "type_2"));

				//match _response.test_1.mappings.type_2.properties: 
				this.IsMatch(_response.test_1.mappings.type_2.properties, new {});

				//match _response.test_2.mappings.type_2.properties: 
				this.IsMatch(_response.test_2.mappings.type_2.properties, new {});

				//is_false _response.test_1.mappings.type_1; 
				this.IsFalse(_response.test_1.mappings.type_1);

				//is_false _response.test_2.mappings.type_3; 
				this.IsFalse(_response.test_2.mappings.type_3);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexIndexMappingType12Tests : IndicesGetMapping110BasicYamlBase
		{
			[Test]
			public void GetIndexIndexMappingType12Test()
			{	

				//do indices.get_mapping 
				this.Do(()=> _client.IndicesGetMapping("test_1,test_2", "type_2"));

				//match _response.test_1.mappings.type_2.properties: 
				this.IsMatch(_response.test_1.mappings.type_2.properties, new {});

				//match _response.test_2.mappings.type_2.properties: 
				this.IsMatch(_response.test_2.mappings.type_2.properties, new {});

				//is_false _response.test_2.mappings.type_3; 
				this.IsFalse(_response.test_2.mappings.type_3);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexMappingType13Tests : IndicesGetMapping110BasicYamlBase
		{
			[Test]
			public void GetIndexMappingType13Test()
			{	

				//do indices.get_mapping 
				this.Do(()=> _client.IndicesGetMapping("*2", "type_2"));

				//match _response.test_2.mappings.type_2.properties: 
				this.IsMatch(_response.test_2.mappings.type_2.properties, new {});

				//is_false _response.test_1; 
				this.IsFalse(_response.test_1);

				//is_false _response.test_2.mappings.type_3; 
				this.IsFalse(_response.test_2.mappings.type_3);

			}
		}
	}
}

