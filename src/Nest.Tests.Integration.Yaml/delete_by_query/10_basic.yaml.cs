using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.DeleteByQuery
{
	public partial class DeleteByQueryTests
	{	


		public class BasicDeleteByQueryTests : YamlTestsBase
		{
			[Test]
			public void BasicDeleteByQueryTest()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//do index 
				_body = new {
					foo= "baz"
				};
				_status = this._client.IndexPost("test_1", "test", "2", _body);
				_response = _status.Deserialize<dynamic>();

				//do index 
				_body = new {
					foo= "foo"
				};
				_status = this._client.IndexPost("test_1", "test", "3", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.refresh 
				_status = this._client.IndicesRefreshGet();
				_response = _status.Deserialize<dynamic>();

				//do delete_by_query 
				_body = new {
					match= new {
						foo= "bar"
					}
				};
				_status = this._client.DeleteByQuery("test_1", _body);
				_response = _status.Deserialize<dynamic>();

				//is_true .ok; 
				this.IsTrue(_response.ok);

				//do indices.refresh 
				_status = this._client.IndicesRefreshGet();
				_response = _status.Deserialize<dynamic>();

				//do count 
				_status = this._client.CountGet("test_1");
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

