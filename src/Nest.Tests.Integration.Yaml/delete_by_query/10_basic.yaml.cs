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
				this.Do(()=> this._client.IndicesRefreshGet());

				//do delete_by_query 
				_body = new {
					match= new {
						foo= "bar"
					}
				};
				this.Do(()=> this._client.DeleteByQuery("test_1", _body));

				//is_true _response.ok; 
				this.IsTrue(_response.ok);

				//do indices.refresh 
				this.Do(()=> this._client.IndicesRefreshGet());

				//do count 
				this.Do(()=> this._client.CountGet("test_1"));

				//match _response.count: 
				this.IsMatch(_response.count, 2);

			}
		}
	}
}

