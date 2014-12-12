using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Scroll2
{
	public partial class Scroll2YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ClearScroll1Tests : YamlTestsBase
		{
			[Test]
			public void ClearScroll1Test()
			{	

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test_scroll", null));

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_scroll", "test", "42", _body));

				//do indices.refresh 
				this.Do(()=> _client.IndicesRefreshForAll());

				//do search 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> _client.Search("test_scroll", _body, nv=>nv
					.AddQueryString("search_type", @"scan")
					.AddQueryString("scroll", @"1m")
				));

				//set scroll_id1 = _response._scroll_id; 
				var scroll_id1 = _response._scroll_id;

				//do clear_scroll 
				this.Do(()=> _client.ClearScroll(scroll_id:  (string)scroll_id1, body:  null));

				//do scroll 
				this.Do(()=> _client.ScrollGet((string)scroll_id1), shouldCatch: @"missing");

				//do clear_scroll 
				this.Do(()=> _client.ClearScroll(scroll_id: (string)scroll_id1, body: null), shouldCatch: @"missing");

			}
		}
	}
}

