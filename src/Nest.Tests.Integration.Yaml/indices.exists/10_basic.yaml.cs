using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesExists
{
	public partial class IndicesExists10BasicYaml10Tests
	{
		
		public class TestIndicesExists10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public TestIndicesExists10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void TestIndicesExistsTests()
			{

				//do indices.exists 
				
				this._client.IndicesExistsHead("test_index", nv=>nv);

				//do indices.create 
				
				this._client.IndicesCreatePost("test_index", null, nv=>nv);

				//do indices.exists 
				
				this._client.IndicesExistsHead("test_index", nv=>nv);
			}
		}
	}
}
