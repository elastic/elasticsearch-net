namespace Nest
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
		void Visit(IFunctionScoreQuery query);
		void Visit(IFuzzyQuery query);
		void Visit(IFuzzyNumericQuery query);
		void Visit(IFuzzyDateQuery query);
		void Visit(IFuzzyStringQuery query);
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
		void Visit(IGeoHashCellQuery query);
		void Visit(IExistsQuery filter);
		void Visit(IDateRangeQuery query);
		void Visit(INumericRangeQuery query);
		void Visit(ITermRangeQuery query);
		void Visit(ITemplateQuery query);

#pragma warning disable 618
		void Visit(ILimitQuery query);
		void Visit(IFilteredQuery query);
		void Visit(INotQuery query);
		void Visit(IOrQuery query);
		void Visit(IAndQuery query);
#pragma warning restore 618

		void Visit(ISpanFirstQuery query);
		void Visit(ISpanNearQuery query);
		void Visit(ISpanNotQuery query);
		void Visit(ISpanOrQuery query);
		void Visit(ISpanTermQuery query);
		void Visit(ISpanQuery query);
		void Visit(ISpanSubQuery query);
		void Visit(ISpanContainingQuery query);
		void Visit(ISpanWithinQuery query);
		void Visit(ISpanMultiTermQuery query);

		void Visit(IGeoIndexedShapeQuery query);
		void Visit(IGeoShapeQuery query);
		void Visit(IGeoShapeMultiPointQuery query);
		void Visit(IGeoShapeMultiPolygonQuery query);
		void Visit(IGeoShapePolygonQuery query);
		void Visit(IGeoShapePointQuery query);
		void Visit(IGeoShapeMultiLineStringQuery query);
		void Visit(IGeoShapeLineStringQuery query);
		void Visit(IGeoShapeEnvelopeQuery query);
		void Visit(IGeoShapeCircleQuery query);
		void Visit(IRawQuery query);
	}

	public class QueryVisitor : IQueryVisitor
	{
		public int Depth { get; set; }

		public VisitorScope Scope { get; set; }

		public virtual void Visit(IQueryVisitor visitor) { }

		public virtual void Visit(IQueryContainer query) { }

		public virtual void Visit(IQuery query) { }

		public virtual void Visit(IBoolQuery query) { }

		public virtual void Visit(IBoostingQuery query) { }

		public virtual void Visit(ICommonTermsQuery query) { }

		public virtual void Visit(IConstantScoreQuery query) { }

		public virtual void Visit(IDisMaxQuery query) { }

#pragma warning disable 618
		public virtual void Visit(ILimitQuery query) { }

		public virtual void Visit(IFilteredQuery query) { }

		public virtual void Visit(INotQuery query) { }

		public virtual void Visit(IOrQuery query) { }

		public virtual void Visit(IAndQuery query) { }
#pragma warning restore 618

		public virtual void Visit(IGeoIndexedShapeQuery query) { }

		public virtual void Visit(ISpanContainingQuery query) { }

		public virtual void Visit(ISpanWithinQuery query) { }

		public virtual void Visit(IDateRangeQuery query) { }

		public virtual void Visit(INumericRangeQuery query) { }

        public virtual void Visit(ITermRangeQuery query) { }

		public virtual void Visit(IFunctionScoreQuery query) { }

		public virtual void Visit(IFuzzyQuery query) { }

		public virtual void Visit(IFuzzyStringQuery query) { }

		public virtual void Visit(IFuzzyNumericQuery query) { }

		public virtual void Visit(IFuzzyDateQuery query) { }

		public virtual void Visit(IGeoShapeQuery query) { }

		public virtual void Visit(IHasChildQuery query) { }

		public virtual void Visit(IHasParentQuery query) { }

		public virtual void Visit(IIdsQuery query) { }

		public virtual void Visit(IIndicesQuery query) { }

		public virtual void Visit(IMatchQuery query) { }

		public virtual void Visit(IMatchAllQuery query) { }

		public virtual void Visit(IMoreLikeThisQuery query) { }

		public virtual void Visit(IMultiMatchQuery query) { }

		public virtual void Visit(INestedQuery query) { }

		public virtual void Visit(IPrefixQuery query) { }

		public virtual void Visit(IQueryStringQuery query) { }

		public virtual void Visit(IRangeQuery query) { }

		public virtual void Visit(IRegexpQuery query) { }

		public virtual void Visit(ISimpleQueryStringQuery query) { }

		public virtual void Visit(ISpanFirstQuery query) { }

		public virtual void Visit(ISpanNearQuery query) { }

		public virtual void Visit(ISpanNotQuery query) { }

		public virtual void Visit(ISpanOrQuery query) { }

		public virtual void Visit(ISpanTermQuery query) { }

		public virtual void Visit(ITermQuery query) { }

		public virtual void Visit(IWildcardQuery query) { }

		public virtual void Visit(ITermsQuery query) { }

		public virtual void Visit(ITypeQuery query) { }

		public virtual void Visit(IScriptQuery query) { }

		public virtual void Visit(IMissingQuery query) { }

		public virtual void Visit(IGeoPolygonQuery query) { }

		public virtual void Visit(IGeoDistanceRangeQuery query) { }

		public virtual void Visit(IGeoDistanceQuery query) { }

		public virtual void Visit(IGeoHashCellQuery query) { }

		public virtual void Visit(ITemplateQuery query) { }

		public virtual void Visit(ISpanMultiTermQuery query) { }

		public virtual void Visit(IGeoShapeMultiPointQuery query) { }

		public virtual void Visit(IGeoShapeMultiPolygonQuery query) { }

		public virtual void Visit(IGeoShapePolygonQuery query) { }

		public virtual void Visit(IGeoShapePointQuery query) { }

		public virtual void Visit(IGeoShapeMultiLineStringQuery query) { }

		public virtual void Visit(IGeoShapeLineStringQuery query) { }

		public virtual void Visit(IGeoShapeEnvelopeQuery query) { }

		public virtual void Visit(ISpanSubQuery query) { }

		public virtual void Visit(IGeoShapeCircleQuery query) { }

		public virtual void Visit(ISpanQuery query) { }

		public virtual void Visit(IGeoBoundingBoxQuery query) { }

		public virtual void Visit(IExistsQuery query) { }

		public virtual void Visit(IRawQuery query) { }
	}
}
