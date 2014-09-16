using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesOptimize1
{
	public partial class IndicesOptimize1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class OptimizeIndexTests1Tests : YamlTestsBase
		{
			[Test]
			public void OptimizeIndexTests1Test()
			{	

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("testing", null));

				//do indices.optimize 
				this.Do(()=> _client.IndicesOptimize("testing", nv=>nv
					.AddQueryString("max_num_segments", 1)
				));

			}
		}
	}
}

