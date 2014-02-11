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


		public class ClearCacheTestTests : YamlTestsBase
		{
			[Test]
			public void ClearCacheTestTest()
			{	

				//do indices.clear_cache 
				_status = this._client.IndicesClearCacheGet();
				_response = _status.Deserialize<dynamic>();

				//is_true .ok; 
				this.IsTrue(_response.ok);

			}
		}
	}
}

