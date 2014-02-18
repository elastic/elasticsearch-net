using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Mget8
{
	public partial class Mget8YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Routing1Tests : YamlTestsBase
		{
			[Test]
			public void Routing1Test()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						index= new {
							number_of_replicas= "0"
						}
					}
				};
				this.Do(()=> this._client.IndicesCreatePut("test_1", _body));

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status", @"green")
				));

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "1", _body, nv=>nv
					.Add("routing", 5)
				));

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_id= "1"
						},
						new {
							_id= "1",
							routing= "4"
						},
						new {
							_id= "1",
							routing= "5"
						}
					}
				};
				this.Do(()=> this._client.MgetPost("test_1", "test", _body, nv=>nv
					.Add("fields", new [] {
						@"_routing"
					})
				));

				//is_false _response.docs[0].found; 
				this.IsFalse(_response.docs[0].found);

				//is_false _response.docs[1].found; 
				this.IsFalse(_response.docs[1].found);

				//is_true _response.docs[2].found; 
				this.IsTrue(_response.docs[2].found);

				//match _response.docs[2]._index: 
				this.IsMatch(_response.docs[2]._index, @"test_1");

				//match _response.docs[2]._type: 
				this.IsMatch(_response.docs[2]._type, @"test");

				//match _response.docs[2]._id: 
				this.IsMatch(_response.docs[2]._id, 1);

				//match _response.docs[2].fields._routing: 
				this.IsMatch(_response.docs[2].fields._routing, 5);

			}
		}
	}
}

