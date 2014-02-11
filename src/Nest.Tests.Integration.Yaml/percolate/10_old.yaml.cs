using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Percolate
{
	public partial class PercolateTests
	{	


		public class BasicPercolationTestsTests : YamlTestsBase
		{
			[Test]
			public void BasicPercolationTestsTest()
			{	

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePost("test_index", null));

				//do index 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> this._client.IndexPost("_percolator", "test_index", "test_percolator", _body));

				//do indices.refresh 
				this.Do(()=> this._client.IndicesRefreshGet());

				//do percolate 
				_body = new {
					doc= new {
						foo= "bar"
					}
				};
				this.Do(()=> this._client.PercolatePost("test_index", "test_type", _body));

				//is_true _response.ok; 
				this.IsTrue(_response.ok);

				//match _response.matches: 
				this.IsMatch(_response.matches, new dynamic[] {
					"test_percolator"
				});

			}
		}
	}
}

