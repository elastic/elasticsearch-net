using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Scroll1
{
	public partial class Scroll1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicScroll1Tests : YamlTestsBase
		{
			[Test]
			public void BasicScroll1Test()
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

				//set scroll_id = _response._scroll_id; 
				var scroll_id = _response._scroll_id;

				//do scroll 
				this.Do(()=> _client.ScrollGet((string)scroll_id));

				//match _response.hits.total: 
				this.IsMatch(_response.hits.total, 1);

				//match _response.hits.hits[0]._id: 
				this.IsMatch(_response.hits.hits[0]._id, 42);

			}
		}
	}
}

