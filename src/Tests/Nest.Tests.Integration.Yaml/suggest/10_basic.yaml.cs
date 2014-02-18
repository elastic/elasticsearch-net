using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Suggest1
{
	public partial class Suggest1YamlTests
	{	
	
		public class Suggest110BasicYamlBase : YamlTestsBase
		{
			public Suggest110BasicYamlBase() : base()
			{	

				//do index 
				_body = new {
					body= "Amsterdam meetup"
				};
				this.Do(()=> this._client.IndexPost("test", "test", "testing_document", _body));

				//do indices.refresh 
				this.Do(()=> this._client.IndicesRefreshPostForAll());

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class BasicTestsForSuggestApi2Tests : Suggest110BasicYamlBase
		{
			[Test]
			public void BasicTestsForSuggestApi2Test()
			{	

				//do suggest 
				_body = new {
					test_suggestion= new {
						text= "The Amsterdma meetpu",
						term= new {
							field= "body"
						}
					}
				};
				this.Do(()=> this._client.SuggestPost(_body));

				//match _response.test_suggestion[1].options[0].text: 
				this.IsMatch(_response.test_suggestion[1].options[0].text, @"amsterdam");

				//match _response.test_suggestion[2].options[0].text: 
				this.IsMatch(_response.test_suggestion[2].options[0].text, @"meetup");

			}
		}
	}
}

