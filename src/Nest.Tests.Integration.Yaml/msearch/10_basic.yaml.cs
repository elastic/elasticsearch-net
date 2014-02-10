using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Msearch
{
	public partial class Msearch10BasicYaml10Tests
	{
		
		public class BasicMultiSearch10Tests
		{
			private readonly RawElasticClient _client;
		
			public BasicMultiSearch10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void BasicMultiSearchTests()
			{

				//do index 
				this._client.IndexPost("test_1", "test", "1", "SERIALIZED BODY HERE", nv=>nv);

				//do index 
				this._client.IndexPost("test_1", "test", "2", "SERIALIZED BODY HERE", nv=>nv);

				//do index 
				this._client.IndexPost("test_1", "test", "3", "SERIALIZED BODY HERE", nv=>nv);

				//do indices.refresh 
				this._client.IndicesRefreshPost(nv=>nv);

				//do msearch 
				this._client.MsearchPost("SERIALIZED BODY HERE", nv=>nv);
			}
		}
	}
}