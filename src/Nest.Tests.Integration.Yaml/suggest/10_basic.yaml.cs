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
				_status = this._client.IndexPost("test", "test", "testing_document", _body);
				_response = _status.Deserialize<dynamic>();

				//do indices.refresh 
				_status = this._client.IndicesRefreshGet();
				_response = _status.Deserialize<dynamic>();

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
				_status = this._client.SuggestPost(_body);
				_response = _status.Deserialize<dynamic>();

			}
		}
	}
}

