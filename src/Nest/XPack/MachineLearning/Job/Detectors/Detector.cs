using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	[ContractJsonConverter(typeof(DetectorConverter))]
	public interface IDetector
	{
		[JsonProperty("detector_description")]
		string DetectorDescription { get; set; }

		[JsonProperty("exclude_frequent")]
		ExcludeFrequent? ExcludeFrequent { get; set; }

		[JsonProperty("function")]
		string Function { get; }

		[JsonProperty("use_null")]
		bool? UseNull { get; set; }

		[JsonProperty("detector_index")]
		int? DetectorIndex { get; set; }
	}

	internal class DetectorConverter : JsonConverter
	{
		public override bool CanWrite => false;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
			throw new NotSupportedException();

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
				return null;

			var jObject = JObject.Load(reader);
			var function = jObject["function"].Value<string>();

			switch (function)
			{
				case "count":
					return jObject.ToObject<CountDetector>(ElasticContractResolver.Empty);
				case "high_count":
					return jObject.ToObject<HighCountDetector>(ElasticContractResolver.Empty);
				case "low_count":
					return jObject.ToObject<LowCountDetector>(ElasticContractResolver.Empty);
				case "non_zero_count":
					return jObject.ToObject<NonZeroCountDetector>(ElasticContractResolver.Empty);
				case "high_non_zero_count":
					return jObject.ToObject<HighNonZeroCountDetector>(ElasticContractResolver.Empty);
				case "low_non_zero_count":
					return jObject.ToObject<LowNonZeroCountDetector>(ElasticContractResolver.Empty);
				case "distinct_count":
					return jObject.ToObject<DistinctCountDetector>(ElasticContractResolver.Empty);
				case "high_distinct_count":
					return jObject.ToObject<HighDistinctCountDetector>(ElasticContractResolver.Empty);
				case "low_distinct_count":
					return jObject.ToObject<LowDistinctCountDetector>(ElasticContractResolver.Empty);
				case "lat_long":
					return jObject.ToObject<LatLongDetector>(ElasticContractResolver.Empty);
				case "info_content":
					return jObject.ToObject<InfoContentDetector>(ElasticContractResolver.Empty);
				case "high_info_content":
					return jObject.ToObject<HighInfoContentDetector>(ElasticContractResolver.Empty);
				case "low_info_content":
					return jObject.ToObject<LowInfoContentDetector>(ElasticContractResolver.Empty);
				case "min":
					return jObject.ToObject<MinDetector>(ElasticContractResolver.Empty);
				case "max":
					return jObject.ToObject<MaxDetector>(ElasticContractResolver.Empty);
				case "median":
					return jObject.ToObject<MedianDetector>(ElasticContractResolver.Empty);
				case "high_median":
					return jObject.ToObject<HighMedianDetector>(ElasticContractResolver.Empty);
				case "low_median":
					return jObject.ToObject<LowMedianDetector>(ElasticContractResolver.Empty);
				case "mean":
					return jObject.ToObject<MeanDetector>(ElasticContractResolver.Empty);
				case "high_mean":
					return jObject.ToObject<HighMeanDetector>(ElasticContractResolver.Empty);
				case "low_mean":
					return jObject.ToObject<LowMeanDetector>(ElasticContractResolver.Empty);
				case "metric":
					return jObject.ToObject<MetricDetector>(ElasticContractResolver.Empty);
				case "varp":
					return jObject.ToObject<VarpDetector>(ElasticContractResolver.Empty);
				case "high_varp":
					return jObject.ToObject<HighVarpDetector>(ElasticContractResolver.Empty);
				case "low_varp":
					return jObject.ToObject<LowVarpDetector>(ElasticContractResolver.Empty);
				case "rare":
					return jObject.ToObject<RareDetector>(ElasticContractResolver.Empty);
				case "freq_rare":
					return jObject.ToObject<FreqRareDetector>(ElasticContractResolver.Empty);
				case "sum":
					 return jObject.ToObject<SumDetector>(ElasticContractResolver.Empty);
				case "high_sum":
					return jObject.ToObject<HighSumDetector>(ElasticContractResolver.Empty);
				case "low_sum":
					return jObject.ToObject<LowSumDetector>(ElasticContractResolver.Empty);
				case "non_null_sum":
					return jObject.ToObject<NonNullSumDetector>(ElasticContractResolver.Empty);
				case "high_non_null_sum":
					return jObject.ToObject<HighNonNullSumDetector>(ElasticContractResolver.Empty);
				case "low_non_null_sum":
					return jObject.ToObject<LowNonNullSumDetector>(ElasticContractResolver.Empty);
				case "time_of_day":
					return jObject.ToObject<TimeOfDayDetector>(ElasticContractResolver.Empty);
				case "time_of_week":
					return jObject.ToObject<TimeOfWeekDetector>(ElasticContractResolver.Empty);
				default:
					throw new JsonSerializationException($"Cannot deserialize detector for unknown function '{function}'");
			}
		}

		public override bool CanConvert(Type objectType) => true;
	}

	public interface IFieldNameDetector : IDetector
	{
		[JsonProperty("field_name")]
		Field FieldName { get; set; }
	}

	public interface IByFieldNameDetector : IDetector
	{
		[JsonProperty("by_field_name")]
		Field ByFieldName { get; set; }
	}

	public interface IOverFieldNameDetector : IDetector
	{
		[JsonProperty("over_field_name")]
		Field OverFieldName { get; set; }
	}

	public interface IPartitionFieldNameDetector : IDetector
	{
		[JsonProperty("partition_field_name")]
		Field PartitionFieldName { get; set; }
	}

	public abstract class DetectorBase : IDetector
	{
		protected DetectorBase(string function)
		{
			Function = function;
		}

		public string DetectorDescription { get; set; }
		public ExcludeFrequent? ExcludeFrequent { get; set; }
		public string Function { get; }
		public bool? UseNull { get; set; }
		public int? DetectorIndex { get; set; }
	}

	public abstract class DetectorDescriptorBase<TDetectorDescriptor, TDetectorInterface> : DescriptorBase<TDetectorDescriptor, TDetectorInterface>, IDetector
		where TDetectorDescriptor : DetectorDescriptorBase<TDetectorDescriptor, TDetectorInterface>, TDetectorInterface
		where TDetectorInterface : class, IDetector
	{
		private readonly string _function;

		string IDetector.DetectorDescription { get; set; }
		ExcludeFrequent? IDetector.ExcludeFrequent { get; set; }
		string IDetector.Function => _function;
		bool? IDetector.UseNull { get; set; }
		int? IDetector.DetectorIndex { get; set; }

		protected DetectorDescriptorBase(string function) => _function = function;

		public TDetectorDescriptor DetectorDescription(string description) => Assign(a => a.DetectorDescription = description);

		public TDetectorDescriptor ExcludeFrequent(ExcludeFrequent excludeFrequent) => Assign(a => a.ExcludeFrequent = excludeFrequent);

		public TDetectorDescriptor UseNull(bool useNull = true) => Assign(a => a.UseNull = useNull);

		public TDetectorDescriptor DetectorIndex(int detectorIndex) => Assign(a => a.DetectorIndex = detectorIndex);
	}

	public class DetectorsDescriptor<T> : DescriptorPromiseBase<DetectorsDescriptor<T>, IList<IDetector>> where T : class
	{
		public DetectorsDescriptor() : base(new List<IDetector>()) {}

		public DetectorsDescriptor<T> Count(Func<CountDetectorDescriptor<T>, ICountDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new CountDetectorDescriptor<T>(CountFunction.Count))));

		public DetectorsDescriptor<T> HighCount(Func<CountDetectorDescriptor<T>, ICountDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new CountDetectorDescriptor<T>(CountFunction.HighCount))));

		public DetectorsDescriptor<T> LowCount(Func<CountDetectorDescriptor<T>, ICountDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new CountDetectorDescriptor<T>(CountFunction.LowCount))));

		public DetectorsDescriptor<T> NonZeroCount(Func<NonZeroCountDetectorDescriptor<T>, INonZeroCountDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new NonZeroCountDetectorDescriptor<T>(NonZeroCountFunction.NonZeroCount))));

		public DetectorsDescriptor<T> HighNonZeroCount(Func<NonZeroCountDetectorDescriptor<T>, INonZeroCountDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new NonZeroCountDetectorDescriptor<T>(NonZeroCountFunction.HighNonZeroCount))));

		public DetectorsDescriptor<T> LowNonZeroCount(Func<NonZeroCountDetectorDescriptor<T>, INonZeroCountDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new NonZeroCountDetectorDescriptor<T>(NonZeroCountFunction.LowNonZeroCount))));

		public DetectorsDescriptor<T> DistinctCount(Func<DistinctCountDetectorDescriptor<T>, IDistinctCountDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new DistinctCountDetectorDescriptor<T>(DistinctCountFunction.DistinctCount))));

		public DetectorsDescriptor<T> HighDistinctCount(Func<DistinctCountDetectorDescriptor<T>, IDistinctCountDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new DistinctCountDetectorDescriptor<T>(DistinctCountFunction.HighDistinctCount))));

		public DetectorsDescriptor<T> LowDistinctCount(Func<DistinctCountDetectorDescriptor<T>, IDistinctCountDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new DistinctCountDetectorDescriptor<T>(DistinctCountFunction.LowDistinctCount))));

		public DetectorsDescriptor<T> InfoContent(Func<InfoContentDetectorDescriptor<T>, IInfoContentDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new InfoContentDetectorDescriptor<T>(InfoContentFunction.InfoContent))));

		public DetectorsDescriptor<T> HighInfoContent(Func<InfoContentDetectorDescriptor<T>, IInfoContentDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new InfoContentDetectorDescriptor<T>(InfoContentFunction.HighInfoContent))));

		public DetectorsDescriptor<T> LowInfoContent(Func<InfoContentDetectorDescriptor<T>, IInfoContentDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new InfoContentDetectorDescriptor<T>(InfoContentFunction.LowInfoContent))));

		public DetectorsDescriptor<T> Min(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.Min))));

		public DetectorsDescriptor<T> Max(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.Max))));

		public DetectorsDescriptor<T> Median(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.Median))));

		public DetectorsDescriptor<T> HighMedian(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.HighMedian))));

		public DetectorsDescriptor<T> LowMedian(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.LowMedian))));

		public DetectorsDescriptor<T> Mean(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.Mean))));

		public DetectorsDescriptor<T> HighMean(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.HighMean))));

		public DetectorsDescriptor<T> LowMean(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.LowMean))));

		public DetectorsDescriptor<T> Metric(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.Metric))));

		public DetectorsDescriptor<T> Varp(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.Varp))));

		public DetectorsDescriptor<T> HighVarp(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.HighVarp))));

		public DetectorsDescriptor<T> LowVarp(Func<MetricDetectorDescriptor<T>, IMetricDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new MetricDetectorDescriptor<T>(MetricFunction.LowVarp))));

		public DetectorsDescriptor<T> Rare(Func<RareDetectorDescriptor<T>, IRareDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new RareDetectorDescriptor<T>(RareFunction.Rare))));

		public DetectorsDescriptor<T> FreqRare(Func<RareDetectorDescriptor<T>, IRareDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new RareDetectorDescriptor<T>(RareFunction.FreqRare))));

		public DetectorsDescriptor<T> Sum(Func<SumDetectorDescriptor<T>, ISumDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new SumDetectorDescriptor<T>(SumFunction.Sum))));

		public DetectorsDescriptor<T> HighSum(Func<SumDetectorDescriptor<T>, ISumDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new SumDetectorDescriptor<T>(SumFunction.HighSum))));

		public DetectorsDescriptor<T> LowSum(Func<SumDetectorDescriptor<T>, ISumDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new SumDetectorDescriptor<T>(SumFunction.LowSum))));

		public DetectorsDescriptor<T> NonNullSum(Func<NonNullSumDetectorDescriptor<T>, INonNullSumDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new NonNullSumDetectorDescriptor<T>(NonNullSumFunction.NonNullSum))));

		public DetectorsDescriptor<T> HighNonNullSum(Func<NonNullSumDetectorDescriptor<T>, INonNullSumDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new NonNullSumDetectorDescriptor<T>(NonNullSumFunction.HighNonNullSum))));

		public DetectorsDescriptor<T> LowNonNullSum(Func<NonNullSumDetectorDescriptor<T>, INonNullSumDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new NonNullSumDetectorDescriptor<T>(NonNullSumFunction.LowNonNullSum))));

		public DetectorsDescriptor<T> TimeOfDay(Func<TimeDetectorDescriptor<T>, ITimeDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new TimeDetectorDescriptor<T>(TimeFunction.TimeOfDay))));

		public DetectorsDescriptor<T> TimeOfWeek(Func<TimeDetectorDescriptor<T>, ITimeDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new TimeDetectorDescriptor<T>(TimeFunction.TimeOfWeek))));
	}
}
