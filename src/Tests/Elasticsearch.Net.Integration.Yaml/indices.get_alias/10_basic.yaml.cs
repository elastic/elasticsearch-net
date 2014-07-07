using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesGetAlias1
{
	public partial class IndicesGetAlias1YamlTests
	{	
	
		public class IndicesGetAlias110BasicYamlBase : YamlTestsBase
		{
			public IndicesGetAlias110BasicYamlBase() : base()
			{	

				//do indices.create 
				_body = new {
					aliases= new {
						test_alias= new {},
						test_blias= new {}
					}
				};
				this.Do(()=> _client.IndicesCreate("test_index", _body));

				//do indices.create 
				_body = new {
					aliases= new {
						test_alias= new {},
						test_blias= new {}
					}
				};
				this.Do(()=> _client.IndicesCreate("test_index_2", _body));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAllAliasesViaAlias2Tests : IndicesGetAlias110BasicYamlBase
		{
			[Test]
			public void GetAllAliasesViaAlias2Test()
			{	

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAliasForAll());

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
		public class GetAllAliasesViaIndexAlias3Tests : IndicesGetAlias110BasicYamlBase
		{
			[Test]
			public void GetAllAliasesViaIndexAlias3Test()
			{	

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAlias("test_index"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//match _response.test_index.aliases.test_blias: 
				this.IsMatch(_response.test_index.aliases.test_blias, new {});

				//is_false _response.test_index_2; 
				this.IsFalse(_response.test_index_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetSpecificAliasViaIndexAliasName4Tests : IndicesGetAlias110BasicYamlBase
		{
			[Test]
			public void GetSpecificAliasViaIndexAliasName4Test()
			{	

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAlias("test_index", "test_alias"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//is_false _response.test_index.aliases.test_blias; 
				this.IsFalse(_response.test_index.aliases.test_blias);

				//is_false _response.test_index_2; 
				this.IsFalse(_response.test_index_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAliasesViaIndexAliasAll5Tests : IndicesGetAlias110BasicYamlBase
		{
			[Test]
			public void GetAliasesViaIndexAliasAll5Test()
			{	

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAlias("test_index", "_all"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//match _response.test_index.aliases.test_blias: 
				this.IsMatch(_response.test_index.aliases.test_blias, new {});

				//is_false _response.test_index_2; 
				this.IsFalse(_response.test_index_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAliasesViaIndexAlias6Tests : IndicesGetAlias110BasicYamlBase
		{
			[Test]
			public void GetAliasesViaIndexAlias6Test()
			{	

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAlias("test_index", "*"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//match _response.test_index.aliases.test_blias: 
				this.IsMatch(_response.test_index.aliases.test_blias, new {});

				//is_false _response.test_index_2; 
				this.IsFalse(_response.test_index_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAliasesViaIndexAliasPrefix7Tests : IndicesGetAlias110BasicYamlBase
		{
			[Test]
			public void GetAliasesViaIndexAliasPrefix7Test()
			{	

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAlias("test_index", "test_a*"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//is_false _response.test_index.aliases.test_blias; 
				this.IsFalse(_response.test_index.aliases.test_blias);

				//is_false _response.test_index_2; 
				this.IsFalse(_response.test_index_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAliasesViaIndexAliasNameName8Tests : IndicesGetAlias110BasicYamlBase
		{
			[Test]
			public void GetAliasesViaIndexAliasNameName8Test()
			{	

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAlias("test_index", "test_alias,test_blias"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//match _response.test_index.aliases.test_blias: 
				this.IsMatch(_response.test_index.aliases.test_blias, new {});

				//is_false _response.test_index_2; 
				this.IsFalse(_response.test_index_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAliasesViaAliasName9Tests : IndicesGetAlias110BasicYamlBase
		{
			[Test]
			public void GetAliasesViaAliasName9Test()
			{	

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAliasForAll("test_alias"));

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
		public class GetAliasesViaAllAliasName10Tests : IndicesGetAlias110BasicYamlBase
		{
			[Test]
			public void GetAliasesViaAllAliasName10Test()
			{	

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAlias("_all", "test_alias"));

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
		public class GetAliasesViaAliasName11Tests : IndicesGetAlias110BasicYamlBase
		{
			[Test]
			public void GetAliasesViaAliasName11Test()
			{	

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAlias("*", "test_alias"));

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
		public class GetAliasesViaPrefAliasName12Tests : IndicesGetAlias110BasicYamlBase
		{
			[Test]
			public void GetAliasesViaPrefAliasName12Test()
			{	

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAlias("*2", "test_alias"));

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
		public class GetAliasesViaNameNameAliasName13Tests : IndicesGetAlias110BasicYamlBase
		{
			[Test]
			public void GetAliasesViaNameNameAliasName13Test()
			{	

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAlias("test_index,test_index_2", "test_alias"));

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
		public class NonExistentAliasOnAnExistingIndexReturnsAnEmptyBody14Tests : IndicesGetAlias110BasicYamlBase
		{
			[Test]
			public void NonExistentAliasOnAnExistingIndexReturnsAnEmptyBody14Test()
			{	

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAlias("test_index", "non-existent"));

				//match this._status: 
				this.IsMatch(this._status, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ExistentAndNonExistentAliasReturnsJustTheExisting15Tests : IndicesGetAlias110BasicYamlBase
		{
			[Test]
			public void ExistentAndNonExistentAliasReturnsJustTheExisting15Test()
			{	

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAlias("test_index", "test_alias,non-existent"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

				//is_false _response[@"test_index"][@"aliases"][@"non-existent"]; 
				this.IsFalse(_response[@"test_index"][@"aliases"][@"non-existent"]);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GettingAliasOnAnNonExistentIndexShouldReturn40416Tests : IndicesGetAlias110BasicYamlBase
		{
			[Test]
			public void GettingAliasOnAnNonExistentIndexShouldReturn40416Test()
			{	

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAlias("non-existent", "foo"), shouldCatch: @"missing");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAliasWithLocalFlag17Tests : IndicesGetAlias110BasicYamlBase
		{
			[Test]
			public void GetAliasWithLocalFlag17Test()
			{	

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAliasForAll(nv=>nv
					.AddQueryString("local", @"true")
				));

				//is_true _response.test_index; 
				this.IsTrue(_response.test_index);

				//is_true _response.test_index_2; 
				this.IsTrue(_response.test_index_2);

			}
		}
	}
}

