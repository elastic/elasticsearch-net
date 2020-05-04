// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(ScheduleFormatter<IWeeklySchedule, WeeklySchedule, ITimeOfWeek>))]
	public interface IWeeklySchedule : ISchedule, IEnumerable<ITimeOfWeek> { }

	public class WeeklySchedule : ScheduleBase, IWeeklySchedule
	{
		private List<ITimeOfWeek> _times;

		public WeeklySchedule(IEnumerable<ITimeOfWeek> times) => _times = times?.ToList();

		public WeeklySchedule(params ITimeOfWeek[] times) => _times = times?.ToList();

		public WeeklySchedule() { }

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public IEnumerator<ITimeOfWeek> GetEnumerator() => _times?.GetEnumerator() ?? Enumerable.Empty<ITimeOfWeek>().GetEnumerator();

		public void Add(ITimeOfWeek time)
		{
			if (_times == null) _times = new List<ITimeOfWeek>();
			_times.Add(time);
		}

		internal override void WrapInContainer(IScheduleContainer container) => container.Weekly = this;

		public static implicit operator WeeklySchedule(ITimeOfWeek[] timesOfWeek) =>
			new WeeklySchedule(timesOfWeek);
	}

	public class WeeklyScheduleDescriptor : DescriptorPromiseBase<WeeklyScheduleDescriptor, WeeklySchedule>
	{
		public WeeklyScheduleDescriptor() : base(new WeeklySchedule()) { }

		public WeeklyScheduleDescriptor Add(Func<TimeOfWeekDescriptor, ITimeOfWeek> selector) =>
			Assign(selector, (a, v) => a.Add(v.InvokeOrDefault(new TimeOfWeekDescriptor())));
	}

	internal class ScheduleFormatter<TSchedule, TReadAsSchedule, TTime> : IJsonFormatter<TSchedule>
		where TSchedule : class, IEnumerable<TTime>
		where TReadAsSchedule : class, TSchedule
	{
		public TSchedule Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();

			var times = token == JsonToken.BeginArray
				? formatterResolver.GetFormatter<IEnumerable<TTime>>().Deserialize(ref reader, formatterResolver)
				: new[] { formatterResolver.GetFormatter<TTime>().Deserialize(ref reader, formatterResolver) };

			var schedule = (TSchedule)typeof(TReadAsSchedule).CreateInstance(times);
			return schedule;
		}

		public void Serialize(ref JsonWriter writer, TSchedule value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			if (value.Count() == 1)
			{
				var formatter = formatterResolver.GetFormatter<TTime>();
				formatter.Serialize(ref writer, value.First(), formatterResolver);
			}
			else
			{
				var formatter = formatterResolver.GetFormatter<IEnumerable<TTime>>();
				formatter.Serialize(ref writer, value, formatterResolver);
			}
		}
	}
}
