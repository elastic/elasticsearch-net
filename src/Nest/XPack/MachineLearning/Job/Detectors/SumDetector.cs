// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;

namespace Nest
{
	public enum SumFunction
	{
		Sum,
		HighSum,
		LowSum
	}

	public enum NonNullSumFunction
	{
		NonNullSum,
		HighNonNullSum,
		LowNonNullSum
	}

	public static class SumFunctionsExtensions
	{
		public static string GetStringValue(this SumFunction sumFunction)
		{
			switch (sumFunction)
			{
				case SumFunction.Sum:
					return "sum";
				case SumFunction.HighSum:
					return "high_sum";
				case SumFunction.LowSum:
					return "low_sum";
				default:
					throw new ArgumentOutOfRangeException(nameof(sumFunction), sumFunction, null);
			}
		}

		public static string GetStringValue(this NonNullSumFunction nonNullSumFunction)
		{
			switch (nonNullSumFunction)
			{
				case NonNullSumFunction.NonNullSum:
					return "non_null_sum";
				case NonNullSumFunction.HighNonNullSum:
					return "high_non_null_sum";
				case NonNullSumFunction.LowNonNullSum:
					return "low_non_null_sum";
				default:
					throw new ArgumentOutOfRangeException(nameof(nonNullSumFunction), nonNullSumFunction, null);
			}
		}
	}

	public interface ISumDetector
		: IDetector, IFieldNameDetector, IByFieldNameDetector, IOverFieldNameDetector,
			IPartitionFieldNameDetector { }

	public interface INonNullSumDetector : IDetector, IFieldNameDetector, IByFieldNameDetector, IPartitionFieldNameDetector { }

	public abstract class SumDetectorBase : DetectorBase, ISumDetector
	{
		protected SumDetectorBase(SumFunction function) : base(function.GetStringValue()) { }

		public Field ByFieldName { get; set; }
		public Field FieldName { get; set; }
		public Field OverFieldName { get; set; }
		public Field PartitionFieldName { get; set; }
	}

	public abstract class NonNullSumDetectorBase : DetectorBase, INonNullSumDetector
	{
		protected NonNullSumDetectorBase(NonNullSumFunction function) : base(function.GetStringValue()) { }

		public Field ByFieldName { get; set; }
		public Field FieldName { get; set; }
		public Field PartitionFieldName { get; set; }
	}

	public class SumDetector : SumDetectorBase
	{
		public SumDetector() : base(SumFunction.Sum) { }
	}

	public class HighSumDetector : SumDetectorBase
	{
		public HighSumDetector() : base(SumFunction.HighSum) { }
	}

	public class LowSumDetector : SumDetectorBase
	{
		public LowSumDetector() : base(SumFunction.LowSum) { }
	}

	public class NonNullSumDetector : NonNullSumDetectorBase
	{
		public NonNullSumDetector() : base(NonNullSumFunction.NonNullSum) { }
	}

	public class HighNonNullSumDetector : NonNullSumDetectorBase
	{
		public HighNonNullSumDetector() : base(NonNullSumFunction.HighNonNullSum) { }
	}

	public class LowNonNullSumDetector : NonNullSumDetectorBase
	{
		public LowNonNullSumDetector() : base(NonNullSumFunction.LowNonNullSum) { }
	}

	public class SumDetectorDescriptor<T> : DetectorDescriptorBase<SumDetectorDescriptor<T>, ISumDetector>, ISumDetector where T : class
	{
		public SumDetectorDescriptor(SumFunction function) : base(function.GetStringValue()) { }

		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IFieldNameDetector.FieldName { get; set; }
		Field IOverFieldNameDetector.OverFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }

		public SumDetectorDescriptor<T> FieldName(Field fieldName) => Assign(fieldName, (a, v) => a.FieldName = v);

		public SumDetectorDescriptor<T> FieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.FieldName = v);

		public SumDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(byFieldName, (a, v) => a.ByFieldName = v);

		public SumDetectorDescriptor<T> ByFieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.ByFieldName = v);

		public SumDetectorDescriptor<T> OverFieldName(Field overFieldName) => Assign(overFieldName, (a, v) => a.OverFieldName = v);

		public SumDetectorDescriptor<T> OverFieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.OverFieldName = v);

		public SumDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) => Assign(partitionFieldName, (a, v) => a.PartitionFieldName = v);

		public SumDetectorDescriptor<T> PartitionFieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.PartitionFieldName = v);
	}

	public class NonNullSumDetectorDescriptor<T> : DetectorDescriptorBase<NonNullSumDetectorDescriptor<T>, INonNullSumDetector>, INonNullSumDetector
		where T : class
	{
		public NonNullSumDetectorDescriptor(NonNullSumFunction function) : base(function.GetStringValue()) { }

		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IFieldNameDetector.FieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }

		public NonNullSumDetectorDescriptor<T> FieldName(Field fieldName) => Assign(fieldName, (a, v) => a.FieldName = v);

		public NonNullSumDetectorDescriptor<T> FieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.FieldName = v);

		public NonNullSumDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(byFieldName, (a, v) => a.ByFieldName = v);

		public NonNullSumDetectorDescriptor<T> ByFieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.ByFieldName = v);

		public NonNullSumDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) => Assign(partitionFieldName, (a, v) => a.PartitionFieldName = v);

		public NonNullSumDetectorDescriptor<T> PartitionFieldName<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.PartitionFieldName = v);
	}
}
