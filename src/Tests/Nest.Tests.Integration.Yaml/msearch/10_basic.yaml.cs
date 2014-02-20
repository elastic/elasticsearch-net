using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Msearch1
{
	public partial class Msearch1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicMultiSearch1Tests : YamlTestsBase
		{
			[Test]
			public void BasicMultiSearch1Test()
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

				//do msearch 
				_body = new dynamic[] {
					new {index="test_1"},
					new {query=new {match_all=new {}}},
					new {index="test_2"},
					new {query=new {match_all=new {}}},
					new {search_type="count",index="test_1"},
					new {query=new {match=new {foo="bar"}}}
				};
				this.Do(()=> _client.Msearch(_body));

				//match _response.responses[0].hits.total: 
				this.IsMatch(_response.responses[0].hits.total, 3);

				//match _response.responses[1].error: 
				this.IsMatch(_response.responses[1].error, @"IndexMissingException[[test_2] missing]");

				//match _response.responses[2].hits.total: 
				this.IsMatch(_response.responses[2].hits.total, 1);

			}
		}
	}
}

