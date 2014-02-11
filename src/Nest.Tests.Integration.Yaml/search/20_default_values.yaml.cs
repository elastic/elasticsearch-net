using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Search
{
	public partial class SearchTests
	{	


		public class DefaultIndexTests : YamlTestsBase
		{
			[Test]
			public void DefaultIndexTest()
			{	

				//do indices.create 
				_status = this._client.IndicesCreatePost("test_2", null);
				_response = _status.Deserialize<dynamic>();

				//do indices.create 
				_status = this._client.IndicesCreatePost("test_1", null);
				_response = _status.Deserialize<dynamic>();

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_1", "test", "1", _body);
				_response = _status.Deserialize<dynamic>();

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_2", "test", "42", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.refresh 
				_status = this._client.IndicesRefreshGet("System.Collections.Generic.List`1[System.Object]");
				_response = _status.Deserialize<dynamic>();

				//do search 
				_body = new {
					query= new {
						match= new {
							foo= "bar"
						}
					}
				};
				_status = this._client.SearchPost("_all", "test", _body);
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

