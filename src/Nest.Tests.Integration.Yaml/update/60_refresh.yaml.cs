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
	public partial class Update60RefreshYaml60Tests
	{
		
		public class Refresh60Tests
		{
			private readonly RawElasticClient _client;
		
			public Refresh60Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void RefreshTests()
			{

				//do indices.create 
				this._client.IndicesCreatePost("test_1", "SERIALIZED BODY HERE", nv=>nv);

				//do cluster.health 
				this._client.ClusterHealthGet(nv=>nv);

				//do update 
				this._client.UpdatePost("test_1", "test", "1", "SERIALIZED BODY HERE", nv=>nv);

				//do search 
				this._client.SearchPost("test_1", "test", "SERIALIZED BODY HERE", nv=>nv);

				//do update 
				this._client.UpdatePost("test_1", "test", "2", "SERIALIZED BODY HERE", nv=>nv);

				//do search 
				this._client.SearchPost("test_1", "test", "SERIALIZED BODY HERE", nv=>nv);
			}
		}
	}
}