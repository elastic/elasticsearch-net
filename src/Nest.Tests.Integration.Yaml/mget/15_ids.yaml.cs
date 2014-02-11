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
		public class IdsTests : YamlTestsBase
		{
			[Test]
			public void IdsTest()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "1", _body));

				//do index 
				_body = new {
					foo= "baz"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test_2", "2", _body));

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status", @"yellow")
				));

				//do mget 
				_body = new {
					ids= new [] {
						"1",
						"2"
					}
				};
				this.Do(()=> this._client.MgetPost("test_1", "test", _body));

				//is_true _response.docs[0].exists; 
				this.IsTrue(_response.docs[0].exists);

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

				//is_false _response.docs[1].exists; 
				this.IsFalse(_response.docs[1].exists);

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
				this.Do(()=> this._client.MgetPost("test_1", _body));

				//is_true _response.docs[0].exists; 
				this.IsTrue(_response.docs[0].exists);

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

				//is_true _response.docs[1].exists; 
				this.IsTrue(_response.docs[1].exists);

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
				this.Do(()=> this._client.MgetPost("test_1", _body));

				//do mget 
				_body = new {};
				this.Do(()=> this._client.MgetPost("test_1", _body));

			}
		}
	}
}

