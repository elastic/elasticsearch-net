using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Suggest
{
	public partial class Suggest10BasicYaml10Tests
	{
		
		public class Setup10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public Setup10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void SetupTests()
			{

				//do index 
				_body = new {
					body= "Amsterdam meetup"
				};
				this._client.IndexPost("test", "test", "testing_document", _body, nv=>nv);

				//do indices.refresh 
				
				this._client.IndicesRefreshGet(nv=>nv);
			}
		}
		
		public class BasicTestsForSuggestApi10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public BasicTestsForSuggestApi10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void BasicTestsForSuggestApiTests()
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
				this._client.SuggestPost(_body, nv=>nv);
			}
		}
	}
}
