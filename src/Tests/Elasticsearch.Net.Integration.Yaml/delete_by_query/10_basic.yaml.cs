using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.DeleteByQuery1
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
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//do index 
				_body = new {
					foo= "baz"
				};
				this.Do(()=> _client.Index("test_1", "test", "2", _body));

				//do index 
				_body = new {
					foo= "foo"
				};
				this.Do(()=> _client.Index("test_1", "test", "3", _body));

				//do indices.refresh 
				this.Do(()=> _client.IndicesRefreshForAll());

				//do delete_by_query 
				_body = new {
					query= new {
						match= new {
							foo= "bar"
						}
					}
				};
				this.Do(()=> _client.DeleteByQuery("test_1", _body));

				//do indices.refresh 
				this.Do(()=> _client.IndicesRefreshForAll());

				//do count 
				this.Do(()=> _client.CountGet("test_1"));

				//match _response.count: 
				this.IsMatch(_response.count, 2);

			}
		}
	}
}

