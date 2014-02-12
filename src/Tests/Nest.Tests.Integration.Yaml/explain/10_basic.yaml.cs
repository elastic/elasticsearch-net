using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Explain
{
	public partial class ExplainTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicMltTests : YamlTestsBase
		{
			[Test]
			public void BasicMltTest()
			{	

				//do index 
				_body = new {
					foo= "bar",
					title= "howdy"
				};
				this.Do(()=> this._client.IndexPost("test_1", "test", "1", _body));

				//do indices.refresh 
				this.Do(()=> this._client.IndicesRefreshGet());

				//do explain 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this.Do(()=> this._client.ExplainPost("test_1", "test", "1", _body));

				//is_true _response.matched; 
				this.IsTrue(_response.matched);

				//is_true _response.ok; 
				this.IsTrue(_response.ok);

				//match _response.explanation.value: 
				this.IsMatch(_response.explanation.value, 1);

			}
		}
	}
}

