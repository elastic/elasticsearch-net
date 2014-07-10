using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Exists4
{
	public partial class Exists4YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ParentWithRoutingTests : YamlTestsBase
		{
			[Test]
			public void ParentWithRoutingTest()
			{	

				//do indices.create 
				_body = new {
					mappings= new {
						test= new {
							_parent= new {
								type= "foo"
							}
						}
					},
					settings= new {
						number_of_shards= "5",
						number_of_replicas= "0"
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
					.AddQueryString("parent", 5)
					.AddQueryString("routing", 4)
				));

				//is_true this._status; 
				this.IsTrue(this._status);

				//do exists 
				this.Do(()=> _client.Exists("test_1", "test", "1", nv=>nv
					.AddQueryString("parent", 5)
					.AddQueryString("routing", 4)
				));

				//is_true this._status; 
				this.IsTrue(this._status);

				//do exists 
				this.Do(()=> _client.Exists("test_1", "test", "1", nv=>nv
					.AddQueryString("parent", 5)
				));

				//is_false this._status; 
				this.IsFalse(this._status);

				//do exists 
				this.Do(()=> _client.Exists("test_1", "test", "1", nv=>nv
					.AddQueryString("routing", 4)
				));

				//is_true this._status; 
				this.IsTrue(this._status);

			}
		}
	}
}

