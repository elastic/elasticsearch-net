using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Search1
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
				this.Do(()=> this._client.IndexPost("test_1", "test", "1", _body));

				//do indices.refresh 
				this.Do(()=> this._client.IndicesRefreshPostForAll());

				//do search 
				_body = @"{ _source: true, query: { match_all: {} } }";
				this.Do(()=> this._client.SearchPost(_body));

				//length _response.hits.hits: 1; 
				this.IsLength(_response.hits.hits, 1);

				//match _response.hits.hits[0]._source.count: 
				this.IsMatch(_response.hits.hits[0]._source.count, 1);

				//do search 
				_body = @"{ _source: false, query: { match_all: {} } }";
				this.Do(()=> this._client.SearchPost(_body));

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
				this.Do(()=> this._client.SearchPost(_body));

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
				this.Do(()=> this._client.SearchPost(_body));

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
				this.Do(()=> this._client.SearchPost(_body, nv=>nv
					.Add("_source_include", @"include.field1")
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
				this.Do(()=> this._client.SearchPost(_body, nv=>nv
					.Add("_source_include", @"include.field1")
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
				this.Do(()=> this._client.SearchPost(_body, nv=>nv
					.Add("_source_exclude", @"count")
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
				this.Do(()=> this._client.SearchPost(_body));

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
				this.Do(()=> this._client.SearchPost(_body));

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
				this.Do(()=> this._client.SearchPost(_body));

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
				this.Do(()=> this._client.SearchPost(_body));

				//match _response.hits.hits[0].fields: 
				this.IsMatch(_response.hits.hits[0].fields, new {
					include.field2= new [] {
						"v2"
					}
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
				this.Do(()=> this._client.SearchPost(_body));

				//match _response.hits.hits[0].fields: 
				this.IsMatch(_response.hits.hits[0].fields, new {
					include.field2= new [] {
						"v2"
					}
				});

				//is_true _response.hits.hits[0]._source; 
				this.IsTrue(_response.hits.hits[0]._source);

			}
		}
	}
}

