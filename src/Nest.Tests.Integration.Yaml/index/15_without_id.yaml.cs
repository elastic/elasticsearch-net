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
	public partial class Index15WithoutIdYaml15Tests
	{
		
		public class IndexWithoutId15Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public IndexWithoutId15Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void IndexWithoutIdTests()
			{

				//do index 
				_body = new {
					foo= "bar"
				};
				this._client.IndexPost("test_1", "test", _body, nv=>nv);

				//do get 
				
				this._client.Get("test_1", "test", "$id", nv=>nv);
			}
		}
	}
}
