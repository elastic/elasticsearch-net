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


		public class ScriptTests : YamlTestsBase
		{
			[Test]
			public void ScriptTest()
			{	

				//do index 
				_body = new {
					foo= "bar",
					count= "1"
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//do update 
				_body = new {
					lang= "mvel",
					script= "ctx._source.foo = bar",
					@params= new {
						bar= "xxx"
					}
				};
				_status = this._client.UpdatePost("test_1", "test", "1", _body, nv=>nv
					.Add("script","1")
				);
				_response = _status.Deserialize<dynamic>();

				//do get 
				_status = this._client.Get("test_1", "test", "1");
				_response = _status.Deserialize<dynamic>();

				//do update 
				_status = this._client.UpdatePost("test_1", "test", "1", null, nv=>nv
					.Add("lang","mvel")
					.Add("script","ctx._source.foo = 'yyy'")
				);
				_response = _status.Deserialize<dynamic>();

				//do get 
				_status = this._client.Get("test_1", "test", "1");
				_response = _status.Deserialize<dynamic>();

				//do update 
				_body = new {
					script= "1",
					lang= "doesnotexist",
					@params= new {
						bar= "xxx"
					}
				};
				_status = this._client.UpdatePost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//do update 
				_status = this._client.UpdatePost("test_1", "test", "1", null, nv=>nv
					.Add("lang","doesnotexist")
					.Add("script","1")
				);
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

