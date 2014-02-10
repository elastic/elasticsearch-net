using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Index
{
	public partial class Index10WithIdYaml10Tests
	{
		
		public class IndexWithId10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public IndexWithId10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void IndexWithIdTests()
			{

				//do index 
				_body = new {
					foo= "bar"
				};
				this._client.IndexPost("test-weird-index-Ã¤Â¸Â­Ã¦â€“â€¡", "weird.type", "1", _body, nv=>nv);

				//do get 
				
				this._client.Get("test-weird-index-Ã¤Â¸Â­Ã¦â€“â€¡", "weird.type", "1", nv=>nv);
			}
		}
	}
}
