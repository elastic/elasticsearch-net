using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Percolate3
{
	public partial class Percolate3YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicPercolationTestsOnAnEmptyCluster1Tests : YamlTestsBase
		{
			[Test]
			public void BasicPercolationTestsOnAnEmptyCluster1Test()
			{	

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test_index", null));

				//do indices.refresh 
				this.Do(()=> _client.IndicesRefreshForAll());

				//do percolate 
				_body = new {
					doc= new {
						foo= "bar"
					}
				};
				this.Do(()=> _client.Percolate("test_index", "test_type", _body));

				//match _response.total: 
				this.IsMatch(_response.total, 0);

				//match _response.matches: 
				this.IsMatch(_response.matches, new string[] {});

				//do count_percolate 
				_body = new {
					doc= new {
						foo= "bar"
					}
				};
				this.Do(()=> _client.CountPercolate("test_index", "test_type", _body));

				//is_false _response.matches; 
				this.IsFalse(_response.matches);

				//match _response.total: 
				this.IsMatch(_response.total, 0);

			}
		}
	}
}

