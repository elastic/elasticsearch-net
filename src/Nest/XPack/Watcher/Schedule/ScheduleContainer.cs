// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Internal;

namespace Nest
{
	[JsonFormatter(typeof(ScheduleContainerFormatter))]
	public interface IScheduleContainer
	{
		CronExpression Cron { get; set; }
		ICronExpressions CronExpressions { get; set; }
		IDailySchedule Daily { get; set; }
		IHourlySchedule Hourly { get; set; }
		Interval Interval { get; set; }
		IMonthlySchedule Monthly { get; set; }
		IWeeklySchedule Weekly { get; set; }
		IYearlySchedule Yearly { get; set; }
	}
	
	public class ScheduleContainer : TriggerBase, IScheduleContainer
	{
		public ScheduleContainer() { }

		public ScheduleContainer(ScheduleBase schedule)
		{
			schedule.ThrowIfNull(nameof(schedule));
			schedule.WrapInContainer(this);
		}

		public CronExpression Cron { get; set; }
		public ICronExpressions CronExpressions { get; set; }
		public IDailySchedule Daily { get; set; }
		public IHourlySchedule Hourly { get; set; }
		public Interval Interval { get; set; }
		public IMonthlySchedule Monthly { get; set; }
		public IWeeklySchedule Weekly { get; set; }
		public IYearlySchedule Yearly { get; set; }

		internal override void WrapInContainer(ITriggerContainer container) => container.Schedule = this;

		public static implicit operator ScheduleContainer(ScheduleBase scheduleBase) => scheduleBase == null
			? null
			: new ScheduleContainer(scheduleBase);		
	}

	public class ScheduleDescriptor : DescriptorBase<ScheduleDescriptor, IScheduleContainer>, IScheduleContainer
	{
		CronExpression IScheduleContainer.Cron { get; set; }
		ICronExpressions IScheduleContainer.CronExpressions { get; set; }
		IDailySchedule IScheduleContainer.Daily { get; set; }
		IHourlySchedule IScheduleContainer.Hourly { get; set; }
		Interval IScheduleContainer.Interval { get; set; }
		IMonthlySchedule IScheduleContainer.Monthly { get; set; }
		IWeeklySchedule IScheduleContainer.Weekly { get; set; }
		IYearlySchedule IScheduleContainer.Yearly { get; set; }

		public ScheduleDescriptor Daily(Func<DailyScheduleDescriptor, IDailySchedule> selector) =>
			Assign(selector.Invoke(new DailyScheduleDescriptor()), (a, v) => a.Daily = v);

		public ScheduleDescriptor Hourly(Func<HourlyScheduleDescriptor, IHourlySchedule> selector) =>
			Assign(selector.Invoke(new HourlyScheduleDescriptor()), (a, v) => a.Hourly = v);

		public ScheduleDescriptor Monthly(Func<MonthlyScheduleDescriptor, IPromise<IMonthlySchedule>> selector) =>
			Assign(selector.Invoke(new MonthlyScheduleDescriptor())?.Value, (a, v) => a.Monthly = v);

		public ScheduleDescriptor Weekly(Func<WeeklyScheduleDescriptor, IPromise<IWeeklySchedule>> selector) =>
			Assign(selector.Invoke(new WeeklyScheduleDescriptor())?.Value, (a, v) => a.Weekly = v);

		public ScheduleDescriptor Yearly(Func<YearlyScheduleDescriptor, IPromise<IYearlySchedule>> selector) =>
			Assign(selector.Invoke(new YearlyScheduleDescriptor())?.Value, (a, v) => a.Yearly = v);

		public ScheduleDescriptor Cron(CronExpression cron) => Assign(cron, (a, v) =>
		{
			if (a.CronExpressions is not null)
				a.CronExpressions.Add(v);
			else if (a.Cron is null)
				a.Cron = v;			
			else
			{
				a.Cron = null;
				a.CronExpressions = new CronExpressions(v);
			}
		});

		// TODO docs
		public ScheduleDescriptor Cron(CronExpressions expressions) => Assign(expressions, (a, v) =>
			a.CronExpressions = v);

		public ScheduleDescriptor Cron(params CronExpression[] expressions) => Assign(expressions, (a, v) =>
			a.CronExpressions = new CronExpressions(expressions));

		public ScheduleDescriptor Cron(IEnumerable<CronExpression> expressions) => Assign(expressions, (a, v) =>
			a.CronExpressions = new CronExpressions(v));

		public ScheduleDescriptor CronExpressions(Func<CronExpressionsDescriptor, IPromise<ICronExpressions>> selector) =>
			Assign(selector.Invoke(new CronExpressionsDescriptor())?.Value, (a, v) => a.CronExpressions = v);

		public ScheduleDescriptor Interval(Interval interval) => Assign(interval, (a, v) => a.Interval = v);
	}

	internal class ScheduleContainerFormatter : IJsonFormatter<IScheduleContainer>
	{
		private static readonly AutomataDictionary ScheduleTypes = new()
		{
			{ Parser.Cron, 0 },
			{ Parser.Hourly, 1 },
			{ Parser.Daily, 2 },
			{ Parser.Weekly, 3 },
			{ Parser.Monthly, 4 },
			{ Parser.Yearly, 5 },
			{ Parser.Interval, 6 }
		};

