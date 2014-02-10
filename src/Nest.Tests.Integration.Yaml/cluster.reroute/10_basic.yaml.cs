using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.ClusterReroute
{
	public partial class ClusterReroute10BasicYaml10Tests
	{
		
		public class BasicSanityCheck10Tests
		{
			private readonly RawElasticClient _client;
		
			public BasicSanityCheck10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void BasicSanityCheckTests()
			{

				//do cluster.reroute 
				this._client.ClusterReroutePost(null, nv=>nv);
			}
		}
	}
}