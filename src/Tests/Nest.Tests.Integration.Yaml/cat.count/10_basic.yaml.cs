using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.CatCount1
{
	public partial class CatCount1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Setup1Tests : YamlTestsBase
		{
			[Test]
			public void Setup1Test()
			{	

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestCatCountHelp2Tests : YamlTestsBase
		{
			[Test]
			public void TestCatCountHelp2Test()
			{	

				//do cat.count 
				this.Do(()=> this._client.CatCountGet(nv=>nv
					.Add("help", @"true")
				));

				//match this._status: 
				this.IsMatch(this._status, @"/^  epoch      .+   \n
    timestamp  .+   \n
    count      .+   \n  $/
");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestCatCountOutput3Tests : YamlTestsBase
		{
			[Test]
			public void TestCatCountOutput3Test()
			{	

				//do cat.count 
				this.Do(()=> this._client.CatCountGet());

				//match this._status: 
				this.IsMatch(this._status, @"/# epoch     timestamp              count ^  \d+   \s  \d{2}:\d{2}:\d{2}  \s  0     \s  $/
");

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("index1", "type1", "1", _body, nv=>nv
					.Add("refresh", @"true")
				));

				//do cat.count 
				this.Do(()=> this._client.CatCountGet());

				//match this._status: 
				this.IsMatch(this._status, @"/# epoch     timestamp              count ^  \d+   \s  \d{2}:\d{2}:\d{2}  \s  1     \s  $/
");

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("index2", "type2", "1", _body, nv=>nv
					.Add("refresh", @"true")
				));

				//do cat.count 
				this.Do(()=> this._client.CatCountGet(nv=>nv
					.Add("h", @"count")
				));

				//match this._status: 
				this.IsMatch(this._status, @"/# count ^  2     \s  $/
");

				//do cat.count 
				this.Do(()=> this._client.CatCountGet("index1"));

				//match this._status: 
				this.IsMatch(this._status, @"/# epoch     timestamp              count ^  \d+   \s  \d{2}:\d{2}:\d{2}  \s  1  \s  $/
");

				//do cat.count 
				this.Do(()=> this._client.CatCountGet("index2", nv=>nv
					.Add("v", @"true")
				));

				//match this._status: 
				this.IsMatch(this._status, @"/^  epoch  \s+  timestamp          \s+  count  \s+  \n
    \d+    \s+  \d{2}:\d{2}:\d{2}  \s+  \d+    \s+  \n  $/
");

			}
		}
	}
}

