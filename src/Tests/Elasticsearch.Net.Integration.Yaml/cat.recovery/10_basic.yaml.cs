using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.CatRecovery1
{
	public partial class CatRecovery1YamlTests
	{	
	
		public class CatRecovery110BasicYamlBase : YamlTestsBase
		{
			public CatRecovery110BasicYamlBase() : base()
			{	

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestCatRecoveryOutput2Tests : CatRecovery110BasicYamlBase
		{
			[Test]
			public void TestCatRecoveryOutput2Test()
			{	

				//do cat.recovery 
				this.Do(()=> _client.CatRecovery());

				//match this._status: 
				this.IsMatch(this._status, @"/^$/
");

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("index1", "type1", "1", _body, nv=>nv
					.AddQueryString("refresh", @"true")
				));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"yellow")
				));

				//do cat.recovery 
				this.Do(()=> _client.CatRecovery());

				//match this._status: 
				this.IsMatch(this._status, @"/^(index1 \s+ \d+ \s+ \d+ \s+
   (gateway|replica|snapshot|relocating) \s+
   (init|index|start|translog|finalize|done) \s+
   ([\w/.-])+ \s+ 
   ([\w/.-])+ \s+ 
   ([\w/.-])+ \s+
   ([\w/.-])+ \s+
   \d+ \s+ \d+\.\d+\% \s+ \d+ \s+ \d+\.\d+\% \s+ \n?)
 {1,}$/
");

			}
		}
	}
}

