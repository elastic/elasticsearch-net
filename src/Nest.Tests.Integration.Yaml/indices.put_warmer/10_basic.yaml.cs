using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesPutWarmer
{
	public partial class IndicesPutWarmerTests
	{	


		public class BasicTestForWarmersTests : YamlTestsBase
		{
			[Test]
			public void BasicTestForWarmersTest()
			{	

				//do indices.create 
				_status = this._client.IndicesCreatePost("test_index", null);
				_response = _status.Deserialize<dynamic>();

				//do cluster.health 
				_status = this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status","yellow")
				);
				_response = _status.Deserialize<dynamic>();

				//do indices.get_warmer 
				_status = this._client.IndicesGetWarmer("test_index", "test_warmer");
				_response = _status.Deserialize<dynamic>();

				//do indices.put_warmer 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				_status = this._client.IndicesPutWarmer("test_index", "test_warmer", _body);
				_response = _status.Deserialize<dynamic>();

				//is_true .ok; 
				this.IsTrue(_response.ok);

				//do indices.get_warmer 
				_status = this._client.IndicesGetWarmer("test_index", "test_warmer");
				_response = _status.Deserialize<dynamic>();

				//do indices.delete_warmer 
				_status = this._client.IndicesDeleteWarmer("test_index");
				_response = _status.Deserialize<dynamic>();

				//is_true .ok; 
				this.IsTrue(_response.ok);

				//do indices.get_warmer 
				_status = this._client.IndicesGetWarmer("test_index", "test_warmer");
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

