using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesSnapshotIndex1
{
	public partial class IndicesSnapshotIndex1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class SnapshotIndexTest1Tests : YamlTestsBase
		{
			[Test]
			public void SnapshotIndexTest1Test()
			{	

				//do indices.snapshot_index 
				this.Do(()=> _client.IndicesSnapshotIndexForAll());

			}
		}
	}
}

