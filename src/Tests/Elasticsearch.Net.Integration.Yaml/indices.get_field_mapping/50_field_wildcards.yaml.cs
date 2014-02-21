using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesGetFieldMapping5
{
	public partial class IndicesGetFieldMapping5YamlTests
	{	
	
		public class IndicesGetFieldMapping550FieldWildcardsYamlBase : YamlTestsBase
		{
			public IndicesGetFieldMapping550FieldWildcardsYamlBase() : base()
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
				this.Do(()=> _client.IndicesCreate("test_index", _body));

				//do indices.create 
				_body = new {
					mappings= new {
						test_type_2= new {
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
				this.Do(()=> _client.IndicesCreate("test_index_2", _body));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.Add("wait_for_status", @"yellow")
				));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetFieldMappingWithForFields2Tests : IndicesGetFieldMapping550FieldWildcardsYamlBase
		{
			[Test]
			public void GetFieldMappingWithForFields2Test()
			{	

				//do indices.get_field_mapping 
				this.Do(()=> _client.IndicesGetFieldMappingForAll("*"));

				//match _response.test_index.mappings.test_type.t1.full_name: 
				this.IsMatch(_response.test_index.mappings.test_type.t1.full_name, @"t1");

				//match _response.test_index.mappings.test_type.t2.full_name: 
				this.IsMatch(_response.test_index.mappings.test_type.t2.full_name, @"t2");

				//match _response[@"test_index"][@"mappings"][@"test_type"][@"obj.t1"][@"full_name"]: 
				this.IsMatch(_response[@"test_index"][@"mappings"][@"test_type"][@"obj.t1"][@"full_name"], @"obj.t1");

				//match _response[@"test_index"][@"mappings"][@"test_type"][@"obj.i_t1"][@"full_name"]: 
				this.IsMatch(_response[@"test_index"][@"mappings"][@"test_type"][@"obj.i_t1"][@"full_name"], @"obj.i_t1");

				//match _response[@"test_index"][@"mappings"][@"test_type"][@"obj.i_t3"][@"full_name"]: 
				this.IsMatch(_response[@"test_index"][@"mappings"][@"test_type"][@"obj.i_t3"][@"full_name"], @"obj.i_t3");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetFieldMappingWithTForFields3Tests : IndicesGetFieldMapping550FieldWildcardsYamlBase
		{
			[Test]
			public void GetFieldMappingWithTForFields3Test()
			{	

				//do indices.get_field_mapping 
				this.Do(()=> _client.IndicesGetFieldMapping("test_index", "t*"));

				//match _response.test_index.mappings.test_type.t1.full_name: 
				this.IsMatch(_response.test_index.mappings.test_type.t1.full_name, @"t1");

				//match _response.test_index.mappings.test_type.t2.full_name: 
				this.IsMatch(_response.test_index.mappings.test_type.t2.full_name, @"t2");

				//match _response.test_index.mappings.test_type.t3.full_name: 
				this.IsMatch(_response.test_index.mappings.test_type.t3.full_name, @"obj.i_t3");

				//length _response.test_index.mappings.test_type: 3; 
				this.IsLength(_response.test_index.mappings.test_type, 3);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetFieldMappingWithT1ForFields4Tests : IndicesGetFieldMapping550FieldWildcardsYamlBase
		{
			[Test]
			public void GetFieldMappingWithT1ForFields4Test()
			{	

				//do indices.get_field_mapping 
				this.Do(()=> _client.IndicesGetFieldMapping("test_index", "*t1"));

				//match _response.test_index.mappings.test_type.t1.full_name: 
				this.IsMatch(_response.test_index.mappings.test_type.t1.full_name, @"t1");

				//match _response[@"test_index"][@"mappings"][@"test_type"][@"obj.t1"][@"full_name"]: 
				this.IsMatch(_response[@"test_index"][@"mappings"][@"test_type"][@"obj.t1"][@"full_name"], @"obj.t1");

				//match _response[@"test_index"][@"mappings"][@"test_type"][@"obj.i_t1"][@"full_name"]: 
				this.IsMatch(_response[@"test_index"][@"mappings"][@"test_type"][@"obj.i_t1"][@"full_name"], @"obj.i_t1");

				//length _response.test_index.mappings.test_type: 3; 
				this.IsLength(_response.test_index.mappings.test_type, 3);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetFieldMappingWithWildcardedRelativeNames5Tests : IndicesGetFieldMapping550FieldWildcardsYamlBase
		{
			[Test]
			public void GetFieldMappingWithWildcardedRelativeNames5Test()
			{	

				//do indices.get_field_mapping 
				this.Do(()=> _client.IndicesGetFieldMapping("test_index", "i_*"));

				//match _response.test_index.mappings.test_type.i_t1.full_name: 
				this.IsMatch(_response.test_index.mappings.test_type.i_t1.full_name, @"obj.i_t1");

				//match _response.test_index.mappings.test_type.i_t3.full_name: 
				this.IsMatch(_response.test_index.mappings.test_type.i_t3.full_name, @"obj.i_t3");

				//length _response.test_index.mappings.test_type: 2; 
				this.IsLength(_response.test_index.mappings.test_type, 2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetFieldMappingShouldWorkUsingAllForIndicesAndTypes6Tests : IndicesGetFieldMapping550FieldWildcardsYamlBase
		{
			[Test]
			public void GetFieldMappingShouldWorkUsingAllForIndicesAndTypes6Test()
			{	

				//do indices.get_field_mapping 
				this.Do(()=> _client.IndicesGetFieldMapping("_all", "_all", "i_*"));

				//match _response.test_index.mappings.test_type.i_t1.full_name: 
				this.IsMatch(_response.test_index.mappings.test_type.i_t1.full_name, @"obj.i_t1");

				//match _response.test_index.mappings.test_type.i_t3.full_name: 
				this.IsMatch(_response.test_index.mappings.test_type.i_t3.full_name, @"obj.i_t3");

				//length _response.test_index.mappings.test_type: 2; 
				this.IsLength(_response.test_index.mappings.test_type, 2);

				//match _response.test_index_2.mappings.test_type_2.i_t1.full_name: 
				this.IsMatch(_response.test_index_2.mappings.test_type_2.i_t1.full_name, @"obj.i_t1");

				//match _response.test_index_2.mappings.test_type_2.i_t3.full_name: 
				this.IsMatch(_response.test_index_2.mappings.test_type_2.i_t3.full_name, @"obj.i_t3");

				//length _response.test_index_2.mappings.test_type_2: 2; 
				this.IsLength(_response.test_index_2.mappings.test_type_2, 2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetFieldMappingShouldWorkUsingForIndicesAndTypes7Tests : IndicesGetFieldMapping550FieldWildcardsYamlBase
		{
			[Test]
			public void GetFieldMappingShouldWorkUsingForIndicesAndTypes7Test()
			{	

				//do indices.get_field_mapping 
				this.Do(()=> _client.IndicesGetFieldMapping("*", "*", "i_*"));

				//match _response.test_index.mappings.test_type.i_t1.full_name: 
				this.IsMatch(_response.test_index.mappings.test_type.i_t1.full_name, @"obj.i_t1");

				//match _response.test_index.mappings.test_type.i_t3.full_name: 
				this.IsMatch(_response.test_index.mappings.test_type.i_t3.full_name, @"obj.i_t3");

				//length _response.test_index.mappings.test_type: 2; 
				this.IsLength(_response.test_index.mappings.test_type, 2);

				//match _response.test_index_2.mappings.test_type_2.i_t1.full_name: 
				this.IsMatch(_response.test_index_2.mappings.test_type_2.i_t1.full_name, @"obj.i_t1");

				//match _response.test_index_2.mappings.test_type_2.i_t3.full_name: 
				this.IsMatch(_response.test_index_2.mappings.test_type_2.i_t3.full_name, @"obj.i_t3");

				//length _response.test_index_2.mappings.test_type_2: 2; 
				this.IsLength(_response.test_index_2.mappings.test_type_2, 2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetFieldMappingShouldWorkUsingCommaSeparatedValuesForIndicesAndTypes8Tests : IndicesGetFieldMapping550FieldWildcardsYamlBase
		{
			[Test]
			public void GetFieldMappingShouldWorkUsingCommaSeparatedValuesForIndicesAndTypes8Test()
			{	

				//do indices.get_field_mapping 
				this.Do(()=> _client.IndicesGetFieldMapping("test_index,test_index_2", "test_type,test_type_2", "i_*"));

				//match _response.test_index.mappings.test_type.i_t1.full_name: 
				this.IsMatch(_response.test_index.mappings.test_type.i_t1.full_name, @"obj.i_t1");

				//match _response.test_index.mappings.test_type.i_t3.full_name: 
				this.IsMatch(_response.test_index.mappings.test_type.i_t3.full_name, @"obj.i_t3");

				//length _response.test_index.mappings.test_type: 2; 
				this.IsLength(_response.test_index.mappings.test_type, 2);

				//match _response.test_index_2.mappings.test_type_2.i_t1.full_name: 
				this.IsMatch(_response.test_index_2.mappings.test_type_2.i_t1.full_name, @"obj.i_t1");

				//match _response.test_index_2.mappings.test_type_2.i_t3.full_name: 
				this.IsMatch(_response.test_index_2.mappings.test_type_2.i_t3.full_name, @"obj.i_t3");

				//length _response.test_index_2.mappings.test_type_2: 2; 
				this.IsLength(_response.test_index_2.mappings.test_type_2, 2);

			}
		}
	}
}

