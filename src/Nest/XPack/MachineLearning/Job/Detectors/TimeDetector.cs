// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
			switch (timeFunction)
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

	public interface ITimeDetector
		: IDetector, IByFieldNameDetector, IOverFieldNameDetector,
			IPartitionFieldNameDetector { }

	public abstract class TimeDetectorBase : DetectorBase, ITimeDetector
	{
		protected TimeDetectorBase(TimeFunction function) : base(function.GetStringValue()) { }

		public Field ByFieldName { get; set; }
		public Field OverFieldName { get; set; }
		public Field PartitionFieldName { get; set; }
	}

	public class TimeOfDayDetector : TimeDetectorBase
	{
		public TimeOfDayDetector() : base(TimeFunction.TimeOfDay) { }
	}

	public class TimeOfWeekDetector : TimeDetectorBase
	{
		public TimeOfWeekDetector() : base(TimeFunction.TimeOfWeek) { }
	}

	public class TimeDetectorDescriptor<T> : DetectorDescriptorBase<TimeDetectorDescriptor<T>, ITimeDetector>, ITimeDetector where T : class
	{
		public TimeDetectorDescriptor(TimeFunction function) : base(function.GetStringValue()) { }

		Field IByFieldNameDetector.ByFieldName { get; set; }
		Field IOverFieldNameDetector.OverFieldName { get; set; }
		Field IPartitionFieldNameDetector.PartitionFieldName { get; set; }

		public TimeDetectorDescriptor<T> ByFieldName(Field byFieldName) => Assign(byFieldName, (a, v) => a.ByFieldName = v);

		public TimeDetectorDescriptor<T> ByFieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.ByFieldName = v);

		public TimeDetectorDescriptor<T> OverFieldName(Field overFieldName) => Assign(overFieldName, (a, v) => a.OverFieldName = v);

		public TimeDetectorDescriptor<T> OverFieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.OverFieldName = v);

		public TimeDetectorDescriptor<T> PartitionFieldName(Field partitionFieldName) => Assign(partitionFieldName, (a, v) => a.PartitionFieldName = v);

		public TimeDetectorDescriptor<T> PartitionFieldName<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.PartitionFieldName = v);
	}
}
