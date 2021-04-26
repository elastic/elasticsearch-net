/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
