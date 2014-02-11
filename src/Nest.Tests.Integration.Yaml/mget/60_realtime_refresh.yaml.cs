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


		public class RealtimeRefreshTests : YamlTestsBase
		{
			[Test]
			public void RealtimeRefreshTest()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						index= new {
							refresh_interval= "-1",
							number_of_replicas= "0"
						}
					}
				};
				_status = this._client.IndicesCreatePost("test_1", _body);
				_response = _status.Deserialize<dynamic>();

				//do cluster.health 
				_status = this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status","green")
				);
				_response = _status.Deserialize<dynamic>();

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//do mget 
				_body = new {
					ids= new [] {
						"1"
					}
				};
				_status = this._client.MgetPost("test_1", "test", _body, nv=>nv
					.Add("realtime","0")
				);
				_response = _status.Deserialize<dynamic>();

				//is_false .docs[0].exists; 
				this.IsFalse(_response.docs[0].exists);

				//do mget 
				_body = new {
					ids= new [] {
						"1"
					}
				};
				_status = this._client.MgetPost("test_1", "test", _body, nv=>nv
					.Add("realtime","1")
				);
				_response = _status.Deserialize<dynamic>();

				//is_true .docs[0].exists; 
				this.IsTrue(_response.docs[0].exists);

				//do mget 
				_body = new {
					ids= new [] {
						"1"
					}
				};
				_status = this._client.MgetPost("test_1", "test", _body, nv=>nv
					.Add("realtime","0")
					.Add("refresh","1")
				);
				_response = _status.Deserialize<dynamic>();

				//is_true .docs[0].exists; 
				this.IsTrue(_response.docs[0].exists);

			}
		}
	}
}

