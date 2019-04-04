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
			Assign(selector.InvokeOrDefault(new CountDetectorDescriptor<T>(CountFunction.Count)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> HighCount(Func<CountDetectorDescriptor<T>, ICountDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new CountDetectorDescriptor<T>(CountFunction.HighCount)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> LowCount(Func<CountDetectorDescriptor<T>, ICountDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new CountDetectorDescriptor<T>(CountFunction.LowCount)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> NonZeroCount(Func<NonZeroCountDetectorDescriptor<T>, INonZeroCountDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new NonZeroCountDetectorDescriptor<T>(NonZeroCountFunction.NonZeroCount)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> HighNonZeroCount(Func<NonZeroCountDetectorDescriptor<T>, INonZeroCountDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new NonZeroCountDetectorDescriptor<T>(NonZeroCountFunction.HighNonZeroCount)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> LowNonZeroCount(Func<NonZeroCountDetectorDescriptor<T>, INonZeroCountDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new NonZeroCountDetectorDescriptor<T>(NonZeroCountFunction.LowNonZeroCount)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> DistinctCount(Func<DistinctCountDetectorDescriptor<T>, IDistinctCountDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new DistinctCountDetectorDescriptor<T>(DistinctCountFunction.DistinctCount)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> HighDistinctCount(Func<DistinctCountDetectorDescriptor<T>, IDistinctCountDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new DistinctCountDetectorDescriptor<T>(DistinctCountFunction.HighDistinctCount)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> LowDistinctCount(Func<DistinctCountDetectorDescriptor<T>, IDistinctCountDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new DistinctCountDetectorDescriptor<T>(DistinctCountFunction.LowDistinctCount)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> InfoContent(Func<InfoContentDetectorDescriptor<T>, IInfoContentDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new InfoContentDetectorDescriptor<T>(InfoContentFunction.InfoContent)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> HighInfoContent(Func<InfoContentDetectorDescriptor<T>, IInfoContentDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new InfoContentDetectorDescriptor<T>(InfoContentFunction.HighInfoContent)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> LowInfoContent(Func<InfoContentDetectorDescriptor<T>, IInfoContentDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new InfoContentDetectorDescriptor<T>(InfoContentFunction.LowInfoContent)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> Min(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.Min)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> Max(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.Max)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> Median(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.Median)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> HighMedian(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.HighMedian)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> LowMedian(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.LowMedian)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> Mean(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.Mean)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> HighMean(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.HighMean)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> LowMean(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.LowMean)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> Metric(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.Metric)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> Varp(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.Varp)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> HighVarp(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.HighVarp)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> LowVarp(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.LowVarp)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> Rare(Func<RareDetectorDescriptor<T>, IRareDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new RareDetectorDescriptor<T>(RareFunction.Rare)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> FreqRare(Func<RareDetectorDescriptor<T>, IRareDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new RareDetectorDescriptor<T>(RareFunction.FreqRare)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> Sum(Func<SumDetectorDescriptor<T>, ISumDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new SumDetectorDescriptor<T>(SumFunction.Sum)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> HighSum(Func<SumDetectorDescriptor<T>, ISumDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new SumDetectorDescriptor<T>(SumFunction.HighSum)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> LowSum(Func<SumDetectorDescriptor<T>, ISumDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new SumDetectorDescriptor<T>(SumFunction.LowSum)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> NonNullSum(Func<NonNullSumDetectorDescriptor<T>, INonNullSumDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new NonNullSumDetectorDescriptor<T>(NonNullSumFunction.NonNullSum)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> HighNonNullSum(Func<NonNullSumDetectorDescriptor<T>, INonNullSumDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new NonNullSumDetectorDescriptor<T>(NonNullSumFunction.HighNonNullSum)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> LowNonNullSum(Func<NonNullSumDetectorDescriptor<T>, INonNullSumDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new NonNullSumDetectorDescriptor<T>(NonNullSumFunction.LowNonNullSum)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> TimeOfDay(Func<TimeDetectorDescriptor<T>, ITimeDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new TimeDetectorDescriptor<T>(TimeFunction.TimeOfDay)), (a, v) => a.Detector = v);

		public ValidateDetectorDescriptor<T> TimeOfWeek(Func<TimeDetectorDescriptor<T>, ITimeDetector> selector = null) =>
			Assign(selector.InvokeOrDefault(new TimeDetectorDescriptor<T>(TimeFunction.TimeOfWeek)), (a, v) => a.Detector = v);
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
