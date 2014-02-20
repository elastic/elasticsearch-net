using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesPutMapping2
{
	public partial class IndicesPutMapping2YamlTests
	{	
	
		public class IndicesPutMapping2AllPathOptionsYamlBase : YamlTestsBase
		{
			public IndicesPutMapping2AllPathOptionsYamlBase() : base()
			{	

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test_index1", null));

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test_index2", null));

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("foo", null));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutOneMappingPerIndex2Tests : IndicesPutMapping2AllPathOptionsYamlBase
		{
			[Test]
			public void PutOneMappingPerIndex2Test()
			{	

				//do indices.put_mapping 
				_body = new {
					test_type= new {
						properties= new {
							text= new {
								type= "string",
								analyzer= "whitespace"
							}
						}
					}
				};
				this.Do(()=> _client.IndicesPutMapping("test_index1", "test_type", _body));

				//do indices.put_mapping 
				_body = new {
					test_type= new {
						properties= new {
							text= new {
								type= "string",
								analyzer= "whitespace"
							}
						}
					}
				};
				this.Do(()=> _client.IndicesPutMapping("test_index2", "test_type", _body));

				//do indices.get_mapping 
				this.Do(()=> _client.IndicesGetMappingForAll());

				//match _response.test_index1.mappings.test_type.properties.text.type: 
				this.IsMatch(_response.test_index1.mappings.test_type.properties.text.type, @"string");

				//match _response.test_index1.mappings.test_type.properties.text.analyzer: 
				this.IsMatch(_response.test_index1.mappings.test_type.properties.text.analyzer, @"whitespace");

				//match _response.test_index2.mappings.test_type.properties.text.type: 
				this.IsMatch(_response.test_index2.mappings.test_type.properties.text.type, @"string");

				//match _response.test_index2.mappings.test_type.properties.text.analyzer: 
				this.IsMatch(_response.test_index2.mappings.test_type.properties.text.analyzer, @"whitespace");

				//is_false _response.foo; 
				this.IsFalse(_response.foo);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutMappingInAllIndex3Tests : IndicesPutMapping2AllPathOptionsYamlBase
		{
			[Test]
			public void PutMappingInAllIndex3Test()
			{	

				//do indices.put_mapping 
				_body = new {
					test_type= new {
						properties= new {
							text= new {
								type= "string",
								analyzer= "whitespace"
							}
						}
					}
				};
				this.Do(()=> _client.IndicesPutMapping("_all", "test_type", _body));

				//do indices.get_mapping 
				this.Do(()=> _client.IndicesGetMappingForAll());

				//match _response.test_index1.mappings.test_type.properties.text.type: 
				this.IsMatch(_response.test_index1.mappings.test_type.properties.text.type, @"string");

				//match _response.test_index1.mappings.test_type.properties.text.analyzer: 
				this.IsMatch(_response.test_index1.mappings.test_type.properties.text.analyzer, @"whitespace");

				//match _response.test_index2.mappings.test_type.properties.text.type: 
				this.IsMatch(_response.test_index2.mappings.test_type.properties.text.type, @"string");

				//match _response.test_index2.mappings.test_type.properties.text.analyzer: 
				this.IsMatch(_response.test_index2.mappings.test_type.properties.text.analyzer, @"whitespace");

				//match _response.foo.mappings.test_type.properties.text.type: 
				this.IsMatch(_response.foo.mappings.test_type.properties.text.type, @"string");

				//match _response.foo.mappings.test_type.properties.text.analyzer: 
				this.IsMatch(_response.foo.mappings.test_type.properties.text.analyzer, @"whitespace");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutMappingInIndex4Tests : IndicesPutMapping2AllPathOptionsYamlBase
		{
			[Test]
			public void PutMappingInIndex4Test()
			{	

				//do indices.put_mapping 
				_body = new {
					test_type= new {
						properties= new {
							text= new {
								type= "string",
								analyzer= "whitespace"
							}
						}
					}
				};
				this.Do(()=> _client.IndicesPutMapping("*", "test_type", _body));

				//do indices.get_mapping 
				this.Do(()=> _client.IndicesGetMappingForAll());

				//match _response.test_index1.mappings.test_type.properties.text.type: 
				this.IsMatch(_response.test_index1.mappings.test_type.properties.text.type, @"string");

				//match _response.test_index1.mappings.test_type.properties.text.analyzer: 
				this.IsMatch(_response.test_index1.mappings.test_type.properties.text.analyzer, @"whitespace");

				//match _response.test_index2.mappings.test_type.properties.text.type: 
				this.IsMatch(_response.test_index2.mappings.test_type.properties.text.type, @"string");

				//match _response.test_index2.mappings.test_type.properties.text.analyzer: 
				this.IsMatch(_response.test_index2.mappings.test_type.properties.text.analyzer, @"whitespace");

				//match _response.foo.mappings.test_type.properties.text.type: 
				this.IsMatch(_response.foo.mappings.test_type.properties.text.type, @"string");

				//match _response.foo.mappings.test_type.properties.text.analyzer: 
				this.IsMatch(_response.foo.mappings.test_type.properties.text.analyzer, @"whitespace");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutMappingInPrefixIndex5Tests : IndicesPutMapping2AllPathOptionsYamlBase
		{
			[Test]
			public void PutMappingInPrefixIndex5Test()
			{	

				//do indices.put_mapping 
				_body = new {
					test_type= new {
						properties= new {
							text= new {
								type= "string",
								analyzer= "whitespace"
							}
						}
					}
				};
				this.Do(()=> _client.IndicesPutMapping("test_index*", "test_type", _body));

				//do indices.get_mapping 
				this.Do(()=> _client.IndicesGetMappingForAll());

				//match _response.test_index1.mappings.test_type.properties.text.type: 
				this.IsMatch(_response.test_index1.mappings.test_type.properties.text.type, @"string");

				//match _response.test_index1.mappings.test_type.properties.text.analyzer: 
				this.IsMatch(_response.test_index1.mappings.test_type.properties.text.analyzer, @"whitespace");

				//match _response.test_index2.mappings.test_type.properties.text.type: 
				this.IsMatch(_response.test_index2.mappings.test_type.properties.text.type, @"string");

				//match _response.test_index2.mappings.test_type.properties.text.analyzer: 
				this.IsMatch(_response.test_index2.mappings.test_type.properties.text.analyzer, @"whitespace");

				//is_false _response.foo; 
				this.IsFalse(_response.foo);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutMappingInListOfIndices6Tests : IndicesPutMapping2AllPathOptionsYamlBase
		{
			[Test]
			public void PutMappingInListOfIndices6Test()
			{	

				//do indices.put_mapping 
				_body = new {
					test_type= new {
						properties= new {
							text= new {
								type= "string",
								analyzer= "whitespace"
							}
						}
					}
				};
				this.Do(()=> _client.IndicesPutMapping("test_index1,test_index2", "test_type", _body));

				//do indices.get_mapping 
				this.Do(()=> _client.IndicesGetMappingForAll());

				//match _response.test_index1.mappings.test_type.properties.text.type: 
				this.IsMatch(_response.test_index1.mappings.test_type.properties.text.type, @"string");

				//match _response.test_index1.mappings.test_type.properties.text.analyzer: 
				this.IsMatch(_response.test_index1.mappings.test_type.properties.text.analyzer, @"whitespace");

				//match _response.test_index2.mappings.test_type.properties.text.type: 
				this.IsMatch(_response.test_index2.mappings.test_type.properties.text.type, @"string");

				//match _response.test_index2.mappings.test_type.properties.text.analyzer: 
				this.IsMatch(_response.test_index2.mappings.test_type.properties.text.analyzer, @"whitespace");

				//is_false _response.foo; 
				this.IsFalse(_response.foo);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutMappingWithBlankIndex7Tests : IndicesPutMapping2AllPathOptionsYamlBase
		{
			[Test]
			public void PutMappingWithBlankIndex7Test()
			{	

				//do indices.put_mapping 
				_body = new {
					test_type= new {
						properties= new {
							text= new {
								type= "string",
								analyzer= "whitespace"
							}
						}
					}
				};
				this.Do(()=> _client.IndicesPutMappingForAll("test_type", _body));

				//do indices.get_mapping 
				this.Do(()=> _client.IndicesGetMappingForAll());

				//match _response.test_index1.mappings.test_type.properties.text.type: 
				this.IsMatch(_response.test_index1.mappings.test_type.properties.text.type, @"string");

				//match _response.test_index1.mappings.test_type.properties.text.analyzer: 
				this.IsMatch(_response.test_index1.mappings.test_type.properties.text.analyzer, @"whitespace");

				//match _response.test_index2.mappings.test_type.properties.text.type: 
				this.IsMatch(_response.test_index2.mappings.test_type.properties.text.type, @"string");

				//match _response.test_index2.mappings.test_type.properties.text.analyzer: 
				this.IsMatch(_response.test_index2.mappings.test_type.properties.text.analyzer, @"whitespace");

				//match _response.foo.mappings.test_type.properties.text.type: 
				this.IsMatch(_response.foo.mappings.test_type.properties.text.type, @"string");

				//match _response.foo.mappings.test_type.properties.text.analyzer: 
				this.IsMatch(_response.foo.mappings.test_type.properties.text.analyzer, @"whitespace");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutMappingWithMissingType8Tests : IndicesPutMapping2AllPathOptionsYamlBase
		{
			[Test]
			public void PutMappingWithMissingType8Test()
			{	

				//do indices.put_mapping 
				this.Do(()=> _client.IndicesPutMappingForAll("", null), shouldCatch: @"param");

			}
		}
	}
}

