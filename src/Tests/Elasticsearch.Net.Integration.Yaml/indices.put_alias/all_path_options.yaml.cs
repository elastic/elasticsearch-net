using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesPutAlias2
{
	public partial class IndicesPutAlias2YamlTests
	{	
	
		public class IndicesPutAlias2AllPathOptionsYamlBase : YamlTestsBase
		{
			public IndicesPutAlias2AllPathOptionsYamlBase() : base()
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
		public class PutAliasPerIndex2Tests : IndicesPutAlias2AllPathOptionsYamlBase
		{
			[Test]
			public void PutAliasPerIndex2Test()
			{	

				//do indices.put_alias 
				this.Do(()=> _client.IndicesPutAlias("test_index1", "alias", null));

				//do indices.put_alias 
				this.Do(()=> _client.IndicesPutAlias("test_index2", "alias", null));

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAliasForAll("alias"));

				//match _response.test_index1.aliases.alias: 
				this.IsMatch(_response.test_index1.aliases.alias, new {});

				//match _response.test_index2.aliases.alias: 
				this.IsMatch(_response.test_index2.aliases.alias, new {});

				//is_false _response.foo; 
				this.IsFalse(_response.foo);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutAliasInAllIndex3Tests : IndicesPutAlias2AllPathOptionsYamlBase
		{
			[Test]
			public void PutAliasInAllIndex3Test()
			{	

				//do indices.put_alias 
				this.Do(()=> _client.IndicesPutAlias("_all", "alias", null));

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAliasForAll("alias"));

				//match _response.test_index1.aliases.alias: 
				this.IsMatch(_response.test_index1.aliases.alias, new {});

				//match _response.test_index2.aliases.alias: 
				this.IsMatch(_response.test_index2.aliases.alias, new {});

				//match _response.foo.aliases.alias: 
				this.IsMatch(_response.foo.aliases.alias, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutAliasInIndex4Tests : IndicesPutAlias2AllPathOptionsYamlBase
		{
			[Test]
			public void PutAliasInIndex4Test()
			{	

				//do indices.put_alias 
				this.Do(()=> _client.IndicesPutAlias("*", "alias", null));

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAliasForAll("alias"));

				//match _response.test_index1.aliases.alias: 
				this.IsMatch(_response.test_index1.aliases.alias, new {});

				//match _response.test_index2.aliases.alias: 
				this.IsMatch(_response.test_index2.aliases.alias, new {});

				//match _response.foo.aliases.alias: 
				this.IsMatch(_response.foo.aliases.alias, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutAliasPrefixIndex5Tests : IndicesPutAlias2AllPathOptionsYamlBase
		{
			[Test]
			public void PutAliasPrefixIndex5Test()
			{	

				//do indices.put_alias 
				this.Do(()=> _client.IndicesPutAlias("test_*", "alias", null));

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAliasForAll("alias"));

				//match _response.test_index1.aliases.alias: 
				this.IsMatch(_response.test_index1.aliases.alias, new {});

				//match _response.test_index2.aliases.alias: 
				this.IsMatch(_response.test_index2.aliases.alias, new {});

				//is_false _response.foo; 
				this.IsFalse(_response.foo);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutAliasInListOfIndices6Tests : IndicesPutAlias2AllPathOptionsYamlBase
		{
			[Test]
			public void PutAliasInListOfIndices6Test()
			{	

				//do indices.put_alias 
				this.Do(()=> _client.IndicesPutAlias("test_index1,test_index2", "alias", null));

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAliasForAll("alias"));

				//match _response.test_index1.aliases.alias: 
				this.IsMatch(_response.test_index1.aliases.alias, new {});

				//match _response.test_index2.aliases.alias: 
				this.IsMatch(_response.test_index2.aliases.alias, new {});

				//is_false _response.foo; 
				this.IsFalse(_response.foo);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutAliasWithBlankIndex7Tests : IndicesPutAlias2AllPathOptionsYamlBase
		{
			[Test]
			public void PutAliasWithBlankIndex7Test()
			{	

				//do indices.put_alias 
				this.Do(()=> _client.IndicesPutAliasForAll("alias", null));

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAliasForAll("alias"));

				//match _response.test_index1.aliases.alias: 
				this.IsMatch(_response.test_index1.aliases.alias, new {});

				//match _response.test_index2.aliases.alias: 
				this.IsMatch(_response.test_index2.aliases.alias, new {});

				//match _response.foo.aliases.alias: 
				this.IsMatch(_response.foo.aliases.alias, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutAliasWithMissingName8Tests : IndicesPutAlias2AllPathOptionsYamlBase
		{
			[Test]
			public void PutAliasWithMissingName8Test()
			{	

				//do indices.put_alias 
				this.Do(()=> _client.IndicesPutAliasForAll("", null), shouldCatch: @"param");

			}
		}
	}
}

