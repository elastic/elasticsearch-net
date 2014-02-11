using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesSnapshotIndex
{
	public partial class IndicesSnapshotIndexTests
	{	


		public class SnapshotIndexTestTests : YamlTestsBase
		{
			[Test]
			public void SnapshotIndexTestTest()
			{	

				//do indices.snapshot_index 
				this.Do(()=> this._client.IndicesSnapshotIndexPost());

				//is_true _response.ok; 
				this.IsTrue(_response.ok);

			}
		}
	}
}

