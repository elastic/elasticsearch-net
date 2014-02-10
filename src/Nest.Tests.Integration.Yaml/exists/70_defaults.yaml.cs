using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Exists
{
	public partial class Exists70DefaultsYaml70Tests
	{
		
		public class ClientSideDefaultType70Tests
		{
			private readonly RawElasticClient _client;
		
			public ClientSideDefaultType70Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void ClientSideDefaultTypeTests()
			{

				//do index 
				this._client.IndexPost("test_1", "test", "1", "SERIALIZED BODY HERE", nv=>nv);

				//do exists 
				this._client.ExistsHead("test_1", "_all", "1", nv=>nv);
			}
		}
	}
}