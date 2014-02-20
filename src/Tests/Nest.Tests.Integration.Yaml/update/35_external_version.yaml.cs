using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Update7
{
	public partial class Update7YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ExternalVersion1Tests : YamlTestsBase
		{
			[Test]
			public void ExternalVersion1Test()
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
					.Add("version", 2)
					.Add("version_type", @"external")
				));

				//match _response._version: 
				this.IsMatch(_response._version, 2);

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
					.Add("version", 2)
					.Add("version_type", @"external")
				), shouldCatch: @"conflict");

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
					.Add("version", 3)
					.Add("version_type", @"external")
				));

				//match _response._version: 
				this.IsMatch(_response._version, 3);

			}
		}
	}
}

