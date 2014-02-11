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
	public partial class ClusterPutSettingsTests
	{	


		public class TestPutSettingsTests : YamlTestsBase
		{
			[Test]
			public void TestPutSettingsTest()
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

