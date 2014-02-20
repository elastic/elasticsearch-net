using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesSegments1
{
	public partial class IndicesSegments1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class SegmentsTest1Tests : YamlTestsBase
		{
			[Test]
			public void SegmentsTest1Test()
			{	

				//do indices.segments 
				this.Do(()=> _client.IndicesSegmentsForAll(nv=>nv
					.Add("allow_no_indices", @"true")
				));

			}
		}
	}
}

