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


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ScriptTests : YamlTestsBase
		{
			[Test]
			public void ScriptTest()
			{	

				//do index 
				_body = new {
					foo= "bar",
					count= "1"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "1", _body));

				//do update 
				_body = new {
					lang= "mvel",
					script= "ctx._source.foo = bar",
					@params= new {
						bar= "xxx"
					}
				};
				this.Do(()=> this._client.UpdatePost("test_1", "test", "1", _body, nv=>nv
					.Add("script", 1)
				));

				//match _response._index: 
				this.IsMatch(_response._index, @"test_1");

				//match _response._type: 
				this.IsMatch(_response._type, @"test");

				//match _response._id: 
				this.IsMatch(_response._id, 1);

				//match _response._version: 
				this.IsMatch(_response._version, 2);

				//do get 
				this.Do(()=> this._client.Get("test_1", "test", "1"));

				//match _response._source.foo: 
				this.IsMatch(_response._source.foo, @"xxx");

				//match _response._source.count: 
				this.IsMatch(_response._source.count, 1);

				//do update 
				this.Do(()=> this._client.UpdatePost("test_1", "test", "1", null, nv=>nv
					.Add("lang", @"mvel")
					.Add("script", @"ctx._source.foo = 'yyy'")
				));

				//match _response._index: 
				this.IsMatch(_response._index, @"test_1");

				//match _response._type: 
				this.IsMatch(_response._type, @"test");

				//match _response._id: 
				this.IsMatch(_response._id, 1);

				//match _response._version: 
				this.IsMatch(_response._version, 3);

				//do get 
				this.Do(()=> this._client.Get("test_1", "test", "1"));

				//match _response._source.foo: 
				this.IsMatch(_response._source.foo, @"yyy");

				//match _response._source.count: 
				this.IsMatch(_response._source.count, 1);

				//do update 
				_body = new {
					script= "1",
					lang= "doesnotexist",
					@params= new {
						bar= "xxx"
					}
				};
				this.Do(()=> this._client.UpdatePost("test_1", "test", "1", _body));

				//do update 
				this.Do(()=> this._client.UpdatePost("test_1", "test", "1", null, nv=>nv
					.Add("lang", @"doesnotexist")
					.Add("script", 1)
				));

			}
		}
	}
}

