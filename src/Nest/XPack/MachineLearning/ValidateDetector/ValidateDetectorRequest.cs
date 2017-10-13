using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ValidateDetectorRequestConverter))]
	public partial interface IValidateDetectorRequest
	{
		[JsonIgnore]
		IDetector Detector { get; set; }
	}

	/// <inheritdoc />
	public partial class ValidateDetectorRequest
	{
		/// <inheritdoc />
		public IDetector Detector { get; set; }
	}

	/// <inheritdoc />
	[DescriptorFor("XpackMlValidateDetector")]
	public partial class ValidateDetectorDescriptor<T> where T : class
	{
		IDetector IValidateDetectorRequest.Detector { get; set; }

		public ValidateDetectorDescriptor<T> Count(Func<CountDetectorDescriptor<T>, ICountDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new CountDetectorDescriptor<T>(CountFunction.Count)));

		public ValidateDetectorDescriptor<T> HighCount(Func<CountDetectorDescriptor<T>, ICountDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new CountDetectorDescriptor<T>(CountFunction.HighCount)));

		public ValidateDetectorDescriptor<T> LowCount(Func<CountDetectorDescriptor<T>, ICountDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new CountDetectorDescriptor<T>(CountFunction.LowCount)));

		public ValidateDetectorDescriptor<T> NonZeroCount(Func<NonZeroCountDetectorDescriptor<T>, INonZeroCountDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new NonZeroCountDetectorDescriptor<T>(NonZeroCountFunction.NonZeroCount)));

		public ValidateDetectorDescriptor<T> HighNonZeroCount(Func<NonZeroCountDetectorDescriptor<T>, INonZeroCountDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new NonZeroCountDetectorDescriptor<T>(NonZeroCountFunction.HighNonZeroCount)));

		public ValidateDetectorDescriptor<T> LowNonZeroCount(Func<NonZeroCountDetectorDescriptor<T>, INonZeroCountDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new NonZeroCountDetectorDescriptor<T>(NonZeroCountFunction.LowNonZeroCount)));

		public ValidateDetectorDescriptor<T> DistinctCount(Func<DistinctCountDetectorDescriptor<T>, IDistinctCountDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new DistinctCountDetectorDescriptor<T>(DistinctCountFunction.DistinctCount)));

		public ValidateDetectorDescriptor<T> HighDistinctCount(Func<DistinctCountDetectorDescriptor<T>, IDistinctCountDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new DistinctCountDetectorDescriptor<T>(DistinctCountFunction.HighDistinctCount)));

		public ValidateDetectorDescriptor<T> LowDistinctCount(Func<DistinctCountDetectorDescriptor<T>, IDistinctCountDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new DistinctCountDetectorDescriptor<T>(DistinctCountFunction.LowDistinctCount)));

		public ValidateDetectorDescriptor<T> InfoContent(Func<InfoContentDetectorDescriptor<T>, IInfoContentDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new InfoContentDetectorDescriptor<T>(InfoContentFunction.InfoContent)));

		public ValidateDetectorDescriptor<T> HighInfoContent(Func<InfoContentDetectorDescriptor<T>, IInfoContentDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new InfoContentDetectorDescriptor<T>(InfoContentFunction.HighInfoContent)));

		public ValidateDetectorDescriptor<T> LowInfoContent(Func<InfoContentDetectorDescriptor<T>, IInfoContentDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new InfoContentDetectorDescriptor<T>(InfoContentFunction.LowInfoContent)));

		public ValidateDetectorDescriptor<T> Min(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.Min)));

		public ValidateDetectorDescriptor<T> Max(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.Max)));

		public ValidateDetectorDescriptor<T> Median(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.Median)));

		public ValidateDetectorDescriptor<T> HighMedian(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.HighMedian)));

		public ValidateDetectorDescriptor<T> LowMedian(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.LowMedian)));

		public ValidateDetectorDescriptor<T> Mean(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.Mean)));

		public ValidateDetectorDescriptor<T> HighMean(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.HighMean)));

		public ValidateDetectorDescriptor<T> LowMean(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.LowMean)));

		public ValidateDetectorDescriptor<T> Metric(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.Metric)));

		public ValidateDetectorDescriptor<T> Varp(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.Varp)));

		public ValidateDetectorDescriptor<T> HighVarp(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.HighVarp)));

		public ValidateDetectorDescriptor<T> LowVarp(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.LowVarp)));

		public ValidateDetectorDescriptor<T> Rare(Func<RareDetectorDescriptor<T>, IRareDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new RareDetectorDescriptor<T>(RareFunction.Rare)));

		public ValidateDetectorDescriptor<T> FreqRare(Func<RareDetectorDescriptor<T>, IRareDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new RareDetectorDescriptor<T>(RareFunction.FreqRare)));

		public ValidateDetectorDescriptor<T> Sum(Func<SumDetectorDescriptor<T>, ISumDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new SumDetectorDescriptor<T>(SumFunction.Sum)));

		public ValidateDetectorDescriptor<T> HighSum(Func<SumDetectorDescriptor<T>, ISumDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new SumDetectorDescriptor<T>(SumFunction.HighSum)));

		public ValidateDetectorDescriptor<T> LowSum(Func<SumDetectorDescriptor<T>, ISumDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new SumDetectorDescriptor<T>(SumFunction.LowSum)));

		public ValidateDetectorDescriptor<T> NonNullSum(Func<NonNullSumDetectorDescriptor<T>, INonNullSumDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new NonNullSumDetectorDescriptor<T>(NonNullSumFunction.NonNullSum)));

		public ValidateDetectorDescriptor<T> HighNonNullSum(Func<NonNullSumDetectorDescriptor<T>, INonNullSumDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new NonNullSumDetectorDescriptor<T>(NonNullSumFunction.HighNonNullSum)));

		public ValidateDetectorDescriptor<T> LowNonNullSum(Func<NonNullSumDetectorDescriptor<T>, INonNullSumDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new NonNullSumDetectorDescriptor<T>(NonNullSumFunction.LowNonNullSum)));

		public ValidateDetectorDescriptor<T> TimeOfDay(Func<TimeDetectorDescriptor<T>, ITimeDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new TimeDetectorDescriptor<T>(TimeFunction.TimeOfDay)));

		public ValidateDetectorDescriptor<T> TimeOfWeek(Func<TimeDetectorDescriptor<T>, ITimeDetector> selector = null) =>
			Assign(a => a.Detector = selector.InvokeOrDefault(new TimeDetectorDescriptor<T>(TimeFunction.TimeOfWeek)));
	}

	internal class ValidateDetectorRequestConverter : JsonConverter
	{
		public override bool CanRead => false;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var request = (IValidateDetectorRequest)value;
			if (request == null)
			{
				writer.WriteNull();
				return;
			}

			serializer.Serialize(writer, request.Detector);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
			throw new NotSupportedException();

		public override bool CanConvert(Type objectType) => true;
	}
}
