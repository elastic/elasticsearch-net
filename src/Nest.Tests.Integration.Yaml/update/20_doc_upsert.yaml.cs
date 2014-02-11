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


		public class DocUpsertTests : YamlTestsBase
		{
			[Test]
			public void DocUpsertTest()
			{	

				//do update 
				_body = new {
					doc= new {
						foo= "bar",
						count= "1"
					},
					upsert= new {
						foo= "baz"
					}
				};
				_status = this._client.UpdatePost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//do get 
				_status = this._client.Get("test_1", "test", "1");
				_response = _status.Deserialize<dynamic>();

				//is_false ._source.count; 
				this.IsFalse(_response._source.count);

				//do update 
				_body = new {
					doc= new {
						foo= "bar",
						count= "1"
					},
					upsert= new {
						foo= "baz"
					}
				};
				_status = this._client.UpdatePost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//do get 
				_status = this._client.Get("test_1", "test", "1");
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

