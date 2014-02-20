using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Explain2
{
	public partial class Explain2YamlTests
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

				//do indices.refresh 
				this.Do(()=> _client.IndicesRefresh("test_1"));

				//do explain 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> _client.Explain("test_1", "test", "1", _body, nv=>nv
					.Add("_source", @"false")
				));

				//match _response._index: 
				this.IsMatch(_response._index, @"test_1");

				//match _response._type: 
				this.IsMatch(_response._type, @"test");

				//match _response._id: 
				this.IsMatch(_response._id, 1);

				//is_false _response.get._source; 
				this.IsFalse(_response.get._source);

				//do explain 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> _client.Explain("test_1", "test", "1", _body, nv=>nv
					.Add("_source", @"true")
				));

				//match _response.get._source.include.field1: 
				this.IsMatch(_response.get._source.include.field1, @"v1");

				//do explain 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> _client.Explain("test_1", "test", "1", _body, nv=>nv
					.Add("_source", @"include.field1")
				));

				//match _response.get._source.include.field1: 
				this.IsMatch(_response.get._source.include.field1, @"v1");

				//is_false _response.get._source.include.field2; 
				this.IsFalse(_response.get._source.include.field2);

				//do explain 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> _client.Explain("test_1", "test", "1", _body, nv=>nv
					.Add("_source_include", @"include.field1")
				));

				//match _response.get._source.include.field1: 
				this.IsMatch(_response.get._source.include.field1, @"v1");

				//is_false _response.get._source.include.field2; 
				this.IsFalse(_response.get._source.include.field2);

				//do explain 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> _client.Explain("test_1", "test", "1", _body, nv=>nv
					.Add("_source_include", @"include.field1,include.field2")
				));

				//match _response.get._source.include.field1: 
				this.IsMatch(_response.get._source.include.field1, @"v1");

				//match _response.get._source.include.field2: 
				this.IsMatch(_response.get._source.include.field2, @"v2");

				//is_false _response.get._source.count; 
				this.IsFalse(_response.get._source.count);

				//do explain 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> _client.Explain("test_1", "test", "1", _body, nv=>nv
					.Add("_source_include", @"include")
					.Add("_source_exclude", @"*.field2")
				));

				//match _response.get._source.include.field1: 
				this.IsMatch(_response.get._source.include.field1, @"v1");

				//is_false _response.get._source.include.field2; 
				this.IsFalse(_response.get._source.include.field2);

				//is_false _response.get._source.count; 
				this.IsFalse(_response.get._source.count);

			}
		}
	}
}

