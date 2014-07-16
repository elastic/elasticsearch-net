using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.ClusterPutSettings1
{
	public partial class ClusterPutSettings1YamlTests
	{	
	
		public class ClusterPutSettings110BasicYamlBase : YamlTestsBase
		{
			public ClusterPutSettings110BasicYamlBase() : base()
			{	

				//skip 0 - 999; 
				this.Skip("0 - 999", "leaves transient metadata behind, need to fix it");

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestPutSettings2Tests : ClusterPutSettings110BasicYamlBase
		{
			[Test]
			public void TestPutSettings2Test()
			{	

				//do cluster.put_settings 
				_body = new {
					transient= new Dictionary<string, object> {
						 { "discovery.zen.minimum_master_nodes",  "1" }
					}
				};
				this.Do(()=> _client.ClusterPutSettings(_body, nv=>nv
					.AddQueryString("flat_settings", @"true")
				));

				//match _response.transient: 
				this.IsMatch(_response.transient, new Dictionary<string, object> {
					{ @"discovery.zen.minimum_master_nodes", @"1" }
				});

				//do cluster.get_settings 
				this.Do(()=> _client.ClusterGetSettings(nv=>nv
					.AddQueryString("flat_settings", @"true")
				));

				//match _response.transient: 
				this.IsMatch(_response.transient, new Dictionary<string, object> {
					{ @"discovery.zen.minimum_master_nodes", @"1" }
				});

			}
		}
	}
}

