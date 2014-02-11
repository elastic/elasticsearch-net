using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Explain
{
	public partial class ExplainTests
	{	


		public class BasicMltTests : YamlTestsBase
		{
			[Test]
			public void BasicMltTest()
			{	

				//do index 
				_body = new {
					foo= "bar",
					title= "howdy"
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.refresh 
				_status = this._client.IndicesRefreshGet();
				_response = _status.Deserialize<dynamic>();

				//do explain 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				_status = this._client.ExplainPost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//is_true .matched; 
				this.IsTrue(_response.matched);

				//is_true .ok; 
				this.IsTrue(_response.ok);

			}
		}
	}
}

