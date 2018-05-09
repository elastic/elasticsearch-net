using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IAction
	{
		[JsonIgnore]
		string Name { get; set; }

		[JsonIgnore]
		ActionType ActionType { get; }

		[JsonProperty("transform")]
		TransformContainer Transform { get; set; }

		[JsonIgnore]
		Time ThrottlePeriod { get; set; }
	}

	public abstract class ActionBase : IAction
	{
		internal ActionBase() {}

		protected ActionBase(string name) => this.Name = name;

		public string Name { get; set; }

		public abstract ActionType ActionType { get; }

		public TransformContainer Transform { get; set; }

		public Time ThrottlePeriod { get; set; }

		public static bool operator false(ActionBase a) => false;

		public static bool operator true(ActionBase a) => false;

		public static ActionBase operator &(ActionBase left, ActionBase right) =>
			new ActionCombinator(left, right);
	}

	internal class ActionCombinator : ActionBase, IAction
	{
		internal List<ActionBase> Actions { get; } = new List<ActionBase>();

		public ActionCombinator(ActionBase left, ActionBase right) : base(null)
		{
			this.AddAction(left);
			this.AddAction(right);
		}

		private void AddAction(ActionBase agg)
		{
			if (agg == null) return;
			var combinator = agg as ActionCombinator;
			if ((combinator?.Actions.HasAny()).GetValueOrDefault(false))
			{
				this.Actions.AddRange(combinator.Actions);
			}
			else this.Actions.Add(agg);
		}

		public override ActionType ActionType => (ActionType)10;
	}

	internal class ActionsJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var dictionary = new Dictionary<string, IAction>();
			var actions = JObject.Load(reader);
			foreach (var child in actions.Children())
			{
				var property = child as JProperty;
				if (property == null) continue;
				var name = property.Name;

				var actionJson = property.Value as JObject;
				if (actionJson == null) continue;

				Time throttlePeriod = null;
				IAction action = null;

				foreach (var prop in actionJson.Properties())
				{
					if (prop.Name == "throttle_period")
						throttlePeriod = prop.Value.ToObject<Time>();
					else
					{
						var actionType = prop.Name.ToEnum<ActionType>();
						switch (actionType)
						{
							case ActionType.Email:
								action = prop.Value.ToObject<EmailAction>();
								break;
							case ActionType.Webhook:
								action = prop.Value.ToObject<WebhookAction>();
								break;
							case ActionType.Index:
								action = prop.Value.ToObject<IndexAction>();
								break;
							case ActionType.Logging:
								action = prop.Value.ToObject<LoggingAction>();
								break;
							case ActionType.HipChat:
								action = prop.Value.ToObject<HipChatAction>();
								break;
							case ActionType.Slack:
								action = prop.Value.ToObject<SlackAction>();
								break;
							case ActionType.PagerDuty:
								action = prop.Value.ToObject<PagerDutyAction>();
								break;
							case null:
								break;
						}

						if (action != null)
						{
							action.Name = name;
							action.ThrottlePeriod = throttlePeriod;
							dictionary.Add(name, action);
						}
					}
				}
			}

			return new Actions(dictionary);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteStartObject();
			var actions = value as IDictionary<string, IAction>;
			if (actions != null)
			{
				foreach (var kvp in actions.Where(kv => kv.Value != null))
				{
					var action = kvp.Value;
					writer.WritePropertyName(kvp.Key);
					writer.WriteStartObject();
					if (action.ThrottlePeriod != null)
					{
						writer.WritePropertyName("throttle_period");
						serializer.Serialize(writer, action.ThrottlePeriod);
					}
					writer.WritePropertyName(kvp.Value.ActionType.GetStringValue());
					serializer.Serialize(writer, action);
					writer.WriteEndObject();
				}
			}
			writer.WriteEndObject();
		}
	}
}
