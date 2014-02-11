using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Mget
{
	public partial class MgetTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class FieldsTests : YamlTestsBase
		{
			[Test]
			public void FieldsTest()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "1", _body));

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status", @"yellow")
				));

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_id= "1"
						},
						new {
							_id= "1",
							fields= "foo"
						},
						new {
							_id= "1",
							fields= new [] {
								"foo"
							}
						},
						new {
							_id= "1",
							fields= new [] {
								"foo",
								"_source"
							}
						}
					}
				};
				this.Do(()=> this._client.MgetPost("test_1", "test", _body));

				//is_false _response.docs[0].fields; 
				this.IsFalse(_response.docs[0].fields);

				//match _response.docs[0]._source: 
				this.IsMatch(_response.docs[0]._source, new {
					foo= "bar"
				});

				//match _response.docs[1].fields.foo: 
				this.IsMatch(_response.docs[1].fields.foo, @"bar");

				//is_false _response.docs[1]._source; 
				this.IsFalse(_response.docs[1]._source);

				//match _response.docs[2].fields.foo: 
				this.IsMatch(_response.docs[2].fields.foo, @"bar");

				//is_false _response.docs[2]._source; 
				this.IsFalse(_response.docs[2]._source);

				//match _response.docs[3].fields.foo: 
				this.IsMatch(_response.docs[3].fields.foo, @"bar");

				//match _response.docs[3]._source: 
				this.IsMatch(_response.docs[3]._source, new {
					foo= "bar"
				});

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_id= "1"
						},
						new {
							_id= "1",
							fields= "foo"
						},
						new {
							_id= "1",
							fields= new [] {
								"foo"
							}
						},
						new {
							_id= "1",
							fields= new [] {
								"foo",
								"_source"
							}
						}
					}
				};
				this.Do(()=> this._client.MgetPost("test_1", "test", _body, nv=>nv
					.Add("fields", @"foo")
				));

				//match _response.docs[0].fields.foo: 
				this.IsMatch(_response.docs[0].fields.foo, @"bar");

				//is_false _response.docs[0]._source; 
				this.IsFalse(_response.docs[0]._source);

				//match _response.docs[1].fields.foo: 
				this.IsMatch(_response.docs[1].fields.foo, @"bar");

				//is_false _response.docs[1]._source; 
				this.IsFalse(_response.docs[1]._source);

				//match _response.docs[2].fields.foo: 
				this.IsMatch(_response.docs[2].fields.foo, @"bar");

				//is_false _response.docs[2]._source; 
				this.IsFalse(_response.docs[2]._source);

				//match _response.docs[3].fields.foo: 
				this.IsMatch(_response.docs[3].fields.foo, @"bar");

				//match _response.docs[3]._source: 
				this.IsMatch(_response.docs[3]._source, new {
					foo= "bar"
				});

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_id= "1"
						},
						new {
							_id= "1",
							fields= "foo"
						},
						new {
							_id= "1",
							fields= new [] {
								"foo"
							}
						},
						new {
							_id= "1",
							fields= new [] {
								"foo",
								"_source"
							}
						}
					}
				};
				this.Do(()=> this._client.MgetPost("test_1", "test", _body, nv=>nv
					.Add("fields", new [] {
						"foo"
					})
				));

				//match _response.docs[0].fields.foo: 
				this.IsMatch(_response.docs[0].fields.foo, @"bar");

				//is_false _response.docs[0]._source; 
				this.IsFalse(_response.docs[0]._source);

				//match _response.docs[1].fields.foo: 
				this.IsMatch(_response.docs[1].fields.foo, @"bar");

				//is_false _response.docs[1]._source; 
				this.IsFalse(_response.docs[1]._source);

				//match _response.docs[2].fields.foo: 
				this.IsMatch(_response.docs[2].fields.foo, @"bar");

				//is_false _response.docs[2]._source; 
				this.IsFalse(_response.docs[2]._source);

				//match _response.docs[3].fields.foo: 
				this.IsMatch(_response.docs[3].fields.foo, @"bar");

				//match _response.docs[3]._source: 
				this.IsMatch(_response.docs[3]._source, new {
					foo= "bar"
				});

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_id= "1"
						},
						new {
							_id= "1",
							fields= "foo"
						},
						new {
							_id= "1",
							fields= new [] {
								"foo"
							}
						},
						new {
							_id= "1",
							fields= new [] {
								"foo",
								"_source"
							}
						}
					}
				};
				this.Do(()=> this._client.MgetPost("test_1", "test", _body, nv=>nv
					.Add("fields", new [] {
						"foo",
						"_source"
					})
				));

				//match _response.docs[0].fields.foo: 
				this.IsMatch(_response.docs[0].fields.foo, @"bar");

				//match _response.docs[0]._source: 
				this.IsMatch(_response.docs[0]._source, new {
					foo= "bar"
				});

				//match _response.docs[1].fields.foo: 
				this.IsMatch(_response.docs[1].fields.foo, @"bar");

				//is_false _response.docs[1]._source; 
				this.IsFalse(_response.docs[1]._source);

				//match _response.docs[2].fields.foo: 
				this.IsMatch(_response.docs[2].fields.foo, @"bar");

				//is_false _response.docs[2]._source; 
				this.IsFalse(_response.docs[2]._source);

				//match _response.docs[3].fields.foo: 
				this.IsMatch(_response.docs[3].fields.foo, @"bar");

				//match _response.docs[3]._source: 
				this.IsMatch(_response.docs[3]._source, new {
					foo= "bar"
				});

			}
		}
	}
}

