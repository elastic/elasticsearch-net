// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Elastic.Transport.Extensions;
using Nest.Utf8Json;
namespace Nest
{
	/// <summary>
	/// A Watcher action
	/// </summary>
	[InterfaceDataContract]
	public interface IAction
	{
		/// <summary>
		/// The type of action
		/// </summary>
		[IgnoreDataMember]
		ActionType ActionType { get; }

		/// <summary>
		/// The name of the action
		/// </summary>
		[IgnoreDataMember]
		string Name { get; set; }

		/// <summary>
		/// Limit how often an action is executed, after it has been executed.
		/// When a throttling period is set, repeated executions of the action are prevented if it has already
		/// executed within the throttling period time frame (now - throttling period).
		/// </summary>
		[IgnoreDataMember]
		Time ThrottlePeriod { get; set; }

		/// <summary>
		/// Trigger the configured action for every element within an array
		/// defined by the path assigned.
		/// <para />
		/// Valid only in Elasticsearch 7.3.0+
		/// </summary>
		[IgnoreDataMember]
		string Foreach { get; set; }

		[IgnoreDataMember]
		/// <summary>The maximum number of iterations that each watch executes. If this limit is reached,
		/// the execution is gracefully stopped. Defaults to <c>100</c>.
		/// </summary>
		int? MaxIterations  { get; set; }

		/// <summary>
		/// Transforms the payload before executing the action. The transformation is only applied
		/// for the payload for this action.
		/// </summary>
		[IgnoreDataMember]
		TransformContainer Transform { get; set; }

		/// <summary>
		/// A condition for the action. Allows a single watch to specify multiple actions, but
		/// further control when each action will be executed.
		/// </summary>
		[IgnoreDataMember]
		ConditionContainer Condition { get; set; }
	}

	/// <inheritdoc />
	public abstract class ActionBase : IAction
	{
		internal ActionBase() { }

		protected ActionBase(string name) => Name = name;

		public abstract ActionType ActionType { get; }

		/// <inheritdoc />
		public string Name { get; set; }

		/// <inheritdoc />
		public Time ThrottlePeriod { get; set; }

		/// <inheritdoc />
		public string Foreach { get; set; }

		/// <inheritdoc />
		public int? MaxIterations { get; set; }

		/// <inheritdoc />
		public TransformContainer Transform { get; set; }

		/// <inheritdoc />
		public ConditionContainer Condition { get; set; }

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

	internal class ActionsInterfaceFormatter : IJsonFormatter<IActions>
	{
		private static readonly ActionsFormatter ActionsFormatter = new ActionsFormatter();

		public void Serialize(ref JsonWriter writer, IActions value, IJsonFormatterResolver formatterResolver)
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

					if (!string.IsNullOrEmpty(action.Foreach))
					{
						writer.WritePropertyName("foreach");
						writer.WriteString(action.Foreach);
						writer.WriteValueSeparator();
					}
					if (action.MaxIterations.HasValue)
					{
						writer.WritePropertyName("max_iterations");
						writer.WriteInt32(action.MaxIterations.Value);
						writer.WriteValueSeparator();
					}

					if (action.Transform != null)
					{
						writer.WritePropertyName("transform");
						formatterResolver.GetFormatter<TransformContainer>().Serialize(ref writer, action.Transform, formatterResolver);
						writer.WriteValueSeparator();
					}

					if (action.Condition != null)
					{
						writer.WritePropertyName("condition");
						formatterResolver.GetFormatter<ConditionContainer>().Serialize(ref writer, action.Condition, formatterResolver);
						writer.WriteValueSeparator();
					}

					writer.WritePropertyName(kvp.Value.ActionType.GetStringValue());

