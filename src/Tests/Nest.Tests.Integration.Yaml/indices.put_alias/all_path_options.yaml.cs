using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesPutAlias2
{
	public partial class IndicesPutAlias2YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Setup1Tests : YamlTestsBase
		{
			[Test]
			public void Setup1Test()
			{	

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePut("test_index1", null));

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePut("test_index2", null));

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePut("foo", null));

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutAliasPerIndex2Tests : YamlTestsBase
		{
			[Test]
			public void PutAliasPerIndex2Test()
			{	

				//do indices.put_alias 
				this.Do(()=> this._client.IndicesPutAlias("test_index1", "alias", null));

				//do indices.put_alias 
				this.Do(()=> this._client.IndicesPutAlias("test_index2", "alias", null));

				//do indices.get_alias 
				this.Do(()=> this._client.IndicesGetAliasForAll("alias"));

				//match _response.test_index1.aliases.alias: 
				this.IsMatch(_response.test_index1.aliases.alias, new {});

				//match _response.test_index2.aliases.alias: 
				this.IsMatch(_response.test_index2.aliases.alias, new {});

				//is_false _response.foo; 
				this.IsFalse(_response.foo);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutAliasInAllIndex3Tests : YamlTestsBase
		{
			[Test]
			public void PutAliasInAllIndex3Test()
			{	

				//do indices.put_alias 
				this.Do(()=> this._client.IndicesPutAlias("_all", "alias", null));

				//do indices.get_alias 
				this.Do(()=> this._client.IndicesGetAliasForAll("alias"));

				//match _response.test_index1.aliases.alias: 
				this.IsMatch(_response.test_index1.aliases.alias, new {});

				//match _response.test_index2.aliases.alias: 
				this.IsMatch(_response.test_index2.aliases.alias, new {});

				//match _response.foo.aliases.alias: 
				this.IsMatch(_response.foo.aliases.alias, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutAliasInIndex4Tests : YamlTestsBase
		{
			[Test]
			public void PutAliasInIndex4Test()
			{	

				//do indices.put_alias 
				this.Do(()=> this._client.IndicesPutAlias("*", "alias", null));

				//do indices.get_alias 
				this.Do(()=> this._client.IndicesGetAliasForAll("alias"));

				//match _response.test_index1.aliases.alias: 
				this.IsMatch(_response.test_index1.aliases.alias, new {});

				//match _response.test_index2.aliases.alias: 
				this.IsMatch(_response.test_index2.aliases.alias, new {});

				//match _response.foo.aliases.alias: 
				this.IsMatch(_response.foo.aliases.alias, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutAliasPrefixIndex5Tests : YamlTestsBase
		{
			[Test]
			public void PutAliasPrefixIndex5Test()
			{	

				//do indices.put_alias 
				this.Do(()=> this._client.IndicesPutAlias("test_*", "alias", null));

				//do indices.get_alias 
				this.Do(()=> this._client.IndicesGetAliasForAll("alias"));

				//match _response.test_index1.aliases.alias: 
				this.IsMatch(_response.test_index1.aliases.alias, new {});

				//match _response.test_index2.aliases.alias: 
				this.IsMatch(_response.test_index2.aliases.alias, new {});

				//is_false _response.foo; 
				this.IsFalse(_response.foo);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutAliasInListOfIndices6Tests : YamlTestsBase
		{
			[Test]
			public void PutAliasInListOfIndices6Test()
			{	

				//do indices.put_alias 
				this.Do(()=> this._client.IndicesPutAlias("test_index1,test_index2", "alias", null));

				//do indices.get_alias 
				this.Do(()=> this._client.IndicesGetAliasForAll("alias"));

				//match _response.test_index1.aliases.alias: 
				this.IsMatch(_response.test_index1.aliases.alias, new {});

				//match _response.test_index2.aliases.alias: 
				this.IsMatch(_response.test_index2.aliases.alias, new {});

				//is_false _response.foo; 
				this.IsFalse(_response.foo);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutAliasWithBlankIndex7Tests : YamlTestsBase
		{
			[Test]
			public void PutAliasWithBlankIndex7Test()
			{	

				//do indices.put_alias 
				this.Do(()=> this._client.IndicesPutAlias("", "alias", null));

				//do indices.get_alias 
				this.Do(()=> this._client.IndicesGetAliasForAll("alias"));

				//match _response.test_index1.aliases.alias: 
				this.IsMatch(_response.test_index1.aliases.alias, new {});

				//match _response.test_index2.aliases.alias: 
				this.IsMatch(_response.test_index2.aliases.alias, new {});

				//match _response.foo.aliases.alias: 
				this.IsMatch(_response.foo.aliases.alias, new {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutAliasWithMissingName8Tests : YamlTestsBase
		{
			[Test]
			public void PutAliasWithMissingName8Test()
			{	

				//do indices.put_alias 
				this.Do(()=> this._client.IndicesPutAliasForAll("", null), shouldCatch: @"param");

			}
		}
	}
}

