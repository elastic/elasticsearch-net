using System;
using System.Collections.Generic;

namespace Nest.QueryDsl.Visitor
{
	public class QueryWalker
	{
		private void Accept(IQueryVisitor visitor, IEnumerable<IQueryContainer> queries, VisitorScope scope = VisitorScope.Query)
		{
			if (!queries.HasAny()) return;
			foreach (var f in queries) this.Accept(visitor, f, scope);
		}

		private void Accept(IQueryVisitor visitor, IQueryContainer query, VisitorScope scope = VisitorScope.Query)
		{
			if (query == null) return;
			visitor.Scope = scope;
			query.Accept(visitor);
		}

		private static void AcceptQuery<T>(T qd, IQueryVisitor visitor, Action<IQueryVisitor, T> scoped)
			where T : class, IQuery
		{
			if (qd == null) return;

			visitor.Depth = visitor.Depth + 1;
			visitor.Visit(qd);
			scoped(visitor, qd);
			visitor.Depth = visitor.Depth - 1;
		}

		public void Walk(IQueryContainer qd, IQueryVisitor visitor)
		{
			visitor.Visit(qd);
			AcceptQuery(qd.MatchAllQuery, visitor, (v, d) => v.Visit(d));
			AcceptQuery(qd.MoreLikeThis, visitor, (v, d) => v.Visit(d));
			AcceptQuery(qd.MultiMatch, visitor, (v, d) => v.Visit(d));
			AcceptQuery(qd.CommonTerms, visitor, (v, d) => v.Visit(d));
			AcceptQuery(qd.Fuzzy, visitor, (v, d) => v.Visit(d));
			AcceptQuery(qd.GeoShape, visitor, (v, d) => v.Visit(d));
			AcceptQuery(qd.Ids, visitor, (v, d) => v.Visit(d));
			AcceptQuery(qd.Prefix, visitor, (v, d) => v.Visit(d));
			AcceptQuery(qd.QueryString, visitor, (v, d) => v.Visit(d));
			AcceptQuery(qd.Range, visitor, (v, d) => v.Visit(d));
			AcceptQuery(qd.Regexp, visitor, (v, d) => v.Visit(d));
			AcceptQuery(qd.SimpleQueryString, visitor, (v, d) => v.Visit(d));
			AcceptQuery(qd.SpanFirst, visitor, (v, d) => v.Visit(d));
			AcceptQuery(qd.SpanNear, visitor, (v, d) => v.Visit(d));
			AcceptQuery(qd.SpanNot, visitor, (v, d) => v.Visit(d));
			AcceptQuery(qd.SpanOr, visitor, (v, d) => v.Visit(d));
			AcceptQuery(qd.SpanTerm, visitor, (v, d) => v.Visit(d));
			AcceptQuery(qd.Term, visitor, (v, d) => v.Visit(d));
			AcceptQuery(qd.Terms, visitor, (v, d) => v.Visit(d));
			AcceptQuery(qd.Wildcard, visitor, (v, d) => v.Visit(d));
			AcceptQuery(qd.Match, visitor, (v, d) => v.Visit(d));
			AcceptQuery(qd.Bool, visitor, (v, d) =>
			{
				v.Visit(d);
				this.Accept(v, d.Must, VisitorScope.Must);
				this.Accept(v, d.MustNot, VisitorScope.MustNot);
				this.Accept(v, d.Should, VisitorScope.Should);
			});
			AcceptQuery(qd.Boosting, visitor, (v, d) =>
			{
				v.Visit(d);
				this.Accept(v, d.PositiveQuery, VisitorScope.PositiveQuery);
				this.Accept(v, d.NegativeQuery, VisitorScope.NegativeQuery);
			});
			AcceptQuery(qd.ConstantScore, visitor, (v, d) =>
			{
				v.Visit(d);
				this.Accept(v, d.Filter);
			});
			AcceptQuery(qd.DisMax, visitor, (v, d) =>
			{
				v.Visit(d);
				this.Accept(v, d.Queries);
			});
			AcceptQuery(qd.Filtered, visitor, (v, d) =>
			{
				visitor.Visit(d);
				this.Accept(v, d.Query);
				this.Accept(v, d.Filter);
			});
			AcceptQuery(qd.FunctionScore, visitor, (v, d) =>
			{
				v.Visit(d);
				this.Accept(v, d.Query);
				//TODO loop over qd.FunctionScore.Functions;
			});
			AcceptQuery(qd.HasChild, visitor, (v, d) =>
			{
				v.Visit(d);
				this.Accept(v, d.Query);
			});
			AcceptQuery(qd.HasParent, visitor, (v, d) =>
			{
				v.Visit(d);
				this.Accept(v, d.Query);
			});
			AcceptQuery(qd.Indices, visitor, (v, d) =>
			{
				v.Visit(d);
				this.Accept(v, d.Query);
				this.Accept(v, d.NoMatchQuery, VisitorScope.NoMatchQuery);
			});
			AcceptQuery(qd.Nested, visitor, (v, d) =>
			{
				v.Visit(d);
				this.Accept(v, d.Query);
			});
		}
	}
}
