using System;
using System.Linq.Expressions;

namespace Nest
{
	public enum TimeFunction
	{
		TimeOfDay,
		TimeOfWeek
	}

	public static class TimeFunctionsExtensions
	{
		public static string GetStringValue(this TimeFunction timeFunction)
		{
			switch(timeFunction)
			{
				case TimeFunction.TimeOfDay:
					return "time_of_day";
				case TimeFunction.TimeOfWeek:
					return "time_of_week";
				default:
					throw new ArgumentOutOfRangeException(nameof(timeFunction), timeFunction, null);
			}
		}
	}

	public interface ITimeDetector : IDetector, IByFieldNameDetector, IOverFieldNameDetector,
		IPartitionFieldNameDetector
	{
	}

	public abstract class TimeDetectorBase : DetectorBase, ITimeDetector
	{
		protected TimeDetectorBase(TimeFunction function) : base(function.GetStringValue()) {}

		public Field ByFieldName { get; set; }
		public Field OverFieldName { get; set; }
		public Field PartitionFieldName { get; set; }
	}

	public class TimeOfDayDetector : TimeDetectorBase
	{
		public TimeOfDayDetector() : base(TimeFunction.TimeOfDay) {}
	}

	public class TimeOfWeekDetector : TimeDetectorBase
	{
		public TimeOfWeekDetector() : base(TimeFunction.TimeOfWeek) {}
	}

	public class TimeDetectorDescriptor<T> : DetectorDescriptorBase<TimeDetectorDescriptor<T>, ITimeDetector>, ITimeDetector where T : class
	{
		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IOverFieldNameDetector.OverFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }

		public TimeDetectorDescriptor(TimeFunction function) : base(function.GetStringValue()) {}

		public TimeDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(a => a.ByFieldName = byFieldName);

		public TimeDetectorDescriptor<T> ByFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.ByFieldName = objectPath);

		public TimeDetectorDescriptor<T> OverFieldName(Field overFieldName) => Assign(a => a.OverFieldName = overFieldName);

		public TimeDetectorDescriptor<T> OverFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.OverFieldName = objectPath);

		public TimeDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) => Assign(a => a.PartitionFieldName = partitionFieldName);

		public TimeDetectorDescriptor<T> PartitionFieldName(Expression<Func<T, object>> objectPath) => Assign(a => a.PartitionFieldName = objectPath);
	}
}
