using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Index
{
	public partial class IndexTests
	{	


		public class IndexWithoutIdTests : YamlTestsBase
		{
			[Test]
			public void IndexWithoutIdTest()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_1", "test", _body);
				_response = _status.Deserialize<dynamic>();

				//is_true .ok; 
				this.IsTrue(_response.ok);

				//is_true ._id; 
				this.IsTrue(_response._id);

				//set id = _id; 
				var id = _response._id;

				//do get 
				_status = this._client.Get("test_1", "test", id);
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

