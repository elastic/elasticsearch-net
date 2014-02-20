using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Update14
{
	public partial class Update14YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Fields1Tests : YamlTestsBase
		{
			[Test]
			public void Fields1Test()
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
					.Add("fields", @"foo,bar,_source")
				));

				//match _response.get._source.foo: 
				this.IsMatch(_response.get._source.foo, @"bar");

				//match _response.get.fields.foo: 
				this.IsMatch(_response.get.fields.foo, new [] {
					@"bar"
				});

				//is_false _response.get.fields.bar; 
				this.IsFalse(_response.get.fields.bar);

			}
		}
	}
}

