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


		public class IndexWithIdTests : YamlTestsBase
		{
			[Test]
			public void IndexWithIdTest()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test-weird-index-Ã¤Â¸Â­Ã¦â€“â€¡", "weird.type", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//is_true .ok; 
				this.IsTrue(_response.ok);

				//do get 
				_status = this._client.Get("test-weird-index-Ã¤Â¸Â­Ã¦â€“â€¡", "weird.type", "1");
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

