using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.ClusterPutSettings
{
	public partial class ClusterPutSettings10BasicYaml10Tests
	{
		
		public class TestPutSettings10Tests
		{
			private readonly RawElasticClient _client;
		
			public TestPutSettings10Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void TestPutSettingsTests()
			{

				//do cluster.put_settings 
				this._client.ClusterPutSettings("SERIALIZED BODY HERE", nv=>nv);

				//do cluster.get_settings 
				this._client.ClusterGetSettings(nv=>nv);
			}
		}
	}
}