using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.GetSource4
{
	public partial class GetSource4YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Routing1Tests : YamlTestsBase
		{
			[Test]
			public void Routing1Test()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						index= new {
							number_of_replicas= "0"
						}
					}
				};
				this.Do(()=> _client.IndicesCreate("test_1", _body));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.Add("wait_for_status", @"green")
				));

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.Add("routing", 5)
				));

				//do get_source 
				this.Do(()=> _client.GetSource("test_1", "test", "1", nv=>nv
					.Add("routing", 5)
				));

				//match this._status: 
				this.IsMatch(this._status, new {
					foo= "bar"
				});

				//do get_source 
				this.Do(()=> _client.GetSource("test_1", "test", "1"), shouldCatch: @"missing");

			}
		}
	}
}

