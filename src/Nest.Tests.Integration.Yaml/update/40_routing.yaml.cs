using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Update
{
	public partial class Update40RoutingYaml40Tests
	{
		
		public class Routing40Tests
		{
			private readonly RawElasticClient _client;
		
			public Routing40Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void RoutingTests()
			{

				//do indices.create 
				this._client.IndicesCreatePost("test_1", "SERIALIZED BODY HERE", nv=>nv);

				//do cluster.health 
				this._client.ClusterHealthGet(nv=>nv);

				//do update 
				this._client.UpdatePost("test_1", "test", "1", "SERIALIZED BODY HERE", nv=>nv);

				//do get 
				this._client.Get("test_1", "test", "1", nv=>nv);

				//do update 
				this._client.UpdatePost("test_1", "test", "1", "SERIALIZED BODY HERE", nv=>nv);

				//do update 
				this._client.UpdatePost("test_1", "test", "1", "SERIALIZED BODY HERE", nv=>nv);
			}
		}
	}
}