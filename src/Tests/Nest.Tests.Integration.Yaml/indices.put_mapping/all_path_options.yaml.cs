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


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Setup1Tests : YamlTestsBase
		{
			[Test]
			public void Setup1Test()
			{	

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePost("test_index1", null));

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePost("test_index2", null));

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePost("foo", null));

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutOneMappingPerIndex2Tests : YamlTestsBase
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
				this.Do(()=> this._client.IndicesPutMappingPost("test_index1", "test_type", _body));

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
				this.Do(()=> this._client.IndicesPutMappingPost("test_index2", "test_type", _body));

				//do indices.get_mapping 
				this.Do(()=> this._client.IndicesGetMappingForAll());

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
		public class PutMappingInAllIndex3Tests : YamlTestsBase
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
				this.Do(()=> this._client.IndicesPutMappingPost("_all", "test_type", _body));

				//do indices.get_mapping 
				this.Do(()=> this._client.IndicesGetMappingForAll());

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
		public class PutMappingInIndex4Tests : YamlTestsBase
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
				this.Do(()=> this._client.IndicesPutMappingPost("*", "test_type", _body));

				//do indices.get_mapping 
				this.Do(()=> this._client.IndicesGetMappingForAll());

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
		public class PutMappingInPrefixIndex5Tests : YamlTestsBase
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
				this.Do(()=> this._client.IndicesPutMappingPost("test_index*", "test_type", _body));

				//do indices.get_mapping 
				this.Do(()=> this._client.IndicesGetMappingForAll());

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
		public class PutMappingInListOfIndices6Tests : YamlTestsBase
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
				this.Do(()=> this._client.IndicesPutMappingPost("test_index1,test_index2", "test_type", _body));

				//do indices.get_mapping 
				this.Do(()=> this._client.IndicesGetMappingForAll());

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
		public class PutMappingWithBlankIndex7Tests : YamlTestsBase
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
				this.Do(()=> this._client.IndicesPutMappingPost("test_type", _body));

				//do indices.get_mapping 
				this.Do(()=> this._client.IndicesGetMappingForAll());

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
		public class PutMappingWithMissingType8Tests : YamlTestsBase
		{
			[Test]
			public void PutMappingWithMissingType8Test()
			{	

				//do indices.put_mapping 
				this.Do(()=> this._client.IndicesPutMappingPost(null), shouldCatch: @"param");

			}
		}
	}
}

