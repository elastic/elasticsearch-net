using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Percolate2
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
				this.Do(()=> this._client.IndicesCreatePut("percolator_index", null));

				//do index 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> this._client.IndexPost("percolator_index", ".percolator", "test_percolator", _body));

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("percolator_index", "test_type", "1", _body));

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePut("my_index", null));

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("my_index", "my_type", "1", _body));

				//do indices.refresh 
				this.Do(()=> this._client.IndicesRefreshPostForAll());

				//do percolate 
				this.Do(()=> this._client.PercolateGet("percolator_index", "test_type", "1"));

				//match _response.matches: 
				this.IsMatch(_response.matches, new dynamic[] {
					new {_index="percolator_index",_id="test_percolator"}
				});

				//do percolate 
				this.Do(()=> this._client.PercolateGet("my_index", "my_type", "1", nv=>nv
					.Add("percolate_index", @"percolator_index")
					.Add("percolate_type", @"test_type")
				));

				//match _response.matches: 
				this.IsMatch(_response.matches, new dynamic[] {
					new {_index="percolator_index",_id="test_percolator"}
				});

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("my_index", "my_type", "1", _body));

				//do percolate 
				this.Do(()=> this._client.PercolateGet("my_index", "my_type", "1", nv=>nv
					.Add("version", 2)
					.Add("percolate_index", @"percolator_index")
					.Add("percolate_type", @"test_type")
				));

				//match _response.matches: 
				this.IsMatch(_response.matches, new dynamic[] {
					new {_index="percolator_index",_id="test_percolator"}
				});

				//do percolate 
				this.Do(()=> this._client.PercolateGet("my_index", "my_type", "1", nv=>nv
					.Add("version", 1)
					.Add("percolate_index", @"percolator_index")
					.Add("percolate_type", @"test_type")
				), shouldCatch: @"conflict");

			}
		}
	}
}

