using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesExistsAlias1
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
				this.Do(()=> this._client.IndicesExistsAliasHeadForAll("test_alias"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePut("test_index", null));

				//do indices.put_alias 
				this.Do(()=> this._client.IndicesPutAlias("test_index", "test_alias", null));

				//do indices.exists_alias 
				this.Do(()=> this._client.IndicesExistsAliasHeadForAll("test_alias"));

				//is_true this._status; 
				this.IsTrue(this._status);

				//do indices.exists_alias 
				this.Do(()=> this._client.IndicesExistsAliasHead("test_index", "test_alias"));

				//is_true this._status; 
				this.IsTrue(this._status);

				//do indices.exists_alias 
				this.Do(()=> this._client.IndicesExistsAliasHead("test_index1", "test_alias"));

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
				this.Do(()=> this._client.IndicesExistsAliasHeadForAll("test_alias", nv=>nv
					.Add("local", @"true")
				));

				//is_false this._status; 
				this.IsFalse(this._status);

			}
		}
	}
}

