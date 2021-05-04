// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;

namespace Nest
{
	public enum MetricFunction
	{
		Min,

		Max,

		Median,
		HighMedian,
		LowMedian,

		Mean,
		HighMean,
		LowMean,

		Metric,

		Varp,
		HighVarp,
		LowVarp
	}

	public static class MetricFunctionsExtensions
	{
		public static string GetStringValue(this MetricFunction metricFunction)
		{
			switch (metricFunction)
			{
				case MetricFunction.Min:
					return "min";
				case MetricFunction.Max:
					return "max";
				case MetricFunction.Median:
					return "median";
				case MetricFunction.HighMedian:
					return "high_median";
				case MetricFunction.LowMedian:
					return "low_median";
				case MetricFunction.Mean:
					return "mean";
				case MetricFunction.HighMean:
					return "high_mean";
				case MetricFunction.LowMean:
					return "low_mean";
				case MetricFunction.Metric:
					return "metric";
				case MetricFunction.Varp:
					return "varp";
				case MetricFunction.HighVarp:
					return "high_varp";
				case MetricFunction.LowVarp:
					return "low_varp";
				default:
					throw new ArgumentOutOfRangeException(nameof(metricFunction), metricFunction, null);
			}
		}
	}

	public interface IMetricDetector
		: IDetector, IByFieldNameDetector, IOverFieldNameDetector,
			IPartitionFieldNameDetector, IFieldNameDetector { }

	public abstract class MetricDetectorBase : DetectorBase, IMetricDetector
	{
		protected MetricDetectorBase(MetricFunction function) : base(function.GetStringValue()) { }

		public Field ByFieldName { get; set; }
		public Field FieldName { get; set; }
		public Field OverFieldName { get; set; }
		public Field PartitionFieldName { get; set; }
	}

	public class MinDetector : MetricDetectorBase
	{
		public MinDetector() : base(MetricFunction.Min) { }
	}

	public class MaxDetector : MetricDetectorBase
	{
		public MaxDetector() : base(MetricFunction.Max) { }
	}

	public class MedianDetector : MetricDetectorBase
	{
		public MedianDetector() : base(MetricFunction.Median) { }
	}

	public class HighMedianDetector : MetricDetectorBase
	{
		public HighMedianDetector() : base(MetricFunction.HighMedian) { }
	}

	public class LowMedianDetector : MetricDetectorBase
	{
		public LowMedianDetector() : base(MetricFunction.LowMedian) { }
	}

	public class MeanDetector : MetricDetectorBase
	{
		public MeanDetector() : base(MetricFunction.Mean) { }
	}

	public class HighMeanDetector : MetricDetectorBase
	{
		public HighMeanDetector() : base(MetricFunction.HighMean) { }
	}

	public class LowMeanDetector : MetricDetectorBase
	{
		public LowMeanDetector() : base(MetricFunction.LowMean) { }
	}

	public class MetricDetector : MetricDetectorBase
	{
		public MetricDetector() : base(MetricFunction.Metric) { }
	}

	public class VarpDetector : MetricDetectorBase
	{
		public VarpDetector() : base(MetricFunction.Varp) { }
	}

	public class HighVarpDetector : MetricDetectorBase
	{
		public HighVarpDetector() : base(MetricFunction.HighVarp) { }
	}

	public class LowVarpDetector : MetricDetectorBase
	{
		public LowVarpDetector() : base(MetricFunction.LowVarp) { }
	}

	public class MetricDetectorDescriptor<T> : DetectorDescriptorBase<MetricDetectorDescriptor<T>, IMetricDetector>, IMetricDetector where T : class
	{
		public MetricDetectorDescriptor(MetricFunction function) : base(function.GetStringValue()) { }

		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IFieldNameDetector.FieldName { get; set; }
		Field IOverFieldNameDetector.OverFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }

		public MetricDetectorDescriptor<T> FieldName(Field fieldName) => Assign(fieldName, (a, v) => a.FieldName = v);

		public MetricDetectorDescriptor<T> FieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.FieldName = v);

		public MetricDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(byFieldName, (a, v) => a.ByFieldName = v);

		public MetricDetectorDescriptor<T> ByFieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.ByFieldName = v);

		public MetricDetectorDescriptor<T> OverFieldName(Field overFieldName) => Assign(overFieldName, (a, v) => a.OverFieldName = v);

		public MetricDetectorDescriptor<T> OverFieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.OverFieldName = v);

		public MetricDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) => Assign(partitionFieldName, (a, v) => a.PartitionFieldName = v);

		public MetricDetectorDescriptor<T> PartitionFieldName<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.PartitionFieldName = v);
	}
}
