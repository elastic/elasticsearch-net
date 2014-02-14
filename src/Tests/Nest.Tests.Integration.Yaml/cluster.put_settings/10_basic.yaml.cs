using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.ClusterPutSettings1
{
	public partial class ClusterPutSettings1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestPutSettings1Tests : YamlTestsBase
		{
			[Test]
			public void TestPutSettings1Test()
			{	

				//do cluster.put_settings 
				_body = new {
					transient= new Dictionary<string, object> {
						 { "discovery.zen.minimum_master_nodes",  "1" }
					}
				};
				this.Do(()=> this._client.ClusterPutSettings(_body, nv=>nv
					.Add("flat_settings", @"true")
				));

				//match _response.transient: 
				this.IsMatch(_response.transient, new Dictionary<string, object> {
					 { "discovery.zen.minimum_master_nodes",  "1" }
				});

				//do cluster.get_settings 
				this.Do(()=> this._client.ClusterGetSettings(nv=>nv
					.Add("flat_settings", @"true")
				));

				//match _response.transient: 
				this.IsMatch(_response.transient, new Dictionary<string, object> {
					 { "discovery.zen.minimum_master_nodes",  "1" }
				});

			}
		}
	}
}

