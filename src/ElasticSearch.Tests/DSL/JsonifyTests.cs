using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ElasticSearch.Client.DSL;
using ElasticSearch.Client;
using Newtonsoft.Json.Converters;
using ElasticSearch.Client.Resolvers.Converters;
using Nest.TestData.Domain;

namespace ElasticSearch.Tests.DSL
{
	[TestFixture]
	public class JsonifyTests
	{
		private	readonly DslTranslator _dsl = new DslTranslator();
		public JsonifyTests()
		{
			
		}


		private bool JsonEquals(string json, string otherjson)
		{
			var nJson = JObject.Parse(json).ToString();
			var nOtherJson = JObject.Parse(otherjson).ToString();
			return nJson == nOtherJson;
		}

		[Test]
		public void TestFromSize()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10);
			var json = _dsl.Serialize(s);
			var expected = "{ from: 0, size: 10 }";
			Assert.True(this.JsonEquals(json, expected));
		}
		[Test]
		public void TestSkipTake()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.Skip(0)
				.Take(10);
			var json = _dsl.Serialize(s);
			var expected = "{ from: 0, size: 10 }";
			Assert.True(this.JsonEquals(json, expected));
		}
		[Test]
		public void TestBasics()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.Skip(0)
				.Take(10)
				.Explain()
				.Version()
				.MinScore(0.4);
			var json = _dsl.Serialize(s);
			var expected = @"{ 
				from: 0, size: 10,
				explain: true, 
				version: true,
				min_score: 0.4
			}";
			Assert.True(this.JsonEquals(json, expected));
		}
		[Test]
		public void TestBasicsIndicesBoost()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.Skip(0)
				.Take(10)
				.Explain()
				.Version()
				.MinScore(0.4)
				.IndicesBoost(b=>b.Add("index1",1.4).Add("index2", 1.3));
				;
			var json = _dsl.Serialize(s);
			var expected = @"{ 
				from: 0, size: 10,
				explain: true, 
				version: true,
				min_score: 0.4,
				indices_boost : {
					index1 : 1.4,
					index2 : 1.3
				}

			}";
			Assert.True(this.JsonEquals(json, expected));
		}
		[Test]
		public void TestPreference()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Preference("_primary");
			var json = _dsl.Serialize(s);
			var expected = @"{ from: 0, size: 10, preference: ""_primary"" }";
			Assert.True(this.JsonEquals(json, expected));
		}
		[Test]
		public void TestExecuteOnPrimary()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.ExecuteOnPrimary();
			var json = _dsl.Serialize(s);
			var expected = @"{ from: 0, size: 10, preference: ""_primary"" }";
			Assert.True(this.JsonEquals(json, expected));
		}
		[Test]
		public void TestExecuteOnLocalShard()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.ExecuteOnLocalShard();
			var json = _dsl.Serialize(s);
			var expected = @"{ from: 0, size: 10, preference: ""_local"" }";
			Assert.True(this.JsonEquals(json, expected));
		}
		[Test]
		public void TestExecuteOnNode()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.ExecuteOnNode("somenode");
			var json = _dsl.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				preference: ""_only_node:somenode"" }";
			Assert.True(this.JsonEquals(json, expected));
		}
		[Test]
		public void TestFields()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Fields(e=>e.Id, e=>e.Name);
			var json = _dsl.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				fields: [""id"", ""name""]
				}";
			Assert.True(this.JsonEquals(json, expected));
		}
		[Test]
		public void TestSort()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Fields(e => e.Id, e => e.Name);
			var json = _dsl.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				fields: [""id"", ""name""]
				}";
			Assert.True(this.JsonEquals(json, expected));
		}
	}
}
