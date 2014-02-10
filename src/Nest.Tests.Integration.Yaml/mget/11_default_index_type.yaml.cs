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
	public partial class Mget11DefaultIndexTypeYaml11Tests
	{
		
		public class DefaultIndexType11Tests
		{
			private readonly RawElasticClient _client;
		
			public DefaultIndexType11Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void DefaultIndexTypeTests()
			{

				//do indices.create 
				this._client.IndicesCreatePost("test_2", null, nv=>nv);

				//do index 
				this._client.IndexPost("test_1", "test", "1", "SERIALIZED BODY HERE", nv=>nv);

				//do cluster.health 
				this._client.ClusterHealthGet(nv=>nv);

				//do mget 
				this._client.MgetPost("test_1", "test", "SERIALIZED BODY HERE", nv=>nv);
			}
		}
	}
}