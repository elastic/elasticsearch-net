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


		public class PartialDocumentTests : YamlTestsBase
		{
			[Test]
			public void PartialDocumentTest()
			{	

				//do index 
				_body = new {
					foo= "bar",
					count= "1",
					nested= new {
						one= "1",
						two= "2"
					}
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//do update 
				_body = new {
					doc= new {
						foo= "baz",
						nested= new {
							one= "3"
						}
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

