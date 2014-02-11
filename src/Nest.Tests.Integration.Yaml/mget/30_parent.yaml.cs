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
					.Add("parent","4")
				);
				_response = _status.Deserialize<dynamic>();

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_1", "test", "2", _body, nv=>nv
					.Add("parent","5")
				);
				_response = _status.Deserialize<dynamic>();

				//do mget 
				_body = new {
					docs= new dynamic[] {
						new {
							_id= "1"
						},
						new {
							_id= "1",
							parent= "5",
							fields= new [] {
								"_parent",
								"_routing"
							}
						},
						new {
							_id= "1",
							parent= "4",
							fields= new [] {
								"_parent",
								"_routing"
							}
						},
						new {
							_id= "2",
							parent= "5",
							fields= new [] {
								"_parent",
								"_routing"
							}
						}
					}
				};
				_status = this._client.MgetPost("test_1", "test", _body);
				_response = _status.Deserialize<dynamic>();

				//is_false .docs[0].exists; 
				this.IsFalse(_response.docs[0].exists);

				//is_false .docs[1].exists; 
				this.IsFalse(_response.docs[1].exists);

				//is_true .docs[2].exists; 
				this.IsTrue(_response.docs[2].exists);

				//is_true .docs[3].exists; 
				this.IsTrue(_response.docs[3].exists);

			}
		}
	}
}

