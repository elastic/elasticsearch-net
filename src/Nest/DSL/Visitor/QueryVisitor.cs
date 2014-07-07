namespace Nest.DSL.Visitor
{
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

		/// <summary>
		/// Visit the query container just before we dispatch into the query it holds
		/// </summary>
		/// <param name="queryDescriptor"></param>
		void Visit(IQueryContainer queryDescriptor);		
		
		/// <summary>
		/// Visit every query item just before they are visited by their specialized Visit() implementation
		/// </summary>
		/// <param name="query">The IQuery object that will be visited</param>
		void Visit(IQuery query);
		
		/// <summary>
		/// Visit the filter container just before we dispatch into the filter it holds
		/// </summary>
		/// <param name="filterDescriptor"></param>
		void Visit(IFilterContainer filterDescriptor);

		/// <summary>
		/// Visit every filer item just before they are visited by their specialized Visit() implementation
		/// </summary>
		/// <param name="filter">The IFilterBase object that will be visited</param>
		void Visit(IFilter filter);
	
		void Visit(IBoolQuery query);
		void Visit(IBoostingQuery query);
		void Visit(ICommonTermsQuery query);
		void Visit(IConstantScoreQuery query);
		void Visit(ICustomBoostFactorQuery query);
		void Visit(ICustomFiltersScoreQuery query);
		void Visit(ICustomScoreQuery query);
		void Visit(IDisMaxQuery query);
		void Visit(IFilteredQuery query);
		void Visit(IFunctionScoreQuery query);
		void Visit(IFuzzyQuery query);
		void Visit(IFuzzyLikeThisQuery query);
		void Visit(IGeoShapeQuery query);
		void Visit(IHasChildQuery query);
		void Visit(IHasParentQuery query);
		void Visit(IIdsQuery query);
		void Visit(IIndicesQuery query);
		void Visit(IMatchQuery query);
		void Visit(IMatchAllQuery query);
		void Visit(IMoreLikeThisQuery query);
		void Visit(IMultiMatchQuery query);
		void Visit(INestedQuery query);
		void Visit(IPrefixQuery query);
		void Visit(IQueryStringQuery query);
		void Visit(IRangeQuery query);
		void Visit(IRegexpQuery query);
		void Visit(ISimpleQueryStringQuery query);
		void Visit(ISpanFirstQuery query);
		void Visit(ISpanNearQuery query);
		void Visit(ISpanNotQuery query);
		void Visit(ISpanOrQuery query);
		void Visit(ISpanTermQuery query);
		void Visit(ITermQuery query);
		void Visit(IWildcardQuery query);
		void Visit(ITermsQuery query);
		void Visit(ITopChildrenQuery query);


		void Visit(ITypeFilter filter);
		void Visit(ITermsBaseFilter filter);
		void Visit(ITermFilter filter);
		void Visit(IScriptFilter filter);
		void Visit(IRegexpFilter filter);
		void Visit(IRangeFilter filter);
		void Visit(IQueryFilter filter);
		void Visit(IPrefixFilter filter);
		void Visit(IOrFilter filter);
		void Visit(INotFilter filter);
		void Visit(INestedFilter filter);
		void Visit(IMissingFilter filter);
		void Visit(IMatchAllFilter filter);
		void Visit(ILimitFilter filter);
		void Visit(IIdsFilter filter);
		void Visit(IHasParentFilter filter);
		void Visit(IHasChildFilter filter);
		void Visit(IGeoShapeBaseFilter filter);
		void Visit(IGeoPolygonFilter filter);
		void Visit(IGeoDistanceRangeFilter filter);
		void Visit(IGeoDistanceFilter filter);
		void Visit(IGeoBoundingBoxFilter filter);
		void Visit(IExistsFilter filter);
		void Visit(IBoolFilter filter);
		void Visit(IAndFilter filter);

	}

	public class QueryVisitor : IQueryVisitor
	{
		
		public int Depth { get; set; }

		public VisitorScope Scope { get; set; }

		public virtual void Visit(IQueryVisitor visitor)
		{
		}

		public virtual void Visit(IQueryContainer baseQuery)
		{
		}

		public virtual void Visit(IQuery query)
		{
		}

		public virtual void Visit(IFilterContainer customFiltersScore)
		{
		}

		public virtual void Visit(IFilter customFiltersScore)
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

		public virtual void Visit(IIdsQuery customFiltersScore)
		{
		}

		public virtual void Visit(IIndicesQuery customFiltersScore)
		{
		}

		public virtual void Visit(IMatchQuery customFiltersScore)
		{
		}

		public virtual void Visit(IMatchAllQuery customFiltersScore)
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

		public virtual void Visit(INestedFilter customFiltersScore)
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

	
	}
}
