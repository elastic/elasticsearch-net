using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IAction
	{
		[IgnoreDataMember]
		ActionType ActionType { get; }

		[IgnoreDataMember]
		string Name { get; set; }

		[IgnoreDataMember]
		Time ThrottlePeriod { get; set; }

		[DataMember(Name = "transform")]
		TransformContainer Transform { get; set; }
	}

	public abstract class ActionBase : IAction
	{
		internal ActionBase() { }

		protected ActionBase(string name) => Name = name;

		public abstract ActionType ActionType { get; }

		public string Name { get; set; }

		public Time ThrottlePeriod { get; set; }

		public TransformContainer Transform { get; set; }

		public static bool operator false(ActionBase a) => false;

		public static bool operator true(ActionBase a) => false;

		public static ActionBase operator &(ActionBase left, ActionBase right) =>
			new ActionCombinator(left, right);
	}

	internal class ActionCombinator : ActionBase, IAction
	{
		public ActionCombinator(ActionBase left, ActionBase right) : base(null)
		{
			AddAction(left);
			AddAction(right);
		}

		public override ActionType ActionType => (ActionType)10;
		internal List<ActionBase> Actions { get; } = new List<ActionBase>();

		private void AddAction(ActionBase agg)
		{
			if (agg == null) return;

			var combinator = agg as ActionCombinator;
			if ((combinator?.Actions.HasAny()).GetValueOrDefault(false))
				Actions.AddRange(combinator.Actions);
			else Actions.Add(agg);
		}
	}

	internal class ActionsFormatter : IJsonFormatter<Actions>
	{
		public Actions Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var dictionary = new Dictionary<string, IAction>();

			var count = 0;
			while (reader.ReadIsInObject(ref count))
			{
				var name = reader.ReadPropertyName();
				var actionCount = 0;

				Time throttlePeriod = null;
				IAction action = null;

				while (reader.ReadIsInObject(ref actionCount))
				{
					var propertyName = reader.ReadPropertyName();
					switch (propertyName)
					{
						case "throttle_period":
							throttlePeriod = formatterResolver.GetFormatter<Time>()
								.Deserialize(ref reader, formatterResolver);
							break;
						default:
							// TODO: Introduce AutomataDictionary
							var actionType = propertyName.ToEnum<ActionType>();
							switch (actionType)
							{
								case ActionType.Email:
									action = formatterResolver.GetFormatter<EmailAction>()
										.Deserialize(ref reader, formatterResolver);
									break;
								case ActionType.Webhook:
									action = formatterResolver.GetFormatter<WebhookAction>()
										.Deserialize(ref reader, formatterResolver);
									break;
								case ActionType.Index:
									action = formatterResolver.GetFormatter<IndexAction>()
										.Deserialize(ref reader, formatterResolver);
									break;
								case ActionType.Logging:
									action = formatterResolver.GetFormatter<LoggingAction>()
										.Deserialize(ref reader, formatterResolver);
									break;
								case ActionType.Slack:
									action = formatterResolver.GetFormatter<SlackAction>()
										.Deserialize(ref reader, formatterResolver);
									break;
								case ActionType.PagerDuty:
									action = formatterResolver.GetFormatter<PagerDutyAction>()
										.Deserialize(ref reader, formatterResolver);
									break;
								case null:
									break;
							}
							break;
					}
				}

				if (action != null)
				{
					action.Name = name;
					action.ThrottlePeriod = throttlePeriod;
					dictionary.Add(name, action);
				}
			}

			return new Actions(dictionary);
		}

		public void Serialize(ref JsonWriter writer, Actions value, IJsonFormatterResolver formatterResolver)
		{
			writer.WriteBeginObject();
			if (value != null)
			{
				var count = 0;
				foreach (var kvp in value.Where(kv => kv.Value != null))
				{
					if (count > 0)
						writer.WriteValueSeparator();

					var action = kvp.Value;
					writer.WritePropertyName(kvp.Key);
					writer.WriteBeginObject();
					if (action.ThrottlePeriod != null)
					{
						writer.WritePropertyName("throttle_period");
						var timeFormatter = formatterResolver.GetFormatter<Time>();
						timeFormatter.Serialize(ref writer, action.ThrottlePeriod, formatterResolver);
						writer.WriteValueSeparator();
					}
					writer.WritePropertyName(kvp.Value.ActionType.GetStringValue());
					var actionFormatter = formatterResolver.GetFormatter<IAction>();
					actionFormatter.Serialize(ref writer, action, formatterResolver);
					writer.WriteEndObject();

					count++;
				}
			}
			writer.WriteEndObject();
		}
	}
}
