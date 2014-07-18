using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest.DSL.Visitor
{
	public class QueryFilterWalker
	{
		private void Accept(IQueryVisitor visitor, IEnumerable<IFilterContainer> filters, VisitorScope scope = VisitorScope.Filter)
		{
			if (!filters.HasAny()) return;
			foreach (var f in filters) this.Accept(visitor, f, scope);
		}

		private void Accept(IQueryVisitor visitor, IFilterContainer filter, VisitorScope scope = VisitorScope.Filter)
		{
			if (filter == null) return;
			visitor.Scope = scope;
			filter.Accept(visitor);
		}

		private void Accept(IQueryVisitor visitor, IEnumerable<IQueryContainer> queries, VisitorScope scope = VisitorScope.Query)
		{
			if (!queries.HasAny()) return;
			foreach (var q in queries) this.Accept(visitor, q, scope);
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

		private static void AcceptFilter<T>(T fd, IQueryVisitor visitor, Action<IQueryVisitor, T> scoped)
			where T : class, IFilter
		{
			if (fd == null) return;

			visitor.Depth = visitor.Depth + 1;
			visitor.Visit(fd);
			scoped(visitor, fd);
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
			AcceptQuery(qd.FuzzyLikeThis, visitor, (v, d) => v.Visit(d));
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
				this.Accept(v, d.Query);
				this.Accept(v, d.Filter);
			});
			AcceptQuery(qd.CustomBoostFactor, visitor, (v, d) =>
			{
				v.Visit(d);
				this.Accept(v, d.Query);
			});
			AcceptQuery(qd.CustomFiltersScore, visitor, (v, d) =>
			{
				v.Visit(d);
				this.Accept(v, d.Query);
				//TODO loop over filters or implement accept on them individually
			});
			AcceptQuery(qd.CustomScore, visitor, (v, d) =>
			{
				v.Visit(d);
				this.Accept(v, d.Query);
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
				this.Accept(v, d.Filter);
			});
			AcceptQuery(qd.TopChildren, visitor, (v, d) =>
			{
				v.Visit(d);
				this.Accept(v, d.Query);
			});
		}

		public void Walk(IFilterContainer fd, IQueryVisitor visitor)
		{
			visitor.Visit(fd);
			AcceptFilter(fd.Range, visitor, (v, d) => v.Visit(d));
			AcceptFilter(fd.Regexp, visitor, (v, d) => v.Visit(d));
			AcceptFilter(fd.Script, visitor, (v, d) => v.Visit(d));
			AcceptFilter(fd.Term, visitor, (v, d) => v.Visit(d));
			AcceptFilter(fd.Terms, visitor, (v, d) => v.Visit(d));
			AcceptFilter(fd.Type, visitor, (v, d) => v.Visit(d));
			AcceptFilter(fd.Exists, visitor, (v, d) => v.Visit(d));
			AcceptFilter(fd.GeoBoundingBox, visitor, (v, d) => v.Visit(d));
			AcceptFilter(fd.GeoDistance, visitor, (v, d) => v.Visit(d));
			AcceptFilter(fd.GeoDistanceRange, visitor, (v, d) => v.Visit(d));
			AcceptFilter(fd.GeoPolygon, visitor, (v, d) => v.Visit(d));
			AcceptFilter(fd.GeoShape, visitor, (v, d) => v.Visit(d));
			AcceptFilter(fd.HasChild, visitor, (v, d) => v.Visit(d));
			AcceptFilter(fd.Ids, visitor, (v, d) => v.Visit(d));
			AcceptFilter(fd.Limit, visitor, (v, d) => v.Visit(d));
			AcceptFilter(fd.MatchAll, visitor, (v, d) => v.Visit(d));
			AcceptFilter(fd.Missing, visitor, (v, d) => v.Visit(d));
			AcceptFilter(fd.Prefix, visitor, (v, d) => v.Visit(d));
			AcceptFilter(fd.And, visitor, (v, d) =>
			{
				v.Visit(d);
				this.Accept(v, d.Filters);
			});
			AcceptFilter(fd.Bool, visitor, (v, d) =>
			{
				v.Visit(d);
				this.Accept(v, d.Must, VisitorScope.Must);
				this.Accept(v, d.MustNot, VisitorScope.MustNot);
				this.Accept(v, d.Should, VisitorScope.Should);
			});
			AcceptFilter(fd.HasParent, visitor, (v, d) =>
			{
				v.Visit(d);
				this.Accept(v, d.Query);
			});
			AcceptFilter(fd.Nested, visitor, (v, d) =>
			{
				v.Visit(d);
				this.Accept(v, d.Query);
				this.Accept(v, d.Filter);
			});
			AcceptFilter(fd.Not, visitor, (v, d) =>
			{
				v.Visit(d);
				this.Accept(v, d.Filter);
			});
			AcceptFilter(fd.Or, visitor, (v, d) =>
			{
				v.Visit(d);
				this.Accept(v, d.Filters);
			});
			AcceptFilter(fd.Query, visitor, (v, d) =>
			{
				v.Visit(d);
				this.Accept(v, d.Query);
			});
		}
	}
}
