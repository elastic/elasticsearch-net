using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.CatAllocation1
{
	public partial class CatAllocation1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Setup1Tests : YamlTestsBase
		{
			[Test]
			public void Setup1Test()
			{	

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Help2Tests : YamlTestsBase
		{
			[Test]
			public void Help2Test()
			{	

				//do cat.allocation 
				this.Do(()=> this._client.CatAllocationGet(nv=>nv
					.Add("help", @"true")
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
		public class EmptyCluster3Tests : YamlTestsBase
		{
			[Test]
			public void EmptyCluster3Test()
			{	

				//do cat.allocation 
				this.Do(()=> this._client.CatAllocationGet());

				//match this._status: 
				this.IsMatch(this._status, @"/^
  ( 0                   \s+
    \d+(\.\d+)?[kmgt]b  \s+
    \d+(\.\d+)?[kmgt]b  \s+
    \d+(\.\d+)?[kmgt]b  \s+
    \d+                 \s+
    [-\w.]+             \s+
    \d+(\.\d+){3}       \s+
    \w.*
    \n
  )+
$/
");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class OneIndex4Tests : YamlTestsBase
		{
			[Test]
			public void OneIndex4Test()
			{	

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePost("test", null));

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status", @"green")
					.Add("timeout", @"1s")
				));

				//do cat.allocation 
				this.Do(()=> this._client.CatAllocationGet());

				//match this._status: 
				this.IsMatch(this._status, @"/^
  ( [1-5]               \s+
    \d+(\.\d+)?[kmgt]b  \s+
    \d+(\.\d+)?[kmgt]b  \s+
    \d+(\.\d+)?[kmgt]b  \s+
    \d+                 \s+
    [-\w.]+             \s+
    \d+(\.\d+){3}       \s+
    \w.*
    \n
  )+
  (
    \d+                 \s+
    UNASSIGNED          \s+
    \n
  )?
$/
");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class NodeId5Tests : YamlTestsBase
		{
			[Test]
			public void NodeId5Test()
			{	

				//do cat.allocation 
				this.Do(()=> this._client.CatAllocationGet("_master"));

				//match this._status: 
				this.IsMatch(this._status, @"/^
  ( 0                   \s+
    \d+(\.\d+)?[kmgt]b  \s+
    \d+(\.\d+)?[kmgt]b  \s+
    \d+(\.\d+)?[kmgt]b  \s+
    \d+                 \s+
    [-\w.]+             \s+
    \d+(\.\d+){3}       \s+
    \w.*
    \n
  )
$/
");

				//do cat.allocation 
				this.Do(()=> this._client.CatAllocationGet("non_existent"));

				//match this._status: 
				this.IsMatch(this._status, @"/^ $/
");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ColumnHeaders6Tests : YamlTestsBase
		{
			[Test]
			public void ColumnHeaders6Test()
			{	

				//do cat.allocation 
				this.Do(()=> this._client.CatAllocationGet(nv=>nv
					.Add("v", @"true")
				));

				//match this._status: 
				this.IsMatch(this._status, @"/^  shards               \s+
    disk.used            \s+
    disk.avail           \s+
    disk.total           \s+
    disk.percent         \s+
    host                 \s+
    ip                   \s+
    node                 \s+
    \n

   ( \s+0                \s+
     \d+(\.\d+)?[kmgt]b  \s+
     \d+(\.\d+)?[kmgt]b  \s+
     \d+(\.\d+)?[kmgt]b  \s+
     \d+                 \s+
     [-\w.]+             \s+
     \d+(\.\d+){3}       \s+
     \w.*
     \n
   )+
 $/
");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class SelectColumns7Tests : YamlTestsBase
		{
			[Test]
			public void SelectColumns7Test()
			{	

				//do cat.allocation 
				this.Do(()=> this._client.CatAllocationGet(nv=>nv
					.Add("h", new [] {
						@"disk.percent",
						@"node"
					})
				));

				//match this._status: 
				this.IsMatch(this._status, @"/^
  ( \d+                 \s+
    \w.*
    \n
  )+
$/
");

				//do cat.allocation 
				this.Do(()=> this._client.CatAllocationGet(nv=>nv
					.Add("h", new [] {
						@"disk.percent",
						@"node"
					})
					.Add("v", @"true")
				));

				//match this._status: 
				this.IsMatch(this._status, @"/^
  disk.percent          \s+
  node                  \s+
  \n
  (
    \s+\d+              \s+
    \w.*
    \n
  )+
$/
");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Bytes8Tests : YamlTestsBase
		{
			[Test]
			public void Bytes8Test()
			{	

				//do cat.allocation 
				this.Do(()=> this._client.CatAllocationGet(nv=>nv
					.Add("bytes", @"g")
				));

				//match this._status: 
				this.IsMatch(this._status, @"/^
  ( 0                   \s+
    \d+                 \s+
    \d+                 \s+
    \d+                 \s+
    \d+                 \s+
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

