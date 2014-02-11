using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Update
{
	public partial class UpdateTests
	{	


		public class RefreshTests : YamlTestsBase
		{
			[Test]
			public void RefreshTest()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						index= new { refresh_interval= "-1" }
					}
				};
				_status = this._client.IndicesCreatePost("test_1", _body);
				_response = _status.Deserialize<dynamic>();

				//do cluster.health 
				_status = this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status","yellow")
				);
				_response = _status.Deserialize<dynamic>();

				//do update 
				_body = new {
					doc= new {
						foo= "baz"
					},
					upsert= new {
						foo= "bar"
					}
				};
				_status = this._client.UpdatePost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//do search 
				_body = new {
					query= new {
						term= new {
							_id= "1"
						}
					}
				};
				_status = this._client.SearchPost("test_1", "test", _body);
				_response = _status.Deserialize<dynamic>();

				//do update 
				_body = new {
					doc= new {
						foo= "baz"
					},
					upsert= new {
						foo= "bar"
					}
				};
				_status = this._client.UpdatePost("test_1", "test", "2", _body, nv=>nv
					.Add("refresh","1")
				);
				_response = _status.Deserialize<dynamic>();

				//do search 
				_body = new {
					query= new {
						term= new {
							_id= "2"
						}
					}
				};
				_status = this._client.SearchPost("test_1", "test", _body);
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

