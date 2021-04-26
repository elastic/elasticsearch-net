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
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Resolvers;


namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(TriggerContainerInterfaceFormatter))]
	public interface ITriggerContainer
	{
		[DataMember(Name ="schedule")]
		IScheduleContainer Schedule { get; set; }
	}

	[JsonFormatter(typeof(TriggerContainerFormatter))]
	public class TriggerContainer : ITriggerContainer, IDescriptor
	{
		public TriggerContainer() { }

		public TriggerContainer(TriggerBase trigger)
		{
			trigger.ThrowIfNull(nameof(trigger));
			trigger.WrapInContainer(this);
		}

		IScheduleContainer ITriggerContainer.Schedule { get; set; }
	}

	public class TriggerDescriptor : TriggerContainer
	{
		private TriggerDescriptor Assign<TValue>(TValue value, Action<ITriggerContainer, TValue> assigner) => Fluent.Assign(this, value, assigner);

		public TriggerDescriptor Schedule(Func<ScheduleDescriptor, IScheduleContainer> selector) =>
			Assign(selector, (a, v) => a.Schedule = v?.InvokeOrDefault(new ScheduleDescriptor()));
	}

	internal class TriggerContainerFormatter : IJsonFormatter<TriggerContainer>
	{
		public TriggerContainer Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var formatter = DynamicObjectResolver.AllowPrivateExcludeNullCamelCase.GetFormatter<TriggerContainer>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}

		public void Serialize(ref JsonWriter writer, TriggerContainer value, IJsonFormatterResolver formatterResolver)
		{
			var queryFormatter = formatterResolver.GetFormatter<ITriggerContainer>();
			queryFormatter.Serialize(ref writer, value, formatterResolver);
		}
	}

	internal class TriggerContainerInterfaceFormatter : IJsonFormatter<ITriggerContainer>
	{
		public ITriggerContainer Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var formatter = formatterResolver.GetFormatter<TriggerContainer>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}

		public void Serialize(ref JsonWriter writer, ITriggerContainer value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var formatter = DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<ITriggerContainer>();
			formatter.Serialize(ref writer, value, formatterResolver);
		}
	}
}
