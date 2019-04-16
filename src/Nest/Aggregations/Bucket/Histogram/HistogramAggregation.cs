using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(HistogramAggregation))]
	public interface IHistogramAggregation : IBucketAggregation
	{
		[DataMember(Name ="extended_bounds")]
		ExtendedBounds<double> ExtendedBounds { get; set; }

		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="interval")]
		double? Interval { get; set; }

		[DataMember(Name ="min_doc_count")]
		int? MinimumDocumentCount { get; set; }

		[DataMember(Name ="missing")]
		double? Missing { get; set; }

		[DataMember(Name ="offset")]
		double? Offset { get; set; }

		[DataMember(Name ="order")]
		HistogramOrder Order { get; set; }

		[DataMember(Name ="script")]
		IScript Script { get; set; }
	}

	public class HistogramAggregation : BucketAggregationBase, IHistogramAggregation
	{
		internal HistogramAggregation() { }

		public HistogramAggregation(string name) : base(name) { }

		public ExtendedBounds<double> ExtendedBounds { get; set; }
		public Field Field { get; set; }
		public double? Interval { get; set; }
		public int? MinimumDocumentCount { get; set; }
		public double? Missing { get; set; }
		public double? Offset { get; set; }
		public HistogramOrder Order { get; set; }
		public IScript Script { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.Histogram = this;
	}

	public class HistogramAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<HistogramAggregationDescriptor<T>, IHistogramAggregation, T>, IHistogramAggregation
		where T : class
	{
		ExtendedBounds<double> IHistogramAggregation.ExtendedBounds { get; set; }
		Field IHistogramAggregation.Field { get; set; }

		double? IHistogramAggregation.Interval { get; set; }

		int? IHistogramAggregation.MinimumDocumentCount { get; set; }

		double? IHistogramAggregation.Missing { get; set; }

		double? IHistogramAggregation.Offset { get; set; }

		HistogramOrder IHistogramAggregation.Order { get; set; }

		IScript IHistogramAggregation.Script { get; set; }

		public HistogramAggregationDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public HistogramAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public HistogramAggregationDescriptor<T> Script(string script) => Assign(a => a.Script = (InlineScript)script);

		public HistogramAggregationDescriptor<T> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptDescriptor()));

		public HistogramAggregationDescriptor<T> Interval(double? interval) => Assign(a => a.Interval = interval);

		public HistogramAggregationDescriptor<T> MinimumDocumentCount(int? minimumDocumentCount) =>
			Assign(a => a.MinimumDocumentCount = minimumDocumentCount);

		public HistogramAggregationDescriptor<T> Order(HistogramOrder order) => Assign(a => a.Order = order);

		public HistogramAggregationDescriptor<T> OrderAscending(string key) =>
			Assign(a => a.Order = new HistogramOrder { Key = key, Order = SortOrder.Descending });

		public HistogramAggregationDescriptor<T> OrderDescending(string key) =>
			Assign(a => a.Order = new HistogramOrder { Key = key, Order = SortOrder.Descending });

		public HistogramAggregationDescriptor<T> ExtendedBounds(double min, double max) =>
			Assign(a => a.ExtendedBounds = new ExtendedBounds<double> { Minimum = min, Maximum = max });

		public HistogramAggregationDescriptor<T> Offset(double? offset) => Assign(a => a.Offset = offset);

		public HistogramAggregationDescriptor<T> Missing(double? missing) => Assign(a => a.Missing = missing);
	}
}
