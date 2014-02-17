using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesGetSettings2
{
	public partial class IndicesGetSettings2YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GettingSettingsForAliasesShouldReturnTheRealIndexAsKey1Tests : YamlTestsBase
		{
			[Test]
			public void GettingSettingsForAliasesShouldReturnTheRealIndexAsKey1Test()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						index= new {
							refresh_interval= "-1",
							number_of_shards= "2",
							number_of_replicas= "3"
						}
					}
				};
				this.Do(()=> this._client.IndicesCreatePut("test-index", _body));

				//do indices.put_alias 
				this.Do(()=> this._client.IndicesPutAlias("test-index", "test-alias", null));

				//do indices.get_settings 
				this.Do(()=> this._client.IndicesGetSettings("test-alias"));

				//match _responseDictionary[@"test-index"][@"settings"][@"index"][@"number_of_replicas"]: 
				this.IsMatch(_responseDictionary[@"test-index"][@"settings"][@"index"][@"number_of_replicas"], 3);

				//match _responseDictionary[@"test-index"][@"settings"][@"index"][@"number_of_shards"]: 
				this.IsMatch(_responseDictionary[@"test-index"][@"settings"][@"index"][@"number_of_shards"], 2);

				//match _responseDictionary[@"test-index"][@"settings"][@"index"][@"refresh_interval"]: 
				this.IsMatch(_responseDictionary[@"test-index"][@"settings"][@"index"][@"refresh_interval"], -1);

			}
		}
	}
}

