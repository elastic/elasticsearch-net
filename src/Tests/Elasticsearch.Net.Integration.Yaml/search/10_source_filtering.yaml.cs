using System.Collections.Generic;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Search1
{
	public partial class Search1YamlTests
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
				this.Do(()=> _client.IndicesRefreshForAll());

				//do search 
				_body = @"{ _source: true, query: { match_all: {} } }";
				this.Do(()=> _client.Search(_body));

				//length _response.hits.hits: 1; 
				this.IsLength(_response.hits.hits, 1);

				//match _response.hits.hits[0]._source.count: 
				this.IsMatch(_response.hits.hits[0]._source.count, 1);

				//do search 
				_body = @"{ _source: false, query: { match_all: {} } }";
				this.Do(()=> _client.Search(_body));

				//length _response.hits.hits: 1; 
				this.IsLength(_response.hits.hits, 1);

				//is_false _response.hits.hits[0]._source; 
				this.IsFalse(_response.hits.hits[0]._source);

				//do search 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> _client.Search(_body));

				//length _response.hits.hits: 1; 
				this.IsLength(_response.hits.hits, 1);

				//match _response.hits.hits[0]._source.count: 
				this.IsMatch(_response.hits.hits[0]._source.count, 1);

				//do search 
				_body = new {
					_source= "include.field1",
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> _client.Search(_body));

				//match _response.hits.hits[0]._source.include.field1: 
				this.IsMatch(_response.hits.hits[0]._source.include.field1, @"v1");

				//is_false _response.hits.hits[0]._source.include.field2; 
				this.IsFalse(_response.hits.hits[0]._source.include.field2);

				//do search 
				_body = new {
					_source= "include.field2",
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> _client.Search(_body, nv=>nv
					.AddQueryString("_source_include", @"include.field1")
				));

				//match _response.hits.hits[0]._source.include.field1: 
				this.IsMatch(_response.hits.hits[0]._source.include.field1, @"v1");

				//is_false _response.hits.hits[0]._source.include.field2; 
				this.IsFalse(_response.hits.hits[0]._source.include.field2);

				//do search 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> _client.Search(_body, nv=>nv
					.AddQueryString("_source_include", @"include.field1")
				));

				//match _response.hits.hits[0]._source.include.field1: 
				this.IsMatch(_response.hits.hits[0]._source.include.field1, @"v1");

				//is_false _response.hits.hits[0]._source.include.field2; 
				this.IsFalse(_response.hits.hits[0]._source.include.field2);

				//do search 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> _client.Search(_body, nv=>nv
					.AddQueryString("_source_exclude", @"count")
				));

				//match _response.hits.hits[0]._source.include: 
				this.IsMatch(_response.hits.hits[0]._source.include, new {
					field1= "v1",
					field2= "v2"
				});

				//is_false _response.hits.hits[0]._source.count; 
				this.IsFalse(_response.hits.hits[0]._source.count);

				//do search 
				_body = new {
					_source= new [] {
						"include.field1",
						"include.field2"
					},
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> _client.Search(_body));

				//match _response.hits.hits[0]._source.include.field1: 
				this.IsMatch(_response.hits.hits[0]._source.include.field1, @"v1");

				//match _response.hits.hits[0]._source.include.field2: 
				this.IsMatch(_response.hits.hits[0]._source.include.field2, @"v2");

				//is_false _response.hits.hits[0]._source.count; 
				this.IsFalse(_response.hits.hits[0]._source.count);

				//do search 
				_body = new {
					_source= new {
						include= new [] {
							"include.field1",
							"include.field2"
						}
					},
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> _client.Search(_body));

				//match _response.hits.hits[0]._source.include.field1: 
				this.IsMatch(_response.hits.hits[0]._source.include.field1, @"v1");

				//match _response.hits.hits[0]._source.include.field2: 
				this.IsMatch(_response.hits.hits[0]._source.include.field2, @"v2");

				//is_false _response.hits.hits[0]._source.count; 
				this.IsFalse(_response.hits.hits[0]._source.count);

				//do search 
				_body = new {
					_source= new {
						includes= "include",
						excludes= "*.field2"
					},
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> _client.Search(_body));

				//match _response.hits.hits[0]._source.include.field1: 
				this.IsMatch(_response.hits.hits[0]._source.include.field1, @"v1");

				//is_false _response.hits.hits[0]._source.include.field2; 
				this.IsFalse(_response.hits.hits[0]._source.include.field2);

				//do search 
				_body = new {
					fields= new [] {
						"include.field2"
					},
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> _client.Search(_body));

				//match _response.hits.hits[0].fields: 
				this.IsMatch(_response.hits.hits[0].fields, new Dictionary<string, object> {
					{ @"include.field2", new [] {"v2"} }
				});

				//is_false _response.hits.hits[0]._source; 
				this.IsFalse(_response.hits.hits[0]._source);

				//do search 
				_body = new {
					fields= new [] {
						"include.field2",
						"_source"
					},
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> _client.Search(_body));

				//match _response.hits.hits[0].fields: 
				this.IsMatch(_response.hits.hits[0].fields, new Dictionary<string, object> {
					{ @"include.field2", new [] {"v2"} }
				});

				//is_true _response.hits.hits[0]._source; 
				this.IsTrue(_response.hits.hits[0]._source);

			}
		}
	}
}

