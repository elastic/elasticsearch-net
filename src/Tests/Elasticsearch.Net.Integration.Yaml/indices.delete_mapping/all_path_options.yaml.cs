using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesDeleteMapping2
{
	public partial class IndicesDeleteMapping2YamlTests
	{	
	
		public class IndicesDeleteMapping2AllPathOptionsYamlBase : YamlTestsBase
		{
			public IndicesDeleteMapping2AllPathOptionsYamlBase() : base()
			{	

				//do indices.create 
				_body = new {
					mappings= new {
						test_type1= new {}
					}
				};
				this.Do(()=> _client.IndicesCreate("test_index1", _body));

				//do indices.create 
				_body = new {
					mappings= new {
						test_type2= new {}
					}
				};
				this.Do(()=> _client.IndicesCreate("test_index2", _body));

				//do indices.create 
				_body = new {
					mappings= new {
						test_type2= new {}
					}
				};
				this.Do(()=> _client.IndicesCreate("foo", _body));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"yellow")
				));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class DeleteWithAllIndex2Tests : IndicesDeleteMapping2AllPathOptionsYamlBase
		{
			[Test]
			public void DeleteWithAllIndex2Test()
			{	

				//do indices.delete_mapping 
				this.Do(()=> _client.IndicesDeleteMapping("_all", "test_type2"));

				//do indices.exists_type 
				this.Do(()=> _client.IndicesExistsType("test_index1", "test_type1"));

				//is_true this._status; 
				this.IsTrue(this._status);

				//do indices.exists_type 
				this.Do(()=> _client.IndicesExistsType("test_index2", "test_type2"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.exists_type 
				this.Do(()=> _client.IndicesExistsType("foo", "test_type2"));

				//is_false this._status; 
				this.IsFalse(this._status);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class DeleteWithIndex3Tests : IndicesDeleteMapping2AllPathOptionsYamlBase
		{
			[Test]
			public void DeleteWithIndex3Test()
			{	

				//do indices.delete_mapping 
				this.Do(()=> _client.IndicesDeleteMapping("*", "test_type2"));

				//do indices.exists_type 
				this.Do(()=> _client.IndicesExistsType("test_index1", "test_type1"));

				//is_true this._status; 
				this.IsTrue(this._status);

				//do indices.exists_type 
				this.Do(()=> _client.IndicesExistsType("test_index2", "test_type2"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.exists_type 
				this.Do(()=> _client.IndicesExistsType("foo", "test_type2"));

				//is_false this._status; 
				this.IsFalse(this._status);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class DeleteWithPrefixIndex4Tests : IndicesDeleteMapping2AllPathOptionsYamlBase
		{
			[Test]
			public void DeleteWithPrefixIndex4Test()
			{	

				//do indices.delete_mapping 
				this.Do(()=> _client.IndicesDeleteMapping("test*", "test_type2"));

				//do indices.exists_type 
				this.Do(()=> _client.IndicesExistsType("test_index1", "test_type1"));

				//is_true this._status; 
				this.IsTrue(this._status);

				//do indices.exists_type 
				this.Do(()=> _client.IndicesExistsType("test_index2", "test_type2"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.exists_type 
				this.Do(()=> _client.IndicesExistsType("foo", "test_type2"));

				//is_true this._status; 
				this.IsTrue(this._status);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class DeleteWithListOfIndices5Tests : IndicesDeleteMapping2AllPathOptionsYamlBase
		{
			[Test]
			public void DeleteWithListOfIndices5Test()
			{	

				//do indices.delete_mapping 
				this.Do(()=> _client.IndicesDeleteMapping("test_index1,test_index2", "test_type2"));

				//do indices.exists_type 
				this.Do(()=> _client.IndicesExistsType("test_index1", "test_type1"));

				//is_true this._status; 
				this.IsTrue(this._status);

				//do indices.exists_type 
				this.Do(()=> _client.IndicesExistsType("test_index2", "test_type2"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.exists_type 
				this.Do(()=> _client.IndicesExistsType("foo", "test_type2"));

				//is_true this._status; 
				this.IsTrue(this._status);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class DeleteWithIndexListAndAllType6Tests : IndicesDeleteMapping2AllPathOptionsYamlBase
		{
			[Test]
			public void DeleteWithIndexListAndAllType6Test()
			{	

				//do indices.delete_mapping 
				this.Do(()=> _client.IndicesDeleteMapping("test_index1,test_index2", "_all"));

				//do indices.exists_type 
				this.Do(()=> _client.IndicesExistsType("test_index1", "test_type1"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.exists_type 
				this.Do(()=> _client.IndicesExistsType("test_index2", "test_type2"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.exists_type 
				this.Do(()=> _client.IndicesExistsType("foo", "test_type2"));

				//is_true this._status; 
				this.IsTrue(this._status);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class DeleteWithIndexListAndType7Tests : IndicesDeleteMapping2AllPathOptionsYamlBase
		{
			[Test]
			public void DeleteWithIndexListAndType7Test()
			{	

				//do indices.delete_mapping 
				this.Do(()=> _client.IndicesDeleteMapping("test_index1,test_index2", "*"));

				//do indices.exists_type 
				this.Do(()=> _client.IndicesExistsType("test_index1", "test_type1"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.exists_type 
				this.Do(()=> _client.IndicesExistsType("test_index2", "test_type2"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.exists_type 
				this.Do(()=> _client.IndicesExistsType("foo", "test_type2"));

				//is_true this._status; 
				this.IsTrue(this._status);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class DeleteWithIndexListAndPrefixType8Tests : IndicesDeleteMapping2AllPathOptionsYamlBase
		{
			[Test]
			public void DeleteWithIndexListAndPrefixType8Test()
			{	

				//do indices.delete_mapping 
				this.Do(()=> _client.IndicesDeleteMapping("test_index1,test_index2", "*2"));

				//do indices.exists_type 
				this.Do(()=> _client.IndicesExistsType("test_index1", "test_type1"));

				//is_true this._status; 
				this.IsTrue(this._status);

				//do indices.exists_type 
				this.Do(()=> _client.IndicesExistsType("test_index2", "test_type2"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.exists_type 
				this.Do(()=> _client.IndicesExistsType("foo", "test_type2"));

				//is_true this._status; 
				this.IsTrue(this._status);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class DeleteWithIndexListAndListOfTypes9Tests : IndicesDeleteMapping2AllPathOptionsYamlBase
		{
			[Test]
			public void DeleteWithIndexListAndListOfTypes9Test()
			{	

				//do indices.delete_mapping 
				this.Do(()=> _client.IndicesDeleteMapping("test_index1,test_index2", "test_type1,test_type2"));

				//do indices.exists_type 
				this.Do(()=> _client.IndicesExistsType("test_index1", "test_type1"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.exists_type 
				this.Do(()=> _client.IndicesExistsType("test_index2", "test_type2"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.exists_type 
				this.Do(()=> _client.IndicesExistsType("foo", "test_type2"));

				//is_true this._status; 
				this.IsTrue(this._status);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Check404OnNoMatchingType10Tests : IndicesDeleteMapping2AllPathOptionsYamlBase
		{
			[Test]
			public void Check404OnNoMatchingType10Test()
			{	

				//do indices.delete_mapping 
				this.Do(()=> _client.IndicesDeleteMapping("*", "non_existent"), shouldCatch: @"missing");

				//do indices.delete_mapping 
				this.Do(()=> _client.IndicesDeleteMapping("non_existent", "test_type1"), shouldCatch: @"missing");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CheckDeleteWithBlankIndexAndBlankType11Tests : IndicesDeleteMapping2AllPathOptionsYamlBase
		{
			[Test]
			public void CheckDeleteWithBlankIndexAndBlankType11Test()
			{	

				//do indices.delete_mapping 
				this.Do(()=> _client.IndicesDeleteMapping("", "", nv=>nv
					.AddQueryString("name", @"test_type1")
				), shouldCatch: @"param");

				//do indices.delete_mapping 
				this.Do(()=> _client.IndicesDeleteMapping("test_index1", ""), shouldCatch: @"param");

			}
		}
	}
}

