using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.GetSource6
{
	public partial class GetSource6YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Realtime1Tests : YamlTestsBase
		{
			[Test]
			public void Realtime1Test()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						refresh_interval= "-1",
						number_of_replicas= "0"
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
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//do get_source 
				this.Do(()=> _client.GetSource("test_1", "test", "1", nv=>nv
					.Add("realtime", 1)
				));

				//match this._status: 
				this.IsMatch(this._status, new {
					foo= "bar"
				});

				//do get_source 
				this.Do(()=> _client.GetSource("test_1", "test", "1", nv=>nv
					.Add("realtime", 0)
				), shouldCatch: @"missing");

				//do get_source 
				this.Do(()=> _client.GetSource("test_1", "test", "1", nv=>nv
					.Add("realtime", 0)
					.Add("refresh", 1)
				));

				//match this._status: 
				this.IsMatch(this._status, new {
					foo= "bar"
				});

			}
		}
	}
}

