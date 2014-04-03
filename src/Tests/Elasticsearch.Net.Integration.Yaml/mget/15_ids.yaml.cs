using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Mget5
{
	public partial class Mget5YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Ids1Tests : YamlTestsBase
		{
			[Test]
			public void Ids1Test()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//do index 
				_body = new {
					foo= "baz"
				};
				this.Do(()=> _client.Index("test_1", "test_2", "2", _body));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"yellow")
				));

				//do mget 
				_body = new {
					ids= new [] {
						"1",
						"2"
					}
				};
				this.Do(()=> _client.Mget("test_1", "test", _body));

				//is_true _response.docs[0].found; 
				this.IsTrue(_response.docs[0].found);

				//match _response.docs[0]._index: 
				this.IsMatch(_response.docs[0]._index, @"test_1");

				//match _response.docs[0]._type: 
				this.IsMatch(_response.docs[0]._type, @"test");

				//match _response.docs[0]._id: 
				this.IsMatch(_response.docs[0]._id, 1);

				//match _response.docs[0]._version: 
				this.IsMatch(_response.docs[0]._version, 1);

				//match _response.docs[0]._source: 
				this.IsMatch(_response.docs[0]._source, new {
					foo= "bar"
				});

				//is_false _response.docs[1].found; 
				this.IsFalse(_response.docs[1].found);

				//match _response.docs[1]._index: 
				this.IsMatch(_response.docs[1]._index, @"test_1");

				//match _response.docs[1]._type: 
				this.IsMatch(_response.docs[1]._type, @"test");

				//match _response.docs[1]._id: 
				this.IsMatch(_response.docs[1]._id, 2);

				//do mget 
				_body = new {
					ids= new [] {
						"1",
						"2"
					}
				};
				this.Do(()=> _client.Mget("test_1", _body));

				//is_true _response.docs[0].found; 
				this.IsTrue(_response.docs[0].found);

				//match _response.docs[0]._index: 
				this.IsMatch(_response.docs[0]._index, @"test_1");

				//match _response.docs[0]._type: 
				this.IsMatch(_response.docs[0]._type, @"test");

				//match _response.docs[0]._id: 
				this.IsMatch(_response.docs[0]._id, 1);

				//match _response.docs[0]._version: 
				this.IsMatch(_response.docs[0]._version, 1);

				//match _response.docs[0]._source: 
				this.IsMatch(_response.docs[0]._source, new {
					foo= "bar"
				});

				//is_true _response.docs[1].found; 
				this.IsTrue(_response.docs[1].found);

				//match _response.docs[1]._index: 
				this.IsMatch(_response.docs[1]._index, @"test_1");

				//match _response.docs[1]._type: 
				this.IsMatch(_response.docs[1]._type, @"test_2");

				//match _response.docs[1]._id: 
				this.IsMatch(_response.docs[1]._id, 2);

				//match _response.docs[1]._version: 
				this.IsMatch(_response.docs[1]._version, 1);

				//match _response.docs[1]._source: 
				this.IsMatch(_response.docs[1]._source, new {
					foo= "baz"
				});

				//do mget 
				_body = new {
					ids= new string[] {}
				};
				this.Do(()=> _client.Mget("test_1", _body), shouldCatch: @"/ActionRequestValidationException.+ no documents to get/");

				//do mget 
				_body = new {};
				this.Do(()=> _client.Mget("test_1", _body), shouldCatch: @"/ActionRequestValidationException.+ no documents to get/");

			}
		}
	}
}

