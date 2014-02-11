using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Msearch
{
	public partial class MsearchTests
	{	


		public class BasicMultiSearchTests : YamlTestsBase
		{
			[Test]
			public void BasicMultiSearchTest()
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

				//do msearch 
				_body = @"{""index"":""test_1""}
{""query"":{""match_all"":{}}}
{""index"":""test_2""}
{""query"":{""match_all"":{}}}
{""search_type"":""count"",""index"":""test_1""}
{""query"":{""match"":{""foo"":""bar""}}}";				_status = this._client.MsearchPost(_body);
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

