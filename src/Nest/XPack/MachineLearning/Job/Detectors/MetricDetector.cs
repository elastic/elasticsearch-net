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

	public interface IMetricDetector : IDetector, IByFieldNameDetector, IOverFieldNameDetector,
		IPartitionFieldNameDetector, IFieldNameDetector
	{
	}

	public abstract class MetricDetectorBase : DetectorBase, IGeographicDetector
	{
		protected MetricDetectorBase(MetricFunction function) : base(function.GetStringValue()) {}

		public Field ByFieldName { get; set; }
		public Field OverFieldName { get; set; }
		public Field PartitionFieldName { get; set; }
		public Field FieldName { get; set; }
	}

	public class MinDetector : MetricDetectorBase
	{
		public MinDetector() : base(MetricFunction.Min) {}
	}

	public class MaxDetector : MetricDetectorBase
	{
		public MaxDetector() : base(MetricFunction.Max) {}
	}

	public class MedianDetector : MetricDetectorBase
	{
		public MedianDetector() : base(MetricFunction.Median) {}
	}

	public class HighMedianDetector : MetricDetectorBase
	{
		public HighMedianDetector() : base(MetricFunction.HighMedian) {}
	}

	public class LowMedianDetector : MetricDetectorBase
	{
		public LowMedianDetector() : base(MetricFunction.LowMedian) {}
	}

	public class MeanDetector : MetricDetectorBase
	{
		public MeanDetector() : base(MetricFunction.Mean) {}
	}

	public class HighMeanDetector : MetricDetectorBase
	{
		public HighMeanDetector() : base(MetricFunction.HighMean) {}
	}

	public class LowMeanDetector : MetricDetectorBase
	{
		public LowMeanDetector() : base(MetricFunction.LowMean) {}
	}

	public class MetricDetector : MetricDetectorBase
	{
		public MetricDetector() : base(MetricFunction.Metric) {}
	}

	public class VarpDetector : MetricDetectorBase
	{
		public VarpDetector() : base(MetricFunction.Varp) {}
	}

	public class HighVarpDetector : MetricDetectorBase
	{
		public HighVarpDetector() : base(MetricFunction.HighVarp) {}
	}

	public class LowVarpDetector : MetricDetectorBase
	{
		public LowVarpDetector() : base(MetricFunction.LowVarp) {}
	}

	public class MetricDetectorDescriptor<T> : DetectorDescriptorBase<MetricDetectorDescriptor<T>, IMetricDetector>, IMetricDetector where T : class
	{
		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IOverFieldNameDetector.OverFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }
		Field IFieldNameDetector.FieldName { get; set; }

		public MetricDetectorDescriptor(MetricFunction function) : base(function.GetStringValue()) {}

		public MetricDetectorDescriptor<T> FieldName(Field fieldName) => Assign(a => a.FieldName = fieldName);

		public MetricDetectorDescriptor<T> FieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.FieldName = objectPath);

		public MetricDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(a => a.ByFieldName = byFieldName);

		public MetricDetectorDescriptor<T> ByFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.ByFieldName = objectPath);

		public MetricDetectorDescriptor<T> OverFieldName(Field overFieldName) => Assign(a => a.OverFieldName = overFieldName);

		public MetricDetectorDescriptor<T> OverFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.OverFieldName = objectPath);

		public MetricDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) => Assign(a => a.PartitionFieldName = partitionFieldName);

		public MetricDetectorDescriptor<T> PartitionFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.PartitionFieldName = objectPath);
	}
}
