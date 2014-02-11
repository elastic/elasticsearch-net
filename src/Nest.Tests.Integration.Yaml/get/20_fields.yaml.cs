using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Get
{
	public partial class GetTests
	{	


		public class FieldsTests : YamlTestsBase
		{
			[Test]
			public void FieldsTest()
			{	

				//do index 
				_body = new {
					foo= "bar",
					count= "1"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "1", _body));

				//do get 
				this.Do(()=> this._client.Get("test_1", "test", "1", nv=>nv
					.Add("fields","foo")
				));

				//match _response._index: 
				this.IsMatch(_response._index, @"test_1");

				//match _response._type: 
				this.IsMatch(_response._type, @"test");

				//match _response._id: 
				this.IsMatch(_response._id, 1);

				//match _response.fields.foo: 
				this.IsMatch(_response.fields.foo, @"bar");

				//is_false _response._source; 
				this.IsFalse(_response._source);

				//do get 
				this.Do(()=> this._client.Get("test_1", "test", "1", nv=>nv
					.Add("fields","System.Collections.Generic.List`1[System.Object]")
				));

				//match _response.fields.foo: 
				this.IsMatch(_response.fields.foo, @"bar");

				//match _response.fields.count: 
				this.IsMatch(_response.fields.count, 1);

				//is_false _response._source; 
				this.IsFalse(_response._source);

				//do get 
				this.Do(()=> this._client.Get("test_1", "test", "1", nv=>nv
					.Add("fields","System.Collections.Generic.List`1[System.Object]")
				));

				//match _response.fields.foo: 
				this.IsMatch(_response.fields.foo, @"bar");

				//match _response.fields.count: 
				this.IsMatch(_response.fields.count, 1);

				//match _response._source.foo: 
				this.IsMatch(_response._source.foo, @"bar");

			}
		}
	}
}

