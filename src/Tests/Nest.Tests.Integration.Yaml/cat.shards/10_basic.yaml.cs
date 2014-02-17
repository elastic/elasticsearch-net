using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.CatShards1
{
	public partial class CatShards1YamlTests
	{	
	
		public class CatShards110BasicYamlBase : YamlTestsBase
		{
			public CatShards110BasicYamlBase() : base()
			{	

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestCatShardsOutput2Tests : CatShards110BasicYamlBase
		{
			[Test]
			public void TestCatShardsOutput2Test()
			{	

				//do cat.shards 
				this.Do(()=> this._client.CatShardsGet());

				//match this._status: 
				this.IsMatch(this._status, @"/^$/
");

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> this._client.IndexPost("index1", "type1", "1", _body, nv=>nv
					.Add("refresh", @"true")
				));

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status", @"yellow")
				));

				//do cat.shards 
				this.Do(()=> this._client.CatShardsGet());

				//match this._status: 
				this.IsMatch(this._status, @"/^(index1 \s+ \d \s+ (p|r) \s+ ((STARTED|INITIALIZING) \s+ (\d \s+ (\d+|\d+[.]\d+)(kb|b) \s+)? \d{1,3}.\d{1,3}.\d{1,3}.\d{1,3} \s+ .+|UNASSIGNED \s+)  \n?){10}$/
");

				//do indices.create 
				_body = new {
					settings= new {
						number_of_replicas= "0"
					}
				};
				this.Do(()=> this._client.IndicesCreatePut("index2", _body));

				//do cluster.health 
				this.Do(()=> this._client.ClusterHealthGet(nv=>nv
					.Add("wait_for_status", @"yellow")
				));

				//do cat.shards 
				this.Do(()=> this._client.CatShardsGet());

				//match this._status: 
				this.IsMatch(this._status, @"/^(index(1|2) \s+ \d \s+ (p|r) \s+ ((STARTED|INITIALIZING) \s+ (\d \s+ (\d+|\d+[.]\d+)(kb|b) \s+)? \d{1,3}.\d{1,3}.\d{1,3}.\d{1,3} \s+ .+|UNASSIGNED \s+) \n?){15}$/
");

				//do cat.shards 
				this.Do(()=> this._client.CatShardsGet("index2"));

				//match this._status: 
				this.IsMatch(this._status, @"/^(index2 \s+ \d \s+ (p|r) \s+ ((STARTED|INITIALIZING) \s+ (\d \s+ (\d+|\d+[.]\d+)(kb|b) \s+)? \d{1,3}.\d{1,3}.\d{1,3}.\d{1,3} \s+ .+|UNASSIGNED \s+) \n?){5}$/
");

			}
		}
	}
}

