using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.DSL.Visitor
{
	public class QueryFilterWalker
	{
		private void Accept(IQueryVisitor visitor, IEnumerable<IFilterDescriptor> filters)
		{
			if (!filters.HasAny()) return;
			foreach (var f in filters) this.Accept(visitor, f);
		}

		private void Accept(IQueryVisitor visitor, IFilterDescriptor filter)
		{
			if (filter == null) return;
			filter.Accept(visitor);
		}

		private void Accept(IQueryVisitor visitor, IEnumerable<IQueryDescriptor> queries)
		{
			if (!queries.HasAny()) return;
			foreach (var q in queries) this.Accept(visitor, q);
		}

		private void Accept(IQueryVisitor visitor, IQueryDescriptor query)
		{
			if (query == null) return;
			query.Accept(visitor);
		}

		public void Walk(IQueryDescriptor qd, IQueryVisitor visitor)
		{
			visitor.Visit(qd);
			if (qd.Bool != null)
			{
				visitor.Visit((IQuery)qd.Bool);
				visitor.Visit(qd.Bool);
				this.Accept(visitor, qd.Bool.Must); 
				this.Accept(visitor, qd.Bool.MustNot);
				this.Accept(visitor, qd.Bool.Should);
			}
			if (qd.Boosting != null)
			{
				visitor.Visit((IQuery)qd.Boosting);
				visitor.Visit(qd.Boosting);
				this.Accept(visitor, qd.Boosting.PositiveQuery);
				this.Accept(visitor, qd.Boosting.NegativeQuery);
			}
			if (qd.CommonTerms != null)
			{
				visitor.Visit((IQuery)qd.CommonTerms);
				visitor.Visit(qd.CommonTerms);
			}
			if (qd.ConstantScore != null)
			{
				visitor.Visit((IQuery)qd.ConstantScore);
				visitor.Visit(qd.ConstantScore);
				this.Accept(visitor, qd.ConstantScore.Query);
				this.Accept(visitor, qd.ConstantScore.Filter);
			}
			if (qd.CustomBoostFactor != null)
			{
				visitor.Visit((IQuery)qd.CustomBoostFactor);
				visitor.Visit(qd.CustomBoostFactor);
				this.Accept(visitor, qd.CustomBoostFactor.Query);
			}
			var customFiltersScoreQuery = qd.CustomFiltersScore;
			if (customFiltersScoreQuery != null)
			{
				visitor.Visit((IQuery)customFiltersScoreQuery);
				visitor.Visit(customFiltersScoreQuery);
				this.Accept(visitor, customFiltersScoreQuery.Query);
				//TODO loop over filters or implement accept on them individually
			}
			if (qd.CustomScore != null)
			{
				visitor.Visit((IQuery)qd.CustomScore);
				visitor.Visit(qd.CustomScore);
				this.Accept(visitor, qd.CustomScore.Query);
			}
			if (qd.DisMax != null)
			{
				visitor.Visit((IQuery)qd.DisMax);
				visitor.Visit(qd.DisMax);
				this.Accept(visitor, qd.DisMax.Queries);
			}
			if (qd.Filtered != null)
			{
				visitor.Visit((IQuery)qd.Filtered);
				visitor.Visit(qd.Filtered);
				this.Accept(visitor, qd.Filtered.Query);
				this.Accept(visitor, qd.Filtered.Filter);
			}
			if (qd.FunctionScore != null)
			{
				visitor.Visit((IQuery)qd.FunctionScore);
				visitor.Visit(qd.FunctionScore);
				this.Accept(visitor, qd.FunctionScore.Query);
				//TODO loop over qd.FunctionScore.Functions;
			}
			if (qd.Fuzzy != null)
			{
				visitor.Visit((IQuery)qd.Fuzzy);
				visitor.Visit(qd.Fuzzy);
			}
			if (qd.FuzzyLikeThis != null)
			{
				visitor.Visit((IQuery)qd.FuzzyLikeThis);
				visitor.Visit(qd.FuzzyLikeThis);
			}
			if (qd.GeoShape != null)
			{
				visitor.Visit((IQuery)qd.GeoShape);
				visitor.Visit(qd.GeoShape);
			}
			if (qd.HasChild != null)
			{
				visitor.Visit((IQuery)qd.HasChild);
				visitor.Visit(qd.HasChild);
				this.Accept(visitor, qd.HasChild.Query);
			}
			if (qd.HasParent != null)
			{
				visitor.Visit((IQuery)qd.HasParent);
				visitor.Visit(qd.HasParent);
				this.Accept(visitor, qd.HasParent.Query);
			}
			if (qd.Ids != null)
			{
				visitor.Visit((IQuery)qd.Ids);
				visitor.Visit(qd.Ids);
			}
			if (qd.Indices != null)
			{
				visitor.Visit((IQuery)qd.Indices);
				visitor.Visit(qd.Indices);
				this.Accept(visitor, qd.Indices.Query);
				this.Accept(visitor, qd.Indices.NoMatchQuery);
			}
			if (qd.Match != null)
			{
				visitor.Visit((IQuery)qd.Match);
				visitor.Visit(qd.Match);
			}
			if (qd.MatchAll != null)
			{
				visitor.Visit((IQuery)qd.MatchAll);
				visitor.Visit(qd.MatchAll);
			}
			if (qd.MoreLikeThis != null)
			{
				visitor.Visit((IQuery)qd.MoreLikeThis);
				visitor.Visit(qd.MoreLikeThis);
			}
			if (qd.MultiMatch != null)
			{
				visitor.Visit((IQuery)qd.MultiMatch);
				visitor.Visit(qd.MultiMatch);
			}
			if (qd.Nested != null)
			{
				visitor.Visit((IQuery)qd.Nested);
				visitor.Visit(qd.Nested);
				this.Accept(visitor, qd.Nested.Query);
			}
			if (qd.Prefix != null)
			{
				visitor.Visit((IQuery)qd.Prefix);
				visitor.Visit(qd.Prefix);
			}
			if (qd.QueryString != null)
			{
				visitor.Visit((IQuery)qd.QueryString);
				visitor.Visit(qd.QueryString);
			}
			if (qd.Range != null)
			{
				visitor.Visit((IQuery)qd.Range);
				visitor.Visit(qd.Range);
			}
			if (qd.RawQuery != null)
			{
				//visitor.Visit((IQuery)qd.RawQuery);
				//visitor.Visit(qd.RawQuery);
			}
			if (qd.Regexp != null)
			{
				visitor.Visit((IQuery)qd.Regexp);
				visitor.Visit(qd.Regexp);
			}
			if (qd.SimpleQueryString != null)
			{
				visitor.Visit((IQuery)qd.SimpleQueryString);
				visitor.Visit(qd.SimpleQueryString);
			}
			if (qd.SpanFirst != null)
			{
				visitor.Visit((IQuery)qd.SpanFirst);
				visitor.Visit(qd.SpanFirst);
			}
			if (qd.SpanNear != null)
			{
				visitor.Visit((IQuery)qd.SpanNear);
				visitor.Visit(qd.SpanNear);
			}
			if (qd.SpanNot != null)
			{
				visitor.Visit((IQuery)qd.SpanNot);
				visitor.Visit(qd.SpanNot);
			}
			if (qd.SpanOr != null)
			{
				visitor.Visit((IQuery)qd.SpanOr);
				visitor.Visit(qd.SpanOr);
			}
			if (qd.SpanTerm != null)
			{
				visitor.Visit((IQuery)qd.SpanTerm);
				visitor.Visit(qd.SpanTerm);
			}
			if (qd.Term != null)
			{
				visitor.Visit((IQuery)qd.Term);
				visitor.Visit(qd.Term);
			}
			if (qd.Terms != null)
			{
				visitor.Visit((IQuery)qd.Terms);
				visitor.Visit(qd.Terms);
			}
			if (qd.TopChildren != null)
			{
				visitor.Visit((IQuery)qd.TopChildren);
				visitor.Visit(qd.TopChildren);
				this.Accept(visitor, qd.TopChildren.Query);
			}
			if (qd.Wildcard != null)
			{
				visitor.Visit((IQuery)qd.Wildcard);
				visitor.Visit(qd.Wildcard);
			}
		}


		public void Walk(IFilterDescriptor fd, IQueryVisitor visitor)
		{
			visitor.Visit(fd);
			if (fd.And != null)
			{
				visitor.Visit((IFilterBase)fd.And);
				visitor.Visit(fd.And);
			}
			if (fd.Bool != null)
			{
				visitor.Visit((IFilterBase)fd.Bool);
				visitor.Visit(fd.Bool);
			}
			if (fd.Exists != null)
			{
				visitor.Visit((IFilterBase)fd.Exists);
				visitor.Visit(fd.Exists);
			}
			if (fd.GeoBoundingBox != null)
			{
				visitor.Visit((IFilterBase)fd.GeoBoundingBox);
				visitor.Visit(fd.GeoBoundingBox);
			}
			if (fd.GeoDistance != null)
			{
				visitor.Visit((IFilterBase)fd.GeoDistance);
				visitor.Visit(fd.GeoDistance);
			}
			if (fd.GeoDistanceRange != null)
			{
				visitor.Visit((IFilterBase)fd.GeoDistanceRange);
				visitor.Visit(fd.GeoDistanceRange);
			}
			if (fd.GeoPolygon != null)
			{
				visitor.Visit((IFilterBase)fd.GeoPolygon);
				visitor.Visit(fd.GeoPolygon);
			}
			if (fd.GeoShape != null)
			{
				visitor.Visit((IFilterBase)fd.GeoShape);
				visitor.Visit(fd.GeoShape);
			}
			if (fd.HasChild != null)
			{
				visitor.Visit((IFilterBase)fd.HasChild);
				visitor.Visit(fd.HasChild);
			}
			if (fd.HasParent != null)
			{
				visitor.Visit((IFilterBase)fd.HasParent);
				visitor.Visit(fd.HasParent);
			}
			if (fd.Ids != null)
			{
				visitor.Visit((IFilterBase)fd.Ids);
				visitor.Visit(fd.Ids);
			}
			if (fd.Limit != null)
			{
				visitor.Visit((IFilterBase)fd.Limit);
				visitor.Visit(fd.Limit);
			}
			if (fd.MatchAll != null)
			{
				visitor.Visit((IFilterBase)fd.MatchAll);
				visitor.Visit(fd.MatchAll);
			}
			if (fd.Missing != null)
			{
				visitor.Visit((IFilterBase)fd.Missing);
				visitor.Visit(fd.Missing);
			}
			if (fd.Nested != null)
			{
				visitor.Visit((IFilterBase)fd.Nested);
				visitor.Visit(fd.Nested);
			}
			if (fd.Not != null)
			{
				visitor.Visit((IFilterBase)fd.Not);
				visitor.Visit(fd.Not);
			}
			if (fd.Or != null)
			{
				visitor.Visit((IFilterBase)fd.Or);
				visitor.Visit(fd.Or);
			}
			if (fd.Prefix != null)
			{
				visitor.Visit((IFilterBase)fd.Prefix);
				visitor.Visit(fd.Prefix);
			}
			if (fd.Query != null)
			{
				visitor.Visit((IFilterBase)fd.Query);
				visitor.Visit(fd.Query);
			}
			if (fd.Range != null)
			{
				visitor.Visit((IFilterBase)fd.Range);
				visitor.Visit(fd.Range);
			}
			if (fd.RawFilter != null)
			{
				//visitor.Visit(fd.RawFilter);
			}
			if (fd.Regexp != null)
			{
				visitor.Visit((IFilterBase)fd.Regexp);
				visitor.Visit(fd.Regexp);
			}
			if (fd.Script != null)
			{
				visitor.Visit((IFilterBase)fd.Script);
				visitor.Visit(fd.Script);
			}
			if (fd.Term != null)
			{
				visitor.Visit((IFilterBase)fd.Term);
				visitor.Visit(fd.Term);
			}
			if (fd.Terms != null)
			{
				visitor.Visit((IFilterBase)fd.Terms);
				visitor.Visit(fd.Terms);
			}
			if (fd.Type != null)
			{
				visitor.Visit((IFilterBase)fd.Type);
				visitor.Visit(fd.Type);
			}
			throw new NotImplementedException();
		}
	}
}
