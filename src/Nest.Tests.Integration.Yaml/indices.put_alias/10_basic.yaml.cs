using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesPutAlias
{
	public partial class IndicesPutAliasTests
	{	


		public class BasicTestForPutAliasTests : YamlTestsBase
		{
			[Test]
			public void BasicTestForPutAliasTest()
			{	

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePost("test_index", null));

				//do indices.exists_alias 
				this.Do(()=> this._client.IndicesExistsAliasHead("test_alias"));

				//is_false this._status.Result; 
				this.IsFalse(this._status.Result);

				//do indices.put_alias 
				this.Do(()=> this._client.IndicesPutAlias("test_alias", null, nv=>nv
					.Add("index","test_index")
				));

				//is_true _response.ok; 
				this.IsTrue(_response.ok);

				//do indices.exists_alias 
				this.Do(()=> this._client.IndicesExistsAliasHead("test_alias"));

				//is_true this._status.Result; 
				this.IsTrue(this._status.Result);

				//do indices.get_alias 
				this.Do(()=> this._client.IndicesGetAlias("test_alias"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {});

			}
		}
	}
}

