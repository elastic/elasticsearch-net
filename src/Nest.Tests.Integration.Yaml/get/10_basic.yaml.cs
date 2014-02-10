using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Get
{
	public partial class Get10BasicYaml10Tests
	{
		
		public class Basic10Tests
		{
			private readonly RawElasticClient _client;
		
			public Basic10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void BasicTests()
			{

				//do index 
				this._client.IndexPost("test_1", "test", "ä¸­æ–‡", "SERIALIZED BODY HERE", nv=>nv);

				//do get 
				this._client.Get("test_1", "test", "ä¸­æ–‡", nv=>nv);

				//do get 
				this._client.Get("test_1", "_all", "ä¸­æ–‡", nv=>nv);
			}
		}
	}
}