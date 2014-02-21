using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Ping1
{
	public partial class Ping1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PingReturnsTrueTests : YamlTestsBase
		{
			[Test]
			public void PingReturnsTrueTest()
			{	

				//do ping 
				this.Do(()=> _client.Ping());

				//is_true this._status; 
				this.IsTrue(this._status);

			}
		}
	}
}

