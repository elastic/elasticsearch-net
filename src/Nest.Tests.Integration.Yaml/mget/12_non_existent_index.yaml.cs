using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Mget
{
	public partial class Mget12NonExistentIndexYaml12Tests
	{
		
		public class NonExistentIndex12Tests
		{
			private readonly RawElasticClient _client;
		
			public NonExistentIndex12Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void NonExistentIndexTests()
			{

				//do index 
				this._client.IndexPost("test_1", "test", "1", "SERIALIZED BODY HERE", nv=>nv);

				//do cluster.health 
				this._client.ClusterHealthGet(nv=>nv);

				//do mget 
				this._client.MgetPost("SERIALIZED BODY HERE", nv=>nv);

				//do mget 
				this._client.MgetPost("SERIALIZED BODY HERE", nv=>nv);
			}
		}
	}
}