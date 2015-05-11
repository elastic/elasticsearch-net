using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using Elasticsearch.Net;

namespace Nest.Tests.Unit.Search.SearchOptions
{
	using System.Collections.Generic;
	using System.Linq;

	[TestFixture]
	public class SearchOptionTests : BaseJsonTests
	{
		[Test]
		public void TestFromSize()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10);
			var json = TestElasticClient.Serialize(s);
			var expected = "{ from: 0, size: 10 }";
			Assert.True(json.JsonEquals(expected));
		}
		[Test]
		public void TestSkipTake()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.Skip(0)
				.Take(10);
			var json = TestElasticClient.Serialize(s);
			var expected = "{ from: 0, size: 10 }";
			Assert.True(json.JsonEquals(expected));
		}
		[Test]
		public void TestBasics()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.Skip(0)
				.Take(10)
				.Explain()
				.Version()
				.MinScore(0.4);
			var json = TestElasticClient.Serialize(s);
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
			var s = new SearchDescriptor<ElasticsearchProject>()
				.Skip(0)
				.Take(10)
				.Explain()
				.Version()
				.MinScore(0.4)
				.IndicesBoost(b => b.Add("index1", 1.4).Add("index2", 1.3));
			;
			var json = TestElasticClient.Serialize(s);
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
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void TestPreference()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Preference("_primary");
			var result = this._client.Search<ElasticsearchProject>(ss=>s);
			StringAssert.Contains("preference=_primary", result.ConnectionStatus.RequestUrl);
		}
		[Test]
		public void TestExecuteOnPrimary()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.ExecuteOnPrimary();
			var result = this._client.Search<ElasticsearchProject>(ss=>s);
			StringAssert.Contains("preference=_primary", result.ConnectionStatus.RequestUrl);
		}
		[Test]
		public void TestExecuteOnPrimaryFirst()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.ExecuteOnPrimaryFirst();
			var result = this._client.Search<ElasticsearchProject>(ss=>s);
			StringAssert.Contains("preference=_primary_first", result.ConnectionStatus.RequestUrl);
		}
		[Test]
		public void TestExecuteOnLocalShard()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.ExecuteOnLocalShard();
			var result = this._client.Search<ElasticsearchProject>(ss=>s);
			StringAssert.Contains("preference=_local", result.ConnectionStatus.RequestUrl);
		}
		[Test]
		public void TestExecuteOnNode()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.ExecuteOnNode("somenode");
			var result = this._client.Search<ElasticsearchProject>(ss=>s);
			//normalize difference between .NET 4.5 and prior
			var url = result.ConnectionStatus.RequestUrl.Replace("%3A", ":");
			StringAssert.Contains("preference=_only_node:somenode", url);
		}
		[Test]
		public void TestExecuteOnPreferredNode()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.ExecuteOnPreferredNode("somenode");
			var result = this._client.Search<ElasticsearchProject>(ss=>s);
			//normalize difference between .NET 4.5 and prior
			var url = result.ConnectionStatus.RequestUrl.Replace("%3A", ":");
			StringAssert.Contains("preference=_prefer_node:somenode", url);
		}
		[Test]
		public void TestFields()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Fields(e => e.Id, e => e.Name);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				fields: [""id"", ""name""]
				}";
			Assert.True(json.JsonEquals(expected));
		}
		[Test]
		public void TestFieldsByName()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Fields("id", "name");
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				fields: [""id"", ""name""]
				}";
			Assert.True(json.JsonEquals(expected));
		}

		[Test]
		public void TestFieldsWithExclusionsByProperty()
		{
		  var fields = typeof(ElasticsearchProject).GetProperties()
			.Select(x => x.Name.ToCamelCase())
			.Except(new List<string> { "followers", "contributors", "nestedFollowers", "myGeoShape" }).ToList();

			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Fields(fields.ConvertAll(x => x.ToCamelCase()).ToArray());

		  var json = TestElasticClient.Serialize(s);

		  const string expected = @"{
		  from: 0,
		  size: 10,
		  fields: [
			""id"",
			""name"",
			""version"",
			""country"",
			""content"",
			""lOC"",
			""origin"",
			""startedOn"",
			""pingIP"",
			""longValue"",
			""floatValue"",
			""doubleValue"",
			""boolValue"",
			""intValues"",
			""floatValues"",
			""locScriptField"",
			""stupidIntIWantAsLong"",
			""myAttachment"",
			""myBinaryField"",
			""product"",
            ""myStringArrayField"",
			""suggest""
		  ]
		}";
		  Assert.True(json.JsonEquals(expected));
		}

		[Test]
		public void TestSort()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Fields(e => e.Id, e => e.Name)
				.SortAscending(e => e.LOC.Suffix("sort"));
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
					sort: [
						{""loc.sort"": { order: ""asc"" }}
					],
					fields: [""id"", ""name""]
				}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void TestSortDescending()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Fields(e => e.Id, e => e.Name)
				.SortAscending(e => e.LOC.Suffix("sort"))
				.SortDescending(e => e.Name.Suffix("sort"));
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
					sort: [
						{""loc.sort"": { order: ""asc"" }},
						{""name.sort"": { order: ""desc"" }}
					],
					fields: [""id"", ""name""]
				}";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void TestSuperSimpleQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q);
			var json = TestElasticClient.Serialize(s);
			var expected = "{ from: 0, size: 10, query : {}}";
			Assert.True(json.JsonEquals(expected));
		}
			
		
		
		[Test]
		public void TestRawQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.QueryRaw(@"{ raw : ""query""}");
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : { raw : ""query""}}";
			Assert.True(json.JsonEquals(expected));
		}
		[Test]
		public void TestRawFilter()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.FilterRaw(@"{ raw : ""query""}");
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, post_filter : { raw : ""query""}}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void TestRawFilterAndQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.FilterRaw(@"{ raw : ""query""}")
				.QueryRaw(@"{ raw : ""query""}");
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : { raw : ""query""}, post_filter : { raw : ""query""}}";
			Assert.True(json.JsonEquals(expected));
		}
		
	}
}
