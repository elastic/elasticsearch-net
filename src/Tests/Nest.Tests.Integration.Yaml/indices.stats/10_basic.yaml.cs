using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesStats1
{
	public partial class IndicesStats1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class StatsTest1Tests : YamlTestsBase
		{
			[Test]
			public void StatsTest1Test()
			{	

				//do indices.stats 
				this.Do(()=> this._client.IndicesStatsGetForAll());

			}
		}
	}
}

