using System;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.ConditionLess
{
	[TestFixture]
	public class SemiConditionLessTests : BaseJsonTests
	{
		public class Criteria
		{
			public string Name1 { get; set; }
			public string Name2 { get; set; }
			public int? Int1 { get; set; }
			public DateTime? Date { get; set; }
		}
		private readonly Criteria _c = new Criteria();

		private void DoSemiConditionlessQuery(Func<QueryDescriptor<ElasticsearchProject>, QueryContainer> query)
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
			 .From(0)
			 .Take(10)
			 .Query(query);

			this.JsonNotEquals(s, System.Reflection.MethodInfo.GetCurrentMethod(), "MatchAll");
		}


		[Test]
		public void Terms()
		{
			this.DoSemiConditionlessQuery(q => q
				.Terms(p => p.Name, new string[] { this._c.Name1, "myterm" }));
			this.DoSemiConditionlessQuery(q => q
				.Terms(p => p.Name, new [] {this._c.Name1, this._c.Name2, "myterm"}));
		}

		
		[Test]
		public void Indices()
		{
			this.DoSemiConditionlessQuery(q => q
			  .Indices(iq => iq
				.Indices(new[] { "_all" })
				.Query(tq => tq.Terms(p => p.Name, new [] {this._c.Name1 }))
				.NoMatchQuery(tq => tq.Terms(p => p.Name, new [] {"myterm" }))
			  )
			);
		}

		[Test]
		public void HasChild()
		{
			this.DoSemiConditionlessQuery(q => q
				.HasChild<Person>(hcq => hcq
					.Query(qq => qq
						.Terms(p => p.FirstName, new [] {this._c.Name1, "myterm"})
					)
				)
			);
		}

		[Test]
		public void TopChildren()
		{
			this.DoSemiConditionlessQuery(q => q
				.TopChildren<Person>(hcq => hcq
					.Query(qq => qq
						.Terms(p => p.FirstName, new [] {this._c.Name1, "myterm"})
					)
				)
			);
		}

		[Test]
		public void Filtered()
		{
			this.DoSemiConditionlessQuery(q => q
				.Filtered(fq => fq
					.Query(qff => qff
						.Terms(p => p.Name, new [] {this._c.Name1, "myterm"})
					)
				)
			);
		}

		[Test]
		public void Dismax()
		{
			this.DoSemiConditionlessQuery(q => q
				.Dismax(dq => dq
					.Queries(qff => qff
						.Terms(p => p.Name, new [] {this._c.Name1, "myterm"})
					)
				)
			);
		}

		[Test]
		public void ConstantScore()
		{
			this.DoSemiConditionlessQuery(q => q
				.ConstantScore(cq => cq
					.Query(qff => qff
						.Terms(p => p.Name, new [] {this._c.Name1, "myterm"})
					)
				)
			);
		}

		[Test]
		public void BoolConditionlessQueries()
		{
			this.DoSemiConditionlessQuery(q => q.Bool(b => b
				.Must(
					mq => mq.Term(p => p.Name, this._c.Name1), 
					mq => mq.Terms(p => p.Name, new [] {this._c.Name2, "myterm" })
				)
				.MustNot(
					mq => mq.Terms(p => p.Name, new [] {this._c.Name1 }), 
					mq => mq.Terms(p => p.Name, new [] {this._c.Name2 })
				)
				.Should(
					mq => mq.Terms(p => p.Name, new [] {this._c.Name1 }), 
					mq => mq.Terms(p => p.Name, new [] {this._c.Name2 })
				)
			));
			this.DoSemiConditionlessQuery(q => q.Bool(b => b
				.Must(
					mq => mq.Term(p => p.Name, this._c.Name1), 
					mq => mq.Terms(p => p.Name, new [] {this._c.Name2 })
				)
				.MustNot(
					mq => mq.Terms(p => p.Name, new [] {this._c.Name1 }), 
					mq => mq.Terms(p => p.Name, new [] {this._c.Name2, "myterm"})
				)
				.Should(
					mq => mq.Terms(p => p.Name, new [] {this._c.Name1 }), 
					mq => mq.Terms(p => p.Name, new [] {this._c.Name2 })
				)
			));
			this.DoSemiConditionlessQuery(q => q.Bool(b => b
				.Must(
					mq => mq.Term(p => p.Name, this._c.Name1), 
					mq => mq.Terms(p => p.Name, new [] {this._c.Name2})
				)
				.MustNot(
					mq => mq.Terms(p => p.Name, new [] {this._c.Name1}), 
					mq => mq.Terms(p => p.Name, new [] {this._c.Name2})
				)
				.Should(
					mq => mq.Terms(p => p.Name, new [] {this._c.Name1}), 
					mq => mq.Terms(p => p.Name, new [] {this._c.Name2, "myterm"})
				)
			));
		}

		[Test]
		public void Boosting()
		{
			this.DoSemiConditionlessQuery(q => q
				.Boosting(bq => bq
					.Positive(qff => qff
						.Terms(p => p.Name, new [] {this._c.Name1, "myterm"})
					)
				)
			);
			this.DoSemiConditionlessQuery(q => q
				.Boosting(bq => bq
					.Negative(qff => qff
						.Terms(p => p.Name, new [] {this._c.Name1, "myterm"})
					)
				)
			);
		}


		[Test]
		public void IdsArray()
		{
			this.DoSemiConditionlessQuery(q => q.Ids(new string[] { string.Empty, "1" }));
		}


		[Test]
		public void SpanFirst()
		{
			this.DoSemiConditionlessQuery(q => q.SpanFirst(s => s.Match(sq => sq.SpanTerm(p => p.Name, "myterm"))));
		}

		[Test]
		public void SpanNear()
		{
			this.DoSemiConditionlessQuery(q => q.SpanNear(s => s.Clauses(sq => sq.SpanTerm(p => p.Name, "myterm"))));
		}

		[Test]
		public void SpanOr()
		{
			this.DoSemiConditionlessQuery(q => q.SpanOr(s => s.Clauses(sq => sq.SpanTerm(p => p.Name, "myterm"))));
		}

		[Test]
		public void SpanNot()
		{
			this.DoSemiConditionlessQuery(q => q.SpanNot(s => s.Include(sq => sq.SpanTerm(p => p.Name, "myterm"))));
			this.DoSemiConditionlessQuery(q => q.SpanNot(s => s.Exclude(sq => sq.SpanTerm(p => p.Name, "myterm"))));

		}
	}
}
