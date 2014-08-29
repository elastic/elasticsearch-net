using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesDeleteAlias1
{
	public partial class IndicesDeleteAlias1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicTestForDeleteAlias1Tests : YamlTestsBase
		{
			[Test]
			public void BasicTestForDeleteAlias1Test()
			{	

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("testind", null));

				//do indices.put_alias 
				_body = new {
					routing= "routing value"
				};
				this.Do(()=> _client.IndicesPutAlias("testind", "testali", _body));

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAliasForAll("testali"));

				//match _response.testind.aliases.testali.search_routing: 
				this.IsMatch(_response.testind.aliases.testali.search_routing, @"routing value");

				//match _response.testind.aliases.testali.index_routing: 
				this.IsMatch(_response.testind.aliases.testali.index_routing, @"routing value");

				//do indices.delete_alias 
				this.Do(()=> _client.IndicesDeleteAlias("testind", "testali"));

				//do indices.get_alias 
				this.Do(()=> _client.IndicesGetAlias("testind", "testali"));

				//match this._status: 
				this.IsMatch(this._status, new {});

			}
		}
	}
}

