using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Template2
{
	public partial class Template2YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class IndexedTemplateQueryTests1Tests : YamlTestsBase
		{
			[Test]
			public void IndexedTemplateQueryTests1Test()
			{	

				//do index 
				_body = new {
					text= "value1_foo"
				};
				this.Do(()=> _client.Index("test", "testtype", "1", _body));

				//do index 
				_body = new {
					text= "value2_foo value3_foo"
				};
				this.Do(()=> _client.Index("test", "testtype", "2", _body));

				//do indices.refresh 
				this.Do(()=> _client.IndicesRefreshForAll());

				//do put_template 
				_body = new {
					template= new {
						query= new {
							match= new {
								text= "new {new {my_value}}"
							}
						},
						size= "new {new {my_size}}"
					}
				};
				

				//match _response._id: 
				this.IsMatch(_response._id, 1);

				//do indices.refresh 
				this.Do(()=> _client.IndicesRefreshForAll());

				//do search_template 
				_body = new {
					template= new {
						id= "1"
					},
					@params= new {
						my_value= "value1_foo",
						my_size= "1"
					}
				};
				this.Do(()=> _client.SearchTemplate(_body));

				//match _response.hits.total: 
				this.IsMatch(_response.hits.total, 1);

				//do search_template 
				_body = new {
					id= "1",
					@params= new {
						my_value= "value1_foo",
						my_size= "1"
					}
				};
				this.Do(()=> _client.SearchTemplate(_body));

				//match _response.hits.total: 
				this.IsMatch(_response.hits.total, 1);

			}
		}
	}
}

