using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class QueryWalker
	{
		public void Walk(IQueryContainer qd, IQueryVisitor visitor)
		{
			visitor.Visit(qd);
			VisitQuery(qd.MatchAll, visitor, (v, d) => v.Visit(d));
			VisitQuery(qd.MoreLikeThis, visitor, (v, d) => v.Visit(d));
			VisitQuery(qd.MultiMatch, visitor, (v, d) => v.Visit(d));
			VisitQuery(qd.CommonTerms, visitor, (v, d) => v.Visit(d));
			VisitQuery(qd.Fuzzy, visitor, (v, d) => 
			{
				v.Visit(d);
				VisitQuery(d as IFuzzyStringQuery, visitor, (vv, dd) => v.Visit(dd));
				VisitQuery(d as IFuzzyNumericQuery, visitor, (vv, dd) => v.Visit(dd));
				VisitQuery(d as IFuzzyDateQuery, visitor, (vv, dd) => v.Visit(dd));
			});
			VisitQuery(qd.Range, visitor, (v, d) => 
			{
				v.Visit(d);
				VisitQuery(d as IDateRangeQuery, visitor, (vv, dd) => v.Visit(dd));
				VisitQuery(d as INumericRangeQuery, visitor, (vv, dd) => v.Visit(dd));
				VisitQuery(d as ITermRangeQuery, visitor, (vv, dd) => v.Visit(dd));
			});
			VisitQuery(qd.GeoShape, visitor, (v, d) => 
			{
				v.Visit(d);
				VisitQuery(d as IGeoIndexedShapeQuery, visitor, (vv, dd) => v.Visit(dd));
				VisitQuery(d as IGeoShapeMultiPointQuery, visitor, (vv, dd) => v.Visit(dd));
				VisitQuery(d as IGeoShapeMultiPolygonQuery, visitor, (vv, dd) => v.Visit(dd));
				VisitQuery(d as IGeoShapePolygonQuery, visitor, (vv, dd) => v.Visit(dd));
				VisitQuery(d as IGeoShapePointQuery, visitor, (vv, dd) => v.Visit(dd));
				VisitQuery(d as IGeoShapeMultiLineStringQuery, visitor, (vv, dd) => v.Visit(dd));
				VisitQuery(d as IGeoShapeLineStringQuery, visitor, (vv, dd) => v.Visit(dd));
				VisitQuery(d as IGeoShapeEnvelopeQuery, visitor, (vv, dd) => v.Visit(dd));
				VisitQuery(d as IGeoShapeCircleQuery, visitor, (vv, dd) => v.Visit(dd));
			});
			VisitQuery(qd.Ids, visitor, (v, d) => v.Visit(d));
			VisitQuery(qd.Prefix, visitor, (v, d) => v.Visit(d));
			VisitQuery(qd.QueryString, visitor, (v, d) => v.Visit(d));
			VisitQuery(qd.Range, visitor, (v, d) => v.Visit(d));
			VisitQuery(qd.Regexp, visitor, (v, d) => v.Visit(d));
			VisitQuery(qd.SimpleQueryString, visitor, (v, d) => v.Visit(d));
			VisitQuery(qd.Term, visitor, (v, d) => v.Visit(d));
			VisitQuery(qd.Terms, visitor, (v, d) => v.Visit(d));
			VisitQuery(qd.Wildcard, visitor, (v, d) => v.Visit(d));
			VisitQuery(qd.Match, visitor, (v, d) => v.Visit(d));
			VisitQuery(qd.Type, visitor, (v, d) => v.Visit(d));
			VisitQuery(qd.Script, visitor, (v, d) => v.Visit(d));
			VisitQuery(qd.Missing, visitor, (v, d) => v.Visit(d));
			VisitQuery(qd.Exists, visitor, (v, d) => v.Visit(d));
			VisitQuery(qd.GeoPolygon, visitor, (v, d) => v.Visit(d));
			VisitQuery(qd.GeoDistanceRange, visitor, (v, d) => v.Visit(d));
			VisitQuery(qd.GeoDistance, visitor, (v, d) => v.Visit(d));
			VisitQuery(qd.GeoBoundingBox, visitor, (v, d) => v.Visit(d));
			VisitQuery(qd.GeoHashCell, visitor, (v, d) => v.Visit(d));
			VisitQuery(qd.Limit, visitor, (v, d) => v.Visit(d));
			VisitQuery(qd.Template, visitor, (v, d) => v.Visit(d));
			VisitQuery(qd.RawQuery, visitor, (v, d) => v.Visit(d));

			VisitQuery(qd.Not, visitor, (v, d) =>
			{
				v.Visit(d);
				Accept(v, d.Filters);
			});
			VisitQuery(qd.And, visitor, (v, d) =>
			{
				v.Visit(d);
				Accept(v, d.Filters);
			});
			VisitQuery(qd.Or, visitor, (v, d) =>
			{
				v.Visit(d);
				Accept(v, d.Filters);
			});
			VisitQuery(qd.Bool, visitor, (v, d) =>
			{
				v.Visit(d);
				Accept(v, d.Must, VisitorScope.Must);
				Accept(v, d.MustNot, VisitorScope.MustNot);
				Accept(v, d.Should, VisitorScope.Should);
			});

			VisitSpan(qd, visitor);

			VisitQuery(qd.Boosting, visitor, (v, d) =>
			{
				v.Visit(d);
				Accept(v, d.PositiveQuery, VisitorScope.PositiveQuery);
				Accept(v, d.NegativeQuery, VisitorScope.NegativeQuery);
			});
			VisitQuery(qd.ConstantScore, visitor, (v, d) =>
			{
				v.Visit(d);
				Accept(v, d.Filter);
			});
			VisitQuery(qd.DisMax, visitor, (v, d) =>
			{
				v.Visit(d);
				Accept(v, d.Queries);
			});
			VisitQuery(qd.Filtered, visitor, (v, d) =>
			{
				visitor.Visit(d);
				Accept(v, d.Query);
				Accept(v, d.Filter);
			});
			VisitQuery(qd.FunctionScore, visitor, (v, d) =>
			{
				v.Visit(d);
				Accept(v, d.Query);
			});
			VisitQuery(qd.HasChild, visitor, (v, d) =>
			{
				v.Visit(d);
				Accept(v, d.Query);
			});
			VisitQuery(qd.HasParent, visitor, (v, d) =>
			{
				v.Visit(d);
				Accept(v, d.Query);
			});
			VisitQuery(qd.Indices, visitor, (v, d) =>
			{
				v.Visit(d);
				Accept(v, d.Query);
				Accept(v, d.NoMatchQuery, VisitorScope.NoMatchQuery);
			});
			VisitQuery(qd.Nested, visitor, (v, d) =>
			{
				v.Visit(d);
				Accept(v, d.Query);
			});
		}

		public void Walk(ISpanQuery qd, IQueryVisitor visitor)
		{
			VisitSpanSubQuery(qd.SpanFirst, visitor, (v, d) =>
			{
				v.Visit(d);
				Accept(visitor, d.Match);
			});
			VisitSpanSubQuery(qd.SpanNear, visitor, (v, d) =>
			{
				v.Visit(d);
				foreach (var q in d.Clauses ?? Enumerable.Empty<ISpanQuery>())
					Accept(visitor, q);
			});
			VisitSpanSubQuery(qd.SpanNot, visitor, (v, d) =>
			{
				v.Visit(d);
				Accept(visitor, d.Include);
				Accept(visitor, d.Exclude);
			});

			VisitSpanSubQuery(qd.SpanOr, visitor, (v, d) =>
			{
				v.Visit(d);
				foreach (var q in d.Clauses ?? Enumerable.Empty<ISpanQuery>())
					Accept(visitor, q);
			});

			VisitSpanSubQuery(qd.SpanTerm, visitor, (v, d) => v.Visit(d));
			VisitSpanSubQuery(qd.SpanMultiTerm, visitor, (v, d) =>
			{
				v.Visit(d);
				Accept(visitor, d.Match);
			});
			VisitSpanSubQuery(qd.SpanContaining, visitor, (v, d) =>
			{
				v.Visit(d);
				Accept(visitor, d.Big);
				Accept(visitor, d.Little);
			});
			VisitSpanSubQuery(qd.SpanWithin, visitor, (v, d) =>
			{
				v.Visit(d);
				Accept(visitor, d.Big);
				Accept(visitor, d.Little);
			});
		}

		private static void Accept(IQueryVisitor visitor, IEnumerable<IQueryContainer> queries, VisitorScope scope = VisitorScope.Query)
		{
			if (!queries.HasAny()) return;
			foreach (var f in queries) Accept(visitor, f, scope);
		}

		private static void Accept(IQueryVisitor visitor, IQueryContainer query, VisitorScope scope = VisitorScope.Query)
		{
			if (query == null) return;
			visitor.Scope = scope;
			query.Accept(visitor);
		}

		private static void Accept(IQueryVisitor visitor, ISpanQuery query, VisitorScope scope = VisitorScope.Span)
		{
			if (query == null) return;
			visitor.Scope = scope;
			query.Accept(visitor);
		}

		private static void VisitSpan<T>(T qd, IQueryVisitor visitor) where T : class, IQueryContainer
		{
			VisitSpanSubQuery(qd.SpanFirst, visitor, (v, d) =>
			{
				v.Visit(d);
				Accept(visitor, d.Match);
			});
			VisitSpanSubQuery(qd.SpanNear, visitor, (v, d) =>
			{
				v.Visit(d);
				foreach (var q in d.Clauses ?? Enumerable.Empty<ISpanQuery>())
					Accept(visitor, q);
			});
			VisitSpanSubQuery(qd.SpanNot, visitor, (v, d) =>
			{
				v.Visit(d);
				Accept(visitor, d.Include);
				Accept(visitor, d.Exclude);
			});

			VisitSpanSubQuery(qd.SpanOr, visitor, (v, d) =>
			{
				v.Visit(d);
				foreach (var q in d.Clauses ?? Enumerable.Empty<ISpanQuery>())
					Accept(visitor, q);
			});

			VisitSpanSubQuery(qd.SpanTerm, visitor, (v, d) => v.Visit(d));
			VisitSpanSubQuery(qd.SpanMultiTerm, visitor, (v, d) =>
			{
				v.Visit(d);
				Accept(visitor, d.Match);
			});
			VisitSpanSubQuery(qd.SpanContaining, visitor, (v, d) =>
			{
				v.Visit(d);
				Accept(visitor, d.Big);
				Accept(visitor, d.Little);
			});
			VisitSpanSubQuery(qd.SpanWithin, visitor, (v, d) =>
			{
				v.Visit(d);
				Accept(visitor, d.Big);
				Accept(visitor, d.Little);
			});
		}

		private static void VisitQuery<T>(T qd, IQueryVisitor visitor, Action<IQueryVisitor, T> scoped)
			where T : class, IQuery
		{
			if (qd == null) return;

			visitor.Depth = visitor.Depth + 1;
			visitor.Visit(qd);
			scoped(visitor, qd);
			visitor.Depth = visitor.Depth - 1;
		}

		private static void VisitSpanSubQuery<T>(T qd, IQueryVisitor visitor, Action<IQueryVisitor, T> scoped)
			where T : class, ISpanSubQuery
		{
			if (qd == null) return;
			VisitQuery(qd, visitor, (v, d) =>
			{
				visitor.Visit(qd as ISpanSubQuery);
				scoped(v, d);
			});
		}
	}
}
