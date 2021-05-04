// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(HistogramAggregation))]
	public interface IHistogramAggregation : IBucketAggregation
	{
		[DataMember(Name ="extended_bounds")]
		ExtendedBounds<double> ExtendedBounds { get; set; }

		[DataMember(Name = "hard_bounds")]
		HardBounds<double> HardBounds { get; set; }

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
		public HardBounds<double> HardBounds { get; set; }
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
		HardBounds<double> IHistogramAggregation.HardBounds { get; set; }
		Field IHistogramAggregation.Field { get; set; }

		double? IHistogramAggregation.Interval { get; set; }

		int? IHistogramAggregation.MinimumDocumentCount { get; set; }

		double? IHistogramAggregation.Missing { get; set; }

		double? IHistogramAggregation.Offset { get; set; }

		HistogramOrder IHistogramAggregation.Order { get; set; }

		IScript IHistogramAggregation.Script { get; set; }

		public HistogramAggregationDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public HistogramAggregationDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.Field = v);

		public HistogramAggregationDescriptor<T> Script(string script) => Assign((InlineScript)script, (a, v) => a.Script = v);

		public HistogramAggregationDescriptor<T> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));

		public HistogramAggregationDescriptor<T> Interval(double? interval) => Assign(interval, (a, v) => a.Interval = v);

		public HistogramAggregationDescriptor<T> MinimumDocumentCount(int? minimumDocumentCount) =>
			Assign(minimumDocumentCount, (a, v) => a.MinimumDocumentCount = v);

		public HistogramAggregationDescriptor<T> Order(HistogramOrder order) => Assign(order, (a, v) => a.Order = v);

		public HistogramAggregationDescriptor<T> OrderAscending(string key) =>
			Assign(new HistogramOrder { Key = key, Order = SortOrder.Descending }, (a, v) => a.Order = v);

		public HistogramAggregationDescriptor<T> OrderDescending(string key) =>
			Assign(new HistogramOrder { Key = key, Order = SortOrder.Descending }, (a, v) => a.Order = v);

		public HistogramAggregationDescriptor<T> ExtendedBounds(double min, double max) =>
			Assign(new ExtendedBounds<double> { Minimum = min, Maximum = max }, (a, v) => a.ExtendedBounds = v);

		public HistogramAggregationDescriptor<T> HardBounds(double min, double max) =>
			Assign(new HardBounds<double> { Minimum = min, Maximum = max }, (a, v) => a.HardBounds = v);

		public HistogramAggregationDescriptor<T> Offset(double? offset) => Assign(offset, (a, v) => a.Offset = v);

		public HistogramAggregationDescriptor<T> Missing(double? missing) => Assign(missing, (a, v) => a.Missing = v);
	}
}
