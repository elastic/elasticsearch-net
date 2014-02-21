using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Mget3
{
	public partial class Mget3YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class NonExistentIndex1Tests : YamlTestsBase
		{
			[Test]
			public void NonExistentIndex1Test()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.Add("wait_for_status", @"yellow")
				));

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_index= "test_2",
							_type= "test",
							_id= "1"
						}
					}
				};
				this.Do(()=> _client.Mget(_body));

				//is_false _response.docs[0].found; 
				this.IsFalse(_response.docs[0].found);

				//match _response.docs[0]._index: 
				this.IsMatch(_response.docs[0]._index, @"test_2");

				//match _response.docs[0]._type: 
				this.IsMatch(_response.docs[0]._type, @"test");

				//match _response.docs[0]._id: 
				this.IsMatch(_response.docs[0]._id, 1);

				//do mget 
				_body = new {
					index= "test_2",
					docs= new dynamic[] {
						new {
							_index= "test_1",
							_type= "test",
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

			}
		}
	}
}

