using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.DeleteByQuery1
{
	public partial class DeleteByQuery1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicDeleteByQuery1Tests : YamlTestsBase
		{
			[Test]
			public void BasicDeleteByQuery1Test()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "1", _body));

				//do index 
				_body = new {
					foo= "baz"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "2", _body));

				//do index 
				_body = new {
					foo= "foo"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "3", _body));

				//do indices.refresh 
				this.Do(()=> this._client.IndicesRefreshPostForAll());

				//do delete_by_query 
				_body = new {
					query= new {
						match= new {
							foo= "bar"
						}
					}
				};
				this.Do(()=> this._client.DeleteByQuery("test_1", _body));

				//do indices.refresh 
				this.Do(()=> this._client.IndicesRefreshPostForAll());

				//do count 
				this.Do(()=> this._client.CountGet("test_1"));

				//match _response.count: 
				this.IsMatch(_response.count, 2);

			}
		}
	}
}

