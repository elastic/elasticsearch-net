using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesRecovery1
{
	public partial class IndicesRecovery1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class IndicesRecoveryTest1Tests : YamlTestsBase
		{
			[Test]
			public void IndicesRecoveryTest1Test()
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
					.AddQueryString("wait_for_status", @"yellow")
				));

				//do indices.recovery 
				this.Do(()=> _client.IndicesRecovery("test_1"));

				//match _response.test_1.shards[0].type: 
				this.IsMatch(_response.test_1.shards[0].type, @"GATEWAY");

				//match _response.test_1.shards[0].stage: 
				this.IsMatch(_response.test_1.shards[0].stage, @"DONE");

				//match _response.test_1.shards[0].primary: 
				this.IsMatch(_response.test_1.shards[0].primary, @"true");

				//match _response.test_1.shards[0].target.ip: 
				this.IsMatch(_response.test_1.shards[0].target.ip, @"/^\d+\.\d+\.\d+\.\d+$/");

				//match _response.test_1.shards[0].index.files.percent: 
				this.IsMatch(_response.test_1.shards[0].index.files.percent, @"/^\d+\.\d\%$/");

				//match _response.test_1.shards[0].index.bytes.percent: 
				this.IsMatch(_response.test_1.shards[0].index.bytes.percent, @"/^\d+\.\d\%$/");

			}
		}
	}
}

