using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Mget6
{
	public partial class Mget6YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Fields1Tests : YamlTestsBase
		{
			[Test]
			public void Fields1Test()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"yellow")
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
				this.Do(()=> _client.Mget("test_1", "test", _body));

				//is_false _response.docs[0].fields; 
				this.IsFalse(_response.docs[0].fields);

				//match _response.docs[0]._source: 
				this.IsMatch(_response.docs[0]._source, new {
					foo= "bar"
				});

				//match _response.docs[1].fields.foo: 
				this.IsMatch(_response.docs[1].fields.foo, new [] {
					@"bar"
				});

				//is_false _response.docs[1]._source; 
				this.IsFalse(_response.docs[1]._source);

				//match _response.docs[2].fields.foo: 
				this.IsMatch(_response.docs[2].fields.foo, new [] {
					@"bar"
				});

				//is_false _response.docs[2]._source; 
				this.IsFalse(_response.docs[2]._source);

				//match _response.docs[3].fields.foo: 
				this.IsMatch(_response.docs[3].fields.foo, new [] {
					@"bar"
				});

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
				this.Do(()=> _client.Mget("test_1", "test", _body, nv=>nv
					.AddQueryString("fields", @"foo")
				));

				//match _response.docs[0].fields.foo: 
				this.IsMatch(_response.docs[0].fields.foo, new [] {
					@"bar"
				});

				//is_false _response.docs[0]._source; 
				this.IsFalse(_response.docs[0]._source);

				//match _response.docs[1].fields.foo: 
				this.IsMatch(_response.docs[1].fields.foo, new [] {
					@"bar"
				});

				//is_false _response.docs[1]._source; 
				this.IsFalse(_response.docs[1]._source);

				//match _response.docs[2].fields.foo: 
				this.IsMatch(_response.docs[2].fields.foo, new [] {
					@"bar"
				});

				//is_false _response.docs[2]._source; 
				this.IsFalse(_response.docs[2]._source);

				//match _response.docs[3].fields.foo: 
				this.IsMatch(_response.docs[3].fields.foo, new [] {
					@"bar"
				});

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
				this.Do(()=> _client.Mget("test_1", "test", _body, nv=>nv
					.AddQueryString("fields", new [] {
						@"foo"
					})
				));

				//match _response.docs[0].fields.foo: 
				this.IsMatch(_response.docs[0].fields.foo, new [] {
					@"bar"
				});

				//is_false _response.docs[0]._source; 
				this.IsFalse(_response.docs[0]._source);

				//match _response.docs[1].fields.foo: 
				this.IsMatch(_response.docs[1].fields.foo, new [] {
					@"bar"
				});

				//is_false _response.docs[1]._source; 
				this.IsFalse(_response.docs[1]._source);

				//match _response.docs[2].fields.foo: 
				this.IsMatch(_response.docs[2].fields.foo, new [] {
					@"bar"
				});

				//is_false _response.docs[2]._source; 
				this.IsFalse(_response.docs[2]._source);

				//match _response.docs[3].fields.foo: 
				this.IsMatch(_response.docs[3].fields.foo, new [] {
					@"bar"
				});

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
				this.Do(()=> _client.Mget("test_1", "test", _body, nv=>nv
					.AddQueryString("fields", new [] {
						@"foo",
						@"_source"
					})
				));

				//match _response.docs[0].fields.foo: 
				this.IsMatch(_response.docs[0].fields.foo, new [] {
					@"bar"
				});

				//match _response.docs[0]._source: 
				this.IsMatch(_response.docs[0]._source, new {
					foo= "bar"
				});

				//match _response.docs[1].fields.foo: 
				this.IsMatch(_response.docs[1].fields.foo, new [] {
					@"bar"
				});

				//is_false _response.docs[1]._source; 
				this.IsFalse(_response.docs[1]._source);

				//match _response.docs[2].fields.foo: 
				this.IsMatch(_response.docs[2].fields.foo, new [] {
					@"bar"
				});

				//is_false _response.docs[2]._source; 
				this.IsFalse(_response.docs[2]._source);

				//match _response.docs[3].fields.foo: 
				this.IsMatch(_response.docs[3].fields.foo, new [] {
					@"bar"
				});

				//match _response.docs[3]._source: 
				this.IsMatch(_response.docs[3]._source, new {
					foo= "bar"
				});

			}
		}
	}
}

