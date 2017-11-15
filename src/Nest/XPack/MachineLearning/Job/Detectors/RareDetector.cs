using System;
using System.Linq.Expressions;

namespace Nest
{
	public enum RareFunction
	{
		Rare,
		FreqRare
	}

	public static class RareFunctionsExtensions
	{
		public static string GetStringValue(this RareFunction rareFunction)
		{
			switch (rareFunction)
			{
				case RareFunction.Rare:
					return "rare";
				case RareFunction.FreqRare:
					return "freq_rare";
				default:
					throw new ArgumentOutOfRangeException(nameof(rareFunction), rareFunction, null);
			}
		}
	}

	public interface IRareDetector : IDetector, IByFieldNameDetector, IOverFieldNameDetector,
		IPartitionFieldNameDetector
	{
	}

	public abstract class RareDetectorBase : DetectorBase, IRareDetector
	{
		public Field ByFieldName { get; set; }
		public Field OverFieldName { get; set; }
		public Field PartitionFieldName { get; set; }

		protected RareDetectorBase(RareFunction function) : base(function.GetStringValue())
		{
		}
	}

	public class RareDetectorDescriptor<T> : DetectorDescriptorBase<RareDetectorDescriptor<T>, IRareDetector>, IRareDetector where T : class
	{
		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IOverFieldNameDetector.OverFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }

		public RareDetectorDescriptor(RareFunction function) : base(function.GetStringValue()) {}

		public RareDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(a => a.ByFieldName = byFieldName);

		public RareDetectorDescriptor<T> ByFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.ByFieldName = objectPath);

		public RareDetectorDescriptor<T> OverFieldName(Field overFieldName) => Assign(a => a.OverFieldName = overFieldName);

		public RareDetectorDescriptor<T> OverFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.OverFieldName = objectPath);

		public RareDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) => Assign(a => a.PartitionFieldName = partitionFieldName);

		public RareDetectorDescriptor<T> PartitionFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.PartitionFieldName = objectPath);
	}

	public class RareDetector : RareDetectorBase
	{
		public RareDetector() : base(RareFunction.Rare) {}
	}

	public class FreqRareDetector : RareDetectorBase
	{
		public FreqRareDetector() : base(RareFunction.FreqRare) {}
	}
}
