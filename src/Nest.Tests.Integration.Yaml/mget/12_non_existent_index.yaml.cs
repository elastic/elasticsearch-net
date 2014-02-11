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


		public class NonExistentIndexTests : YamlTestsBase
		{
			[Test]
			public void NonExistentIndexTest()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "1", _body));

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status","yellow")
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
				this.Do(()=> this._client.MgetPost(_body));

				//is_false _response.docs[0].exists; 
				this.IsFalse(_response.docs[0].exists);

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
				this.Do(()=> this._client.MgetPost(_body));

				//is_true _response.docs[0].exists; 
				this.IsTrue(_response.docs[0].exists);

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

