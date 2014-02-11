using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Delete
{
	public partial class DeleteTests
	{	


		public class ExternalVersionTests : YamlTestsBase
		{
			[Test]
			public void ExternalVersionTest()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body, nv=>nv
					.Add("version_type","external")
					.Add("version","5")
				);
				_response = _status.Deserialize<dynamic>();

				//do delete 
				_status = this._client.Delete("test_1", "test", "1", nv=>nv
					.Add("version_type","external")
					.Add("version","4")
				);
				_response = _status.Deserialize<dynamic>();

				//do delete 
				_status = this._client.Delete("test_1", "test", "1", nv=>nv
					.Add("version_type","external")
					.Add("version","6")
				);
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

