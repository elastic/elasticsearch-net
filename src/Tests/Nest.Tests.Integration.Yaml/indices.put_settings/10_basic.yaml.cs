using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesPutSettings1
{
	public partial class IndicesPutSettings1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestIndicesSettings1Tests : YamlTestsBase
		{
			[Test]
			public void TestIndicesSettings1Test()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						index= new {
							number_of_replicas= "0"
						}
					}
				};
				this.Do(()=> this._client.IndicesCreatePut("test-index", _body));

				//do indices.get_settings 
				this.Do(()=> this._client.IndicesGetSettings("test-index", nv=>nv
					.Add("flat_settings", @"true")
				));

				//match _response[@"test-index"][@"settings"][@"index.number_of_replicas"]: 
				this.IsMatch(_response[@"test-index"][@"settings"][@"index.number_of_replicas"], 0);

				//do indices.put_settings 
				_body = new {
					number_of_replicas= "1"
				};
				this.Do(()=> this._client.IndicesPutSettingsForAll(_body));

				//do indices.get_settings 
				this.Do(()=> this._client.IndicesGetSettingsForAll(nv=>nv
					.Add("flat_settings", @"false")
				));

				//match _response[@"test-index"][@"settings"][@"index"][@"number_of_replicas"]: 
				this.IsMatch(_response[@"test-index"][@"settings"][@"index"][@"number_of_replicas"], 1);

			}
		}
	}
}

