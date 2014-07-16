using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.CatCount1
{
	public partial class CatCount1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestCatCountHelp1Tests : YamlTestsBase
		{
			[Test]
			public void TestCatCountHelp1Test()
			{	

				//do cat.count 
				this.Do(()=> _client.CatCount(nv=>nv
					.AddQueryString("help", @"true")
				));

				//match this._status: 
				this.IsMatch(this._status, @"/^  epoch      .+   \n
    timestamp  .+   \n
    count      .+   \n  $/
");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestCatCountOutput2Tests : YamlTestsBase
		{
			[Test]
			public void TestCatCountOutput2Test()
			{	

				//do cat.count 
				this.Do(()=> _client.CatCount());

				//match this._status: 
				this.IsMatch(this._status, @"/# epoch     timestamp              count
^  \d+   \s  \d{2}:\d{2}:\d{2}  \s  0     \s  $/
");

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("index1", "type1", "1", _body, nv=>nv
					.AddQueryString("refresh", @"true")
				));

				//do cat.count 
				this.Do(()=> _client.CatCount());

				//match this._status: 
				this.IsMatch(this._status, @"/# epoch     timestamp              count
^  \d+   \s  \d{2}:\d{2}:\d{2}  \s  1     \s  $/
");

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("index2", "type2", "1", _body, nv=>nv
					.AddQueryString("refresh", @"true")
				));

				//do cat.count 
				this.Do(()=> _client.CatCount(nv=>nv
					.AddQueryString("h", @"count")
				));

				//match this._status: 
				this.IsMatch(this._status, @"/# count
^  2     \s  $/
");

				//do cat.count 
				this.Do(()=> _client.CatCount("index1"));

				//match this._status: 
				this.IsMatch(this._status, @"/# epoch     timestamp              count
^  \d+   \s  \d{2}:\d{2}:\d{2}  \s  1  \s  $/
");

				//do cat.count 
				this.Do(()=> _client.CatCount("index2", nv=>nv
					.AddQueryString("v", @"true")
				));

				//match this._status: 
				this.IsMatch(this._status, @"/^  epoch  \s+  timestamp          \s+  count  \s+  \n
    \d+    \s+  \d{2}:\d{2}:\d{2}  \s+  \d+    \s+  \n  $/
");

			}
		}
	}
}

