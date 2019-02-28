using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Elasticsearch.Net;


namespace Nest
{
	[JsonFormatter(typeof(DetectorFormatter))]
	public interface IDetector
	{
		[DataMember(Name = "detector_description")]
		string DetectorDescription { get; set; }

		[DataMember(Name = "detector_index")]
		int? DetectorIndex { get; set; }

		[DataMember(Name = "exclude_frequent")]
		ExcludeFrequent? ExcludeFrequent { get; set; }

		[DataMember(Name = "function")]
		string Function { get; }

		[DataMember(Name = "use_null")]
		bool? UseNull { get; set; }
	}

	internal class DetectorFormatter : IJsonFormatter<IDetector>
	{
		private static readonly AutomataDictionary Functions = new AutomataDictionary
		{
			{ "count", 0 },
			{ "high_count", 1 },
			{ "low_count", 2 },
			{ "non_zero_count", 3 },
			{ "high_non_zero_count", 4 },
			{ "low_non_zero_count", 5 },
			{ "distinct_count", 6 },
			{ "high_distinct_count", 7 },
			{ "low_distinct_count", 8 },
			{ "lat_long", 9 },
			{ "info_content", 10 },
			{ "high_info_content", 11 },
			{ "low_info_content", 12 },
			{ "min", 13 },
			{ "max", 14 },
			{ "median", 15 },
			{ "high_median", 16 },
			{ "low_median", 17 },
			{ "mean", 18 },
			{ "high_mean", 19 },
			{ "low_mean", 20 },
			{ "metric", 21 },
			{ "varp", 22 },
			{ "high_varp", 23 },
			{ "low_varp", 24 },
			{ "rare", 25 },
			{ "freq_rare", 26 },
			{ "sum", 27 },
			{ "high_sum", 28 },
			{ "low_sum", 29 },
			{ "non_null_sum", 30 },
			{ "high_non_null_sum", 31 },
			{ "low_non_null_sum", 32 },
			{ "time_of_day", 33 },
			{ "time_of_week", 34 }
		};

		public IDetector Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.Null)
				return null;

			var segment = reader.ReadNextBlockSegment();
			var segmentReader = new JsonReader(segment.Array, segment.Offset);
			var count = 0;
			ArraySegment<byte> function = default;

			while (segmentReader.ReadIsInObject(ref count))
			{
				if (segmentReader.ReadPropertyName() == "function")
				{
					function = segmentReader.ReadStringSegmentRaw();
					break;
				}

				// skip value
				segmentReader.ReadNextBlock();
			}

			segmentReader = new JsonReader(segment.Array, segment.Offset);

			if (Functions.TryGetValue(function, out var value))
			{
				switch (value)
				{
					case 0:
						return Deserialize<CountDetector>(ref segmentReader, formatterResolver);
					case 1:
						return Deserialize<HighCountDetector>(ref segmentReader, formatterResolver);
					case 2:
						return Deserialize<LowCountDetector>(ref segmentReader, formatterResolver);
					case 3:
						return Deserialize<NonZeroCountDetector>(ref segmentReader, formatterResolver);
					case 4:
						return Deserialize<HighNonZeroCountDetector>(ref segmentReader, formatterResolver);
					case 5:
						return Deserialize<LowNonZeroCountDetector>(ref segmentReader, formatterResolver);
					case 6:
						return Deserialize<DistinctCountDetector>(ref segmentReader, formatterResolver);
					case 7:
						return Deserialize<HighDistinctCountDetector>(ref segmentReader, formatterResolver);
					case 8:
						return Deserialize<LowDistinctCountDetector>(ref segmentReader, formatterResolver);
					case 9:
						return Deserialize<LatLongDetector>(ref segmentReader, formatterResolver);
					case 10:
						return Deserialize<InfoContentDetector>(ref segmentReader, formatterResolver);
					case 11:
						return Deserialize<HighInfoContentDetector>(ref segmentReader, formatterResolver);
					case 12:
						return Deserialize<LowInfoContentDetector>(ref segmentReader, formatterResolver);
					case 13:
						return Deserialize<MinDetector>(ref segmentReader, formatterResolver);
					case 14:
						return Deserialize<MaxDetector>(ref segmentReader, formatterResolver);
					case 15:
						return Deserialize<MedianDetector>(ref segmentReader, formatterResolver);
					case 16:
						return Deserialize<HighMedianDetector>(ref segmentReader, formatterResolver);
					case 17:
						return Deserialize<LowMedianDetector>(ref segmentReader, formatterResolver);
					case 18:
						return Deserialize<MeanDetector>(ref segmentReader, formatterResolver);
					case 19:
						return Deserialize<HighMeanDetector>(ref segmentReader, formatterResolver);
					case 20:
						return Deserialize<LowMeanDetector>(ref segmentReader, formatterResolver);
					case 21:
						return Deserialize<MetricDetector>(ref segmentReader, formatterResolver);
					case 22:
						return Deserialize<VarpDetector>(ref segmentReader, formatterResolver);
					case 23:
						return Deserialize<HighVarpDetector>(ref segmentReader, formatterResolver);
					case 24:
						return Deserialize<LowVarpDetector>(ref segmentReader, formatterResolver);
					case 25:
						return Deserialize<RareDetector>(ref segmentReader, formatterResolver);
					case 26:
						return Deserialize<FreqRareDetector>(ref segmentReader, formatterResolver);
					case 27:
						return Deserialize<SumDetector>(ref segmentReader, formatterResolver);
					case 28:
						return Deserialize<HighSumDetector>(ref segmentReader, formatterResolver);
					case 29:
						return Deserialize<LowSumDetector>(ref segmentReader, formatterResolver);
					case 30:
						return Deserialize<NonNullSumDetector>(ref segmentReader, formatterResolver);
					case 31:
						return Deserialize<HighNonNullSumDetector>(ref segmentReader, formatterResolver);
					case 32:
						return Deserialize<LowNonNullSumDetector>(ref segmentReader, formatterResolver);
					case 33:
						return Deserialize<TimeOfDayDetector>(ref segmentReader, formatterResolver);
					case 34:
						return Deserialize<TimeOfWeekDetector>(ref segmentReader, formatterResolver);
				}
			}

