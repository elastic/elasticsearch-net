using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Suggest
{
	public partial class SuggestTests
	{	
	
		public class Suggest10BasicYamlBase : YamlTestsBase
		{
			public Suggest10BasicYamlBase() : base()
			{	

				//do index 
				_body = new {
					body= "Amsterdam meetup"
				};
				this.Do(()=> this._client.IndexPost("test", "test", "testing_document", _body));

				//do indices.refresh 
				this.Do(()=> this._client.IndicesRefreshGet());

			}
		}


		public class BasicTestsForSuggestApiTests : Suggest10BasicYamlBase
		{
			[Test]
			public void BasicTestsForSuggestApiTest()
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

				//match _response.test_suggestion[0].options[0].text: 
				this.IsMatch(_response.test_suggestion[0].options[0].text, @"amsterdam");

				//match _response.test_suggestion[1].options[0].text: 
				this.IsMatch(_response.test_suggestion[1].options[0].text, @"meetup");

			}
		}
	}
}

