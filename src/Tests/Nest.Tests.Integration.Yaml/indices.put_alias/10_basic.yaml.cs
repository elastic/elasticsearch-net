using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesPutAlias1
{
	public partial class IndicesPutAlias1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicTestForPutAlias1Tests : YamlTestsBase
		{
			[Test]
			public void BasicTestForPutAlias1Test()
			{	

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test_index", null));

				//do indices.exists_alias 
				this.Do(()=> _client.IndicesExistsAliasForAll("test_alias"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.put_alias 
				this.Do(()=> _client.IndicesPutAlias("test_index", "test_alias", null));

				//do indices.exists_alias 
				this.Do(()=> _client.IndicesExistsAliasForAll("test_alias"));

				//is_true this._status; 
				this.IsTrue(this._status);

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAliasForAll("test_alias"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

			}
		}
	}
}

