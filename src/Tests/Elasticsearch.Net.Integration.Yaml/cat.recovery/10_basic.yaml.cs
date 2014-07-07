using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.CatRecovery1
{
	public partial class CatRecovery1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestCatRecoveryOutput1Tests : YamlTestsBase
		{
			[Test]
			public void TestCatRecoveryOutput1Test()
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
				this.IsMatch(this._status, @"/^
(
  index1      \s+
  \d          \s+                                 # shard
  \d+         \s+                                 # time
  (gateway|replica|snapshot|relocating)     \s+   # type
  (init|index|start|translog|finalize|done) \s+   # stage
  [-\w./]+    \s+                                 # source_host
  [-\w./]+    \s+                                 # target_host
  [-\w./]+    \s+                                 # repository
  [-\w./]+    \s+                                 # snapshot
  \d+         \s+                                 # files
  \d+\.\d+%   \s+                                 # files_percent
  \d+         \s+                                 # bytes
  \d+\.\d+%   \s+                                 # bytes_percent
  \n
)+
$/
");

			}
		}
	}
}

