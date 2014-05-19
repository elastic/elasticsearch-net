using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class ParseQueryTests : BaseParserTests
	{
		private BaseQuery Query1 = Query<object>.Term("w", "x");
		private BaseQuery Query2 = Query<object>.Term("y", "z");
		private BaseQuery Query3 = Query<object>.Term("a", "b");
		
		private BaseFilterDescriptor Filter1 = Filter<object>.Term("w", "x");
		private BaseFilterDescriptor Filter2 = Filter<object>.Term("y", "z");
		private BaseFilterDescriptor Filter3 = Filter<object>.Term("a", "b");

		[Test]
		public void Term_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.Term,
				f=>f.Term(p=>p.Name, "hello world")
			);
			q.Field.Should().Be("name");
			q.Value.Should().Be("hello world");
		}
		
		[Test]
		public void Bool_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.Bool,
				f=>f.Bool(b=>b
					.Must(Query1)
					.Should(Query2)
					.MustNot(Query3)
					.Boost(1.1)
					.DisableCoord()
					.MinimumNumberShouldMatch(2)
				)
			);
			q.Boost.Should().Be(1.1);
			q.DisableCoord.Should().BeTrue();
			q.MinimumNumberShouldMatches.Should().Be("2");
			q.Must.Should().NotBeEmpty().And.HaveCount(1);
			q.Should.Should().NotBeEmpty().And.HaveCount(1);
			q.MustNot.Should().NotBeEmpty().And.HaveCount(1);

			AssertIsTermQuery(q.Must.First(), Query1);
			AssertIsTermQuery(q.Should.First(), Query2);
			AssertIsTermQuery(q.MustNot.First(), Query3);
		}

		[Test]
		public void Boosting_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.Boosting,
				f=>f.Boosting(bq => bq
					.Positive(pq=>Query1)
					.Negative(nq=>Query2)
					.NegativeBoost(1.1)
				)
			);
			q.NegativeBoost.Should().Be(1.1);

			AssertIsTermQuery(q.NegativeQuery, Query2);
			AssertIsTermQuery(q.PositiveQuery, Query1);
		}

		[Test]
		public void CommonTerms_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.CommonTerms,
				f=>f.CommonTerms(ct=>ct
					.Analyzer("my_analyzer")
					.Boost(1.1)
					.CutOffFrequency(2.0)
					.DisableCoord()
					.HighFrequencyOperator(Operator.or)
					.LowFrequencyOperator(Operator.and)
					.MinumumShouldMatch(2)
					.OnField(p=>p.Name)
					.Query("query")
				)
			);
			q.Analyzer.Should().Be("my_analyzer");
			q.Boost.Should().Be(1.1);
			q.CutoffFrequency.Should().Be(2.0);
			q.DisableCoord.Should().BeTrue();
			q.HighFrequencyOperator.Should().Be(Operator.or);
			q.LowFrequencyOperator.Should().Be(Operator.and);
			q.MinimumShouldMatch.Should().Be(2);
			q.Field.Should().Be("name");
			q.Query.Should().Be("query");
		}

		[Test]
		public void ConstantScore_Query_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.ConstantScore,
				f=>f.ConstantScore(cs=>cs
					.Boost(2.0)	
					.Query(csq=>Query3)
				)
			);
			q.Boost.Should().Be(2.0);
			AssertIsTermQuery(q.Query, Query3);
		}
		[Test]
		public void ConstantScore_Filter_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.ConstantScore,
				f=>f.ConstantScore(cs=>cs
					.Boost(2.0)	
					.Filter(ff=>Filter1)
				)
			);
			q.Boost.Should().Be(2.0);
			AssertIsTermFilter(q.Filter, Filter1);
		}
		[Test]
		public void CustomBoostFactor_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.CustomBoostFactor,
				f=>f.CustomBoostFactor(cbf=>cbf
					.BoostFactor(2.1)
					.Query(cbfq=>Query2)
				)
			);
			q.BoostFactor.Should().Be(2.1);
			AssertIsTermQuery(q.Query, Query2);
		}

		[Test]
		public void CustomFiltersScore_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.CustomFiltersScore,
				f=>f.CustomFiltersScore(cfs=>cfs
					.Language("as")
					.MaxBoost("maxboost")
					.Query(qq=>Query1)
					.ScoreMode(ScoreMode.avg)
					.Filters(ff=>ff
						.Filter(sf=>Filter1)
						.Boost(2.3)
						.Script("My complex script")
						.Params(p=>p.Add("param", "paramvalue"))
						.Lang("mvel")
					)	
				)
			);
			q.Lang.Should().Be("as");
			q.MaxBoost.Should().Be("maxboost");
			q.ScoreMode.Should().Be(ScoreMode.avg);
			
			AssertIsTermQuery(q.Query, Query1);

			q.Filters.Should().NotBeEmpty().And.HaveCount(1);
			var fsf = q.Filters.First();
			fsf.Boost.Should().Be(2.3);
			fsf.Script.Should().Be("My complex script");
			fsf.Lang.Should().Be("mvel");
			fsf.Params.Should().NotBeEmpty().And.HaveCount(1);
			var param = fsf.Params.First();
			param.Key.Should().Be("param");
			param.Value.Should().Be("paramvalue");

			AssertIsTermFilter(fsf.Filter, Filter1);
		}

		[Test]
		public void CustomScore_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.CustomScore,
				f=>f.CustomScore(cs=>cs
					.Query(qq=>Query1)
					.Script("My complex script")
					.Lang("mvel")
					.Params(p=>p.Add("param", "paramvalue"))
				)
			);
			q.Script.Should().Be("My complex script");
			q.Lang.Should().Be("mvel");
			q.Params.Should().NotBeEmpty().And.HaveCount(1);
			var param = q.Params.First();
			param.Key.Should().Be("param");
			param.Value.Should().Be("paramvalue");

			AssertIsTermQuery(q.Query, Query1);
		}

		[Test]
		public void DisMax_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.DisMax,
				f=>f.Dismax(d=>d
					.Boost(1.5)
					.Queries(qq=>Query1, qq=>Query2)
					.TieBreaker(1.1)
				)
			);
			q.Boost.Should().Be(1.5);
			q.TieBreaker.Should().Be(1.1);
			q.Queries.Should().NotBeEmpty().And.HaveCount(2);
			AssertIsTermQuery(q.Queries.First(), Query1);
			AssertIsTermQuery(q.Queries.Last(), Query2);
		}
		
		[Test]
		public void Filtered_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.Filtered,
				f=>f.Filtered(fq=>fq
					.Filter(ff=>Filter1)
					.Query(qq=>Query1)
				)
			);
			AssertIsTermFilter(q.Filter, Filter1);
			AssertIsTermQuery(q.Query, Query1);
		}

		[Test]
		public void FunctionScore_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.FunctionScore,
				f=>f.FunctionScore(fq=>fq
					.BoostMode(FunctionBoostMode.avg)
					.Functions(
						ff => ff.Gauss(x => x.StartedOn, d => d.Scale("42w")),
						ff => ff.Linear(x => x.FloatValue, d => d.Scale("0.3")),
						ff => ff.Exp(x => x.DoubleValue, d => d.Scale("0.5")),
						ff => ff.BoostFactor(2).Filter(bff=>Filter1)
					)
					.Query(qq=>Query1)
					.RandomScore(1337)
					.ScoreMode(FunctionScoreMode.first)
					.ScriptScore(s=>s
						.Script("My complex script")
						.Params(p=>p.Add("param", "paramvalue"))
						.Lang("mvel")
					)
				)
			);

			q.BoostMode.Should().Be(FunctionBoostMode.avg);
			q.RandomScore.Should().Be(1337);
			q.ScoreMode.Should().Be(FunctionScoreMode.first);
			q.ScriptScore.Should().NotBeNull();
			q.ScriptScore.Lang.Should().Be("mvel");
			q.ScriptScore.Script.Should().Be("My complex script");
			var param = q.ScriptScore.Params.FirstOrDefault();
			param.Should().NotBeNull();
			param.Key.Should().Be("param");
			param.Value.Should().Be("paramvalue");
			q.Functions.Should().NotBeEmpty().And.HaveCount(4);

			//TODO rip out state from all these function descriptors
			var functions = q.Functions.ToList();

		}

		[Test]
		public void Fuzzy_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.Fuzzy,
				f=>f.Fuzzy(fq=>fq
					.Boost(2.2)
					.Fuzziness(2.1)
					.MaxExpansions(4)
					.OnField(p=>p.Name)
					.PrefixLength(3)
					.Rewrite(RewriteMultiTerm.constant_score_filter)
					.Value("findme")
				)
			);

			q.Boost.Should().Be(2.2);
			q.Fuzziness.Should().Be("2.1");
			q.MaxExpansions.Should().Be(4);
			q.Field.Should().Be("name");
			q.Rewrite.Should().Be(RewriteMultiTerm.constant_score_filter);

			var stringFuzzy = (IStringFuzzyQuery) q;
			stringFuzzy.PrefixLength.Should().Be(3);
			stringFuzzy.Value.Should().Be("findme");
		}

		[Test]
		public void FuzzyNumeric_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.Fuzzy,
				f=>f.FuzzyNumeric(fq=>fq
					.Boost(2.2)
					.Fuzziness("AUTO")
					.MaxExpansions(4)
					.Transpositions()
					.OnField(p=>p.Name)
					.Value(2)
				)
			);

			q.Boost.Should().Be(2.2);
			q.Fuzziness.Should().Be("AUTO");
			q.MaxExpansions.Should().Be(4);
			q.Field.Should().Be("name");
			q.Transpositions.Should().BeTrue();

			var stringFuzzy = (IFuzzyNumericQuery) q;
			stringFuzzy.Value.Should().Be(2);
		}

		[Test]
		public void FuzzyDate_Deserializes()
		{
			var date = DateTime.UtcNow;
			var q = this.TestBaseQueryProperties(
				f=>f.Fuzzy,
				f=>f.FuzzyDate(fq=>fq
					.Boost(2.2)
					.Fuzziness(2.1)
					.MaxExpansions(4)
					.OnField(p=>p.Name)
					.Value(date)
					.Fuzziness("1d")
					.UnicodeAware()
				)
			);

			q.Boost.Should().Be(2.2);
			q.Fuzziness.Should().Be("1d");
			q.MaxExpansions.Should().Be(4);
			q.Field.Should().Be("name");
			q.UnicodeAware.Should().BeTrue();

			var stringFuzzy = (IFuzzyDateQuery) q;
			stringFuzzy.Value.Should().Be(date);
		}

		[Test]
		public void FuzzyLikeThis_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.FuzzyLikeThis,
				f=>f.FuzzyLikeThis(flq=>flq
					.Analyzer("my-analyzer")	
					.Boost(30.1)
					.IgnoreTermFrequency()
					.LikeText("likeme")
					.MaxQueryTerms(2)
					.MinimumSimilarity(0.3)
					.OnFields(p=>p.Name, p=>p.Origin)
					.PrefixLength(2)
				)
			);

			q.Analyzer.Should().Be("my-analyzer");
			q.Boost.Should().Be(30.1);
			q.IgnoreTermFrequency.Should().BeTrue();
			q.LikeText.Should().Be("likeme");
			q.MaxQueryTerms.Should().Be(2);
			q.MinSimilarity.Should().Be(0.3);
			q.Fields.Should().BeEquivalentTo(new [] { "name", "origin"});
			q.PrefixLength.Should().Be(2);
		}

		[Test]
		public void GeoShape_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.GeoShape,
				f=>f.GeoShape(gq=>gq
					.OnField(p=>p.MyGeoShape)
					.Coordinates(new [] { new [] {13.0, 53.0}, new [] { 14.0, 52.0} })
					.Type("enveloppe")
				)
			);

			q.Field.Should().Be("myGeoShape");
			q.Shape.Should().NotBeNull();
			q.Shape.Type.Should().Be("enveloppe");
			q.Shape.Coordinates.SelectMany(c=>c).Should()
				.BeEquivalentTo(new [] {13.0, 53.0, 14.0, 52.0 });
		}

		[Test]
		public void HasChild_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.HasChild,
				f=>f.HasChild<Person>(hq=>hq
					.Query(qq=>Query2)
					.Scope("my_scope")
					.Score(ChildScoreType.avg)
				)
			);
			q.Type.Should().Be("person");
			q.Scope.Should().Be("my_scope");
			q.ScoreType.Should().Be(ChildScoreType.avg);
			AssertIsTermQuery(q.Query, Query2);
		}

		[Test]
		public void HasParent_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.HasParent,
				f=>f.HasParent<Person>(hp=>hp
					.Query(qq=>Query3)
					.Scope("my_scope")
					.Score(ParentScoreType.score)
				)
			);

			q.Type.Should().Be("person");
			q.Scope.Should().Be("my_scope");
			q.ScoreType.Should().Be(ParentScoreType.score);
			AssertIsTermQuery(q.Query, Query3);
		}

		[Test]
		public void Ids_Deserializes()
		{
			var types = new [] {"type1, type2"};
			var values = new []{ "value" };
			var q = this.TestBaseQueryProperties(
				f=>f.Ids,
				f=>f.Ids(types, values)
			);

			q.Type.Should().BeEquivalentTo(types);
			q.Values.ShouldAllBeEquivalentTo(values);
		}

		[Test]
		public void Indices_Deserializes()
		{
			var indices = new [] {"index1", "index2"};
			var q = this.TestBaseQueryProperties(
				f=>f.Indices,
				f=>f.Indices(i=>i
					.Indices(indices)
					.NoMatchQuery(qq=>Query1)
					.Query(qq=>Query2)
				)
			);
			q.Indices.ShouldBeEquivalentTo(indices);
			AssertIsTermQuery(q.NoMatchQuery, Query1);
			AssertIsTermQuery(q.Query, Query2);
		}

		[Test]
		public void Match_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.Match,
				f=>f.Match(m=>m
					.Analyzer("my-analyzer")
					.Boost(2.1)
					.CutoffFrequency(1.31)
					.Fuzziness(2.3)
					.Lenient()
					.MaxExpansions(2)
					.OnField(p=>p.Name)
					.Operator(Operator.and)
					.PrefixLength(2)
					.Query("querytext")
					.Rewrite(RewriteMultiTerm.constant_score_boolean)
					.Slop(2)
				)
			);

			q.Type.Should().Be(null);
			q.Analyzer.Should().Be("my-analyzer");
			q.Boost.Should().Be(2.1);
			q.CutoffFrequency.Should().Be(1.31);
			q.Fuzziness.Should().Be(2.3);
			q.Lenient.Should().BeTrue();
			q.MaxExpansions.Should().Be(2);
			q.Field.Should().Be("name");
			q.Operator.Should().Be(Operator.and);
			q.PrefixLength.Should().Be(2);
			q.Query.Should().Be("querytext");
			q.Rewrite.Should().Be(RewriteMultiTerm.constant_score_boolean);
			q.Slop.Should().Be(2);
		}

		[Test]
		public void MatchPhrasePhrefix_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.Match,
				f=>f.MatchPhrasePrefix(m=>m
					.Analyzer("my-analyzer")
					.Boost(2.1)
					.CutoffFrequency(1.31)
					.Fuzziness(2.3)
					.Lenient()
					.MaxExpansions(2)
					.OnField(p=>p.Name)
					.Operator(Operator.and)
					.PrefixLength(2)
					.Query("querytext")
					.Rewrite(RewriteMultiTerm.constant_score_boolean)
					.Slop(2)
				)
			);
			q.Type.Should().Be("phrase_prefix");
			q.Analyzer.Should().Be("my-analyzer");
			q.Boost.Should().Be(2.1);
			q.CutoffFrequency.Should().Be(1.31);
			q.Fuzziness.Should().Be(2.3);
			q.Lenient.Should().BeTrue();
			q.MaxExpansions.Should().Be(2);
			q.Field.Should().Be("name");
			q.Operator.Should().Be(Operator.and);
			q.PrefixLength.Should().Be(2);
			q.Query.Should().Be("querytext");
			q.Rewrite.Should().Be(RewriteMultiTerm.constant_score_boolean);
			q.Slop.Should().Be(2);
		}

		[Test]
		public void MatchPhrase_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.Match,
				f=>f.MatchPhrase(m=>m
					.Analyzer("my-analyzer")
					.Boost(2.1)
					.CutoffFrequency(1.31)
					.Fuzziness(2.3)
					.Lenient()
					.MaxExpansions(2)
					.OnField(p=>p.Name)
					.Operator(Operator.and)
					.PrefixLength(2)
					.Query("querytext")
					.Rewrite(RewriteMultiTerm.constant_score_boolean)
					.Slop(2)
				)
			);
			q.Type.Should().Be("phrase");
			q.Analyzer.Should().Be("my-analyzer");
			q.Boost.Should().Be(2.1);
			q.CutoffFrequency.Should().Be(1.31);
			q.Fuzziness.Should().Be(2.3);
			q.Lenient.Should().BeTrue();
			q.MaxExpansions.Should().Be(2);
			q.Field.Should().Be("field");
			q.Operator.Should().Be(Operator.and);
			q.PrefixLength.Should().Be(2);
			q.Query.Should().Be("querytext");
			q.Rewrite.Should().Be(RewriteMultiTerm.constant_score_boolean);
			q.Slop.Should().Be(2);
		}

		[Test]
		public void MatchAll_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.MatchAll,
				f=>f.MatchAll(1.0, "normField")
			);
			q.Boost.Should().Be(1.0);
			q.NormField.Should().Be("normField");
		}
		[Test]
		public void MoreLikeThis_Deserializes()
		{
			var stopWords = new [] {"no", "stopwords"};
			var q = this.TestBaseQueryProperties(
				f=>f.MoreLikeThis,
				f=>f.MoreLikeThis(mlt=>mlt
					.Analyzer("my-analyzer")
					.Boost(2.1)
					.BoostTerms(1.2)
					.LikeText("likeme")
					.MaxDocumentFrequency(2)
					.MaxQueryTerms(3)
					.MaxWordLength(10)
					.MinDocumentFrequency(2)
					.MinTermFrequency(2)
					.MinWordLength(2)
					.OnFields(p=>p.Name, p=>p.MyGeoShape)
					.StopWords(stopWords)
					.TermMatchPercentage(0.9)
				)
			);
			q.Analyzer.Should().Be("my-analyzer");
			q.Boost.Should().Be(2.1);
			q.BoostTerms.Should().Be(1.2);
			q.LikeText.Should().Be("likeme");
			q.MaxDocumentFrequency.Should().Be(2);
			q.MaxQueryTerms.Should().Be(3);
			q.MaxWordLength.Should().Be(10);
			q.MinDocumentFrequency.Should().Be(2);
			q.MinTermFrequency.Should().Be(2);
			q.MinWordLength.Should().Be(2);
			q.Fields.Should().BeEquivalentTo(new [] { "name", "myGeoShape"});
			q.StopWords.Should().BeEquivalentTo(stopWords);
			q.TermMatchPercentage.Should().Be(0.9);
		}
		[Test]
		public void MultiMatch_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.MultiMatch,
				f=>f.MultiMatch(mq=>mq
					.Analyzer("my-analyzer")
					.Boost(2.5)
					.CutoffFrequency(0.8)
					.Fuzziness(0.8)
					.MaxExpansions(2)
					.OnFields(p=>p.Name,p=>p.MyGeoShape)
					.Operator(Operator.or)
					.PrefixLength(2)
					.Query("querytext")
					.Rewrite(RewriteMultiTerm.top_terms_N)
					.Slop(2)
					.TieBreaker(2.0)
					.Type(TextQueryType.BEST_FIELDS)
				)
			);
			q.Analyzer.Should().Be("my-analyzer");
			q.Boost.Should().Be(2.5);
			q.CutoffFrequency.Should().Be(0.8);
			q.Fuzziness.Should().Be(0.8);
			q.MaxExpansions.Should().Be(2);
			q.Fields.Should().BeEquivalentTo(new [] { "name", "myGeoShape"});
			q.Operator.Should().Be(Operator.or);
			q.PrefixLength.Should().Be(2);
			q.Query.Should().Be("querytext");
			q.Rewrite.Should().Be(RewriteMultiTerm.top_terms_N);
			q.Slop.Should().Be(2);
			q.TieBreaker.Should().Be(2.0);
			q.Type.Should().Be(TextQueryType.BEST_FIELDS);
		}
		[Test]
		public void Nested_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.Nested,
				f=>f.Nested(nq=>nq
					.Path(p=>p.NestedFollowers)
					.Query(qq=>Query1)
					.Scope("scopey")
					.Score(NestedScore.max)
				)
			);
			q.Score.Should().Be(NestedScore.max);
			q.Scope.Should().Be("scopey");
			q.Path.Should().Be("nestedFollowers");
			AssertIsTermQuery(q.Query, Query1);
		}

		[Test]
		public void Prefix_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.Prefix,
				f=>f.Prefix(pq=>pq
					.Boost(2.1)
					.OnField(p=>p.Name)
					.Rewrite(RewriteMultiTerm.constant_score_boolean)
					.Value("prefix*")
				)
			);
			q.Boost.Should().Be(2.1);
			q.Field.Should().Be("name");
			q.Rewrite.Should().Be(RewriteMultiTerm.constant_score_boolean);
			q.Value.Should().Be("prefix*");
		}
		[Test]
		public void QueryString_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.QueryString,
				f=>f.QueryString(qs=>qs
					.AllowLeadingWildcard()
					.AnalyzeWildcard()
					.Analyzer("my-analyzer")
					.AutoGeneratePhraseQueries()
					.Boost(1.1)
					.DefaultOperator(Operator.and)
					.EnablePositionIncrements()
					.FuzzyMinimumSimilarity(2.1)
					.FuzzyPrefixLength(2)
					.Lenient()
					.LowercaseExpendedTerms()
					.MinimumShouldMatchPercentage(2)
					.DefaultField(p=>p.Name)
					.OnFields(p=>p.Name, p=>p.Origin)
					.PhraseSlop(2.1)
					.Query("q")
					.Rewrite(RewriteMultiTerm.constant_score_default)
					.TieBreaker(4.1)
					.UseDisMax()
				)
			);
			q.AllowLeadingWildcard.Should().BeTrue();
			q.AnalyzeWildcard.Should().BeTrue();
			q.Analyzer.Should().Be("my-analyzer");
			q.AutoGeneratePhraseQueries.Should().BeTrue();
			q.Boost.Should().Be(1.1);
			q.DefaultOperator.Should().Be(Operator.and);
			q.EnablePositionIncrements.Should().BeTrue();
			q.FuzzyMinimumSimilarity.Should().Be(2.1);
			q.FuzzyPrefixLength.Should().Be(2);
			q.Lenient.Should().BeTrue();
			q.LowercaseExpendedTerms.Should().BeTrue();
			q.MinimumShouldMatchPercentage.Should().Be("2%");
			q.DefaultField.Should().Be("name");
			q.Fields.Should().BeEquivalentTo(new []{"name", "origin"});
			q.PhraseSlop.Should().Be(2.1);
			q.Query.Should().Be("q");
			q.Rewrite.Should().Be(RewriteMultiTerm.constant_score_default);
			q.TieBreaker.Should().Be(4.1);
			q.UseDismax.Should().BeTrue();
		}

		[Test]
		public void RangeEquals_Deserializes()
		{
			//todo simplify range query like the range filter to only support gt gte lt lte
			var q = this.TestBaseQueryProperties(
				f=>f.Range,
				f=>f.Range(r=>r
					.OnField(p=>p.Name)
					.Boost(2.1)
					.GreaterOrEquals(2)
					.LowerOrEquals(10)
				)
			);
			q.Field.Should().Be("name");
			q.Boost.Should().Be(2.1);
			q.GreaterThanOrEqualTo.Should().Be("2");
			q.LowerThanOrEqualTo.Should().Be("10");
		}

		[Test]
		public void Range_Deserializes()
		{
			var from = DateTime.UtcNow.Date;
			var to = from.AddDays(2);

			//todo simplify range query like the range filter to only support gt gte lt lte
			var q = this.TestBaseQueryProperties(
				f=>f.Range,
				f=>f.Range(r=>r
					.OnField(p=>p.Name)
					.Boost(2.1)
					.Greater(from, "yyyy-MM-dd")
					.Lower(to)
				)
			);
			q.Field.Should().Be("name");
			q.Boost.Should().Be(2.1);
			q.GreaterThan.Should().Be("2014-05-19");
			q.LowerThan.Should().Be("2014-05-21T00:00:00.000");
		}
		[Test]
		public void Regexp_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.Regexp,
				f=>f.Regexp(r=>r
					.Boost(1.0)
					.Flags("FLAGS")
					.OnField(p=>p.Name)
					.Value("asdasd")
				)
			);

			q.Boost.Should().Be(1.0);
			q.Flags.Should().Be("FLAGS");
			q.Field.Should().Be("name");
			q.Value.Should().Be("asdasd");
		}
		[Test]
		public void SimpleQueryString_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.SimpleQueryString,
				f=>f.SimpleQueryString(sq=>sq
					.Analyzer("my-analyzer")
					.DefaultOperator(Operator.and)
					.Flags("ASFAS")
					.Locale("en")
					.LowercaseExpendedTerms()
					.DefaultField(p=>p.Name)
					.OnFields(p=>p.Name, p=>p.Origin)
					.Query("some query")
				)
			);
			q.Analyzer.Should().Be("my-analyzer");
			q.DefaultOperator.Should().Be(Operator.and);
			q.Flags.Should().Be("ASFAS");
			q.Locale.Should().Be("en");
			q.LowercaseExpendedTerms.Should().BeTrue();
			q.DefaultField.Should().Be("name");
			q.Fields.Should().BeEquivalentTo(new []{ "name", "origin"});
			q.Query.Should().Be("some query");

		}
		[Test]
		public void SpanFirst_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.SpanFirst,
				f=>f.SpanFirst(sf=>sf
					.MatchTerm(p => p.Name, "elasticsearch.pm", 1.1)
					.End(3)
				)
			);
			q.End.Should().Be(3);
		}
		[Test]
		public void SpanNear_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.SpanNear,
				f=>f.SpanNear(sq=>sq
					.Clauses(
						c => c.SpanTerm(p => p.Name, "elasticsearch.pm", 1.1),
						c => c.SpanFirst(sf => sf
							.MatchTerm(p => p.Name, "elasticsearch.pm", 1.1)
							.End(3)
						)
					)
					.Slop(3)
					.CollectPayloads(false)
					.InOrder(false)	
				)
			);
		}
		[Test]
		public void SpanNot_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.SpanNot,
				f=>f.SpanNot(sn=>sn
					.Include(e =>e.SpanTerm(p => p.Name, "elasticsearch.pm", 1.1))
					.Exclude(e=>e.SpanTerm(p => p.Name, "elasticsearch.pm", 1.1))
				)
			);
		}
		[Test]
		public void SpanOr_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.SpanOr,
				f=>f.SpanOr(sq=>sq
					.Clauses(
						c => c.SpanTerm(p => p.Name, "elasticsearch.pm", 1.1),
						c => c.SpanFirst(sf => sf
							.MatchTerm(p => p.Name, "elasticsearch.pm", 1.1)
							.End(3)
						)
					)
				)
			);
		}
		[Test]
		public void SpanTerm_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.SpanTerm,
				f=>f.SpanTerm(sq=>sq
					.Boost(2.3)
					.OnField(p=>p.Name)
					.Value("query")
				)
			);
		}
		[Test]
		public void Terms_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.Terms,
				f=>f.Terms(p=>p.Name, new [] {"term1", "term2" })
			);
			q.Terms.Should().NotBeEmpty().And.HaveCount(2);
			q.Field.Should().Be("name");
		}
		
		[Test]
		public void TermsDescriptor_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.Terms,
				f=>f.TermsDescriptor(td=>td
					.OnField(p=>p.Name)
					.Boost(1.2)
					.MinimumShouldMatch(2)	
					.DisableCoord()
					.OnExternalField<ElasticsearchProject>(ef=>ef
						.Id(1)
						.Path(p=>p.Id)
						.Index("index")
						.Type("type")
					)
				)
			);
			q.Boost.Should().Be(1.2);
			q.Field.Should().Be("name");
			q.DisableCoord.Should().BeTrue();
			q.MinimumShouldMatch.Should().Be(2);
			q.ExternalField.Should().NotBeNull();
			q.ExternalField._Id.Should().Be("1");
			q.ExternalField._Index.Should().Be("index");
			q.ExternalField._Type.Should().Be("type");
			q.ExternalField._Path.Should().Be("id");
			
		}
		[Test]
		public void WildCard_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.Wildcard,
				f=>f.Wildcard(wq=>wq
					.Boost(1.1)
					.Rewrite(RewriteMultiTerm.constant_score_boolean)
					.OnField(p=>p.Name)
					.Value("wild*")
				)
			);
			q.Boost.Should().Be(1.1);
			q.Field.Should().Be("name");
			q.Value.Should().Be("wild*");
			q.Rewrite.Should().Be(RewriteMultiTerm.constant_score_boolean);
		}
		private T TestBaseQueryProperties<T>(
			Func<IQueryDescriptor, T> queryBaseDescriptor,
			Func<QueryDescriptor<ElasticsearchProject>, BaseQuery> create
			)
			where T : IQuery
		{
			var descriptor = this.GetSearchDescriptorForQuery(s=>s
				.Query(create)
			);
			var query = queryBaseDescriptor(descriptor.Query);
			query.Should().NotBeNull();
			return query;
		}
	}
}
