// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;

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

	public interface ICountDetector
		: IDetector, IByFieldNameDetector, IOverFieldNameDetector,
			IPartitionFieldNameDetector { }

	public interface INonZeroCountDetector : IDetector, IByFieldNameDetector, IPartitionFieldNameDetector { }

	public interface IDistinctCountDetector
		: IDetector, IByFieldNameDetector, IOverFieldNameDetector,
			IPartitionFieldNameDetector, IFieldNameDetector { }

	public abstract class CountDetectorBase : DetectorBase, ICountDetector
	{
		protected CountDetectorBase(CountFunction function) : base(function.GetStringValue()) { }

		public Field ByFieldName { get; set; }
		public Field OverFieldName { get; set; }
		public Field PartitionFieldName { get; set; }
	}

	public abstract class NonZeroCountDetectorBase : DetectorBase, INonZeroCountDetector
	{
		protected NonZeroCountDetectorBase(NonZeroCountFunction function) : base(function.GetStringValue()) { }

		public Field ByFieldName { get; set; }
		public Field PartitionFieldName { get; set; }
	}

	public abstract class DistinctCountDetectorBase : DetectorBase, IDistinctCountDetector
	{
		protected DistinctCountDetectorBase(DistinctCountFunction function) : base(function.GetStringValue()) { }

		public Field ByFieldName { get; set; }
		public Field FieldName { get; set; }
		public Field OverFieldName { get; set; }
		public Field PartitionFieldName { get; set; }
	}

	public class CountDetectorDescriptor<T> : DetectorDescriptorBase<CountDetectorDescriptor<T>, ICountDetector>, ICountDetector where T : class
	{
		public CountDetectorDescriptor(CountFunction function) : base(function.GetStringValue()) { }

		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IOverFieldNameDetector.OverFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }

		public CountDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(byFieldName, (a, v) => a.ByFieldName = v);

		public CountDetectorDescriptor<T> ByFieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.ByFieldName = v);

		public CountDetectorDescriptor<T> OverFieldName(Field overFieldName) => Assign(overFieldName, (a, v) => a.OverFieldName = v);

		public CountDetectorDescriptor<T> OverFieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.OverFieldName = v);

		public CountDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) => Assign(partitionFieldName, (a, v) => a.PartitionFieldName = v);

		public CountDetectorDescriptor<T> PartitionFieldName<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.PartitionFieldName = v);
	}

	public class NonZeroCountDetectorDescriptor<T>
		: DetectorDescriptorBase<NonZeroCountDetectorDescriptor<T>, INonZeroCountDetector>, INonZeroCountDetector where T : class
	{
		public NonZeroCountDetectorDescriptor(NonZeroCountFunction function) : base(function.GetStringValue()) { }

		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }

		public NonZeroCountDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(byFieldName, (a, v) => a.ByFieldName = v);

		public NonZeroCountDetectorDescriptor<T> ByFieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.ByFieldName = v);

		public NonZeroCountDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) =>
			Assign(partitionFieldName, (a, v) => a.PartitionFieldName = v);

		public NonZeroCountDetectorDescriptor<T> PartitionFieldName<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.PartitionFieldName = v);
	}

	public class DistinctCountDetectorDescriptor<T>
		: DetectorDescriptorBase<DistinctCountDetectorDescriptor<T>, IDistinctCountDetector>, IDistinctCountDetector where T : class
	{
		public DistinctCountDetectorDescriptor(DistinctCountFunction function) : base(function.GetStringValue()) { }

		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IFieldNameDetector.FieldName { get; set; }
		Field IOverFieldNameDetector.OverFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }

		public DistinctCountDetectorDescriptor<T> FieldName(Field fieldName) => Assign(fieldName, (a, v) => a.FieldName = v);

		public DistinctCountDetectorDescriptor<T> FieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.FieldName = v);

		public DistinctCountDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(byFieldName, (a, v) => a.ByFieldName = v);

		public DistinctCountDetectorDescriptor<T> ByFieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.ByFieldName = v);

		public DistinctCountDetectorDescriptor<T> OverFieldName(Field overFieldName) => Assign(overFieldName, (a, v) => a.OverFieldName = v);

		public DistinctCountDetectorDescriptor<T> OverFieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.OverFieldName = v);

		public DistinctCountDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) =>
			Assign(partitionFieldName, (a, v) => a.PartitionFieldName = v);

		public DistinctCountDetectorDescriptor<T> PartitionFieldName<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.PartitionFieldName = v);
	}

	public class CountDetector : CountDetectorBase
	{
		public CountDetector() : base(CountFunction.Count) { }
	}

	public class HighCountDetector : CountDetectorBase
	{
		public HighCountDetector() : base(CountFunction.HighCount) { }
	}

	public class LowCountDetector : CountDetectorBase
	{
		public LowCountDetector() : base(CountFunction.LowCount) { }
	}

	/// <summary>
	/// Detects anomalies when the number of events in a bucket is anomalous, but it ignores cases where the bucket count is zero.
	/// Use this function if you know your data is sparse or has gaps and the gaps are not important.
	/// </summary>
	public class NonZeroCountDetector : NonZeroCountDetectorBase
	{
		public NonZeroCountDetector() : base(NonZeroCountFunction.NonZeroCount) { }
	}

	/// <summary>
	/// Detects anomalies when the number of events in a bucket is unusually high and it ignores cases where the bucket count is zero.
	/// </summary>
	public class HighNonZeroCountDetector : NonZeroCountDetectorBase
	{
		public HighNonZeroCountDetector() : base(NonZeroCountFunction.HighNonZeroCount) { }
	}

	public class DistinctCountDetector : DistinctCountDetectorBase
	{
		public DistinctCountDetector() : base(DistinctCountFunction.DistinctCount) { }
	}

	public class HighDistinctCountDetector : DistinctCountDetectorBase
	{
		public HighDistinctCountDetector() : base(DistinctCountFunction.HighDistinctCount) { }
	}

	public class LowDistinctCountDetector : DistinctCountDetectorBase
	{
		public LowDistinctCountDetector() : base(DistinctCountFunction.LowDistinctCount) { }
	}

	/// <summary>
	/// Detects anomalies when the number of events in a bucket is unusually low and it ignores cases where the bucket count is zero.
	/// </summary>
	public class LowNonZeroCountDetector : NonZeroCountDetectorBase
	{
		public LowNonZeroCountDetector() : base(NonZeroCountFunction.LowNonZeroCount) { }
	}
}
