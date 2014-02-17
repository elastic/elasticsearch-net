using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesGetAliases1
{
	public partial class IndicesGetAliases1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Setup1Tests : YamlTestsBase
		{
			[Test]
			public void Setup1Test()
			{	

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePut("test_index", null));

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePut("test_index_2", null));

				//do indices.put_alias 
				this.Do(()=> this._client.IndicesPutAlias("test_index", "test_alias", null));

				//do indices.put_alias 
				this.Do(()=> this._client.IndicesPutAlias("test_index", "test_blias", null));

				//do indices.put_alias 
				this.Do(()=> this._client.IndicesPutAlias("test_index_2", "test_alias", null));

				//do indices.put_alias 
				this.Do(()=> this._client.IndicesPutAlias("test_index_2", "test_blias", null));

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAllAliasesViaAliases2Tests : YamlTestsBase
		{
			[Test]
			public void GetAllAliasesViaAliases2Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> this._client.IndicesGetAliasesForAll());

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//match _response.test_index.aliases.test_blias: 
				this.IsMatch(_response.test_index.aliases.test_blias, new {});

				//match _response.test_index_2.aliases.test_alias: 
				this.IsMatch(_response.test_index_2.aliases.test_alias, new {});

				//match _response.test_index_2.aliases.test_blias: 
				this.IsMatch(_response.test_index_2.aliases.test_blias, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAllAliasesViaIndexAliases3Tests : YamlTestsBase
		{
			[Test]
			public void GetAllAliasesViaIndexAliases3Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> this._client.IndicesGetAliases("test_index"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//match _response.test_index.aliases.test_blias: 
				this.IsMatch(_response.test_index.aliases.test_blias, new {});

				//is_false _response.test_index_2; 
				this.IsFalse(_response.test_index_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetSpecificAliasViaIndexAliasesName4Tests : YamlTestsBase
		{
			[Test]
			public void GetSpecificAliasViaIndexAliasesName4Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> this._client.IndicesGetAliases("test_index", "test_alias"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//is_false _response.test_index.aliases.test_blias; 
				this.IsFalse(_response.test_index.aliases.test_blias);

				//is_false _response.test_index_2; 
				this.IsFalse(_response.test_index_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAliasesViaIndexAliasesAll5Tests : YamlTestsBase
		{
			[Test]
			public void GetAliasesViaIndexAliasesAll5Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> this._client.IndicesGetAliases("test_index", "_all"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//match _response.test_index.aliases.test_blias: 
				this.IsMatch(_response.test_index.aliases.test_blias, new {});

				//is_false _response.test_index_2; 
				this.IsFalse(_response.test_index_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAliasesViaIndexAliases6Tests : YamlTestsBase
		{
			[Test]
			public void GetAliasesViaIndexAliases6Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> this._client.IndicesGetAliases("test_index", "*"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//match _response.test_index.aliases.test_blias: 
				this.IsMatch(_response.test_index.aliases.test_blias, new {});

				//is_false _response.test_index_2; 
				this.IsFalse(_response.test_index_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAliasesViaIndexAliasesPrefix7Tests : YamlTestsBase
		{
			[Test]
			public void GetAliasesViaIndexAliasesPrefix7Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> this._client.IndicesGetAliases("test_index", "test_a*"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//is_false _response.test_index.aliases.test_blias; 
				this.IsFalse(_response.test_index.aliases.test_blias);

				//is_false _response.test_index_2; 
				this.IsFalse(_response.test_index_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAliasesViaIndexAliasesNameName8Tests : YamlTestsBase
		{
			[Test]
			public void GetAliasesViaIndexAliasesNameName8Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> this._client.IndicesGetAliases("test_index", "test_alias,test_blias"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//match _response.test_index.aliases.test_blias: 
				this.IsMatch(_response.test_index.aliases.test_blias, new {});

				//is_false _response.test_index_2; 
				this.IsFalse(_response.test_index_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAliasesViaAliasesName9Tests : YamlTestsBase
		{
			[Test]
			public void GetAliasesViaAliasesName9Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> this._client.IndicesGetAliasesForAll("test_alias"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//match _response.test_index_2.aliases.test_alias: 
				this.IsMatch(_response.test_index_2.aliases.test_alias, new {});

				//is_false _response.test_index.aliases.test_blias; 
				this.IsFalse(_response.test_index.aliases.test_blias);

				//is_false _response.test_index_2.aliases.test_blias; 
				this.IsFalse(_response.test_index_2.aliases.test_blias);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAliasesViaAllAliasesName10Tests : YamlTestsBase
		{
			[Test]
			public void GetAliasesViaAllAliasesName10Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> this._client.IndicesGetAliases("_all", "test_alias"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//match _response.test_index_2.aliases.test_alias: 
				this.IsMatch(_response.test_index_2.aliases.test_alias, new {});

				//is_false _response.test_index.aliases.test_blias; 
				this.IsFalse(_response.test_index.aliases.test_blias);

				//is_false _response.test_index_2.aliases.test_blias; 
				this.IsFalse(_response.test_index_2.aliases.test_blias);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAliasesViaAliasesName11Tests : YamlTestsBase
		{
			[Test]
			public void GetAliasesViaAliasesName11Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> this._client.IndicesGetAliases("*", "test_alias"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//match _response.test_index_2.aliases.test_alias: 
				this.IsMatch(_response.test_index_2.aliases.test_alias, new {});

				//is_false _response.test_index.aliases.test_blias; 
				this.IsFalse(_response.test_index.aliases.test_blias);

				//is_false _response.test_index_2.aliases.test_blias; 
				this.IsFalse(_response.test_index_2.aliases.test_blias);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAliasesViaPrefAliasesName12Tests : YamlTestsBase
		{
			[Test]
			public void GetAliasesViaPrefAliasesName12Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> this._client.IndicesGetAliases("*2", "test_alias"));

				//match _response.test_index_2.aliases.test_alias: 
				this.IsMatch(_response.test_index_2.aliases.test_alias, new {});

				//is_false _response.test_index.aliases.test_alias; 
				this.IsFalse(_response.test_index.aliases.test_alias);

				//is_false _response.test_index.aliases.test_blias; 
				this.IsFalse(_response.test_index.aliases.test_blias);

				//is_false _response.test_index_2.aliases.test_blias; 
				this.IsFalse(_response.test_index_2.aliases.test_blias);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAliasesViaNameNameAliasesName13Tests : YamlTestsBase
		{
			[Test]
			public void GetAliasesViaNameNameAliasesName13Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> this._client.IndicesGetAliases("test_index,test_index_2", "test_alias"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//match _response.test_index_2.aliases.test_alias: 
				this.IsMatch(_response.test_index_2.aliases.test_alias, new {});

				//is_false _response.test_index.aliases.test_blias; 
				this.IsFalse(_response.test_index.aliases.test_blias);

				//is_false _response.test_index_2.aliases.test_blias; 
				this.IsFalse(_response.test_index_2.aliases.test_blias);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class NonExistentAliasOnAnExistingIndexReturnsMatchingIndcies14Tests : YamlTestsBase
		{
			[Test]
			public void NonExistentAliasOnAnExistingIndexReturnsMatchingIndcies14Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> this._client.IndicesGetAliases("test_index", "non-existent"));

				//match _response.test_index.aliases: 
				this.IsMatch(_response.test_index.aliases, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ExistentAndNonExistentAliasReturnsJustTheExisting15Tests : YamlTestsBase
		{
			[Test]
			public void ExistentAndNonExistentAliasReturnsJustTheExisting15Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> this._client.IndicesGetAliases("test_index", "test_alias,non-existent"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//is_false _responseDictionary[@"test_index"][@"aliases"][@"non-existent"]; 
				this.IsFalse(_responseDictionary[@"test_index"][@"aliases"][@"non-existent"]);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GettingAliasOnAnNonExistentIndexShouldReturn40416Tests : YamlTestsBase
		{
			[Test]
			public void GettingAliasOnAnNonExistentIndexShouldReturn40416Test()
			{	

				//skip 1 - 999; 
				this.Skip("1 - 999", "not implemented yet");

				//do indices.get_aliases 
				this.Do(()=> this._client.IndicesGetAliases("non-existent", "foo"), shouldCatch: @"missing");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAliasesWithLocalFlag17Tests : YamlTestsBase
		{
			[Test]
			public void GetAliasesWithLocalFlag17Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> this._client.IndicesGetAliasesForAll(nv=>nv
					.Add("local", @"true")
				));

				//is_true _response.test_index; 
				this.IsTrue(_response.test_index);

				//is_true _response.test_index_2; 
				this.IsTrue(_response.test_index_2);

			}
		}
	}
}

