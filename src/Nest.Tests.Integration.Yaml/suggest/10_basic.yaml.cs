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

				//skip 0 - 0.90.2; 
				this.Skip("0 - 0.90.2", "Suggest is broken on 0.90.2 - see #3246");

				//do index 
				_body = new {
					body= "Amsterdam meetup"
				};
				this.Do(()=> this._client.IndexPost("test", "test", "testing_document", _body));

				//do indices.refresh 
				this.Do(()=> this._client.IndicesRefreshGet());

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
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

