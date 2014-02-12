using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesUpdateAliases
{
	public partial class IndicesUpdateAliasesTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicTestForAliasesTests : YamlTestsBase
		{
			[Test]
			public void BasicTestForAliasesTest()
			{	

				//skip 0 - 0.90.0; 
				this.Skip("0 - 0.90.0", "Exists alias not supported before 0.90.1");

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePost("test_index", null));

				//do indices.exists_alias 
				this.Do(()=> this._client.IndicesExistsAliasHead("test_alias"));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do indices.update_aliases 
				_body = new {
					actions= new [] {
						new {
							add= new {
								index= "test_index",
								alias= "test_alias",
								routing= "routing_value"
							}
						}
					}
				};
				this.Do(()=> this._client.IndicesUpdateAliasesPost(_body));

				//is_true _response.ok; 
				this.IsTrue(_response.ok);

				//do indices.exists_alias 
				this.Do(()=> this._client.IndicesExistsAliasHead("test_alias"));

				//is_true this._status; 
				this.IsTrue(this._status);

				//do indices.get_aliases 
				this.Do(()=> this._client.IndicesGetAliases("test_index"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {
					index_routing= "routing_value",
					search_routing= "routing_value"
				});

			}
		}
	}
}