					switch (action.ActionType)
					{
						case ActionType.Email:
							Serialize<IEmailAction>(ref writer, action, formatterResolver);
							break;
						case ActionType.Webhook:
							Serialize<IWebhookAction>(ref writer, action, formatterResolver);
							break;
						case ActionType.Index:
							Serialize<IIndexAction>(ref writer, action, formatterResolver);
							break;
						case ActionType.Logging:
							Serialize<ILoggingAction>(ref writer, action, formatterResolver);
							break;
						case ActionType.Slack:
							Serialize<ISlackAction>(ref writer, action, formatterResolver);
							break;
						case ActionType.PagerDuty:
							Serialize<IPagerDutyAction>(ref writer, action, formatterResolver);
							break;
						default:
							var actionFormatter = formatterResolver.GetFormatter<IAction>();
							actionFormatter.Serialize(ref writer, action, formatterResolver);
							break;
					}

					writer.WriteEndObject();
					count++;
				}
			}
			writer.WriteEndObject();
		}

		private static void Serialize<TAction>(ref JsonWriter writer, IAction value, IJsonFormatterResolver formatterResolver)
			where TAction : class, IAction
		{
			var formatter = formatterResolver.GetFormatter<TAction>();
			formatter.Serialize(ref writer, value as TAction, formatterResolver);
		}
		public IActions Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			ActionsFormatter.Deserialize(ref reader, formatterResolver);
	}

	internal class ActionsFormatter : IJsonFormatter<Actions>
	{
		private static readonly ActionsInterfaceFormatter ActionsInterfaceFormatter = new ActionsInterfaceFormatter();

		private static readonly AutomataDictionary Fields = new AutomataDictionary
		{
			{ "throttle_period", 0 },
			{ "throttle_period_in_millis", 0 },
			{ "email", 1 },
			{ "webhook", 2 },
			{ "index", 3 },
			{ "logging", 4 },
			{ "slack", 5 },
			{ "pagerduty", 6 },
			{ "foreach", 7 },
			{ "transform", 8 },
			{ "condition", 9 },
			{ "max_iterations", 10 },
		};

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
				string @foreach = null;
				int? maxIterations = null;
				TransformContainer transform = null;
				ConditionContainer condition = null;

				while (reader.ReadIsInObject(ref actionCount))
				{
					var propertyName = reader.ReadPropertyNameSegmentRaw();

					if (Fields.TryGetValue(propertyName, out var value))
					{
						switch (value)
						{
							case 0:
								throttlePeriod = formatterResolver.GetFormatter<Time>()
									.Deserialize(ref reader, formatterResolver);
								break;
							case 1:
								action = formatterResolver.GetFormatter<EmailAction>()
									.Deserialize(ref reader, formatterResolver);
								break;
							case 2:
								action = formatterResolver.GetFormatter<WebhookAction>()
									.Deserialize(ref reader, formatterResolver);
								break;
							case 3:
								action = formatterResolver.GetFormatter<IndexAction>()
									.Deserialize(ref reader, formatterResolver);
								break;
							case 4:
								action = formatterResolver.GetFormatter<LoggingAction>()
									.Deserialize(ref reader, formatterResolver);
								break;
							case 5:
								action = formatterResolver.GetFormatter<SlackAction>()
									.Deserialize(ref reader, formatterResolver);
								break;
							case 6:
								action = formatterResolver.GetFormatter<PagerDutyAction>()
									.Deserialize(ref reader, formatterResolver);
								break;
							case 7:
								@foreach = reader.ReadString();
								break;
							case 8:
								transform = formatterResolver.GetFormatter<TransformContainer>()
									.Deserialize(ref reader, formatterResolver);
								break;
							case 9:
								condition = formatterResolver.GetFormatter<ConditionContainer>()
									.Deserialize(ref reader, formatterResolver);
								break;
							case 10:
								maxIterations = reader.ReadInt32();
								break;
						}
					}
					else
						reader.ReadNextBlock();
				}

				if (action != null)
				{
					action.Name = name;
					action.ThrottlePeriod = throttlePeriod;
					action.Foreach = @foreach;
					action.MaxIterations = maxIterations;
					action.Transform = transform;
					action.Condition = condition;
					dictionary.Add(name, action);
				}
			}

			return new Actions(dictionary);
		}

		public void Serialize(ref JsonWriter writer, Actions value, IJsonFormatterResolver formatterResolver) =>
			ActionsInterfaceFormatter.Serialize(ref writer, value, formatterResolver);
	}
}
