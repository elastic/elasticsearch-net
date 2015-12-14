namespace Nest.QueryDsl.Visitor
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
	
		void Visit(IBoolQuery query);
		void Visit(IBoostingQuery query);
		void Visit(ICommonTermsQuery query);
		void Visit(IConstantScoreQuery query);
		void Visit(IDisMaxQuery query);
#pragma warning disable 618
		void Visit(IFilteredQuery query);
#pragma warning restore 618
		void Visit(IFunctionScoreQuery query);
		void Visit(IFuzzyQuery query);
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
		void Visit(ITypeQuery filter);
		void Visit(IScriptQuery filter);
		void Visit(IMissingQuery filter);
		void Visit(IGeoPolygonQuery filter);
		void Visit(IGeoDistanceRangeQuery filter);
		void Visit(IGeoDistanceQuery filter);
		void Visit(IGeoBoundingBoxQuery filter);
		void Visit(IExistsQuery filter);
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

		public virtual void Visit(IDisMaxQuery customFiltersScore)
		{
		}

#pragma warning disable 618
		public virtual void Visit(IFilteredQuery customFiltersScore)
		{
		}
#pragma warning restore 618

		public virtual void Visit(IFunctionScoreQuery customFiltersScore)
		{
		}

		public virtual void Visit(IFuzzyQuery customFiltersScore)
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


		public virtual void Visit(ITypeQuery customFiltersScore)
		{
		}

		public virtual void Visit(IScriptQuery customFiltersScore)
		{
		}

		public virtual void Visit(IMissingQuery customFiltersScore)
		{
		}

		public virtual void Visit(IGeoPolygonQuery customFiltersScore)
		{
		}

		public virtual void Visit(IGeoDistanceRangeQuery customFiltersScore)
		{
		}

		public virtual void Visit(IGeoDistanceQuery customFiltersScore)
		{
		}

        public virtual void Visit(IGeoHashCellQuery filter)
        {
        }

		public virtual void Visit(IGeoBoundingBoxQuery customFiltersScore)
		{
		}

		public virtual void Visit(IExistsQuery customFiltersScore)
		{
		}
	}
}
