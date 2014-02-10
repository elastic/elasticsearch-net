using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Delete
{
	public partial class Delete30RoutingYaml30Tests
	{
		
		public class Routing30Tests
		{
			private readonly RawElasticClient _client;
		
			public Routing30Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void RoutingTests()
			{

				//do index 
				this._client.IndexPost("test_1", "test", "1", "SERIALIZED BODY HERE", nv=>nv);

				//do cluster.health 
				this._client.ClusterHealthGet(nv=>nv);

				//do delete 
				this._client.Delete("test_1", "test", "1", nv=>nv);

				//do delete 
				this._client.Delete("test_1", "test", "1", nv=>nv);
			}
		}
	}
}