using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Nest;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using Nest.TestData.Domain;

namespace Nest.Tests.DSL
{
	[TestFixture]
	public class JsonifyTests
	{
		public JsonifyTests()
		{

		}

		[Test]
		public void TestFromSize()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10);
			var json = ElasticClient.Serialize(s);
			var expected = "{ from: 0, size: 10 }";
			Assert.True(json.JsonEquals(expected));
		}
		[Test]
		public void TestSkipTake()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.Skip(0)
				.Take(10);
			var json = ElasticClient.Serialize(s);
			var expected = "{ from: 0, size: 10 }";
			Assert.True(json.JsonEquals(expected));
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
			var json = ElasticClient.Serialize(s);
			var expected = @"{ 
				from: 0, size: 10,
				explain: true, 
				version: true,
				min_score: 0.4
			}";
			Assert.True(json.JsonEquals(expected));
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
				.IndicesBoost(b => b.Add("index1", 1.4).Add("index2", 1.3));
			;
			var json = ElasticClient.Serialize(s);
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
			Assert.True(json.JsonEquals(expected));
		}
		[Test]
		public void TestPreference()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Preference("_primary");
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, preference: ""_primary"" }";
			Assert.True(json.JsonEquals(expected));
		}
		[Test]
		public void TestExecuteOnPrimary()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.ExecuteOnPrimary();
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, preference: ""_primary"" }";
			Assert.True(json.JsonEquals(expected));
		}
		[Test]
		public void TestExecuteOnLocalShard()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.ExecuteOnLocalShard();
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, preference: ""_local"" }";
			Assert.True(json.JsonEquals(expected));
		}
		[Test]
		public void TestExecuteOnNode()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.ExecuteOnNode("somenode");
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				preference: ""_only_node:somenode"" }";
			Assert.True(json.JsonEquals(expected));
		}
		[Test]
		public void TestFields()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Fields(e => e.Id, e => e.Name);
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				fields: [""id"", ""name""]
				}";
			Assert.True(json.JsonEquals(expected));
		}
		[Test]
		public void TestSort()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Fields(e => e.Id, e => e.Name)
				.SortAscending(e => e.LOC);
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
					sort: {
						""loc.sort"": ""asc""
					},
					fields: [""id"", ""name""]
				}";
			Assert.True(json.JsonEquals(expected));
		}
		[Test]
		public void TestSortDescending()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Fields(e => e.Id, e => e.Name)
				.SortAscending(e => e.LOC)
				.SortDescending(e => e.Name);
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
					sort: {
						""loc.sort"": ""asc"",
						""name.sort"": ""desc""
					},
					fields: [""id"", ""name""]
				}";
			Assert.True(json.JsonEquals(expected));
		}

		[Test]
		public void TestSuperSimpleQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q);
			var json = ElasticClient.Serialize(s);
			var expected = "{ from: 0, size: 10, query : {}}";
			Assert.True(json.JsonEquals(expected));
		}
			
		
		
		[Test]
		public void TestRawQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.QueryRaw(@"{ raw : ""query""}");
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : { raw : ""query""}}";
			Assert.True(json.JsonEquals(expected));
		}
		[Test]
		public void TestRawFilter()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.FilterRaw(@"{ raw : ""query""}");
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, filter : { raw : ""query""}}";
			Assert.True(json.JsonEquals(expected));
		}
		[Test]
		public void TestRawFilterAndQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.FilterRaw(@"{ raw : ""query""}")
				.QueryRaw(@"{ raw : ""query""}");
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : { raw : ""query""}, filter : { raw : ""query""}}";
			Assert.True(json.JsonEquals(expected));
		}
		

		/*
		[Test]
		public void TestSuperSimpleQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.Term(e=>e.Name, "nest", boost: 1.0)
					.Bool(b=>b
						.Must(m=> m
							.Term(e => e.Name, "nest", boost: 1.0)
						)
						.MustNot(m=>m
							.Term(e => e.Name, "nest12", boost: 1.0)
						)
						.Should(s=>s
							.Term(e => e.Name, "nestle", boost: 1.0)
						)
						.MinimumNumberShouldMatch(1)
						.Boost(1.0)
					)
				);
			var json = ElasticClient.Serialize(s);
			var expected = "{ from: 0, size: 10, query : {}}";
			Assert.True(json.JsonEquals(expected));
		}*/
	}
}