		public IScheduleContainer Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				return null;

			reader.ReadNext(); // {
			var property = reader.ReadPropertyNameSegmentRaw();

			IScheduleContainer item = null;
			if (ScheduleTypes.TryGetValue(property, out var value))
			{
				switch (value)
				{
					case 0:
						item = GetCronExpressionContainer(ref reader, formatterResolver);
						break;

					case 1:
						item = GetScheduleContainer<IHourlySchedule>(ref reader, formatterResolver);
						break;

					case 2:
						item = GetScheduleContainer<IDailySchedule>(ref reader, formatterResolver);
						break;

					case 3:
						item = GetScheduleContainer<IWeeklySchedule>(ref reader, formatterResolver);
						break;

					case 4:
						item = GetScheduleContainer<IMonthlySchedule>(ref reader, formatterResolver);
						break;

					case 5:
						item = GetScheduleContainer<IYearlySchedule>(ref reader, formatterResolver);
						break;

					case 6:
						item = GetScheduleContainer<Interval>(ref reader, formatterResolver);
						break;
				}
			}
			else
				reader.ReadNextBlock();

			reader.ReadNext(); // }
			return item;
		}

		private IScheduleContainer GetScheduleContainer<T>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var formatter = formatterResolver.GetFormatter<T>();
			var schedule = formatter.Deserialize(ref reader, formatterResolver);
			return schedule is ScheduleBase scheduleBase ? new ScheduleContainer(scheduleBase) : null;
		}

		private IScheduleContainer GetCronExpressionContainer(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();

			if (token == JsonToken.String)
			{
				var expression = reader.ReadString();

				if (!string.IsNullOrEmpty(expression))
				{
					return new ScheduleContainer(new CronExpression(expression));
				}
				
				throw new Exception("Trigger schedule string value should not be null or empty");
			}

			if (token == JsonToken.BeginArray)
			{
				var keys = new List<CronExpression>();
				var count = 0;

				while (reader.ReadIsInArray(ref count))
				{
					string keyItem;

					var keyToken = reader.GetCurrentJsonToken();

					if (keyToken == JsonToken.String)
					{
						keyItem = reader.ReadString();
						keys.Add(new CronExpression(keyItem));
					}
				}
				return new ScheduleContainer(new CronExpressions(keys));
			}

			throw new Exception("Unexpected JSON in trigger schedule");
		}

		public void Serialize(ref JsonWriter writer, IScheduleContainer value, IJsonFormatterResolver formatterResolver)
		{
			writer.WriteBeginObject();

			if (value.CronExpressions is not null)
			{
				writer.WritePropertyName(Parser.Cron);
				var formatter = formatterResolver.GetFormatter<ICronExpressions>();
				formatter.Serialize(ref writer, value.CronExpressions, formatterResolver);
			}
			else if (value.Cron is not null)
			{
				writer.WritePropertyName(Parser.Cron);
				var formatter = formatterResolver.GetFormatter<CronExpression>();
				formatter.Serialize(ref writer, value.Cron, formatterResolver);
			}
			else if (value.Hourly is not null)
			{
				writer.WritePropertyName(Parser.Hourly);
				var formatter = formatterResolver.GetFormatter<IHourlySchedule>();
				formatter.Serialize(ref writer, value.Hourly, formatterResolver);
			}
			else if (value.Daily is not null)
			{
				writer.WritePropertyName(Parser.Daily);
				var formatter = formatterResolver.GetFormatter<IDailySchedule>();
				formatter.Serialize(ref writer, value.Daily, formatterResolver);
			}
			else if (value.Weekly is not null)
			{
				writer.WritePropertyName(Parser.Weekly);
				var formatter = formatterResolver.GetFormatter<IWeeklySchedule>();
				formatter.Serialize(ref writer, value.Weekly, formatterResolver);
			}
			else if (value.Monthly is not null)
			{
				writer.WritePropertyName(Parser.Monthly);
				var formatter = formatterResolver.GetFormatter<IMonthlySchedule>();
				formatter.Serialize(ref writer, value.Monthly, formatterResolver);
			}
			else if (value.Yearly is not null)
			{
				writer.WritePropertyName(Parser.Yearly);
				var formatter = formatterResolver.GetFormatter<IYearlySchedule>();
				formatter.Serialize(ref writer, value.Yearly, formatterResolver);
			}
			else if (value.Interval is not null)
			{
				writer.WritePropertyName(Parser.Interval);
				var formatter = formatterResolver.GetFormatter<Interval>();
				formatter.Serialize(ref writer, value.Interval, formatterResolver);
			}

			writer.WriteEndObject();
		}

		private static class Parser
		{
			public const string Cron = "cron";
			public const string Hourly = "hourly";
			public const string Daily = "daily";
			public const string Weekly = "weekly";
			public const string Monthly = "monthly";
			public const string Yearly = "yearly";
			public const string Interval = "interval";
		}
	}
}
