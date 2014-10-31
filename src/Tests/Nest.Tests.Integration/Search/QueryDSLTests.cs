using System.Linq;
using Elasticsearch.Net;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Search
{
	[TestFixture]
	public class QueryDSLTests : IntegrationTests
	{
		[Test]
		public void MatchAll()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.From(0)
				.Size(10)
				.Fields(f => f.Id, f => f.Country)
				.SortAscending(f => f.LOC)
				.SortDescending(f => f.Country)
				.Query(q => q
					.MatchAll()
				)
			);
			Assert.NotNull(results);
			Assert.True(results.IsValid);
			Assert.NotNull(results.FieldSelections);
			Assert.GreaterOrEqual(results.FieldSelections.Count(), 10);

		}
		[Test]
		public void MatchAllShortcut()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.From(0)
				.Size(10)
				.Source(source=>source.Include(f => f.Id, f => f.Country))
				.SortAscending(f => f.LOC)
				.SortDescending(f => f.Country)
				.MatchAll()
			);
			Assert.NotNull(results);
			Assert.True(results.IsValid);
			Assert.NotNull(results.Documents);
			Assert.GreaterOrEqual(results.Documents.Count(), 10);
			Assert.True(results.Documents.All(d => !string.IsNullOrEmpty(d.Country)));
		}
		[Test]
		public void TestTermQuery()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.From(0)
				.Size(10)
				.Fields(f => f.Id, f => f.Name)
				.SortAscending(f => f.LOC)
				.SortDescending(f => f.Name)
				.Query(q => q
					.Term(f => f.Name, "elasticsearch.pm")
				)
			);
			Assert.NotNull(results);
			Assert.True(results.IsValid);
			Assert.NotNull(results.FieldSelections);
			Assert.GreaterOrEqual(results.FieldSelections.Count(), 1);
		}
		[Test]
		public void TestWildcardQuery()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.From(0)
				.Size(10)
				.Fields(f => f.Id, f => f.Name)
				.SortAscending(f => f.LOC)
				.SortDescending(f => f.Name)
				.Query(q => q
					.Wildcard(f => f.Name, "elasticsearch.*")
				)
			);
			Assert.NotNull(results);
			Assert.True(results.IsValid);
			Assert.NotNull(results.FieldSelections);
			Assert.GreaterOrEqual(results.FieldSelections.Count(), 1);
		}
		[Test]
		public void TestWildcardQueryBoostAndRewrite()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.From(0)
				.Size(10)
				.Fields(f => f.Id, f => f.Name)
				.SortAscending(f => f.LOC)
				.SortDescending(f => f.Name)
				.Query(q => q
					.Wildcard(f => f.Name, "elasticsearch.*", Boost: 1.0, Rewrite: RewriteMultiTerm.ScoringBoolean)
				)
			);
			Assert.NotNull(results);
			Assert.True(results.IsValid);
			Assert.NotNull(results.FieldSelections);
			Assert.GreaterOrEqual(results.FieldSelections.Count(), 1);
		}
		[Test]
		public void TestPrefixQuery()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.From(0)
				.Size(10)
				.Fields(f => f.Id, f => f.Name)
				.SortAscending(f => f.LOC)
				.SortDescending(f => f.Name)
				.Query(q => q
					.Prefix(f => f.Name.Suffix("sort"), "el")
				)
			);
			Assert.NotNull(results);
			Assert.True(results.IsValid);
			Assert.NotNull(results.FieldSelections);
			Assert.GreaterOrEqual(results.FieldSelections.Count(), 1);
		}
		[Test]
		public void TestTermFacet()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.From(0)
				.Size(10)
				.Fields(f => f.Id, f => f.Name)
				.SortAscending(f => f.LOC)
				.SortDescending(f => f.Name)
				.MatchAll()
				.FacetTerm(t => t.OnField(f => f.Country).Size(20))
			);
			Assert.Greater(results.Facet<TermFacet>(f => f.Country).Items.Count(), 0);
			Assert.Greater(results.FacetItems<TermItem>(f => f.Country).Count(), 0);
		}

		[Test]
		public void TestCommonTerms()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Query(q => q
					.CommonTerms(t => t
						.OnField(p => p.Name)
						.Query("elasticsearch")
					)
				)
			);

			Assert.True(results.IsValid);
			Assert.Greater(results.Hits.Count(), 0);
		}

		[Test]
		public void TermSuggest()
		{
			var country = this.Client.Search<ElasticsearchProject>(s => s.Size(1)).Documents.First().Country;
			var wrongCountry = country + "x";

			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Query(q => q.MatchAll())
				.SuggestTerm("mySuggest", m => m.SuggestMode(SuggestMode.Always).Text(wrongCountry).Size(1).OnField("country"))
			);

			Assert.NotNull(results);
			Assert.True(results.IsValid);

			Assert.NotNull(results.Suggest);
			Assert.NotNull(results.Suggest.Values);

			Assert.AreEqual(results.Suggest.Values.Count, 1);
			Assert.AreEqual(results.Suggest.Values.First().Count(), 1);

			Assert.NotNull(results.Suggest.Values.First().First().Options);
			Assert.GreaterOrEqual(results.Suggest.Values.First().First().Options.Count(), 1);

			Assert.AreEqual(results.Suggest.Values.First().First().Options.First().Text, country);

			Assert.AreEqual(results.Suggest["mySuggest"].First().Options.First().Text, country);
		}

		[Test]
		public void PhraseSuggest()
		{
			var text = this.Client.Search<ElasticsearchProject>(s => s.Size(1)).Documents.First().Content.Split(' ');
			var phrase = string.Join(" ", text.Take(2)) + "x";
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Query(q => q.MatchAll())
				.SuggestPhrase("myPhraseSuggest", m => m.Text(phrase).Size(1).OnField("content"))
			);

			Assert.NotNull(results);
			Assert.True(results.IsValid);

			Assert.NotNull(results.Suggest);
			Assert.NotNull(results.Suggest.Values);

			Assert.AreEqual(results.Suggest.Values.Count, 1);
			Assert.AreEqual(results.Suggest.Values.First().Count(), 1);

			Assert.NotNull(results.Suggest.Values.First().First().Options);
			Assert.GreaterOrEqual(results.Suggest.Values.First().First().Options.Count(), 1);

		}

	}
}
