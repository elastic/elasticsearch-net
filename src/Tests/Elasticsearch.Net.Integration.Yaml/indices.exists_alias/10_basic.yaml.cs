using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesExistsAlias1
{
	public partial class IndicesExistsAlias1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestIndicesExistsAlias1Tests : YamlTestsBase
		{
			[Test]
			public void TestIndicesExistsAlias1Test()
			{	

				//do indices.exists_alias 
				this.Do(()=> _client.IndicesExistsAliasForAll("test_alias"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test_index", null));

				//do indices.put_alias 
				this.Do(()=> _client.IndicesPutAlias("test_index", "test_alias", null));

				//do indices.exists_alias 
				this.Do(()=> _client.IndicesExistsAliasForAll("test_alias"));

				//is_true this._status; 
				this.IsTrue(this._status);

				//do indices.exists_alias 
				this.Do(()=> _client.IndicesExistsAlias("test_index", "test_alias"));

				//is_true this._status; 
				this.IsTrue(this._status);

				//do indices.exists_alias 
				this.Do(()=> _client.IndicesExistsAlias("test_index1", "test_alias"));

				//is_false this._status; 
				this.IsFalse(this._status);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestIndicesExistsAliasWithLocalFlag2Tests : YamlTestsBase
		{
			[Test]
			public void TestIndicesExistsAliasWithLocalFlag2Test()
			{	

				//do indices.exists_alias 
				this.Do(()=> _client.IndicesExistsAliasForAll("test_alias", nv=>nv
					.AddQueryString("local", @"true")
				));

				//is_false this._status; 
				this.IsFalse(this._status);

			}
		}
	}
}

