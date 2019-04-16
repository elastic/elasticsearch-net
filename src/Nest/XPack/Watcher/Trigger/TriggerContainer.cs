using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;


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
		private TriggerDescriptor Assign(Action<ITriggerContainer> assigner) => Fluent.Assign(this, assigner);

		public TriggerDescriptor Schedule(Func<ScheduleDescriptor, IScheduleContainer> selector) =>
			Assign(a => a.Schedule = selector?.InvokeOrDefault(new ScheduleDescriptor()));
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
