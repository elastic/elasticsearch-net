using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Update5
{
	public partial class Update5YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ScriptUpsert1Tests : YamlTestsBase
		{
			[Test]
			public void ScriptUpsert1Test()
			{	

				//do update 
				_body = new {
					script= "ctx._source.foo = bar",
					@params= new {
						bar= "xxx"
					},
					upsert= new {
						foo= "baz"
					}
				};
				this.Do(()=> _client.Update("test_1", "test", "1", _body));

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1"));

				//match _response._source.foo: 
				this.IsMatch(_response._source.foo, @"baz");

				//do update 
				_body = new {
					script= "ctx._source.foo = bar",
					@params= new {
						bar= "xxx"
					},
					upsert= new {
						foo= "baz"
					}
				};
				this.Do(()=> _client.Update("test_1", "test", "1", _body));

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1"));

				//match _response._source.foo: 
				this.IsMatch(_response._source.foo, @"xxx");

			}
		}
	}
}

