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
	public partial class Delete45ParentWithRoutingYaml45Tests
	{
		
		public class ParentWithRouting45Tests
		{
			private readonly RawElasticClient _client;
		
			public ParentWithRouting45Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void ParentWithRoutingTests()
			{

				//do indices.create 
				this._client.IndicesCreatePost("test_1", "SERIALIZED BODY HERE", nv=>nv);

				//do cluster.health 
				this._client.ClusterHealthGet(nv=>nv);

				//do index 
				this._client.IndexPost("test_1", "test", "1", "SERIALIZED BODY HERE", nv=>nv);

				//do delete 
				this._client.Delete("test_1", "test", "1", nv=>nv);

				//do delete 
				this._client.Delete("test_1", "test", "1", nv=>nv);
			}
		}
	}
}