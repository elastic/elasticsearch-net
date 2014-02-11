using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Scroll
{
	public partial class ScrollTests
	{	


		public class BasicScrollTests : YamlTestsBase
		{
			[Test]
			public void BasicScrollTest()
			{	

				//do indices.create 
				_status = this._client.IndicesCreatePost("test_scroll", null);
				_response = _status.Deserialize<dynamic>();

				//do index 
				_body = new {
					foo= "bar"
				};
				_status = this._client.IndexPost("test_scroll", "test", "42", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.refresh 
				_status = this._client.IndicesRefreshGet();
				_response = _status.Deserialize<dynamic>();

				//do search 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				_status = this._client.SearchPost("test_scroll", _body, nv=>nv
					.Add("search_type","scan")
					.Add("scroll","1m")
				);
				_response = _status.Deserialize<dynamic>();

				//set scroll_id = _scroll_id; 
				var scroll_id = _response._scroll_id;

				//do scroll 
				_status = this._client.ScrollGet(scroll_id);
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

