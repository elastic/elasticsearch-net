using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Get
{
	public partial class GetTests
	{	


		public class FieldsTests : YamlTestsBase
		{
			[Test]
			public void FieldsTest()
			{	

				//do index 
				_body = new {
					foo= "bar",
					count= "1"
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//do get 
				_status = this._client.Get("test_1", "test", "1", nv=>nv
					.Add("fields","foo")
				);
				_response = _status.Deserialize<dynamic>();

				//is_false ._source; 
				this.IsFalse(_response._source);

				//do get 
				_status = this._client.Get("test_1", "test", "1", nv=>nv
					.Add("fields","System.Collections.Generic.List`1[System.Object]")
				);
				_response = _status.Deserialize<dynamic>();

				//is_false ._source; 
				this.IsFalse(_response._source);

				//do get 
				_status = this._client.Get("test_1", "test", "1", nv=>nv
					.Add("fields","System.Collections.Generic.List`1[System.Object]")
				);
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

