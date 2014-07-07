using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.CatShards1
{
	public partial class CatShards1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestCatShardsOutput1Tests : YamlTestsBase
		{
			[Test]
			public void TestCatShardsOutput1Test()
			{	

				//do cat.shards 
				this.Do(()=> _client.CatShards());

				//match this._status: 
				this.IsMatch(this._status, @"/^$/
");

				//do indices.create 
				_body = new {
					settings= new {
						number_of_shards= "5",
						number_of_replicas= "1"
					}
				};
				this.Do(()=> _client.IndicesCreate("index1", _body));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"yellow")
				));

				//do cat.shards 
				this.Do(()=> _client.CatShards());

				//match this._status: 
				this.IsMatch(this._status, @"/^(index1 \s+ \d \s+ (p|r) \s+ ((STARTED|INITIALIZING) \s+ (\d \s+ (\d+|\d+[.]\d+)(kb|b) \s+)? \d{1,3}.\d{1,3}.\d{1,3}.\d{1,3} \s+ .+|UNASSIGNED \s+)  \n?){10}$/
");

				//do indices.create 
				_body = new {
					settings= new {
						number_of_shards= "5",
						number_of_replicas= "0"
					}
				};
				this.Do(()=> _client.IndicesCreate("index2", _body));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"yellow")
					.AddQueryString("wait_for_relocating_shards", 0)
				));

				//do cat.shards 
				this.Do(()=> _client.CatShards());

				//match this._status: 
				this.IsMatch(this._status, @"/^(index(1|2) \s+ \d \s+ (p|r) \s+ ((STARTED|INITIALIZING) \s+ (\d \s+ (\d+|\d+[.]\d+)(kb|b) \s+)? \d{1,3}.\d{1,3}.\d{1,3}.\d{1,3} \s+ .+|UNASSIGNED \s+) \n?){15}$/
");

				//do cat.shards 
				this.Do(()=> _client.CatShards("index2"));

				//match this._status: 
				this.IsMatch(this._status, @"/^(index2 \s+ \d \s+ (p|r) \s+ ((STARTED|INITIALIZING) \s+ (\d \s+ (\d+|\d+[.]\d+)(kb|b) \s+)? \d{1,3}.\d{1,3}.\d{1,3}.\d{1,3} \s+ .+|UNASSIGNED \s+) \n?){5}$/
");

			}
		}
	}
}

