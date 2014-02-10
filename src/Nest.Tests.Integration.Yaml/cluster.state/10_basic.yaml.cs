using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.ClusterState
{
	public partial class ClusterState10BasicYaml10Tests
	{
		
		public class ClusterStateTest10Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public ClusterStateTest10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void ClusterStateTestTests()
			{

				//do cluster.state 
				
				this._client.ClusterStateGet(nv=>nv);
			}
		}
	}
}
