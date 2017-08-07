using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

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

	public interface ISumDetector : IDetector, IFieldNameDetector, IByFieldNameDetector, IOverFieldNameDetector,
		IPartitionFieldNameDetector
	{
	}

	public interface INonNullSumDetector : IDetector, IFieldNameDetector, IByFieldNameDetector, IPartitionFieldNameDetector
	{
	}

	public abstract class SumDetectorBase : DetectorBase, ISumDetector
	{
		public Field FieldName { get; set; }
		public Field ByFieldName { get; set; }
		public Field OverFieldName { get; set; }
		public Field PartitionFieldName { get; set; }

		protected SumDetectorBase(SumFunction function) : base(function.GetStringValue())
		{
		}
	}

	public abstract class NonNullSumDetectorBase : DetectorBase, INonNullSumDetector
	{
		public Field FieldName { get; set; }
		public Field ByFieldName { get; set; }
		public Field PartitionFieldName { get; set; }

		protected NonNullSumDetectorBase(NonNullSumFunction function) : base(function.GetStringValue())
		{
		}
	}

	public class SumDetector : SumDetectorBase
	{
		public SumDetector() : base(SumFunction.Sum) {}
	}

	public class HighSumDetector : SumDetectorBase
	{
		public HighSumDetector() : base(SumFunction.HighSum) {}
	}

	public class LowSumDetector : SumDetectorBase
	{
		public LowSumDetector() : base(SumFunction.LowSum) {}
	}

	public class NonNullSumDetector : NonNullSumDetectorBase
	{
		public NonNullSumDetector() : base(NonNullSumFunction.NonNullSum) {}
	}

	public class HighNonNullSumDetector : NonNullSumDetectorBase
	{
		public HighNonNullSumDetector() : base(NonNullSumFunction.HighNonNullSum) {}
	}

	public class LowNonNullSumDetector : NonNullSumDetectorBase
	{
		public LowNonNullSumDetector() : base(NonNullSumFunction.LowNonNullSum) {}
	}

	public class SumDetectorDescriptor<T> : DetectorDescriptorBase<SumDetectorDescriptor<T>, ISumDetector>, ISumDetector where T : class
	{
		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IOverFieldNameDetector.OverFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }
		Field IFieldNameDetector.FieldName { get; set; }

		public SumDetectorDescriptor(SumFunction function) : base(function.GetStringValue()) {}

		public SumDetectorDescriptor<T> FieldName(Field fieldName) => Assign(a => a.FieldName = fieldName);

		public SumDetectorDescriptor<T> FieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.FieldName = objectPath);

		public SumDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(a => a.ByFieldName = byFieldName);

		public SumDetectorDescriptor<T> ByFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.ByFieldName = objectPath);

		public SumDetectorDescriptor<T> OverFieldName(Field overFieldName) => Assign(a => a.OverFieldName = overFieldName);

		public SumDetectorDescriptor<T> OverFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.OverFieldName = objectPath);

		public SumDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) => Assign(a => a.PartitionFieldName = partitionFieldName);

		public SumDetectorDescriptor<T> PartitionFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.PartitionFieldName = objectPath);
	}

	public class NonNullSumDetectorDescriptor<T> : DetectorDescriptorBase<NonNullSumDetectorDescriptor<T>, INonNullSumDetector>, INonNullSumDetector where T : class
	{
		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }
		Field IFieldNameDetector.FieldName { get; set; }

		public NonNullSumDetectorDescriptor(NonNullSumFunction function) : base(function.GetStringValue()) {}

		public NonNullSumDetectorDescriptor<T> FieldName(Field fieldName) => Assign(a => a.FieldName = fieldName);

		public NonNullSumDetectorDescriptor<T> FieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.FieldName = objectPath);

		public NonNullSumDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(a => a.ByFieldName = byFieldName);

		public NonNullSumDetectorDescriptor<T> ByFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.ByFieldName = objectPath);

		public NonNullSumDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) => Assign(a => a.PartitionFieldName = partitionFieldName);

		public NonNullSumDetectorDescriptor<T> PartitionFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.PartitionFieldName = objectPath);
	}
}
