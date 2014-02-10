using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Scroll
{
	public partial class Scroll10BasicYaml10Tests
	{
		
		public class BasicScroll10Tests
		{
			private readonly RawElasticClient _client;
		
			public BasicScroll10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void BasicScrollTests()
			{

				//do indices.create 
				this._client.IndicesCreatePost("test_scroll", null, nv=>nv);

				//do index 
				this._client.IndexPost("test_scroll", "test", "42", "SERIALIZED BODY HERE", nv=>nv);

				//do indices.refresh 
				this._client.IndicesRefreshGet(nv=>nv);

				//do search 
				this._client.SearchPost("test_scroll", "SERIALIZED BODY HERE", nv=>nv);

				//do scroll 
				this._client.ScrollGet("$scroll_id", nv=>nv);
			}
		}
	}
}