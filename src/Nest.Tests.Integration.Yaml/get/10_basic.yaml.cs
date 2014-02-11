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


		public class BasicTests : YamlTestsBase
		{
			[Test]
			public void BasicTest()
			{	

				//do index 
				_body = new {
					foo= "Hello= Ã¤Â¸Â­Ã¦â€“â€¡"
				};
				_status = this._client.IndexPost("test_1", "test", "Ã¤Â¸Â­Ã¦â€“â€¡", _body);
				_response = _status.Deserialize<dynamic>();

				//do get 
				_status = this._client.Get("test_1", "test", "Ã¤Â¸Â­Ã¦â€“â€¡");
				_response = _status.Deserialize<dynamic>();

				//do get 
				_status = this._client.Get("test_1", "_all", "Ã¤Â¸Â­Ã¦â€“â€¡");
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

