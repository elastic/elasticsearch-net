using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Ping
{
	public partial class Ping10PingYaml10Tests
	{
		
		public class Ping10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public Ping10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void PingTests()
			{

				//do ping 
				
				this._client.PingHead(nv=>nv);
			}
		}
	}
}
