using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Update7
{
	public partial class Update7YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class NotSupportedVersions1Tests : YamlTestsBase
		{
			[Test]
			public void NotSupportedVersions1Test()
			{	

				//do update 
				_body = new {
					doc= new {
						foo= "baz"
					},
					upsert= new {
						foo= "bar"
					}
				};
				this.Do(()=> _client.Update("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("version", 2)
					.AddQueryString("version_type", @"external")
				), shouldCatch: @"/Validation|Invalid/");

				//do update 
				_body = new {
					doc= new {
						foo= "baz"
					},
					upsert= new {
						foo= "bar"
					}
				};
				this.Do(()=> _client.Update("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("version", 2)
					.AddQueryString("version_type", @"external_gte")
				), shouldCatch: @"/Validation|Invalid/");

			}
		}
	}
}

