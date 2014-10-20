using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Search5
{
	public partial class Search5YamlTests
	{	
	
		public class Search5Issue4895YamlBase : YamlTestsBase
		{
			public Search5Issue4895YamlBase() : base()
			{	

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test", null));

				//do index 
				_body = new {
					user= "foo",
					amount= "35",
					data= "some"
				};
				this.Do(()=> _client.Index("test", "test", "1", _body));

				//do indices.refresh 
				this.Do(()=> _client.IndicesRefresh("test"));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestWithLocalPreferencePlacedInQueryBodyShouldFail2Tests : Search5Issue4895YamlBase
		{
			[Test]
			public void TestWithLocalPreferencePlacedInQueryBodyShouldFail2Test()
			{	

				//do search 
				_body = new {
					query= new {
						term= new {
							data= "some"
						},
						preference= "_local"
					},
					fields= new [] {
						"user",
						"amount"
					}
				};
				this.Do(()=> _client.Search("test", "test", _body), shouldCatch: @"request");

			}
		}
	}
}