			throw new Exception($"Unknown function {function.Utf8String()}");
		}

		public void Serialize(ref JsonWriter writer, IDetector value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			switch (value.Function)
			{
				case "count":
				case "high_count":
				case "low_count":
					Serialize<ICountDetector>(ref writer, value, formatterResolver);
					break;
				case "non_zero_count":
				case "high_non_zero_count":
				case "low_non_zero_count":
					Serialize<INonZeroCountDetector>(ref writer, value, formatterResolver);
					break;
				case "distinct_count":
				case "high_distinct_count":
				case "low_distinct_count":
					Serialize<IDistinctCountDetector>(ref writer, value, formatterResolver);
					break;
				case "lat_long":
					Serialize<IGeographicDetector>(ref writer, value, formatterResolver);
					break;
				case "info_content":
				case "high_info_content":
				case "low_info_content":
					Serialize<IInfoContentDetector>(ref writer, value, formatterResolver);
					break;
				case "min":
				case "max":
				case "median":
				case "high_median":
				case "low_median":
				case "mean":
				case "high_mean":
				case "low_mean":
				case "metric":
				case "varp":
				case "high_varp":
				case "low_varp":
					Serialize<IMetricDetector>(ref writer, value, formatterResolver);
					break;
				case "rare":
				case "freq_rare":
					Serialize<IRareDetector>(ref writer, value, formatterResolver);
					break;
				case "sum":
				case "high_sum":
				case "low_sum":
					Serialize<ISumDetector>(ref writer, value, formatterResolver);
					break;
				case "non_null_sum":
				case "high_non_null_sum":
				case "low_non_null_sum":
					Serialize<INonNullSumDetector>(ref writer, value, formatterResolver);
					break;
				case "time_of_day":
				case "time_of_week":
					Serialize<ITimeDetector>(ref writer, value, formatterResolver);
					break;
			}
		}

		private static void Serialize<TDetector>(ref JsonWriter writer, IDetector value, IJsonFormatterResolver formatterResolver)
			where TDetector : class, IDetector
		{
			var formatter = formatterResolver.GetFormatter<TDetector>();
			formatter.Serialize(ref writer, value as TDetector, formatterResolver);
		}

		private static TDetector Deserialize<TDetector>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
			where TDetector : IDetector
		{
			var formatter = formatterResolver.GetFormatter<TDetector>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}

	public interface IFieldNameDetector : IDetector
	{
		[DataMember(Name = "field_name")]
		Field FieldName { get; set; }
	}

	public interface IByFieldNameDetector : IDetector
	{
		[DataMember(Name = "by_field_name")]
		Field ByFieldName { get; set; }
	}

	public interface IOverFieldNameDetector : IDetector
	{
		[DataMember(Name = "over_field_name")]
		Field OverFieldName { get; set; }
	}

	public interface IPartitionFieldNameDetector : IDetector
	{
		[DataMember(Name = "partition_field_name")]
		Field PartitionFieldName { get; set; }
	}

	public abstract class DetectorBase : IDetector
	{
		protected DetectorBase(string function) => Function = function;

		public string DetectorDescription { get; set; }
		public int? DetectorIndex { get; set; }
		public ExcludeFrequent? ExcludeFrequent { get; set; }
		public string Function { get; }
		public bool? UseNull { get; set; }
	}

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

		public TDetectorDescriptor DetectorDescription(string description) => Assign(a => a.DetectorDescription = description);

		public TDetectorDescriptor ExcludeFrequent(ExcludeFrequent? excludeFrequent) => Assign(a => a.ExcludeFrequent = excludeFrequent);

		public TDetectorDescriptor UseNull(bool? useNull = true) => Assign(a => a.UseNull = useNull);

		public TDetectorDescriptor DetectorIndex(int? detectorIndex) => Assign(a => a.DetectorIndex = detectorIndex);
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
