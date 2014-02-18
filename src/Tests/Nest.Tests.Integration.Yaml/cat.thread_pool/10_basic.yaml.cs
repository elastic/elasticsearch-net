using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.CatThreadPool1
{
	public partial class CatThreadPool1YamlTests
	{	
	
		public class CatThreadPool110BasicYamlBase : YamlTestsBase
		{
			public CatThreadPool110BasicYamlBase() : base()
			{	

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestCatThreadPoolOutput2Tests : CatThreadPool110BasicYamlBase
		{
			[Test]
			public void TestCatThreadPoolOutput2Test()
			{	

				//do cat.thread_pool 
				this.Do(()=> this._client.CatThreadPoolGet());

				//match this._status: 
				this.IsMatch(this._status, @"/  #host       ip                          bulk.active       bulk.queue       bulk.rejected       index.active       index.queue       index.rejected       search.active       search.queue       search.rejected ^  (\S+   \s+  (\d{1,3}\.){3}\d{1,3}  \s+  \d+          \s+  \d+         \s+  \d+            \s+  \d+           \s+  \d+          \s+  \d+             \s+  \d+            \s+  \d+           \s+  \d+              \s+  \n)+  $/
");

				//do cat.thread_pool 
				this.Do(()=> this._client.CatThreadPoolGet(nv=>nv
					.Add("v", @"true")
				));

				//match this._status: 
				this.IsMatch(this._status, @"/^ host  \s+  ip                     \s+  bulk.active  \s+  bulk.queue  \s+  bulk.rejected  \s+  index.active  \s+  index.queue  \s+  index.rejected  \s+  search.active  \s+  search.queue  \s+  search.rejected  \s+  \n
  (\S+   \s+  (\d{1,3}\.){3}\d{1,3}  \s+  \d+          \s+  \d+         \s+  \d+            \s+  \d+           \s+  \d+          \s+  \d+             \s+  \d+            \s+  \d+           \s+  \d+              \s+  \n)+  $/
");

				//do cat.thread_pool 
				this.Do(()=> this._client.CatThreadPoolGet(nv=>nv
					.Add("h", @"pid,id,h,i,po")
				));

				//match this._status: 
				this.IsMatch(this._status, @"/  #pid       id          host       ip                          port ^  (\d+  \s+  \S{4}  \s+  \S+   \s+  (\d{1,3}\.){3}\d{1,3}  \s+  \d{4}  \n)+  $/
");

				//do cat.thread_pool 
				this.Do(()=> this._client.CatThreadPoolGet(nv=>nv
					.Add("h", @"id,ba,fa,gea,ga,ia,maa,ma,oa,pa")
					.Add("v", @"true")
					.Add("full_id", @"true")
				));

				//match this._status: 
				this.IsMatch(this._status, @"/^  id   \s+  ba   \s+  fa   \s+  gea  \s+  ga   \s+  ia   \s+  maa  \s+  ma   \s+  oa   \s+  pa  \s+  \n
   (\S+  \s+  \d+  \s+  \d+  \s+  \d+  \s+  \d+  \s+  \d+  \s+  \d+  \s+  \d+  \s+  \d+  \s+  \d+ \s+  \n)+  $/
");

			}
		}
	}
}

