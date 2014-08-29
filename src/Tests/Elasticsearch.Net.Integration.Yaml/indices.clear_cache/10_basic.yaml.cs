using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesClearCache1
{
	public partial class IndicesClearCache1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ClearCacheTest1Tests : YamlTestsBase
		{
			[Test]
			public void ClearCacheTest1Test()
			{	

				//do indices.clear_cache 
				this.Do(()=> _client.IndicesClearCacheForAll());

			}
		}
	}
}

