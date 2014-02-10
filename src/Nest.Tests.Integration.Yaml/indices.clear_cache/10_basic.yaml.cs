using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.IndicesClearCache
{
	public partial class IndicesClearCache10BasicYaml10Tests
	{
		
		public class ClearCacheTest10Tests
		{
			private readonly RawElasticClient _client;
		
			public ClearCacheTest10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void ClearCacheTestTests()
			{

				//do indices.clear_cache 
				this._client.IndicesClearCachePost(nv=>nv);
			}
		}
	}
}