using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Explain1
{
	public partial class Explain1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicMlt1Tests : YamlTestsBase
		{
			[Test]
			public void BasicMlt1Test()
			{	

				//do index 
				_body = new {
					foo= "bar",
					title= "howdy"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//do indices.refresh 
				this.Do(()=> _client.IndicesRefreshForAll());

				//do explain 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> _client.Explain("test_1", "test", "1", _body));

				//is_true _response.matched; 
				this.IsTrue(_response.matched);

				//match _response.explanation.value: 
				this.IsMatch(_response.explanation.value, 1);

			}
		}
	}
}

