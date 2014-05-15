using System;
using System.Collections.Generic;
using System.Linq;
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
				f=>f.TermQueryDescriptor,
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
				)
			);
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
		}
		[Test]
		public void ConstantScore_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.ConstantScore,
				f=>f.ConstantScore(cs=>cs
					.Boost(2.0)	
					.Filter(ff=>Filter1)
					.Query(csq=>Query3)
				)
			);
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
		}
		[Test]
		public void CustomFiltersScore_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.CustomFiltersScore,
				f=>f.CustomFiltersScore(cfs=>cfs
					.Filters( ff=>ff
						.Filter(sf=>Filter1)
						.Boost(2.3)
						.Script("My complex script")
						.Params(p=>p.Add("param", "paramvalue"))
						.Lang("mvel")
					)	
				)
			);
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
		}
		[Test]
		public void DisMax_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.DisMax,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void Filtered_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.Filtered,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void FunctionScore_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.FunctionScore,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void Fuzzy_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.Fuzzy,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void FuzzyLikeThis_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.FuzzyLikeThis,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void GeoShape_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.GeoShape,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void HasChild_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.HasChild,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void HasParent_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.HasParent,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void Ids_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.Ids,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void Indices_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.Indices,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void Match_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.Match,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void MatchAll_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.MatchAll,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void MoreLikeThis_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.MoreLikeThis,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void MultiMatch_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.MultiMatch,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void Nested_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.Nested,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void Prefix_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.Prefix,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void QueryString_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.QueryString,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void Range_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.Range,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void Regexp_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.Regexp,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void SimpleQueryString_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.SimpleQueryString,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void SpanFirst_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.SpanFirst,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void SpanNear_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.SpanNear,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void SpanNot_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.SpanNot,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void SpanOr_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.SpanOr,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void SpanTerm_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.SpanTerm,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void Terms_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.Terms,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void TopChildren_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.TopChildren,
				f=>f.Term(p=>p.Name, "hello world")
			);
		}
		[Test]
		public void WildCard_Deserializes()
		{
			var q = this.TestBaseQueryProperties(
				f=>f.Wildcard,
				f=>f.Term(p=>p.Name, "hello world")
			);
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
