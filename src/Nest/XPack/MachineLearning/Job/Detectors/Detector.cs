using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Nest
{
	/// <summary>
	/// A machine learning detector
	/// </summary>
	[ContractJsonConverter(typeof(DetectorConverter))]
	public interface IDetector
	{
		/// <summary>
		/// A description of the detector. For example, "Low event rate".
		/// </summary>
		[JsonProperty("detector_description")]
		string DetectorDescription { get; set; }

		/// <summary>
		/// A unique identifier for the detector. This identifier is based on the order of the
		/// detectors in the analysis config, starting at zero. You can use this
		/// identifier when you want to update a specific detector.
		/// </summary>
		[JsonProperty("detector_index")]
		int? DetectorIndex { get; set; }

		/// <summary>
		/// If set, frequent entities are excluded from influencing the anomaly results.
		/// Entities can be considered frequent over time or frequent in a population.
		/// If you are working with both over and by fields, then you can set exclude_frequent
		/// to all for both fields, or to by or over for those specific fields.
		/// </summary>
		[JsonProperty("exclude_frequent")]
		ExcludeFrequent? ExcludeFrequent { get; set; }

		/// <summary>
		/// The analysis function used
		/// </summary>
		[JsonProperty("function")]
		string Function { get; }

		/// <summary>
		/// Defines whether a new series is used as the null series when there
		/// is no value for the by or partition fields. The default value is <c>false</c>.
		/// </summary>
		[JsonProperty("use_null")]
		bool? UseNull { get; set; }

		/// <summary>
		/// Custom rules enable you to change the behaviour of anomaly detectors based on domain-specific knowledge.
		/// Custom rules describe when a detector should take a certain action instead of following its default behaviour.
		/// To specify the "when", a rule uses a scope and conditions. You can think of scope as the categorical
		/// specification of a rule, while conditions are the numerical part. A rule can have a scope,
		/// one or more conditions, or a combination of scope and conditions.
		/// </summary>
		[JsonProperty("custom_rules")]
		IEnumerable<IDetectionRule> CustomRules { get; set; }
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

	/// <summary>
	/// A machine learning detector with a field name
	/// </summary>
	public interface IFieldNameDetector : IDetector
	{
		/// <summary>
		/// The field that the detector uses in the function. If you use an event
		/// rate function such as count or rare, do not specify this field.
		/// </summary>
		[JsonProperty("field_name")]
		Field FieldName { get; set; }
	}

	/// <summary>
	/// A machine learning detector with a by field
	/// </summary>
	public interface IByFieldNameDetector : IDetector
	{
		/// <summary>
		/// The field used to split the data. In particular, this property is used for analyzing the splits with
		/// respect to their own history. It is used for finding unusual values in the context of the split.
		/// </summary>
		[JsonProperty("by_field_name")]
		Field ByFieldName { get; set; }
	}

	/// <summary>
	/// A machine learning detector with an over field
	/// </summary>
	public interface IOverFieldNameDetector : IDetector
	{
		/// <summary>
		/// The field used to split the data. In particular, this property is used for analyzing the splits
		/// with respect to the history of all splits. It is used for finding unusual values in the population of all splits.
		/// </summary>
		[JsonProperty("over_field_name")]
		Field OverFieldName { get; set; }
	}

	/// <summary>
	/// A machine learning detector with a partition field
	/// </summary>
	public interface IPartitionFieldNameDetector : IDetector
	{
		/// <summary>
		/// The field used to segment the analysis. When you use this property, you have completely
		/// independent baselines for each value of this field.
		/// </summary>
		[JsonProperty("partition_field_name")]
		Field PartitionFieldName { get; set; }
	}

	/// <inheritdoc />
	public abstract class DetectorBase : IDetector
	{
		protected DetectorBase(string function) => Function = function;

		/// <inheritdoc />
		public string DetectorDescription { get; set; }
		/// <inheritdoc />
		public int? DetectorIndex { get; set; }
		/// <inheritdoc />
		public ExcludeFrequent? ExcludeFrequent { get; set; }
		/// <inheritdoc />
		public string Function { get; }
		/// <inheritdoc />
		public bool? UseNull { get; set; }
		/// <inheritdoc />
		public IEnumerable<IDetectionRule> CustomRules { get; set; }
	}

	/// <inheritdoc cref="IDetector" />
	public abstract class DetectorDescriptorBase<TDetectorDescriptor, TDetectorInterface>
		: DescriptorBase<TDetectorDescriptor, TDetectorInterface>, IDetector
		where TDetectorDescriptor : DetectorDescriptorBase<TDetectorDescriptor, TDetectorInterface>, TDetectorInterface
		where TDetectorInterface : class, IDetector
	{
		private readonly string _function;

		protected DetectorDescriptorBase(string function) => _function = function;

		string IDetector.DetectorDescription { get; set; }
		int? IDetector.DetectorIndex { get; set; }
		ExcludeFrequent? IDetector.ExcludeFrequent { get; set; }
		string IDetector.Function => _function;
		bool? IDetector.UseNull { get; set; }
		IEnumerable<IDetectionRule> IDetector.CustomRules { get; set; }

		/// <inheritdoc cref="IDetector.DetectorDescription" />
		public TDetectorDescriptor DetectorDescription(string description) => Assign(a => a.DetectorDescription = description);

		/// <inheritdoc cref="IDetector.ExcludeFrequent" />
		public TDetectorDescriptor ExcludeFrequent(ExcludeFrequent? excludeFrequent) => Assign(a => a.ExcludeFrequent = excludeFrequent);

		/// <inheritdoc cref="IDetector.UseNull" />
		public TDetectorDescriptor UseNull(bool? useNull = true) => Assign(a => a.UseNull = useNull);

		/// <inheritdoc cref="IDetector.DetectorIndex" />
		public TDetectorDescriptor DetectorIndex(int? detectorIndex) => Assign(a => a.DetectorIndex = detectorIndex);

		/// <inheritdoc cref="IDetector.CustomRules" />
		public TDetectorDescriptor CustomRules(Func<DetectionRulesDescriptor, IPromise<List<IDetectionRule>>> selector) =>
			Assign(a => a.CustomRules = selector.Invoke(new DetectionRulesDescriptor()).Value);
	}

	public class DetectorsDescriptor<T> : DescriptorPromiseBase<DetectorsDescriptor<T>, IList<IDetector>> where T : class
	{
		public DetectorsDescriptor() : base(new List<IDetector>()) { }

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

		public DetectorsDescriptor<T> LatLong(Func<LatLongDetectorDescriptor<T>, IGeographicDetector> selector = null) =>
			Assign(a => a.AddIfNotNull(selector.InvokeOrDefault(new LatLongDetectorDescriptor<T>())));
	}
}
