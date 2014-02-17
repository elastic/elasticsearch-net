using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesDeleteMapping2
{
	public partial class IndicesDeleteMapping2YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Setup1Tests : YamlTestsBase
		{
			[Test]
			public void Setup1Test()
			{	

				//do indices.create 
				_body = new {
					mappings= new {
						test_type1= new {}
					}
				};
				this.Do(()=> this._client.IndicesCreatePut("test_index1", _body));

				//do indices.create 
				_body = new {
					mappings= new {
						test_type2= new {}
					}
				};
				this.Do(()=> this._client.IndicesCreatePut("test_index2", _body));

				//do indices.create 
				_body = new {
					mappings= new {
						test_type2= new {}
					}
				};
				this.Do(()=> this._client.IndicesCreatePut("foo", _body));

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class DeleteWithAllIndex2Tests : YamlTestsBase
		{
			[Test]
			public void DeleteWithAllIndex2Test()
			{	

				//do indices.delete_mapping 
				this.Do(()=> this._client.IndicesDeleteMapping("_all", "test_type2"));

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("test_index1", "test_type1"));

				//is_true this._status; 
				this.IsTrue(this._status);

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("test_index2", "test_type2"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("foo", "test_type2"));

				//is_false this._status; 
				this.IsFalse(this._status);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class DeleteWithIndex3Tests : YamlTestsBase
		{
			[Test]
			public void DeleteWithIndex3Test()
			{	

				//do indices.delete_mapping 
				this.Do(()=> this._client.IndicesDeleteMapping("*", "test_type2"));

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("test_index1", "test_type1"));

				//is_true this._status; 
				this.IsTrue(this._status);

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("test_index2", "test_type2"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("foo", "test_type2"));

				//is_false this._status; 
				this.IsFalse(this._status);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class DeleteWithPrefixIndex4Tests : YamlTestsBase
		{
			[Test]
			public void DeleteWithPrefixIndex4Test()
			{	

				//do indices.delete_mapping 
				this.Do(()=> this._client.IndicesDeleteMapping("test*", "test_type2"));

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("test_index1", "test_type1"));

				//is_true this._status; 
				this.IsTrue(this._status);

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("test_index2", "test_type2"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("foo", "test_type2"));

				//is_true this._status; 
				this.IsTrue(this._status);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class DeleteWithListOfIndices5Tests : YamlTestsBase
		{
			[Test]
			public void DeleteWithListOfIndices5Test()
			{	

				//do indices.delete_mapping 
				this.Do(()=> this._client.IndicesDeleteMapping("test_index1,test_index2", "test_type2"));

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("test_index1", "test_type1"));

				//is_true this._status; 
				this.IsTrue(this._status);

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("test_index2", "test_type2"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("foo", "test_type2"));

				//is_true this._status; 
				this.IsTrue(this._status);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class DeleteWithIndexListAndAllType6Tests : YamlTestsBase
		{
			[Test]
			public void DeleteWithIndexListAndAllType6Test()
			{	

				//do indices.delete_mapping 
				this.Do(()=> this._client.IndicesDeleteMapping("test_index1,test_index2", "_all"));

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("test_index1", "test_type1"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("test_index2", "test_type2"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("foo", "test_type2"));

				//is_true this._status; 
				this.IsTrue(this._status);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class DeleteWithIndexListAndType7Tests : YamlTestsBase
		{
			[Test]
			public void DeleteWithIndexListAndType7Test()
			{	

				//do indices.delete_mapping 
				this.Do(()=> this._client.IndicesDeleteMapping("test_index1,test_index2", "*"));

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("test_index1", "test_type1"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("test_index2", "test_type2"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("foo", "test_type2"));

				//is_true this._status; 
				this.IsTrue(this._status);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class DeleteWithIndexListAndPrefixType8Tests : YamlTestsBase
		{
			[Test]
			public void DeleteWithIndexListAndPrefixType8Test()
			{	

				//do indices.delete_mapping 
				this.Do(()=> this._client.IndicesDeleteMapping("test_index1,test_index2", "*2"));

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("test_index1", "test_type1"));

				//is_true this._status; 
				this.IsTrue(this._status);

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("test_index2", "test_type2"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("foo", "test_type2"));

				//is_true this._status; 
				this.IsTrue(this._status);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class DeleteWithIndexListAndListOfTypes9Tests : YamlTestsBase
		{
			[Test]
			public void DeleteWithIndexListAndListOfTypes9Test()
			{	

				//do indices.delete_mapping 
				this.Do(()=> this._client.IndicesDeleteMapping("test_index1,test_index2", "test_type1,test_type2"));

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("test_index1", "test_type1"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("test_index2", "test_type2"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.exists_type 
				this.Do(()=> this._client.IndicesExistsTypeHead("foo", "test_type2"));

				//is_true this._status; 
				this.IsTrue(this._status);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Check404OnNoMatchingType10Tests : YamlTestsBase
		{
			[Test]
			public void Check404OnNoMatchingType10Test()
			{	

				//do indices.delete_mapping 
				this.Do(()=> this._client.IndicesDeleteMapping("*", "non_existent"), shouldCatch: @"missing");

				//do indices.delete_mapping 
				this.Do(()=> this._client.IndicesDeleteMapping("non_existent", "test_type1"), shouldCatch: @"missing");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CheckDeleteWithBlankIndexAndBlankType11Tests : YamlTestsBase
		{
			[Test]
			public void CheckDeleteWithBlankIndexAndBlankType11Test()
			{	

				//do indices.delete_mapping 
				this.Do(()=> this._client.IndicesDeleteMapping("", "", nv=>nv
					.Add("name", @"test_type1")
				), shouldCatch: @"param");

				//do indices.delete_mapping 
				this.Do(()=> this._client.IndicesDeleteMapping("test_index1", ""), shouldCatch: @"param");

			}
		}
	}
}

