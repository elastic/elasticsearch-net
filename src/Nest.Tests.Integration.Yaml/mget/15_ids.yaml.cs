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


		public class IdsTests : YamlTestsBase
		{
			[Test]
			public void IdsTest()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//do index 
				_body = new {
					foo= "baz"
				};
				_status = this._client.IndexPost("test_1", "test_2", "2", _body);
				_response = _status.Deserialize<dynamic>();

				//do cluster.health 
				_status = this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status","yellow")
				);
				_response = _status.Deserialize<dynamic>();

				//do mget 
				_body = new {
					ids= new [] {
						"1",
						"2"
					}
				};
				_status = this._client.MgetPost("test_1", "test", _body);
				_response = _status.Deserialize<dynamic>();

				//is_true .docs[0].exists; 
				this.IsTrue(_response.docs[0].exists);

				//is_false .docs[1].exists; 
				this.IsFalse(_response.docs[1].exists);

				//do mget 
				_body = new {
					ids= new [] {
						"1",
						"2"
					}
				};
				_status = this._client.MgetPost("test_1", _body);
				_response = _status.Deserialize<dynamic>();

				//is_true .docs[0].exists; 
				this.IsTrue(_response.docs[0].exists);

				//is_true .docs[1].exists; 
				this.IsTrue(_response.docs[1].exists);

				//do mget 
				_body = new {
					ids= new string[] {}
				};
				_status = this._client.MgetPost("test_1", _body);
				_response = _status.Deserialize<dynamic>();

				//do mget 
				_body = new {};
				_status = this._client.MgetPost("test_1", _body);
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

