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


		public class FieldsTests : YamlTestsBase
		{
			[Test]
			public void FieldsTest()
			{	

				//do update 
				_body = new {
					doc= new {
						foo= "baz"
					},
					upsert= new {
						foo= "bar"
					}
				};
				_status = this._client.UpdatePost("test_1", "test", "1", _body, nv=>nv
					.Add("fields","foo,bar,_source")
				);
				_response = _status.Deserialize<dynamic>();

				//is_false .get.fields.bar; 
				this.IsFalse(_response.get.fields.bar);

			}
		}
	}
}

