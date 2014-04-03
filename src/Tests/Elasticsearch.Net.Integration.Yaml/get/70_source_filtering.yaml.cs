using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Get8
{
	public partial class Get8YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class SourceFiltering1Tests : YamlTestsBase
		{
			[Test]
			public void SourceFiltering1Test()
			{	

				//do index 
				_body = new {
					include= new {
						field1= "v1",
						field2= "v2"
					},
					count= "1"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("_source", @"false")
				));

				//match _response._index: 
				this.IsMatch(_response._index, @"test_1");

				//match _response._type: 
				this.IsMatch(_response._type, @"test");

				//match _response._id: 
				this.IsMatch(_response._id, 1);

				//is_false _response._source; 
				this.IsFalse(_response._source);

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("_source", @"true")
				));

				//match _response._source.include.field1: 
				this.IsMatch(_response._source.include.field1, @"v1");

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("_source", @"include.field1")
				));

				//match _response._source.include.field1: 
				this.IsMatch(_response._source.include.field1, @"v1");

				//is_false _response._source.include.field2; 
				this.IsFalse(_response._source.include.field2);

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("_source_include", @"include.field1")
				));

				//match _response._source.include.field1: 
				this.IsMatch(_response._source.include.field1, @"v1");

				//is_false _response._source.include.field2; 
				this.IsFalse(_response._source.include.field2);

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("_source_include", @"include.field1,include.field2")
				));

				//match _response._source.include.field1: 
				this.IsMatch(_response._source.include.field1, @"v1");

				//match _response._source.include.field2: 
				this.IsMatch(_response._source.include.field2, @"v2");

				//is_false _response._source.count; 
				this.IsFalse(_response._source.count);

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("_source_include", @"include")
					.AddQueryString("_source_exclude", @"*.field2")
				));

				//match _response._source.include.field1: 
				this.IsMatch(_response._source.include.field1, @"v1");

				//is_false _response._source.include.field2; 
				this.IsFalse(_response._source.include.field2);

				//is_false _response._source.count; 
				this.IsFalse(_response._source.count);

				//do get 
				this.Do(()=> _client.Get("test_1", "test", "1", nv=>nv
					.AddQueryString("fields", @"count")
					.AddQueryString("_source", @"true")
				));

				//match _response._index: 
				this.IsMatch(_response._index, @"test_1");

				//match _response._type: 
				this.IsMatch(_response._type, @"test");

				//match _response._id: 
				this.IsMatch(_response._id, 1);

				//match _response.fields.count: 
				this.IsMatch(_response.fields.count, new [] {
					@"1"
				});

				//match _response._source.include.field1: 
				this.IsMatch(_response._source.include.field1, @"v1");

			}
		}
	}
}

