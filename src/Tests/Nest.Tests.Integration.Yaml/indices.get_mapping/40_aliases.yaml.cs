using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesGetMapping4
{
	public partial class IndicesGetMapping4YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GettingMappingForAliasesShouldReturnTheRealIndexAsKey1Tests : YamlTestsBase
		{
			[Test]
			public void GettingMappingForAliasesShouldReturnTheRealIndexAsKey1Test()
			{	

				//do indices.create 
				_body = new {
					mappings= new {
						test_type= new {
							properties= new {
								text= new {
									type= "string",
									analyzer= "whitespace"
								}
							}
						}
					}
				};
				this.Do(()=> _client.IndicesCreate("test_index", _body));

				//do indices.put_alias 
				this.Do(()=> _client.IndicesPutAlias("test_index", "test_alias", null));

				//do indices.get_mapping 
				this.Do(()=> _client.IndicesGetMapping("test_alias"));

				//match _response.test_index.mappings.test_type.properties.text.type: 
				this.IsMatch(_response.test_index.mappings.test_type.properties.text.type, @"string");

				//match _response.test_index.mappings.test_type.properties.text.analyzer: 
				this.IsMatch(_response.test_index.mappings.test_type.properties.text.analyzer, @"whitespace");

			}
		}
	}
}

