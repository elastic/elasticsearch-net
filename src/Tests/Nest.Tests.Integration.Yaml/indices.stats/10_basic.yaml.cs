using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesStats
{
	public partial class IndicesStatsTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class StatsTestTests : YamlTestsBase
		{
			[Test]
			public void StatsTestTest()
			{	

				//do indices.stats 
				this.Do(()=> this._client.IndicesStatsGet());

				//is_true _response.ok; 
				this.IsTrue(_response.ok);

			}
		}
	}
}

