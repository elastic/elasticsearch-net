using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Exists
{
	public partial class ExistsTests
	{	


		public class ParentTests : YamlTestsBase
		{
			[Test]
			public void ParentTest()
			{	

				//do indices.create 
				_body = new {
					mappings= new {
						test= new {
							_parent= new {
								type= "foo"
							}
						}
					}
				};
				_status = this._client.IndicesCreatePost("test_1", _body);
				_response = _status.Deserialize<dynamic>();

				//do cluster.health 
				_status = this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status","yellow")
				);
				_response = _status.Deserialize<dynamic>();

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body, nv=>nv
					.Add("parent","5")
				);
				_response = _status.Deserialize<dynamic>();

				//do exists 
				_status = this._client.ExistsHead("test_1", "test", "1", nv=>nv
					.Add("parent","5")
				);
				_response = _status.Deserialize<dynamic>();

				//is_true ; 
				this.IsTrue(_response);

				//do exists 
				_status = this._client.ExistsHead("test_1", "test", "1");
				_response = _status.Deserialize<dynamic>();

				//is_false ; 
				this.IsFalse(_response);

			}
		}
	}
}

