using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The method for transforming the data. Defines the pivot function group by fields and the aggregation to reduce the data
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(TransformPivot))]
	public interface ITransformPivot
	{
		/// <summary>
		/// Defines the initial page size to use for the composite aggregation for each checkpoint. If circuit breaker exceptions occur, the page
		/// size is dynamically adjusted to a lower value. The minimum value is 10 and the maximum is 10,000. The default value is 500.
		/// </summary>
		[DataMember(Name = "max_page_search_size")]
		int? MaxPageSearchSize { get; set; }

		/// <summary>
		/// Defines how to aggregate the grouped data.
		/// <para />
		/// Only a subset of aggregations are supported.
		/// </summary>
		[DataMember(Name = "aggs")]
		AggregationDictionary Aggregations { get; set; }

		/// <summary>
		/// Defines how to group the data. More than one grouping can be defined per pivot.
		/// </summary>
		[DataMember(Name ="group_by")]
		IEnumerable<ISingleGroupSource> GroupBy { get; set; }
	}

	/// <inheritdoc />
	public class TransformPivot : ITransformPivot
	{
		/// <inheritdoc />
		public int? MaxPageSearchSize { get; set; }

		/// <inheritdoc />
		public AggregationDictionary Aggregations { get; set; }

		/// <inheritdoc />
		public IEnumerable<ISingleGroupSource> GroupBy { get; set; }
	}

	/// <inheritdoc cref="ITransformPivot"/>
	public class TransformPivotDescriptor<TInferDocument> : DescriptorBase<TransformPivotDescriptor<TInferDocument>, ITransformPivot>, ITransformPivot
		where TInferDocument : class
	{
		int? ITransformPivot.MaxPageSearchSize { get; set; }
		AggregationDictionary ITransformPivot.Aggregations { get; set; }
		IEnumerable<ISingleGroupSource> ITransformPivot.GroupBy { get; set; }

		/// <inheritdoc cref="ITransformPivot.MaxPageSearchSize" />
		public TransformPivotDescriptor<TInferDocument> MaxPageSearchSize(int? maxPageSearchSize) =>
			Assign(maxPageSearchSize, (a, v) => a.MaxPageSearchSize = v);

		/// <inheritdoc cref="ITransformPivot.Aggregations" />
		public TransformPivotDescriptor<TInferDocument> Aggregations(
			Func<AggregationContainerDescriptor<TInferDocument>, IAggregationContainer> aggregationsSelector
		) =>
			Assign(aggregationsSelector(new AggregationContainerDescriptor<TInferDocument>())?.Aggregations, (a, v) => a.Aggregations = v);

		/// <inheritdoc cref="ITransformPivot.Aggregations" />
		public TransformPivotDescriptor<TInferDocument> Aggregations(AggregationDictionary aggregations) =>
			Assign(aggregations, (a, v) => a.Aggregations = v);

		/// <inheritdoc cref="ITransformPivot.GroupBy" />
		public TransformPivotDescriptor<TInferDocument> GroupBy(
			Func<SingleGroupSourcesDescriptor<TInferDocument>, IPromise<IList<ISingleGroupSource>>> selector
		) =>
			Assign(selector, (a, v) => a.GroupBy = v?.Invoke(new SingleGroupSourcesDescriptor<TInferDocument>())?.Value);

	}
}
