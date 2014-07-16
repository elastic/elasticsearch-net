using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesStats1
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
				this.Do(()=> _client.IndicesStatsForAll());

			}
		}
	}
}

