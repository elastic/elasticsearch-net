using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.CatAllocation1
{
	public partial class CatAllocation1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Help1Tests : YamlTestsBase
		{
			[Test]
			public void Help1Test()
			{	

				//do cat.allocation 
				this.Do(()=> _client.CatAllocation(nv=>nv
					.AddQueryString("help", @"true")
				));

				//match this._status: 
				this.IsMatch(this._status, @"/^  shards           .+   \n
    disk.used        .+   \n
    disk.avail       .+   \n
    disk.total       .+   \n
    disk.percent     .+   \n
    host             .+   \n
    ip               .+   \n
    node             .+   \n
$/
");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class EmptyCluster2Tests : YamlTestsBase
		{
			[Test]
			public void EmptyCluster2Test()
			{	

				//do cat.allocation 
				this.Do(()=> _client.CatAllocation());

				//match this._status: 
				this.IsMatch(this._status, @"/^
  ( 0                      \s+
    \d+(\.\d+)?[kmgt]?b    \s+
    (\d+(\.\d+)?[kmgt]b    \s+)?  #no value from client nodes
    (\d+(\.\d+)?[kmgt]b    \s+)?  #no value from client nodes
    (\d+                   \s+)?  #no value from client nodes
    [-\w.]+                \s+
    \d+(\.\d+){3}          \s+
    \w.*
    \n
  )+
$/
");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class OneIndex3Tests : YamlTestsBase
		{
			[Test]
			public void OneIndex3Test()
			{	

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test", null));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"green")
					.AddQueryString("timeout", @"1s")
				));

				//do cat.allocation 
				this.Do(()=> _client.CatAllocation());

				//match this._status: 
				this.IsMatch(this._status, @"/^
  ( \s*                          #allow leading spaces to account for right-justified text
    \d+                    \s+
    \d+(\.\d+)?[kmgt]?b    \s+
    (\d+(\.\d+)?[kmgt]b   \s+)?  #no value from client nodes
    (\d+(\.\d+)?[kmgt]b   \s+)?  #no value from client nodes
    (\d+                  \s+)?  #no value from client nodes
    [-\w.]+                \s+
    \d+(\.\d+){3}          \s+
    \w.*
    \n
  )+
  (
    \s*                          #allow leading spaces to account for right-justified text
    \d+                    \s+
    UNASSIGNED             \s+
    \n
  )?
$/
");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class NodeId4Tests : YamlTestsBase
		{
			[Test]
			public void NodeId4Test()
			{	

				//do cat.allocation 
				this.Do(()=> _client.CatAllocation("_master"));

				//match this._status: 
				this.IsMatch(this._status, @"/^
  ( 0                      \s+
    \d+(\.\d+)?[kmgt]?b    \s+
    (\d+(\.\d+)?[kmgt]b   \s+)?  #no value from client nodes
    (\d+(\.\d+)?[kmgt]b   \s+)?  #no value from client nodes
    (\d+                  \s+)?  #no value from client nodes
    [-\w.]+                \s+
    \d+(\.\d+){3}          \s+
    \w.*
    \n
  )
$/
");

				//do cat.allocation 
				this.Do(()=> _client.CatAllocation("non_existent"));

				//match this._status: 
				this.IsMatch(this._status, @"/^
$/
");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ColumnHeaders5Tests : YamlTestsBase
		{
			[Test]
			public void ColumnHeaders5Test()
			{	

				//do cat.allocation 
				this.Do(()=> _client.CatAllocation(nv=>nv
					.AddQueryString("v", @"true")
				));

				//match this._status: 
				this.IsMatch(this._status, @"/^  shards                  \s+
    disk.used               \s+
    disk.avail              \s+
    disk.total              \s+
    disk.percent            \s+
    host                    \s+
    ip                      \s+
    node                    \s+
    \n

   ( \s*                          #allow leading spaces to account for right-justified text
     0                      \s+
     \d+(\.\d+)?[kmgt]?b    \s+
     (\d+(\.\d+)?[kmgt]b   \s+)?  #no value from client nodes
     (\d+(\.\d+)?[kmgt]b   \s+)?  #no value from client nodes
     (\d+                  \s+)?  #no value from client nodes
     [-\w.]+                \s+
     \d+(\.\d+){3}          \s+
     \w.*
     \n
   )+
 $/
");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class SelectColumns6Tests : YamlTestsBase
		{
			[Test]
			public void SelectColumns6Test()
			{	

				//do cat.allocation 
				this.Do(()=> _client.CatAllocation(nv=>nv
					.AddQueryString("h", new [] {
						@"disk.percent",
						@"node"
					})
				));

				//match this._status: 
				this.IsMatch(this._status, @"/^
  ( \d*                 \s+
    \w.*
    \n
  )+
$/
");

				//do cat.allocation 
				this.Do(()=> _client.CatAllocation(nv=>nv
					.AddQueryString("h", new [] {
						@"disk.percent",
						@"node"
					})
					.AddQueryString("v", @"true")
				));

				//match this._status: 
				this.IsMatch(this._status, @"/^
  disk.percent          \s+
  node                  \s+
  \n
  (
    \s+\d*           \s+
    \w.*
    \n
  )+
$/
");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Bytes7Tests : YamlTestsBase
		{
			[Test]
			public void Bytes7Test()
			{	

				//do cat.allocation 
				this.Do(()=> _client.CatAllocation(nv=>nv
					.AddQueryString("bytes", @"g")
				));

				//match this._status: 
				this.IsMatch(this._status, @"/^
  ( 0                   \s+
    \d+                 \s+
    (\d+                 \s+)?  #no value from client nodes
    (\d+                 \s+)?  #no value from client nodes
    (\d+                 \s+)?  #no value from client nodes
    [-\w.]+             \s+
    \d+(\.\d+){3}       \s+
    \w.*
    \n
  )+
$/
");

			}
		}
	}
}

