using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Percolate4
{
	public partial class Percolate4YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicPercolationHighlightQueryTest1Tests : YamlTestsBase
		{
			[Test]
			public void BasicPercolationHighlightQueryTest1Test()
			{	

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test_index", null));

				//do index 
				_body = new {
					query= new {
						match= new {
							foo= "bar"
						}
					}
				};
				this.Do(()=> _client.Index("test_index", ".percolator", "test_percolator", _body));

				//do indices.refresh 
				this.Do(()=> _client.IndicesRefreshForAll());

				//do percolate 
				_body = new {
					doc= new {
						foo= "bar foo"
					},
					size= "1",
					highlight= new {
						fields= new {
							foo= new {
								highlight_query= new {
									match= new {
										foo= "foo"
									}
								}
							}
						}
					}
				};
				this.Do(()=> _client.Percolate("test_index", "test_type", _body));

				//match _response.total: 
				this.IsMatch(_response.total, 1);

			}
		}
	}
}

