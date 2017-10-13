using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public enum CountFunction
	{
		Count,
		HighCount,
		LowCount
	}

	public enum NonZeroCountFunction
	{
		NonZeroCount,
		LowNonZeroCount,
		HighNonZeroCount
	}

	public enum DistinctCountFunction
	{
		DistinctCount,
		LowDistinctCount,
		HighDistinctCount
	}

	public static class CountFunctionExtensions
	{
		public static string GetStringValue(this CountFunction countFunction)
		{
			switch (countFunction)
			{
				case CountFunction.Count:
					return "count";
				case CountFunction.HighCount:
					return "high_count";
				case CountFunction.LowCount:
					return "low_count";
				default:
					throw new ArgumentOutOfRangeException(nameof(countFunction), countFunction, null);
			}
		}

		public static string GetStringValue(this NonZeroCountFunction nonZeroCountFunction)
		{
			switch (nonZeroCountFunction)
			{
				case NonZeroCountFunction.NonZeroCount:
					return "non_zero_count";
				case NonZeroCountFunction.LowNonZeroCount:
					return "low_non_zero_count";
				case NonZeroCountFunction.HighNonZeroCount:
					return "high_non_zero_count";
				default:
					throw new ArgumentOutOfRangeException(nameof(nonZeroCountFunction), nonZeroCountFunction, null);
			}
		}

		public static string GetStringValue(this DistinctCountFunction distinctCountFunction)
		{
			switch (distinctCountFunction)
			{
				case DistinctCountFunction.DistinctCount:
					return "distinct_count";
				case DistinctCountFunction.LowDistinctCount:
					return "low_distinct_count";
				case DistinctCountFunction.HighDistinctCount:
					return "high_distinct_count";
				default:
					throw new ArgumentOutOfRangeException(nameof(distinctCountFunction), distinctCountFunction, null);
			}
		}
	}

	public interface ICountDetector : IDetector, IByFieldNameDetector, IOverFieldNameDetector,
		IPartitionFieldNameDetector
	{
	}

	public interface INonZeroCountDetector : IDetector, IByFieldNameDetector, IPartitionFieldNameDetector
	{
	}

	public interface IDistinctCountDetector : IDetector, IByFieldNameDetector, IOverFieldNameDetector,
		IPartitionFieldNameDetector, IFieldNameDetector
	{
	}

	public abstract class CountDetectorBase : DetectorBase, ICountDetector
	{
		public Field ByFieldName { get; set; }
		public Field OverFieldName { get; set; }
		public Field PartitionFieldName { get; set; }

		protected CountDetectorBase(CountFunction function) : base(function.GetStringValue())
		{
		}
	}

	public abstract class NonZeroCountDetectorBase : DetectorBase, INonZeroCountDetector
	{
		public Field ByFieldName { get; set; }
		public Field PartitionFieldName { get; set; }

		protected NonZeroCountDetectorBase(NonZeroCountFunction function) : base(function.GetStringValue())
		{
		}
	}

	public abstract class DistinctCountDetectorBase : DetectorBase, IDistinctCountDetector
	{
		public Field ByFieldName { get; set; }
		public Field PartitionFieldName { get; set; }
		public Field OverFieldName { get; set; }
		public Field FieldName { get; set; }

		protected DistinctCountDetectorBase(DistinctCountFunction function) : base(function.GetStringValue())
		{
		}
	}

	public class CountDetectorDescriptor<T> : DetectorDescriptorBase<CountDetectorDescriptor<T>, ICountDetector>, ICountDetector where T : class
	{
		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IOverFieldNameDetector.OverFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }

		public CountDetectorDescriptor(CountFunction function) : base(function.GetStringValue()) {}

		public CountDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(a => a.ByFieldName = byFieldName);

		public CountDetectorDescriptor<T> ByFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.ByFieldName = objectPath);

		public CountDetectorDescriptor<T> OverFieldName(Field overFieldName) => Assign(a => a.OverFieldName = overFieldName);

		public CountDetectorDescriptor<T> OverFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.OverFieldName = objectPath);

		public CountDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) => Assign(a => a.PartitionFieldName = partitionFieldName);

		public CountDetectorDescriptor<T> PartitionFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.PartitionFieldName = objectPath);
	}

	public class NonZeroCountDetectorDescriptor<T> : DetectorDescriptorBase<NonZeroCountDetectorDescriptor<T>, INonZeroCountDetector>, INonZeroCountDetector where T : class
	{
		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }

		public NonZeroCountDetectorDescriptor(NonZeroCountFunction function) : base(function.GetStringValue()) {}

		public NonZeroCountDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(a => a.ByFieldName = byFieldName);

		public NonZeroCountDetectorDescriptor<T> ByFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.ByFieldName = objectPath);

		public NonZeroCountDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) => Assign(a => a.PartitionFieldName = partitionFieldName);

		public NonZeroCountDetectorDescriptor<T> PartitionFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.PartitionFieldName = objectPath);
	}

	public class DistinctCountDetectorDescriptor<T> : DetectorDescriptorBase<DistinctCountDetectorDescriptor<T>, IDistinctCountDetector>, IDistinctCountDetector where T : class
	{
		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IOverFieldNameDetector.OverFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }
		Field IFieldNameDetector.FieldName { get; set; }

		public DistinctCountDetectorDescriptor(DistinctCountFunction function) : base(function.GetStringValue()) {}

		public DistinctCountDetectorDescriptor<T> FieldName(Field fieldName) => Assign(a => a.FieldName = fieldName);

		public DistinctCountDetectorDescriptor<T> FieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.FieldName = objectPath);

		public DistinctCountDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(a => a.ByFieldName = byFieldName);

		public DistinctCountDetectorDescriptor<T> ByFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.ByFieldName = objectPath);

		public DistinctCountDetectorDescriptor<T> OverFieldName(Field overFieldName) => Assign(a => a.OverFieldName = overFieldName);

		public DistinctCountDetectorDescriptor<T> OverFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.OverFieldName = objectPath);

		public DistinctCountDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) => Assign(a => a.PartitionFieldName = partitionFieldName);

		public DistinctCountDetectorDescriptor<T> PartitionFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.PartitionFieldName = objectPath);
	}

	public class CountDetector : CountDetectorBase
	{
		public CountDetector() : base(CountFunction.Count) {}
	}

	public class HighCountDetector : CountDetectorBase
	{
		public HighCountDetector() : base(CountFunction.HighCount) {}
	}

	public class LowCountDetector : CountDetectorBase
	{
		public LowCountDetector() : base(CountFunction.LowCount) {}
	}

	/// <summary>
	/// Detects anomalies when the number of events in a bucket is anomalous, but it ignores cases where the bucket count is zero.
	/// Use this function if you know your data is sparse or has gaps and the gaps are not important.
	/// </summary>
	public class NonZeroCountDetector : NonZeroCountDetectorBase
	{
		public NonZeroCountDetector() : base(NonZeroCountFunction.NonZeroCount) {}
	}

	/// <summary>
	/// Detects anomalies when the number of events in a bucket is unusually high and it ignores cases where the bucket count is zero.
	/// </summary>
	public class HighNonZeroCountDetector : NonZeroCountDetectorBase
	{
		public HighNonZeroCountDetector() : base(NonZeroCountFunction.HighNonZeroCount) {}
	}

	public class DistinctCountDetector : DistinctCountDetectorBase
	{
		public DistinctCountDetector() : base(DistinctCountFunction.DistinctCount) {}
	}

	public class HighDistinctCountDetector : DistinctCountDetectorBase
	{
		public HighDistinctCountDetector() : base(DistinctCountFunction.HighDistinctCount) {}
	}

	public class LowDistinctCountDetector : DistinctCountDetectorBase
	{
		public LowDistinctCountDetector() : base(DistinctCountFunction.LowDistinctCount) {}
	}

	/// <summary>
	/// Detects anomalies when the number of events in a bucket is unusually low and it ignores cases where the bucket count is zero.
	/// </summary>
	public class LowNonZeroCountDetector : NonZeroCountDetectorBase
	{
		public LowNonZeroCountDetector() : base(NonZeroCountFunction.LowNonZeroCount) {}
	}
}
