using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.ClusterReroute2
{
	public partial class ClusterReroute2YamlTests
	{	
	
		public class ClusterReroute211ExplainYamlBase : YamlTestsBase
		{
			public ClusterReroute211ExplainYamlBase() : base()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						number_of_shards= "1",
						number_of_replicas= "0"
					}
				};
				this.Do(()=> _client.IndicesCreate("test_index", _body));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"green")
				));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ExplainApiWithEmptyCommandList2Tests : ClusterReroute211ExplainYamlBase
		{
			[Test]
			public void ExplainApiWithEmptyCommandList2Test()
			{	

				//do cluster.reroute 
				_body = new {
					commands= new string[] {}
				};
				this.Do(()=> _client.ClusterReroute(_body, nv=>nv
					.AddQueryString("explain", @"true")
					.AddQueryString("dry_run", @"true")
				));

				//match _response.explanations: 
				this.IsMatch(_response.explanations, new string[] {});

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ExplainApiForNonExistantNodeShard3Tests : ClusterReroute211ExplainYamlBase
		{
			[Test]
			public void ExplainApiForNonExistantNodeShard3Test()
			{	

				//do cluster.state 
				this.Do(()=> _client.ClusterState("master_node"));

				//set node_id = _response.master_node; 
				var node_id = _response.master_node;

				//do cluster.reroute 
				_body = new {
					commands= new [] {
						new {
							cancel= new {
								index= "test_index",
								shard= "9",
								node= node_id
							}
						}
					}
				};
				this.Do(()=> _client.ClusterReroute(_body, nv=>nv
					.AddQueryString("explain", @"true")
					.AddQueryString("dry_run", @"true")
				));

				//match _response.explanations[0].command: 
				this.IsMatch(_response.explanations[0].command, @"cancel");

				//match _response.explanations[0].parameters: 
				this.IsMatch(_response.explanations[0].parameters, new {
					index= "test_index",
					shard= "9",
					node= node_id,
					allow_primary= "false"
				});

				//match _response.explanations[0].decisions[0].decider: 
				this.IsMatch(_response.explanations[0].decisions[0].decider, @"cancel_allocation_command");

				//match _response.explanations[0].decisions[0].decision: 
				this.IsMatch(_response.explanations[0].decisions[0].decision, @"NO");

				//is_true _response.explanations[0].decisions[0].explanation; 
				this.IsTrue(_response.explanations[0].decisions[0].explanation);

			}
		}
	}
}

