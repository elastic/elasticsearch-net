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


		public class BasicMultiGetTests : YamlTestsBase
		{
			[Test]
			public void BasicMultiGetTest()
			{	

				//do indices.create 
				_status = this._client.IndicesCreatePost("test_2", null);
				_response = _status.Deserialize<dynamic>();

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.flush 
				_status = this._client.IndicesFlushGet(nv=>nv
					.Add("refresh","true")
				);
				_response = _status.Deserialize<dynamic>();

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_index= "test_2",
							_type= "test",
							_id= "1"
						},
						new {
							_index= "test_1",
							_type= "none",
							_id= "1"
						},
						new {
							_index= "test_1",
							_type= "test",
							_id= "2"
						},
						new {
							_index= "test_1",
							_type= "test",
							_id= "1"
						}
					}
				};
				_status = this._client.MgetPost(_body);
				_response = _status.Deserialize<dynamic>();

				//is_false .docs[0].exists; 
				this.IsFalse(_response.docs[0].exists);

				//is_false .docs[1].exists; 
				this.IsFalse(_response.docs[1].exists);

				//is_false .docs[2].exists; 
				this.IsFalse(_response.docs[2].exists);

				//is_true .docs[3].exists; 
				this.IsTrue(_response.docs[3].exists);

			}
		}
	}
}

