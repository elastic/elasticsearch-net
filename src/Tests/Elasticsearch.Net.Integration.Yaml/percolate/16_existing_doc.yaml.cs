using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Percolate2
{
	public partial class Percolate2YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PercolateExistingDocuments1Tests : YamlTestsBase
		{
			[Test]
			public void PercolateExistingDocuments1Test()
			{	

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("percolator_index", null));

				//do index 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> _client.Index("percolator_index", ".percolator", "test_percolator", _body));

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("percolator_index", "test_type", "1", _body));

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("my_index", null));

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("my_index", "my_type", "1", _body));

				//do indices.refresh 
				this.Do(()=> _client.IndicesRefreshForAll());

				//do percolate 
				this.Do(()=> _client.PercolateGet("percolator_index", "test_type", "1"));

				//match _response.matches: 
				this.IsMatch(_response.matches, new dynamic[] {
					new {_index="percolator_index",_id="test_percolator"}
				});

				//do percolate 
				this.Do(()=> _client.PercolateGet("my_index", "my_type", "1", nv=>nv
					.AddQueryString("percolate_index", @"percolator_index")
					.AddQueryString("percolate_type", @"test_type")
				));

				//match _response.matches: 
				this.IsMatch(_response.matches, new dynamic[] {
					new {_index="percolator_index",_id="test_percolator"}
				});

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("my_index", "my_type", "1", _body));

				//do percolate 
				this.Do(()=> _client.PercolateGet("my_index", "my_type", "1", nv=>nv
					.AddQueryString("version", 2)
					.AddQueryString("percolate_index", @"percolator_index")
					.AddQueryString("percolate_type", @"test_type")
				));

				//match _response.matches: 
				this.IsMatch(_response.matches, new dynamic[] {
					new {_index="percolator_index",_id="test_percolator"}
				});

				//do percolate 
				this.Do(()=> _client.PercolateGet("my_index", "my_type", "1", nv=>nv
					.AddQueryString("version", 1)
					.AddQueryString("percolate_index", @"percolator_index")
					.AddQueryString("percolate_type", @"test_type")
				), shouldCatch: @"conflict");

			}
		}
	}
}

