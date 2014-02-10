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
				this._client.IndexPost("test", "test", "testing_document", "SERIALIZED BODY HERE", nv=>nv);

				//do indices.refresh 
				this._client.IndicesRefreshPost(nv=>nv);
			}
		}
		
		public class BasicTestsForSuggestApi10Tests
		{
			private readonly RawElasticClient _client;
		
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
				this._client.SuggestPost("SERIALIZED BODY HERE", nv=>nv);
			}
		}
	}
}