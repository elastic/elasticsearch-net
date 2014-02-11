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


		public class DocAsUpsertTests : YamlTestsBase
		{
			[Test]
			public void DocAsUpsertTest()
			{	

				//do update 
				_body = new {
					doc= new {
						foo= "bar",
						count= "1"
					},
					doc_as_upsert= "1"
				};
				_status = this._client.UpdatePost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//do get 
				_status = this._client.Get("test_1", "test", "1");
				_response = _status.Deserialize<dynamic>();

				//do update 
				_body = new {
					doc= new {
						count= "2"
					},
					doc_as_upsert= "1"
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

