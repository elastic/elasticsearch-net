using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesUpdateAliases1
{
	public partial class IndicesUpdateAliases1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicTestForAliases1Tests : YamlTestsBase
		{
			[Test]
			public void BasicTestForAliases1Test()
			{	

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePost("test_index", null));

				//do indices.exists_alias 
				this.Do(()=> this._client.IndicesExistsAliasHeadForAll("test_alias"));

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
				this.Do(()=> this._client.IndicesUpdateAliasesPostForAll(_body));

				//do indices.exists_alias 
				this.Do(()=> this._client.IndicesExistsAliasHeadForAll("test_alias"));

				//is_true this._status; 
				this.IsTrue(this._status);

				//do indices.get_alias 
				this.Do(()=> this._client.IndicesGetAlias("test_index", "test_alias"));

				//match _response.test_index.aliases.test_alias: 
				this.IsMatch(_response.test_index.aliases.test_alias, new {
					index_routing= "routing_value",
					search_routing= "routing_value"
				});

			}
		}
	}
}

