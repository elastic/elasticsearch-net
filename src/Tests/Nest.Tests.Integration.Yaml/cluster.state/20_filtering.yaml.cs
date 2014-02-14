using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.ClusterState2
{
	public partial class ClusterState2YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Setup1Tests : YamlTestsBase
		{
			[Test]
			public void Setup1Test()
			{	

				//do index 
				_body = new {
					text= "The quick brown fox is brown."
				};
				this.Do(()=> this._client.IndexPost("testidx", "testtype", "testing_document", _body));

				//do indices.refresh 
				this.Do(()=> this._client.IndicesRefreshPostForAll());

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class FilteringTheClusterStateByBlocksShouldReturnTheBlocksFieldEvenIfTheResponseIsEmpty2Tests : YamlTestsBase
		{
			[Test]
			public void FilteringTheClusterStateByBlocksShouldReturnTheBlocksFieldEvenIfTheResponseIsEmpty2Test()
			{	

				//do cluster.state 
				this.Do(()=> this._client.ClusterStateGet("blocks"));

				//is_true _response.blocks; 
				this.IsTrue(_response.blocks);

				//is_false _response.nodes; 
				this.IsFalse(_response.nodes);

				//is_false _response.metadata; 
				this.IsFalse(_response.metadata);

				//is_false _response.routing_table; 
				this.IsFalse(_response.routing_table);

				//is_false _response.routing_nodes; 
				this.IsFalse(_response.routing_nodes);

				//is_false _response.allocations; 
				this.IsFalse(_response.allocations);

				//length _response.blocks: 0; 
				this.IsLength(_response.blocks, 0);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class FilteringTheClusterStateByBlocksShouldReturnTheBlocks3Tests : YamlTestsBase
		{
			[Test]
			public void FilteringTheClusterStateByBlocksShouldReturnTheBlocks3Test()
			{	

				//do indices.put_settings 
				_body = new Dictionary<string, object> {
					 { "index.blocks.read_only",  "true" }
				};
				this.Do(()=> this._client.IndicesPutSettings("testidx", _body));

				//do cluster.state 
				this.Do(()=> this._client.ClusterStateGet("blocks"));

				//is_true _response.blocks; 
				this.IsTrue(_response.blocks);

				//is_false _response.nodes; 
				this.IsFalse(_response.nodes);

				//is_false _response.metadata; 
				this.IsFalse(_response.metadata);

				//is_false _response.routing_table; 
				this.IsFalse(_response.routing_table);

				//is_false _response.routing_nodes; 
				this.IsFalse(_response.routing_nodes);

				//is_false _response.allocations; 
				this.IsFalse(_response.allocations);

				//length _response.blocks: 1; 
				this.IsLength(_response.blocks, 1);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class FilteringTheClusterStateByNodesOnlyShouldWork4Tests : YamlTestsBase
		{
			[Test]
			public void FilteringTheClusterStateByNodesOnlyShouldWork4Test()
			{	

				//do cluster.state 
				this.Do(()=> this._client.ClusterStateGet("nodes"));

				//is_false _response.blocks; 
				this.IsFalse(_response.blocks);

				//is_true _response.nodes; 
				this.IsTrue(_response.nodes);

				//is_false _response.metadata; 
				this.IsFalse(_response.metadata);

				//is_false _response.routing_table; 
				this.IsFalse(_response.routing_table);

				//is_false _response.routing_nodes; 
				this.IsFalse(_response.routing_nodes);

				//is_false _response.allocations; 
				this.IsFalse(_response.allocations);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class FilteringTheClusterStateByMetadataOnlyShouldWork5Tests : YamlTestsBase
		{
			[Test]
			public void FilteringTheClusterStateByMetadataOnlyShouldWork5Test()
			{	

				//do cluster.state 
				this.Do(()=> this._client.ClusterStateGet("metadata"));

				//is_false _response.blocks; 
				this.IsFalse(_response.blocks);

				//is_false _response.nodes; 
				this.IsFalse(_response.nodes);

				//is_true _response.metadata; 
				this.IsTrue(_response.metadata);

				//is_false _response.routing_table; 
				this.IsFalse(_response.routing_table);

				//is_false _response.routing_nodes; 
				this.IsFalse(_response.routing_nodes);

				//is_false _response.allocations; 
				this.IsFalse(_response.allocations);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class FilteringTheClusterStateByRoutingTableOnlyShouldWork6Tests : YamlTestsBase
		{
			[Test]
			public void FilteringTheClusterStateByRoutingTableOnlyShouldWork6Test()
			{	

				//do cluster.state 
				this.Do(()=> this._client.ClusterStateGet("routing_table"));

				//is_false _response.blocks; 
				this.IsFalse(_response.blocks);

				//is_false _response.nodes; 
				this.IsFalse(_response.nodes);

				//is_false _response.metadata; 
				this.IsFalse(_response.metadata);

				//is_true _response.routing_table; 
				this.IsTrue(_response.routing_table);

				//is_true _response.routing_nodes; 
				this.IsTrue(_response.routing_nodes);

				//is_true _response.allocations; 
				this.IsTrue(_response.allocations);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class FilteringTheClusterStateForSpecificIndexTemplatesShouldWork7Tests : YamlTestsBase
		{
			[Test]
			public void FilteringTheClusterStateForSpecificIndexTemplatesShouldWork7Test()
			{	

				//do indices.put_template 
				_body = new {
					template= "test-*",
					settings= new {
						number_of_shards= "1"
					}
				};
				this.Do(()=> this._client.IndicesPutTemplateForAll("test1", _body));

				//do indices.put_template 
				_body = new {
					template= "test-*",
					settings= new {
						number_of_shards= "2"
					}
				};
				this.Do(()=> this._client.IndicesPutTemplateForAll("test2", _body));

				//do indices.put_template 
				_body = new {
					template= "foo-*",
					settings= new {
						number_of_shards= "3"
					}
				};
				this.Do(()=> this._client.IndicesPutTemplateForAll("foo", _body));

				//do cluster.state 
				this.Do(()=> this._client.ClusterStateGet("metadata", nv=>nv
					.Add("index_templates", new [] {
						@"test1",
						@"test2"
					})
				));

				//is_false _response.blocks; 
				this.IsFalse(_response.blocks);

				//is_false _response.nodes; 
				this.IsFalse(_response.nodes);

				//is_true _response.metadata; 
				this.IsTrue(_response.metadata);

				//is_false _response.routing_table; 
				this.IsFalse(_response.routing_table);

				//is_false _response.routing_nodes; 
				this.IsFalse(_response.routing_nodes);

				//is_false _response.allocations; 
				this.IsFalse(_response.allocations);

				//is_true _response.metadata.templates.test1; 
				this.IsTrue(_response.metadata.templates.test1);

				//is_true _response.metadata.templates.test2; 
				this.IsTrue(_response.metadata.templates.test2);

				//is_false _response.metadata.templates.foo; 
				this.IsFalse(_response.metadata.templates.foo);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class FilteringTheClusterStateByIndicesShouldWorkInRoutingTableAndMetadata8Tests : YamlTestsBase
		{
			[Test]
			public void FilteringTheClusterStateByIndicesShouldWorkInRoutingTableAndMetadata8Test()
			{	

				//do index 
				_body = new {
					text= "The quick brown fox is brown."
				};
				this.Do(()=> this._client.IndexPost("another", "type", "testing_document", _body));

				//do indices.refresh 
				this.Do(()=> this._client.IndicesRefreshPostForAll());

				//do cluster.state 
				this.Do(()=> this._client.ClusterStateGet("routing_table,metadata", "testidx"));

				//is_false _response.metadata.indices.another; 
				this.IsFalse(_response.metadata.indices.another);

				//is_false _response.routing_table.indices.another; 
				this.IsFalse(_response.routing_table.indices.another);

				//is_true _response.metadata.indices.testidx; 
				this.IsTrue(_response.metadata.indices.testidx);

				//is_true _response.routing_table.indices.testidx; 
				this.IsTrue(_response.routing_table.indices.testidx);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class FilteringTheClusterStateUsingAllForIndicesAndMetricsShouldWork9Tests : YamlTestsBase
		{
			[Test]
			public void FilteringTheClusterStateUsingAllForIndicesAndMetricsShouldWork9Test()
			{	

				//do cluster.state 
				this.Do(()=> this._client.ClusterStateGet("_all", "_all"));

				//is_true _response.blocks; 
				this.IsTrue(_response.blocks);

				//is_true _response.nodes; 
				this.IsTrue(_response.nodes);

				//is_true _response.metadata; 
				this.IsTrue(_response.metadata);

				//is_true _response.routing_table; 
				this.IsTrue(_response.routing_table);

				//is_true _response.routing_nodes; 
				this.IsTrue(_response.routing_nodes);

				//is_true _response.allocations; 
				this.IsTrue(_response.allocations);

			}
		}
	}
}

