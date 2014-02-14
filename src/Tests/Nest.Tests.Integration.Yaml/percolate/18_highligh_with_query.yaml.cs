using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Percolate4
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
				this.Do(()=> this._client.IndicesCreatePost("test_index", null));

				//do index 
				_body = new {
					query= new {
						match= new {
							foo= "bar"
						}
					}
				};
				this.Do(()=> this._client.IndexPost("test_index", ".percolator", "test_percolator", _body));

				//do indices.refresh 
				this.Do(()=> this._client.IndicesRefreshPostForAll());

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
				this.Do(()=> this._client.PercolatePost("test_index", "test_type", _body));

				//match _response.total: 
				this.IsMatch(_response.total, 1);

			}
		}
	}
}

