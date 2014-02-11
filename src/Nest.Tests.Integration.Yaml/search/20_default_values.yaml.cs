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
				this.Do(()=> this._client.IndicesCreatePost("test_2", null));

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePost("test_1", null));

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "1", _body));

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("test_2", "test", "42", _body));

				//do indices.refresh 
				this.Do(()=> this._client.IndicesRefreshGet("System.Collections.Generic.List`1[System.Object]"));

				//do search 
				_body = new {
					query= new {
						match= new {
							foo= "bar"
						}
					}
				};
				this.Do(()=> this._client.SearchPost("_all", "test", _body));

				//match _response.hits.total: 
				this.IsMatch(_response.hits.total, 2);

				//match _response.hits.hits[0]._index: 
				this.IsMatch(_response.hits.hits[0]._index, @"test_1");

				//match _response.hits.hits[0]._type: 
				this.IsMatch(_response.hits.hits[0]._type, @"test");

				//match _response.hits.hits[0]._id: 
				this.IsMatch(_response.hits.hits[0]._id, 1);

				//match _response.hits.hits[1]._index: 
				this.IsMatch(_response.hits.hits[1]._index, @"test_2");

				//match _response.hits.hits[1]._type: 
				this.IsMatch(_response.hits.hits[1]._type, @"test");

				//match _response.hits.hits[1]._id: 
				this.IsMatch(_response.hits.hits[1]._id, 42);

			}
		}
	}
}

