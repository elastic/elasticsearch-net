using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Update
{
	public partial class UpdateTests
	{	


		public class DocUpsertTests : YamlTestsBase
		{
			[Test]
			public void DocUpsertTest()
			{	

				//do update 
				_body = new {
					doc= new {
						foo= "bar",
						count= "1"
					},
					upsert= new {
						foo= "baz"
					}
				};
				this.Do(()=> this._client.UpdatePost("test_1", "test", "1", _body));

				//do get 
				this.Do(()=> this._client.Get("test_1", "test", "1"));

				//match _response._source.foo: 
				this.IsMatch(_response._source.foo, @"baz");

				//is_false _response._source.count; 
				this.IsFalse(_response._source.count);

				//do update 
				_body = new {
					doc= new {
						foo= "bar",
						count= "1"
					},
					upsert= new {
						foo= "baz"
					}
				};
				this.Do(()=> this._client.UpdatePost("test_1", "test", "1", _body));

				//do get 
				this.Do(()=> this._client.Get("test_1", "test", "1"));

				//match _response._source.foo: 
				this.IsMatch(_response._source.foo, @"bar");

				//match _response._source.count: 
				this.IsMatch(_response._source.count, 1);

			}
		}
	}
}

