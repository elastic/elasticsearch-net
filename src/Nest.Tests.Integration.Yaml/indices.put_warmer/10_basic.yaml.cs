using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesPutWarmer
{
	public partial class IndicesPutWarmer10BasicYaml10Tests
	{
		
		public class BasicTestForWarmers10Tests
		{
			private readonly RawElasticClient _client;
		
			public BasicTestForWarmers10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void BasicTestForWarmersTests()
			{

				//do indices.create 
				this._client.IndicesCreatePost("test_index", null, nv=>nv);

				//do cluster.health 
				this._client.ClusterHealthGet(nv=>nv);

				//do indices.get_warmer 
				this._client.IndicesGetWarmer("test_index", "test_warmer", nv=>nv);

				//do indices.put_warmer 
				this._client.IndicesPutWarmer("test_index", "test_warmer", "SERIALIZED BODY HERE", nv=>nv);

				//do indices.get_warmer 
				this._client.IndicesGetWarmer("test_index", "test_warmer", nv=>nv);

				//do indices.delete_warmer 
				this._client.IndicesDeleteWarmer("test_index", nv=>nv);

				//do indices.get_warmer 
				this._client.IndicesGetWarmer("test_index", "test_warmer", nv=>nv);
			}
		}
	}
}