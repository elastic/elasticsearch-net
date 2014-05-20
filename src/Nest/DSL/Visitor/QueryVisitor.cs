using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.DSL.Visitor
{
	
	public enum VisitorScope
	{
		Filter,
		Query,
		Must,
		MustNot,
		Should,
		PositiveQuery,
		NegativeQuery,
		NoMatchQuery,

	}

	public interface IQueryVisitor
	{
		/// <summary>
		/// The current depth of the node being visited
		/// </summary>
		int Depth { get; set; }
		
		/// <summary>
		/// Hints the relation with the parent, i,e queries inside a Must clause will have VisitorScope.Must set.
		/// </summary>
		VisitorScope Scope { get; set; }

		void Visit(IQueryDescriptor baseQuery);
		void Visit(IBoolQuery baseQuery);
		void Visit(IBoostingQuery baseQuery);
		void Visit(ICommonTermsQuery commonTerms);
		void Visit(IConstantScoreQuery constantScore);
		void Visit(ICustomBoostFactorQuery customBoostFactor);
		void Visit(ICustomFiltersScoreQuery customFiltersScore);
		void Visit(ICustomScoreQuery customFiltersScore);
		void Visit(IDisMaxQuery customFiltersScore);
		void Visit(IFilteredQuery customFiltersScore);
		void Visit(IFunctionScoreQuery customFiltersScore);
		void Visit(IFuzzyQuery customFiltersScore);
		void Visit(IFuzzyLikeThisQuery customFiltersScore);
		void Visit(IGeoShapeQuery customFiltersScore);
		void Visit(IHasChildQuery customFiltersScore);
		void Visit(IHasParentQuery customFiltersScore);
		void Visit(IdsQuery customFiltersScore);
		void Visit(IIndicesQuery customFiltersScore);
		void Visit(IMatchQuery customFiltersScore);
		void Visit(MatchAll customFiltersScore);
		void Visit(IMoreLikeThisQuery customFiltersScore);
		void Visit(IMultiMatchQuery customFiltersScore);
		void Visit(INestedQuery customFiltersScore);
		void Visit(IPrefixQuery customFiltersScore);
		void Visit(IQueryStringQuery customFiltersScore);
		void Visit(IRangeQuery customFiltersScore);
		void Visit(IRegexpQuery customFiltersScore);
		void Visit(ISimpleQueryStringQuery customFiltersScore);
		void Visit(ISpanFirstQuery customFiltersScore);
		void Visit(ISpanNearQuery customFiltersScore);
		void Visit(ISpanNotQuery customFiltersScore);
		void Visit(ISpanOrQuery customFiltersScore);
		void Visit(ISpanTermQuery customFiltersScore);
		void Visit(ITermQuery customFiltersScore);
		void Visit(IWildcardQuery customFiltersScore);
		void Visit(ITermsQuery customFiltersScore);
		void Visit(ITopChildrenQuery customFiltersScore);
		/// <summary>
		/// Visit every query item just before they are visited by their specialized Visit() implementation
		/// </summary>
		/// <param name="query">The IQuery object that will be visited</param>
		void Visit(IQuery query);

		void Visit(BaseFilterDescriptor customFiltersScore);
		//void Visit(IFilterBase customFiltersScore);
		void Visit(ITypeFilter customFiltersScore);
		void Visit(ITermsBaseFilter customFiltersScore);
		void Visit(ITermFilter customFiltersScore);
		void Visit(IScriptFilter customFiltersScore);
		void Visit(IRegexpFilter customFiltersScore);
		void Visit(IRangeFilter customFiltersScore);
		void Visit(IQueryFilter customFiltersScore);
		void Visit(IPrefixFilter customFiltersScore);
		void Visit(IOrFilter customFiltersScore);
		void Visit(INotFilter customFiltersScore);
		void Visit(INestedFilterDescriptor customFiltersScore);
		void Visit(IMissingFilter customFiltersScore);
		void Visit(IMatchAllFilter customFiltersScore);
		void Visit(ILimitFilter customFiltersScore);
		void Visit(IIdsFilter customFiltersScore);
		void Visit(IHasParentFilter customFiltersScore);
		void Visit(IHasChildFilter customFiltersScore);
		void Visit(IGeoShapeBaseFilter customFiltersScore);
		void Visit(IGeoPolygonFilter customFiltersScore);
		void Visit(IGeoDistanceRangeFilter customFiltersScore);
		void Visit(IGeoDistanceFilter customFiltersScore);
		void Visit(IGeoBoundingBoxFilter customFiltersScore);
		void Visit(IExistsFilter customFiltersScore);
		void Visit(IBoolFilter customFiltersScore);
		void Visit(IAndFilter customFiltersScore);
		void Visit(IFilterBase customFiltersScore);
		void Visit(IFilterDescriptor customFiltersScore);
	}

	public class QueryVisitor : IQueryVisitor
	{
	
		public int Depth { get; set; }

		public VisitorScope Scope { get; set; }

		public virtual void Visit(IQueryVisitor visitor)
		{
		}

		public virtual void Visit(IQueryDescriptor baseQuery)
		{
		}

		public virtual void Visit(IBoolQuery baseQuery)
		{
		}

		public virtual void Visit(IBoostingQuery baseQuery)
		{
		}

		public virtual void Visit(ICommonTermsQuery commonTerms)
		{
		}

		public virtual void Visit(IConstantScoreQuery constantScore)
		{
		}

		public virtual void Visit(ICustomBoostFactorQuery customBoostFactor)
		{
		}

		public virtual void Visit(ICustomFiltersScoreQuery customFiltersScore)
		{
		}

		public virtual void Visit(ICustomScoreQuery customFiltersScore)
		{
		}

		public virtual void Visit(IDisMaxQuery customFiltersScore)
		{
		}

		public virtual void Visit(IFilteredQuery customFiltersScore)
		{
		}

		public virtual void Visit(IFunctionScoreQuery customFiltersScore)
		{
		}

		public virtual void Visit(IFuzzyQuery customFiltersScore)
		{
		}

		public virtual void Visit(IFuzzyLikeThisQuery customFiltersScore)
		{
		}

		public virtual void Visit(IGeoShapeQuery customFiltersScore)
		{
		}

		public virtual void Visit(IHasChildQuery customFiltersScore)
		{
		}

		public virtual void Visit(IHasParentQuery customFiltersScore)
		{
		}

		public virtual void Visit(IdsQuery customFiltersScore)
		{
		}

		public virtual void Visit(IIndicesQuery customFiltersScore)
		{
		}

		public virtual void Visit(IMatchQuery customFiltersScore)
		{
		}

		public virtual void Visit(MatchAll customFiltersScore)
		{
		}

		public virtual void Visit(IMoreLikeThisQuery customFiltersScore)
		{
		}

		public virtual void Visit(IMultiMatchQuery customFiltersScore)
		{
		}

		public virtual void Visit(INestedQuery customFiltersScore)
		{
		}

		public virtual void Visit(IPrefixQuery customFiltersScore)
		{
		}

		public virtual void Visit(IQueryStringQuery customFiltersScore)
		{
		}

		public virtual void Visit(IRangeQuery customFiltersScore)
		{
		}

		public virtual void Visit(IRegexpQuery customFiltersScore)
		{
		}

		public virtual void Visit(ISimpleQueryStringQuery customFiltersScore)
		{
		}

		public virtual void Visit(ISpanFirstQuery customFiltersScore)
		{
		}

		public virtual void Visit(ISpanNearQuery customFiltersScore)
		{
		}

		public virtual void Visit(ISpanNotQuery customFiltersScore)
		{
		}

		public virtual void Visit(ISpanOrQuery customFiltersScore)
		{
		}

		public virtual void Visit(ISpanTermQuery customFiltersScore)
		{
		}

		public virtual void Visit(ITermQuery customFiltersScore)
		{
		}

		public virtual void Visit(IWildcardQuery customFiltersScore)
		{
		}

		public virtual void Visit(ITermsQuery customFiltersScore)
		{
		}

		public virtual void Visit(ITopChildrenQuery customFiltersScore)
		{
		}

		public virtual void Visit(IQuery query)
		{
		}

		public virtual void Visit(BaseFilterDescriptor customFiltersScore)
		{
		}

		public virtual void Visit(ITypeFilter customFiltersScore)
		{
		}

		public void Visit(ITermsBaseFilter customFiltersScore)
		{
		}

		public virtual void Visit(ITermFilter customFiltersScore)
		{
		}

		public virtual void Visit(IScriptFilter customFiltersScore)
		{
		}

		public virtual void Visit(IRegexpFilter customFiltersScore)
		{
		}

		public virtual void Visit(IRangeFilter customFiltersScore)
		{
		}

		public virtual void Visit(IQueryFilter customFiltersScore)
		{
		}

		public virtual void Visit(IPrefixFilter customFiltersScore)
		{
		}

		public virtual void Visit(IOrFilter customFiltersScore)
		{
		}

		public virtual void Visit(INotFilter customFiltersScore)
		{
		}

		public virtual void Visit(INestedFilterDescriptor customFiltersScore)
		{
		}

		public virtual void Visit(IMissingFilter customFiltersScore)
		{
		}

		public virtual void Visit(IMatchAllFilter customFiltersScore)
		{
		}

		public virtual void Visit(ILimitFilter customFiltersScore)
		{
		}

		public virtual void Visit(IIdsFilter customFiltersScore)
		{
		}

		public virtual void Visit(IHasParentFilter customFiltersScore)
		{
		}

		public virtual void Visit(IHasChildFilter customFiltersScore)
		{
		}

		public virtual void Visit(IGeoShapeBaseFilter customFiltersScore)
		{
		}

		public virtual void Visit(IGeoPolygonFilter customFiltersScore)
		{
		}

		public virtual void Visit(IGeoDistanceRangeFilter customFiltersScore)
		{
		}

		public virtual void Visit(IGeoDistanceFilter customFiltersScore)
		{
		}

		public virtual void Visit(IGeoBoundingBoxFilter customFiltersScore)
		{
		}

		public virtual void Visit(IExistsFilter customFiltersScore)
		{
		}

		public virtual void Visit(IBoolFilter customFiltersScore)
		{
		}

		public virtual void Visit(IAndFilter customFiltersScore)
		{
		}

		public virtual void Visit(IFilterBase customFiltersScore)
		{
		}

		public virtual void Visit(IFilterDescriptor customFiltersScore)
		{
		}
	}
}
