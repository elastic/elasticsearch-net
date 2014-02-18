using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Scroll1
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
				this.Do(()=> this._client.IndicesCreatePut("test_scroll", null));

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("test_scroll", "test", "42", _body));

				//do indices.refresh 
				this.Do(()=> this._client.IndicesRefreshPostForAll());

				//do search 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> this._client.SearchPost("test_scroll", _body, nv=>nv
					.Add("search_type", @"scan")
					.Add("scroll", @"1m")
				));

				//set scroll_id = _response._scroll_id; 
				var scroll_id = _response._scroll_id;

				//do scroll 
				this.Do(()=> this._client.ScrollGet((string)scroll_id));

				//match _response.hits.total: 
				this.IsMatch(_response.hits.total, 1);

				//match _response.hits.hits[0]._id: 
				this.IsMatch(_response.hits.hits[0]._id, 42);

			}
		}
	}
}

