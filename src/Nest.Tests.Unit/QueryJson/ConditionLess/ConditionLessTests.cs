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
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.QueryJson.ConditionLess
{
	[TestFixture]
	public class ConditionLessTests : BaseJsonTests
	{
		public class Criteria
		{
			public string Name1 { get; set; }
			public string Name2 { get; set; }
			public int? Int1 { get; set; }
			public DateTime? Date { get; set; }
		}
		private readonly Criteria _c = new Criteria();

		private void DoConditionlessQuery(Func<QueryDescriptor<ElasticSearchProject>, BaseQuery> query)
		{
			var criteria = new Criteria { };
			var s = new SearchDescriptor<ElasticSearchProject>()
				//.Strict()
				.From(0)
				.Take(10)
				.Query(query);

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod(), "MatchAll");
		}

		[Test]
		public void QueryString()
		{
			this.DoConditionlessQuery(q => q.QueryString(qs => qs.Query(_c.Name1)));
		}

		[Test]
		public void Terms()
		{
			this.DoConditionlessQuery(q => q.Terms(p => p.Name, new string[] { _c.Name1 }));
			this.DoConditionlessQuery(q => q.Terms(p => p.Name, _c.Name1, _c.Name2));
		}


		[Test]
		public void TermsDescriptor()
		{
			this.DoConditionlessQuery(q => q.TermsDescriptor(qs => qs.OnField(p => p.Name).Terms(_c.Name1)));
		}

		[Test]
		public void Fuzzy()
		{
			this.DoConditionlessQuery(q => q.Fuzzy(fq => fq.OnField(p => p.Name).Value(_c.Name1)));
		}

		[Test]
		public void FuzzyNumeric()
		{
			this.DoConditionlessQuery(q => q.FuzzyNumeric(fnq => fnq.OnField(p => p.LOC).Value(_c.Int1)));
		}

		[Test]
		public void FuzzyDate()
		{
			this.DoConditionlessQuery(q => q.FuzzyDate(fdq => fdq.OnField(p => p.StartedOn).Value(_c.Date)));
		}

		[Test]
		public void Text()
		{
			this.DoConditionlessQuery(q => q.Text(tq => tq.OnField(p => p.Name).QueryString(_c.Name1)));
		}

		[Test]
		public void TextPhrase()
		{
			this.DoConditionlessQuery(q => q.Text(tq => tq.OnField(p => p.Name).QueryString(_c.Name1)));
		}

		[Test]
		public void TextPhrasePrefix()
		{
			this.DoConditionlessQuery(q => q.Text(tq => tq.OnField(p => p.Name).QueryString(_c.Name1)));
		}

		[Test]
		public void Nested()
		{
			this.DoConditionlessQuery(q => q
			  .Nested(qn => qn
				.Path(p => p.Followers)
				.Query(nqq => nqq
				  .Text(tq => tq
					.OnField(p => p.Name)
					.QueryString(_c.Name1)
				  )
				)
			  )
			);
		}

		[Test]
		public void Indices()
		{
			this.DoConditionlessQuery(q => q
			  .Indices(iq => iq
				.Indices(new[] { "_all" })
				.Query(tq => tq.Terms(p => p.Name, _c.Name1))
			  )
			);
			this.DoConditionlessQuery(q => q
			  .Indices(iq => iq
				.Indices(new[] { "_all" })
				.Query(tq => tq.Terms(p => p.Name, _c.Name1))
				.NoMatchQuery(tq => tq.Terms(p => p.Name, _c.Name1))
			  )
			);
		}

		[Test]
		public void Range()
		{
			//From and to are allowed to be null only field is not not
			this.DoConditionlessQuery(q => q.Range(rq => rq.OnField(string.Empty).From(0).To(1)));
		}

		[Test]
		public void FuzzyLikeThis()
		{
			this.DoConditionlessQuery(q => q.FuzzyLikeThis(fq => fq.OnFields(p => p.Name).LikeText(_c.Name1)));
		}

		[Test]
		public void MoreLikeThis()
		{
			this.DoConditionlessQuery(q => q.MoreLikeThis(mlt => mlt.OnFields(p => p.Name).LikeText(_c.Name1)));
		}

		[Test]
		public void HasChild()
		{
			this.DoConditionlessQuery(q => q.HasChild<Person>(hcq => hcq.Query(qq => qq.Terms(p => p.FirstName, _c.Name1))));
		}

		[Test]
		public void TopChildren()
		{
			this.DoConditionlessQuery(q => q.TopChildren<Person>(hcq => hcq.Query(qq => qq.Terms(p => p.FirstName, _c.Name1))));
		}

		[Test]
		public void Filtered()
		{
			//TODO test .Filter
			this.DoConditionlessQuery(q => q.Filtered(fq => fq.Query(qff => qff.Terms(p => p.Name, _c.Name1))));
		}

		[Test]
		public void Dismax()
		{
			this.DoConditionlessQuery(q => q.Dismax(dq => dq.Queries(qff => qff.Terms(p => p.Name, _c.Name1))));
		}

		[Test]
		public void ConstantScore()
		{
			this.DoConditionlessQuery(q => q.ConstantScore(cq => cq.Query(qff => qff.Terms(p => p.Name, _c.Name1))));
		}

		[Test]
		public void CustomBoostFactor()
		{
			this.DoConditionlessQuery(q => q.CustomBoostFactor(cbfq => cbfq.Query(qff => qff.Terms(p => p.Name, _c.Name1))));
		}

		[Test]
		public void CustomScore()
		{
			this.DoConditionlessQuery(q => q.CustomScore(csq => csq.Query(qff => qff.Terms(p => p.Name, _c.Name1))));
		}

		[Test]
		public void BoolEmpty()
		{
			this.DoConditionlessQuery(q => q.Bool(b => { }));
		}
		[Test]
		public void BoolEmptyClauses()
		{
			this.DoConditionlessQuery(q => q.Bool(b => b
				.Must()
				.MustNot()
				.Should()
			));
		}
		[Test]
		public void BoolConditionlessQueries()
		{
			this.DoConditionlessQuery(q => q.Bool(b => b
				.Must(mq => mq.Term(p => p.Name, _c.Name1), mq => mq.Term(p => p.Name, _c.Name2))
				.MustNot(mq => mq.Terms(p => p.Name, _c.Name1), mq => mq.Terms(p => p.Name, _c.Name2))
				.Should(mq => mq.Terms(p => p.Name, _c.Name1), mq => mq.Terms(p => p.Name, _c.Name2))
			));
		}

		[Test]
		public void Boosting()
		{
			this.DoConditionlessQuery(q => q.Boosting(bq => bq.Positive(qff => qff.Terms(p => p.Name, _c.Name1))));
			this.DoConditionlessQuery(q => q.Boosting(bq => bq.Negative(qff => qff.Terms(p => p.Name, _c.Name1))));
		}


		[Test]
		public void Term()
		{
			this.DoConditionlessQuery(q => q.Term(p => p.Name, _c.Name1));
			this.DoConditionlessQuery(q => q.Term("", "myterm"));
		}

		[Test]
		public void TermString()
		{
			this.DoConditionlessQuery(q => q.Term(string.Empty, _c.Name1));
		}

		[Test]
		public void Wildcard()
		{
			this.DoConditionlessQuery(q => q.Wildcard(p => p.Name, _c.Name1));
		}

		[Test]
		public void WildcardString()
		{
			this.DoConditionlessQuery(q => q.Wildcard(string.Empty, _c.Name1));
		}

		[Test]
		public void Prefix()
		{
			this.DoConditionlessQuery(q => q.Prefix(p => p.Name, _c.Name1));
		}

		[Test]
		public void PrefixString()
		{
			this.DoConditionlessQuery(q => q.Prefix(string.Empty, _c.Name1));
		}

		[Test]
		public void Ids()
		{
			this.DoConditionlessQuery(q => q.Ids(null));
		}

		[Test]
		public void IdsArray()
		{
			this.DoConditionlessQuery(q => q.Ids(new string[] { string.Empty }));
		}

		[Test]
		public void SpanTerm()
		{
			this.DoConditionlessQuery(q => q.SpanTerm(p => p.Name, _c.Name1));
		}

		[Test]
		public void SpanTermNoField()
		{
			this.DoConditionlessQuery(q => q.SpanTerm(string.Empty, _c.Name1));
		}

		[Test]
		public void SpanFirst()
		{
			this.DoConditionlessQuery(q => q.SpanFirst(s => s.Match(sq => sq.SpanTerm(p => p.Name, _c.Name1))));
		}

		[Test]
		public void SpanNear()
		{
			this.DoConditionlessQuery(q => q.SpanNear(s => s.Clauses()));
			this.DoConditionlessQuery(q => q.SpanNear(s => s.Clauses(sq => sq.SpanTerm(p => p.Name, _c.Name1))));
		}

		[Test]
		public void SpanOr()
		{
			this.DoConditionlessQuery(q => q.SpanOr(s => s.Clauses()));
			this.DoConditionlessQuery(q => q.SpanOr(s => s.Clauses(sq => sq.SpanTerm(p => p.Name, _c.Name1))));
		}

		[Test]
		public void SpanNot()
		{
			this.DoConditionlessQuery(q => q.SpanNot(s => s.Include(null)));
			this.DoConditionlessQuery(q => q.SpanNot(s => s.Include(sq => sq.SpanTerm(p => p.Name, _c.Name1))));
			this.DoConditionlessQuery(q => q.SpanNot(s => s.Exclude(null)));
			this.DoConditionlessQuery(q => q.SpanNot(s => s.Exclude(sq => sq.SpanTerm(p => p.Name, _c.Name1))));

		}
	}
}
