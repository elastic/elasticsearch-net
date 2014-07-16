using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Mget4
{
	public partial class Mget4YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class MissingMetadata1Tests : YamlTestsBase
		{
			[Test]
			public void MissingMetadata1Test()
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
							_index= "test_1",
							_type= "test"
						}
					}
				};
				this.Do(()=> _client.Mget(_body), shouldCatch: @"/ActionRequestValidationException.+ id is missing/");

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_type= "test",
							_id= "1"
						}
					}
				};
				this.Do(()=> _client.Mget(_body), shouldCatch: @"/ActionRequestValidationException.+ index is missing/");

				//do mget 
				_body = new {
					docs= new dynamic[] {}
				};
				this.Do(()=> _client.Mget(_body), shouldCatch: @"/ActionRequestValidationException.+ no documents to get/");

				//do mget 
				_body = new {};
				this.Do(()=> _client.Mget(_body), shouldCatch: @"/ActionRequestValidationException.+ no documents to get/");

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_index= "test_1",
							_id= "1"
						}
					}
				};
				this.Do(()=> _client.Mget(_body));

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

			}
		}
	}
}

