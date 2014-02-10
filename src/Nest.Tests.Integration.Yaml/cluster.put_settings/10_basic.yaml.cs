using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.ClusterPutSettings
{
	public partial class ClusterPutSettings10BasicYaml10Tests
	{
		
		public class TestPutSettings10Tests : YamlTestsBase
		{
			private readonly RawElasticClient _client;
			private object _body;
			private ConnectionStatus _status;
			private dynamic _response;
		
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
				_body = new {
					transient= new {
						discovery= new { zen= new { minimum_master_nodes=  "1" } }
					}
				};
				_status = this._client.ClusterPutSettings(_body);
				_response = _status.Deserialize<dynamic>();

				//do cluster.get_settings 
				
				_status = this._client.ClusterGetSettings();
				_response = _status.Deserialize<dynamic>();
			}
		}
	}
}
