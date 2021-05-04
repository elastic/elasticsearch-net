// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[MapsApi("ml.validate_detector.json")]
	[JsonFormatter(typeof(ValidateDetectorRequestFormatter))]
	public partial interface IValidateDetectorRequest
	{
		[IgnoreDataMember]
		IDetector Detector { get; set; }
	}

	/// <inheritdoc />
	public partial class ValidateDetectorRequest
	{
		/// <inheritdoc />
		public IDetector Detector { get; set; }
	}

	/// <inheritdoc />
	public partial class ValidateDetectorDescriptor<TDocument> where TDocument : class
	{
		IDetector IValidateDetectorRequest.Detector { get; set; }

		public ValidateDetectorDescriptor<TDocument> Count(Func<CountDetectorDescriptor<TDocument>, ICountDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new CountDetectorDescriptor<TDocument>(CountFunction.Count)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> HighCount(Func<CountDetectorDescriptor<TDocument>, ICountDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new CountDetectorDescriptor<TDocument>(CountFunction.HighCount)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> LowCount(Func<CountDetectorDescriptor<TDocument>, ICountDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new CountDetectorDescriptor<TDocument>(CountFunction.LowCount)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> NonZeroCount(Func<NonZeroCountDetectorDescriptor<TDocument>, INonZeroCountDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new NonZeroCountDetectorDescriptor<TDocument>(NonZeroCountFunction.NonZeroCount)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> HighNonZeroCount(Func<NonZeroCountDetectorDescriptor<TDocument>, INonZeroCountDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new NonZeroCountDetectorDescriptor<TDocument>(NonZeroCountFunction.HighNonZeroCount)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> LowNonZeroCount(Func<NonZeroCountDetectorDescriptor<TDocument>, INonZeroCountDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new NonZeroCountDetectorDescriptor<TDocument>(NonZeroCountFunction.LowNonZeroCount)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> DistinctCount(Func<DistinctCountDetectorDescriptor<TDocument>, IDistinctCountDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new DistinctCountDetectorDescriptor<TDocument>(DistinctCountFunction.DistinctCount)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> HighDistinctCount(Func<DistinctCountDetectorDescriptor<TDocument>, IDistinctCountDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new DistinctCountDetectorDescriptor<TDocument>(DistinctCountFunction.HighDistinctCount)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> LowDistinctCount(Func<DistinctCountDetectorDescriptor<TDocument>, IDistinctCountDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new DistinctCountDetectorDescriptor<TDocument>(DistinctCountFunction.LowDistinctCount)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> InfoContent(Func<InfoContentDetectorDescriptor<TDocument>, IInfoContentDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new InfoContentDetectorDescriptor<TDocument>(InfoContentFunction.InfoContent)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> HighInfoContent(Func<InfoContentDetectorDescriptor<TDocument>, IInfoContentDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new InfoContentDetectorDescriptor<TDocument>(InfoContentFunction.HighInfoContent)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> LowInfoContent(Func<InfoContentDetectorDescriptor<TDocument>, IInfoContentDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new InfoContentDetectorDescriptor<TDocument>(InfoContentFunction.LowInfoContent)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> Min(Func<MetricDetectorDescriptor<TDocument>, IMetricDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new MetricDetectorDescriptor<TDocument>(MetricFunction.Min)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> Max(Func<MetricDetectorDescriptor<TDocument>, IMetricDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new MetricDetectorDescriptor<TDocument>(MetricFunction.Max)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> Median(Func<MetricDetectorDescriptor<TDocument>, IMetricDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new MetricDetectorDescriptor<TDocument>(MetricFunction.Median)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> HighMedian(Func<MetricDetectorDescriptor<TDocument>, IMetricDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new MetricDetectorDescriptor<TDocument>(MetricFunction.HighMedian)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> LowMedian(Func<MetricDetectorDescriptor<TDocument>, IMetricDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new MetricDetectorDescriptor<TDocument>(MetricFunction.LowMedian)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> Mean(Func<MetricDetectorDescriptor<TDocument>, IMetricDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new MetricDetectorDescriptor<TDocument>(MetricFunction.Mean)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> HighMean(Func<MetricDetectorDescriptor<TDocument>, IMetricDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new MetricDetectorDescriptor<TDocument>(MetricFunction.HighMean)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> LowMean(Func<MetricDetectorDescriptor<TDocument>, IMetricDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new MetricDetectorDescriptor<TDocument>(MetricFunction.LowMean)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> Metric(Func<MetricDetectorDescriptor<TDocument>, IMetricDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new MetricDetectorDescriptor<TDocument>(MetricFunction.Metric)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> Varp(Func<MetricDetectorDescriptor<TDocument>, IMetricDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new MetricDetectorDescriptor<TDocument>(MetricFunction.Varp)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> HighVarp(Func<MetricDetectorDescriptor<TDocument>, IMetricDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new MetricDetectorDescriptor<TDocument>(MetricFunction.HighVarp)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> LowVarp(Func<MetricDetectorDescriptor<TDocument>, IMetricDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new MetricDetectorDescriptor<TDocument>(MetricFunction.LowVarp)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> Rare(Func<RareDetectorDescriptor<TDocument>, IRareDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new RareDetectorDescriptor<TDocument>(RareFunction.Rare)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> FreqRare(Func<RareDetectorDescriptor<TDocument>, IRareDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new RareDetectorDescriptor<TDocument>(RareFunction.FreqRare)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> Sum(Func<SumDetectorDescriptor<TDocument>, ISumDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new SumDetectorDescriptor<TDocument>(SumFunction.Sum)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> HighSum(Func<SumDetectorDescriptor<TDocument>, ISumDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new SumDetectorDescriptor<TDocument>(SumFunction.HighSum)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> LowSum(Func<SumDetectorDescriptor<TDocument>, ISumDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new SumDetectorDescriptor<TDocument>(SumFunction.LowSum)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> NonNullSum(Func<NonNullSumDetectorDescriptor<TDocument>, INonNullSumDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new NonNullSumDetectorDescriptor<TDocument>(NonNullSumFunction.NonNullSum)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> HighNonNullSum(Func<NonNullSumDetectorDescriptor<TDocument>, INonNullSumDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new NonNullSumDetectorDescriptor<TDocument>(NonNullSumFunction.HighNonNullSum)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> LowNonNullSum(Func<NonNullSumDetectorDescriptor<TDocument>, INonNullSumDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new NonNullSumDetectorDescriptor<TDocument>(NonNullSumFunction.LowNonNullSum)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> TimeOfDay(Func<TimeDetectorDescriptor<TDocument>, ITimeDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new TimeDetectorDescriptor<TDocument>(TimeFunction.TimeOfDay)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<TDocument> TimeOfWeek(Func<TimeDetectorDescriptor<TDocument>, ITimeDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new TimeDetectorDescriptor<TDocument>(TimeFunction.TimeOfWeek)), (a, v) => a.Detector = v);
	}

	internal class ValidateDetectorRequestFormatter : IJsonFormatter<IValidateDetectorRequest>
	{
		public void Serialize(ref JsonWriter writer, IValidateDetectorRequest value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var detectorFormatter = formatterResolver.GetFormatter<IDetector>();
			detectorFormatter.Serialize(ref writer, value.Detector, formatterResolver);
		}

		public IValidateDetectorRequest Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var formatter = formatterResolver.GetFormatter<ValidateDetectorRequest>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}
}
