using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesGetAliases1
{
	public partial class IndicesGetAliases1YamlTests
	{	
	
		public class IndicesGetAliases110BasicYamlBase : YamlTestsBase
		{
			public IndicesGetAliases110BasicYamlBase() : base()
			{	

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test_index", null));

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test_index_2", null));

				//do indices.put_alias 
				this.Do(()=> _client.IndicesPutAlias("test_index", "test_alias", null));

				//do indices.put_alias 
				this.Do(()=> _client.IndicesPutAlias("test_index", "test_blias", null));

				//do indices.put_alias 
				this.Do(()=> _client.IndicesPutAlias("test_index_2", "test_alias", null));

				//do indices.put_alias 
				this.Do(()=> _client.IndicesPutAlias("test_index_2", "test_blias", null));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAllAliasesViaAliases2Tests : IndicesGetAliases110BasicYamlBase
		{
			[Test]
			public void GetAllAliasesViaAliases2Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> _client.IndicesGetAliasesForAll());

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
		public class GetAllAliasesViaIndexAliases3Tests : IndicesGetAliases110BasicYamlBase
		{
			[Test]
			public void GetAllAliasesViaIndexAliases3Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> _client.IndicesGetAliases("test_index"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//match _response.test_index.aliases.test_blias: 
				this.IsMatch(_response.test_index.aliases.test_blias, new {});

				//is_false _response.test_index_2; 
				this.IsFalse(_response.test_index_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetSpecificAliasViaIndexAliasesName4Tests : IndicesGetAliases110BasicYamlBase
		{
			[Test]
			public void GetSpecificAliasViaIndexAliasesName4Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> _client.IndicesGetAliases("test_index", "test_alias"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//is_false _response.test_index.aliases.test_blias; 
				this.IsFalse(_response.test_index.aliases.test_blias);

				//is_false _response.test_index_2; 
				this.IsFalse(_response.test_index_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAliasesViaIndexAliasesAll5Tests : IndicesGetAliases110BasicYamlBase
		{
			[Test]
			public void GetAliasesViaIndexAliasesAll5Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> _client.IndicesGetAliases("test_index", "_all"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//match _response.test_index.aliases.test_blias: 
				this.IsMatch(_response.test_index.aliases.test_blias, new {});

				//is_false _response.test_index_2; 
				this.IsFalse(_response.test_index_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAliasesViaIndexAliases6Tests : IndicesGetAliases110BasicYamlBase
		{
			[Test]
			public void GetAliasesViaIndexAliases6Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> _client.IndicesGetAliases("test_index", "*"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//match _response.test_index.aliases.test_blias: 
				this.IsMatch(_response.test_index.aliases.test_blias, new {});

				//is_false _response.test_index_2; 
				this.IsFalse(_response.test_index_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAliasesViaIndexAliasesPrefix7Tests : IndicesGetAliases110BasicYamlBase
		{
			[Test]
			public void GetAliasesViaIndexAliasesPrefix7Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> _client.IndicesGetAliases("test_index", "test_a*"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//is_false _response.test_index.aliases.test_blias; 
				this.IsFalse(_response.test_index.aliases.test_blias);

				//is_false _response.test_index_2; 
				this.IsFalse(_response.test_index_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAliasesViaIndexAliasesNameName8Tests : IndicesGetAliases110BasicYamlBase
		{
			[Test]
			public void GetAliasesViaIndexAliasesNameName8Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> _client.IndicesGetAliases("test_index", "test_alias,test_blias"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//match _response.test_index.aliases.test_blias: 
				this.IsMatch(_response.test_index.aliases.test_blias, new {});

				//is_false _response.test_index_2; 
				this.IsFalse(_response.test_index_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAliasesViaAliasesName9Tests : IndicesGetAliases110BasicYamlBase
		{
			[Test]
			public void GetAliasesViaAliasesName9Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> _client.IndicesGetAliasesForAll("test_alias"));

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
		public class GetAliasesViaAllAliasesName10Tests : IndicesGetAliases110BasicYamlBase
		{
			[Test]
			public void GetAliasesViaAllAliasesName10Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> _client.IndicesGetAliases("_all", "test_alias"));

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
		public class GetAliasesViaAliasesName11Tests : IndicesGetAliases110BasicYamlBase
		{
			[Test]
			public void GetAliasesViaAliasesName11Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> _client.IndicesGetAliases("*", "test_alias"));

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
		public class GetAliasesViaPrefAliasesName12Tests : IndicesGetAliases110BasicYamlBase
		{
			[Test]
			public void GetAliasesViaPrefAliasesName12Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> _client.IndicesGetAliases("*2", "test_alias"));

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
		public class GetAliasesViaNameNameAliasesName13Tests : IndicesGetAliases110BasicYamlBase
		{
			[Test]
			public void GetAliasesViaNameNameAliasesName13Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> _client.IndicesGetAliases("test_index,test_index_2", "test_alias"));

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
		public class NonExistentAliasOnAnExistingIndexReturnsMatchingIndcies14Tests : IndicesGetAliases110BasicYamlBase
		{
			[Test]
			public void NonExistentAliasOnAnExistingIndexReturnsMatchingIndcies14Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> _client.IndicesGetAliases("test_index", "non-existent"));

				//match _response.test_index.aliases: 
				this.IsMatch(_response.test_index.aliases, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ExistentAndNonExistentAliasReturnsJustTheExisting15Tests : IndicesGetAliases110BasicYamlBase
		{
			[Test]
			public void ExistentAndNonExistentAliasReturnsJustTheExisting15Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> _client.IndicesGetAliases("test_index", "test_alias,non-existent"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//is_false _response[@"test_index"][@"aliases"][@"non-existent"]; 
				this.IsFalse(_response[@"test_index"][@"aliases"][@"non-existent"]);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GettingAliasOnAnNonExistentIndexShouldReturn40416Tests : IndicesGetAliases110BasicYamlBase
		{
			[Test]
			public void GettingAliasOnAnNonExistentIndexShouldReturn40416Test()
			{	

				//skip 1 - 999; 
				this.Skip("1 - 999", "not implemented yet");

				//do indices.get_aliases 
				this.Do(()=> _client.IndicesGetAliases("non-existent", "foo"), shouldCatch: @"missing");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAliasesWithLocalFlag17Tests : IndicesGetAliases110BasicYamlBase
		{
			[Test]
			public void GetAliasesWithLocalFlag17Test()
			{	

				//do indices.get_aliases 
				this.Do(()=> _client.IndicesGetAliasesForAll(nv=>nv
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

