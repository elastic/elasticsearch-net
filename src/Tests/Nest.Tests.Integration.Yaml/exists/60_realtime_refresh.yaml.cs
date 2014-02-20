using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.Exists5
{
	public partial class Exists5YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class RealtimeRefresh1Tests : YamlTestsBase
		{
			[Test]
			public void RealtimeRefresh1Test()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						index= new {
							refresh_interval= "-1",
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
				this.Do(()=> _client.Index("test_1", "test", "1", _body));

				//do exists 
				this.Do(()=> _client.Exists("test_1", "test", "1", nv=>nv
					.Add("realtime", 1)
				));

				//is_true this._status; 
				this.IsTrue(this._status);

				//do exists 
				this.Do(()=> _client.Exists("test_1", "test", "1", nv=>nv
					.Add("realtime", 0)
				));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do exists 
				this.Do(()=> _client.Exists("test_1", "test", "1", nv=>nv
					.Add("realtime", 0)
					.Add("refresh", 1)
				));

				//is_true this._status; 
				this.IsTrue(this._status);

			}
		}
	}
}

