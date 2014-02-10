using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Explain
{
	public partial class Explain10BasicYaml10Tests
	{
		
		public class BasicMlt10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public BasicMlt10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void BasicMltTests()
			{

				//do index 
				_body = new {
					foo= "bar",
					title= "howdy"
				};
				this._client.IndexPost("test_1", "test", "1", _body, nv=>nv);

				//do indices.refresh 
				
				this._client.IndicesRefreshGet(nv=>nv);

				//do explain 
				_body = new {
					query= new {
						match_all= new {}
					}
				};
				this._client.ExplainPost("test_1", "test", "1", _body, nv=>nv);
			}
		}
	}
}
