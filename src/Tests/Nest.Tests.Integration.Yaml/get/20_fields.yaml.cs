using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Get3
{
	public partial class Get3YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Fields1Tests : YamlTestsBase
		{
			[Test]
			public void Fields1Test()
			{	

				//do index 
				_body = new {
					foo= "bar",
					count= "1"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.Add("fields", @"foo")
				));

				//match _response._index: 
				this.IsMatch(_response._index, @"test_1");

				//match _response._type: 
				this.IsMatch(_response._type, @"test");

				//match _response._id: 
				this.IsMatch(_response._id, 1);

				//match _response.fields.foo: 
				this.IsMatch(_response.fields.foo, new [] {
					@"bar"
				});

				//is_false _response._source; 
				this.IsFalse(_response._source);

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.Add("fields", new [] {
						@"foo",
						@"count"
					})
				));

				//match _response.fields.foo: 
				this.IsMatch(_response.fields.foo, new [] {
					@"bar"
				});

				//match _response.fields.count: 
				this.IsMatch(_response.fields.count, new [] {
					@"1"
				});

				//is_false _response._source; 
				this.IsFalse(_response._source);

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.Add("fields", new [] {
						@"foo",
						@"count",
						@"_source"
					})
				));

				//match _response.fields.foo: 
				this.IsMatch(_response.fields.foo, new [] {
					@"bar"
				});

				//match _response.fields.count: 
				this.IsMatch(_response.fields.count, new [] {
					@"1"
				});

				//match _response._source.foo: 
				this.IsMatch(_response._source.foo, @"bar");

			}
		}
	}
}

