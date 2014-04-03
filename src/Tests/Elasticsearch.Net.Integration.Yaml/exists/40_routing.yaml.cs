using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Exists3
{
	public partial class Exists3YamlTests
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
					.AddQueryString("wait_for_status", @"green")
				));

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("routing", 5)
				));

				//do exists 
				this.Do(()=> _client.Exists("test_1", "test", "1", nv=>nv
					.AddQueryString("routing", 5)
				));

				//is_true this._status; 
				this.IsTrue(this._status);

				//do exists 
				this.Do(()=> _client.Exists("test_1", "test", "1"));

				//is_false this._status; 
				this.IsFalse(this._status);

			}
		}
	}
}

