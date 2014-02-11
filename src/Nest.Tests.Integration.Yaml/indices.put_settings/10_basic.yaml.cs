using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesPutSettings
{
	public partial class IndicesPutSettingsTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestIndicesSettingsTests : YamlTestsBase
		{
			[Test]
			public void TestIndicesSettingsTest()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						index= new {
							number_of_replicas= "0"
						}
					}
				};
				this.Do(()=> this._client.IndicesCreatePost("test-index", _body));

				//do indices.get_settings 
				this.Do(()=> this._client.IndicesGetSettings("test-index"));

				//match _responseDictionary[@"test-index"][@"settings"][@"index"][@"number_of_replicas"]: 
				this.IsMatch(_responseDictionary[@"test-index"][@"settings"][@"index"][@"number_of_replicas"], 0);

				//do indices.put_settings 
				_body = new {
					number_of_replicas= "1"
				};
				this.Do(()=> this._client.IndicesPutSettings(_body));

				//do indices.get_settings 
				this.Do(()=> this._client.IndicesGetSettings());

				//match _responseDictionary[@"test-index"][@"settings"][@"index"][@"number_of_replicas"]: 
				this.IsMatch(_responseDictionary[@"test-index"][@"settings"][@"index"][@"number_of_replicas"], 1);

			}
		}
	}
}

