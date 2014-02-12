using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesClearCache
{
	public partial class IndicesClearCacheTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ClearCacheTestTests : YamlTestsBase
		{
			[Test]
			public void ClearCacheTestTest()
			{	

				//do indices.clear_cache 
				this.Do(()=> this._client.IndicesClearCacheGet());

				//is_true _response.ok; 
				this.IsTrue(_response.ok);

			}
		}
	}
}

