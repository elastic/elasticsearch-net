using System.Linq;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using Nest.Tests.MockData;

namespace Nest.Tests.Integration.Search
{
	[TestFixture]
	public class QueryDSLTests : IntegrationTests
	{
		[Test]
		public void MatchAll()
		{
			var results = this._client.Search<ElasticSearchProject>(s => s
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
			Assert.NotNull(results.Documents);
			Assert.GreaterOrEqual(results.Documents.Count(), 10);
		}
		[Test]
		public void MatchAllShortcut()
		{
			var results = this._client.Search<ElasticSearchProject>(s => s
				.From(0)
				.Size(10)
		.Fields(f => f.Id, f => f.Country)
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
			var results = this._client.Search<ElasticSearchProject>(s => s
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
			Assert.NotNull(results.Documents);
			Assert.GreaterOrEqual(results.Documents.Count(), 1);
		}
		[Test]
		public void TestWildcardQuery()
		{
			var results = this._client.Search<ElasticSearchProject>(s => s
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
			Assert.NotNull(results.Documents);
			Assert.GreaterOrEqual(results.Documents.Count(), 1);
		}
		[Test]
		public void TestWildcardQueryBoostAndRewrite()
		{
			var results = this._client.Search<ElasticSearchProject>(s => s
				.From(0)
				.Size(10)
				.Fields(f => f.Id, f => f.Name)
				.SortAscending(f => f.LOC)
				.SortDescending(f => f.Name)
				.Query(q => q
					.Wildcard(f => f.Name, "elasticsearch.*", Boost: 1.0, Rewrite: RewriteMultiTerm.scoring_boolean)
				)
			);
			Assert.NotNull(results);
			Assert.True(results.IsValid);
			Assert.NotNull(results.Documents);
			Assert.GreaterOrEqual(results.Documents.Count(), 1);
		}
		[Test]
		public void TestPrefixQuery()
		{
			var results = this._client.Search<ElasticSearchProject>(s => s
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
			Assert.NotNull(results.Documents);
			Assert.GreaterOrEqual(results.Documents.Count(), 1);
		}
		[Test]
		public void TestTermFacet()
		{
			var results = this._client.Search<ElasticSearchProject>(s => s
				.From(0)
				.Size(10)
				.Fields(f => f.Id, f => f.Name)
				.SortAscending(f => f.LOC)
				.SortDescending(f => f.Name)
				.MatchAll()
				.FacetTerm(t => t.OnField(f=>f.Country).Size(20))
			);
			Assert.Greater(results.Facet<TermFacet>(f => f.Country).Items.Count(), 0);
			Assert.Greater(results.FacetItems<TermItem>(f=>f.Country).Count(), 0);
		}
        [Test]
        public void TestPartialFields()
        {
            var results = this._client.Search<ElasticSearchProject>(s =>
                s.From(0)
                .Size(10)
                .PartialFields(pf => pf.PartialField("partial1").Include("country", "origin.lon"), pf => pf.PartialField("partial2").Exclude("country"))
                .MatchAll());
            Assert.True(results.Hits.Hits.All(h => h.PartialFields["partial2"].Country == null && h.PartialFields["partial2"].Origin != null));
            // this test depends on fact, that no origin has longitude part equal to 0, lat is ommited by elasticsearch in results, so presumably deserialized to 0.
            Assert.True(results.Hits.Hits.All(h => h.PartialFields["partial1"].Origin.lon != 0 && h.PartialFields["partial1"].Origin.lat == 0));
        }

        [Test]
        public void TermSuggest()
        {
            var results = this._client.Search<ElasticSearchProject>(s => s
                .Query(q => q.MatchAll())
                .TermSuggest("myTermSuggest", m => m.SuggestMode(SuggestMode.Always).Text("Sanskrti").Size(1).OnField("country"))
            );

            Assert.NotNull(results);
            Assert.True(results.IsValid);

            Assert.NotNull(results.Suggest);
            Assert.NotNull(results.Suggest.Values);

            Assert.AreEqual(results.Suggest.Values.Count, 1);
            Assert.AreEqual(results.Suggest.Values.First().Count(), 1);

            Assert.NotNull(results.Suggest.Values.First().First().Options);
            Assert.GreaterOrEqual(results.Suggest.Values.First().First().Options.Count(), 1);

            Assert.AreEqual(results.Suggest.Values.First().First().Options.First().Text, "Sanskrit");
        }

        [Test]
        public void PhraseSuggest()
        {
            var results = this._client.Search<ElasticSearchProject>(s => s
                .Query(q => q.MatchAll())
                .PhraseSuggest("myPhraseSuggest", m => m.Text("Nostrud frankufrter dseerunt ulalmco").Size(1).OnField("content"))
            );

            Assert.NotNull(results);
            Assert.True(results.IsValid);

            Assert.NotNull(results.Suggest);
            Assert.NotNull(results.Suggest.Values);

            Assert.AreEqual(results.Suggest.Values.Count, 1);
            Assert.AreEqual(results.Suggest.Values.First().Count(), 1);

            Assert.NotNull(results.Suggest.Values.First().First().Options);
            Assert.GreaterOrEqual(results.Suggest.Values.First().First().Options.Count(), 1);

            Assert.AreEqual(results.Suggest.Values.First().First().Options.First().Text, "nostrud frankfurter deserunt ulalmco");
        }

        [Test]
        public void TestCustomFiltersScore()
        {
            //Counting score with script and boost
	        var defaultProject = NestTestData.Data.First();
            var result = this._client.Search<ElasticSearchProject>(s => s
                .From(0)
                .Size(10)
                .Query(q => q
                    .CustomFiltersScore(cfs => cfs
                        .Filters(f => f
                            .Filter(fd => fd
                                .Bool(bqd => bqd
                                    .Should(x => x.Range(r => r.From(83).To(85).OnField(qfd => qfd.FloatValue)))))
                            .Script("myVal*2"), f => f
                            .Filter(fd => fd
                                .Query(fq => fq
                                    .QueryString(qs => qs
                                        .Query("true")
                                        .OnField(fs => fs.BoolValue)
                                        .Operator(Operator.and))))
                             .Boost(1.5F))
                        .Query(qd => qd
                            .QueryString(qs => qs
                            .Query(defaultProject.Country)
                                .OnField(fs => fs.Country)
                                .Operator(Operator.and)))
                        .ScoreMode(ScoreMode.total)
                        .Params(x => x.Add("myVal", 0.6F)))));

            //Counting score with boost only (1.2F equals myVal from script before)
            var result2 = this._client.Search<ElasticSearchProject>(s => s
                .From(0)
                .Size(10)
                .Query(q => q
                    .CustomFiltersScore(cfs => cfs
                        .Filters(f => f
                            .Filter(fd => fd
                                .Bool(bqd => bqd
                                    .Should(x => x.Range(r => r.From(83).To(85).OnField(qfd => qfd.FloatValue)))))
                            .Boost(1.2F), f => f
                            .Filter(fd => fd
                                .Query(fq => fq
                                    .QueryString(qs => qs
                                        .Query("true")
                                        .OnField(fs => fs.BoolValue)
                                        .Operator(Operator.and))))
                             .Boost(1.5F))
                        .Query(qd => qd
                            .QueryString(qs => qs
								.Query(defaultProject.Country)
                                .OnField(fs => fs.Country)
                                .Operator(Operator.and)))
                        .ScoreMode(ScoreMode.total))));
            Assert.AreEqual(result.Hits.Hits.First().Score, result2.Hits.Hits.First().Score);
        }
	}
}
