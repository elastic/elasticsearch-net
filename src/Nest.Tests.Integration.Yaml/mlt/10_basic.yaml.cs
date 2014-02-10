using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Mlt
{
	public partial class Mlt10BasicYaml10Tests
	{
		
		public class BasicMlt10Tests
		{
			private readonly RawElasticClient _client;
		
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
				this._client.IndexPost("test_1", "test", "1", "SERIALIZED BODY HERE", nv=>nv);

				//do indices.refresh 
				this._client.IndicesRefreshGet(nv=>nv);

				//do cluster.health 
				this._client.ClusterHealthGet(nv=>nv);

				//do mlt 
				this._client.MltGet("test_1", "test", "1", nv=>nv);
			}
		}
	}
}