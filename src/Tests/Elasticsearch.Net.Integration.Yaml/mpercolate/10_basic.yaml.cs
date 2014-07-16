using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Mpercolate1
{
	public partial class Mpercolate1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicMultiPercolate1Tests : YamlTestsBase
		{
			[Test]
			public void BasicMultiPercolate1Test()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("percolator_index", "my_type", "1", _body));

				//do index 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> _client.Index("percolator_index", ".percolator", "test_percolator", _body));

				//do mpercolate 
				_body = new dynamic[] {
					new {percolate=new {index="percolator_index",type="my_type"}},
					new {doc=new {foo="bar"}},
					new {percolate=new {index="percolator_index1",type="my_type"}},
					new {doc=new {foo="bar"}},
					new {percolate=new {index="percolator_index",type="my_type",id="1"}},
					new {doc=new {foo="bar"}}
				};
				this.Do(()=> _client.Mpercolate(_body));

				//match _response.responses[0].total: 
				this.IsMatch(_response.responses[0].total, 1);

				//match _response.responses[1].error: 
				this.IsMatch(_response.responses[1].error, @"IndexMissingException[[percolator_index1] missing]");

				//match _response.responses[2].total: 
				this.IsMatch(_response.responses[2].total, 1);

			}
		}
	}
}

