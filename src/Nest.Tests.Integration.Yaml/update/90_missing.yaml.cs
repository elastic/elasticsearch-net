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


		public class MissingDocumentPartialDocTests : YamlTestsBase
		{
			[Test]
			public void MissingDocumentPartialDocTest()
			{	

				//do update 
				_body = new {
					doc= new {
						foo= "bar"
					}
				};
				_status = this._client.UpdatePost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//do update 
				_body = new {
					doc= new {
						foo= "bar"
					}
				};
				_status = this._client.UpdatePost("test_1", "test", "1", _body, nv=>nv
					.Add("ignore","404")
				);
				_response = _status.Deserialize<dynamic>();

			}
		}

		public class MissingDocumentScriptTests : YamlTestsBase
		{
			[Test]
			public void MissingDocumentScriptTest()
			{	

				//do update 
				_body = new {
					script= "ctx._source.foo = bar",
					@params= new {
						bar= "xxx"
					}
				};
				_status = this._client.UpdatePost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//do update 
				_body = new {
					script= "ctx._source.foo = bar",
					@params= new {
						bar= "xxx"
					}
				};
				_status = this._client.UpdatePost("test_1", "test", "1", _body, nv=>nv
					.Add("ignore","404")
				);
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

