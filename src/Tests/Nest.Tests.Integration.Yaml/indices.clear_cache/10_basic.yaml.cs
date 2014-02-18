using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesClearCache1
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
				this.Do(()=> this._client.IndicesClearCachePostForAll());

			}
		}
	}
}

