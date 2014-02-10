using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesOpen
{
	public partial class IndicesOpen10BasicYaml10Tests
	{
		
		public class BasicTestForIndexOpenClose10Tests
		{
			private readonly RawElasticClient _client;
		
			public BasicTestForIndexOpenClose10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void BasicTestForIndexOpenCloseTests()
			{

				//do indices.create 
				this._client.IndicesCreatePost("test_index", null, nv=>nv);

				//do cluster.health 
				this._client.ClusterHealthGet(nv=>nv);

				//do indices.close 
				this._client.IndicesClosePost("test_index", nv=>nv);

				//do search 
				this._client.SearchPost("test_index", null, nv=>nv);

				//do indices.open 
				this._client.IndicesOpenPost("test_index", nv=>nv);

				//do cluster.health 
				this._client.ClusterHealthGet(nv=>nv);

				//do search 
				this._client.SearchPost("test_index", null, nv=>nv);
			}
		}
	}
}