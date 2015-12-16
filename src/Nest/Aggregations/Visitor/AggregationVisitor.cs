using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Aggregations.Visitor
{

    public interface IAggregationVisitor
    {
        /// <summary>
		/// The current depth of the node being visited
		/// </summary>
		int Depth { get; set; }

        /// <summary>
		/// Hints the relation with the parent, i.e aggregations contained inside a bucket aggregation will have VisitorScope.Bucket set.
		/// </summary>
        VisitorScope Scope { get; set; }

        /// <summary>
		/// Visit the aggregation container just before we dispatch into the aggregation it holds
		/// </summary>
		/// <param name="aggregationContainer"></param>
		void Visit(IAggregationContainer aggregationContainer);

        /// <summary>
		/// Visit every aggregation item just before they are visited by their specialized Visit() implementation
		/// </summary>
		/// <param name="aggregation">The IAggregation object that will be visited</param>
		void Visit(IAggregation aggregation);
        void Visit(IAverageAggregation aggregation);
        void Visit(IValueCountAggregation aggregation);
        void Visit(IMaxAggregation aggregation);
        void Visit(IMinAggregation aggregation);
        void Visit(IStatsAggregator aggregation);
        void Visit(ISumAggregation aggregation);
        void Visit(IExtendedStatsAggregation aggregation);
        void Visit(IDateHistogramAggregation aggregation);
        void Visit(IPercentilesAggregation aggregation);
        void Visit(IDateRangeAggregation aggregation);
        void Visit(IFilterAggregation aggregation);
        void Visit(IFiltersAggregation aggregation);
        void Visit(IGeoDistanceAggregation aggregation);
        void Visit(IGeoHashGridAggregation aggregation);
        void Visit(IGeoBoundsAggregation aggregation);
        void Visit(IHistogramAggregation aggregation);
        void Visit(IGlobalAggregation aggregation);
        void Visit(IIpRangeAggregation aggregation);
        void Visit(ICardinalityAggregation aggregation);
        void Visit(IMissingAggregation aggregation);
        void Visit(INestedAggregation aggregation);
        void Visit(IReverseNestedAggregation aggregation);
        void Visit(IRangeAggregation aggregation);
        void Visit(ITermsAggregation aggregation);
        void Visit(ISignificantTermsAggregation aggregation);
        void Visit(IPercentileRanksAggregation aggregation);
        void Visit(ITopHitsAggregation aggregation);
        void Visit(IChildrenAggregation aggregation);
        void Visit(IScriptedMetricAggregation aggregation);
        void Visit(IAverageBucketAggregation aggregation);
        void Visit(IDerivativeAggregation aggregation);
        void Visit(IMaxBucketAggregation aggregation);
        void Visit(IMinBucketAggregation aggregation);
        void Visit(ISumBucketAggregation aggregation);
        void Visit(IMovingAverageAggregation aggregation);
        void Visit(ICumulativeSumAggregation aggregation);
        void Visit(ISerialDifferencingAggregation aggregation);
        void Visit(IBucketScriptAggregation aggregation);
        void Visit(IBucketSelectorAggregation aggregation);
        void Visit(ISamplerAggregation aggregation);
    }

    public class AggregationVisitor : IAggregationVisitor
    {
        public int Depth { get; set; }

        public VisitorScope Scope { get; set; }

        public void Visit(IValueCountAggregation aggregation)
        {
        }

        public void Visit(IMinAggregation aggregation)
        {
        }

        public void Visit(ISumAggregation aggregation)
        {
        }

        public void Visit(IDateHistogramAggregation aggregation)
        {
        }

        public void Visit(IDateRangeAggregation aggregation)
        {
        }

        public void Visit(IFiltersAggregation aggregation)
        {
        }

        public void Visit(IGeoHashGridAggregation aggregation)
        {
        }

        public void Visit(IHistogramAggregation aggregation)
        {
        }

        public void Visit(IIpRangeAggregation aggregation)
        {
        }

        public void Visit(IMissingAggregation aggregation)
        {
        }

        public void Visit(IReverseNestedAggregation aggregation)
        {
        }

        public void Visit(ITermsAggregation aggregation)
        {
        }

        public void Visit(IPercentileRanksAggregation aggregation)
        {
        }

        public void Visit(IChildrenAggregation aggregation)
        {
        }

        public void Visit(IAverageBucketAggregation aggregation)
        {
        }

        public void Visit(IMaxBucketAggregation aggregation)
        {
        }

        public void Visit(ISumBucketAggregation aggregation)
        {
        }

        public void Visit(ICumulativeSumAggregation aggregation)
        {
        }

        public void Visit(IBucketScriptAggregation aggregation)
        {
        }

        public void Visit(ISamplerAggregation aggregation)
        {
        }

        public void Visit(IBucketSelectorAggregation aggregation)
        {
        }

        public void Visit(ISerialDifferencingAggregation aggregation)
        {
        }

        public void Visit(IMovingAverageAggregation aggregation)
        {
        }

        public void Visit(IMinBucketAggregation aggregation)
        {
        }

        public void Visit(IDerivativeAggregation aggregation)
        {
        }

        public void Visit(IScriptedMetricAggregation aggregation)
        {
        }

        public void Visit(ITopHitsAggregation aggregation)
        {
        }

        public void Visit(ISignificantTermsAggregation aggregation)
        {
        }

        public void Visit(IRangeAggregation aggregation)
        {
        }

        public void Visit(INestedAggregation aggregation)
        {
        }

        public void Visit(ICardinalityAggregation aggregation)
        {
        }

        public void Visit(IGlobalAggregation aggregation)
        {
        }

        public void Visit(IGeoBoundsAggregation aggregation)
        {
        }

        public void Visit(IGeoDistanceAggregation aggregation)
        {
        }

        public void Visit(IFilterAggregation aggregation)
        {
        }

        public void Visit(IPercentilesAggregation aggregation)
        {
        }

        public void Visit(IExtendedStatsAggregation aggregation)
        {
        }

        public void Visit(IStatsAggregator aggregation)
        {
        }

        public void Visit(IMaxAggregation aggregation)
        {
        }

        public void Visit(IAverageAggregation aggregation)
        {
        }

        public void Visit(IAggregation aggregation)
        {
        }

        public void Visit(IAggregationContainer aggregationContainer)
        {
        }
    }
}
