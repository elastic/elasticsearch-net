using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IExecuteWatchResponse : IResponse
	{
		[JsonProperty("_id")]
		string Id { get; set; }

		[JsonProperty("watch_record")]
		WatchRecord WatchRecord { get; set; }
	}

	public class ExecuteWatchResponse : ResponseBase, IExecuteWatchResponse
	{
		public string Id { get; set; }

		public WatchRecord WatchRecord { get; set; }
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum ActionExecutionState
	{
		[EnumMember(Value = "awaits_execution")]
		AwaitsExecution,

		[EnumMember(Value = "checking")]
		Checking,

		[EnumMember(Value = "execution_not_needed")]
		ExecutionNotNeeded,

		[EnumMember(Value = "throttled")]
		Throttled,

		[EnumMember(Value = "executed")]
		Executed,

		[EnumMember(Value = "failed")]
		Failed,

		[EnumMember(Value = "deleted_while_queued")]
		DeletedWhileQueued,

		[EnumMember(Value = "not_executed_already_queued")]
		NotExecutedAlreadyQueued
	}

	[JsonObject]
	public class WatchRecord
	{
		[JsonProperty("condition")]
		public ConditionContainer Condition { get; set; }

		[JsonProperty("input")]
		public InputContainer Input { get; set; }

		[JsonProperty("messages")]
		public IReadOnlyCollection<string> Messages { get; set; }

		[JsonProperty("metadata")]
		public IReadOnlyDictionary<string, object> Metadata { get; set; }

		[JsonProperty("result")]
		public ExecutionResult Result { get; set; }

		[JsonProperty("state")]
		public ActionExecutionState? State { get; set; }

		[JsonProperty("trigger_event")]
		public TriggerEventResult TriggerEvent { get; set; }

		[JsonProperty("user")]
		public string User { get; set; }

		[JsonProperty("watch_id")]
		public string WatchId { get; set; }
	}

	[JsonObject]
	public class TriggerEventResult
	{
		[JsonProperty("manual")]
		public ITriggerEventContainer Manual { get; set; }

		[JsonProperty("triggered_time")]
		public DateTimeOffset? TriggeredTime { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }
	}

	public class ExecutionResult
	{
		[JsonProperty("actions")]
		public IReadOnlyCollection<ExecutionResultAction> Actions { get; set; }

		[JsonProperty("condition")]
		public ExecutionResultCondition Condition { get; set; }

		[JsonProperty("execution_duration")]
		public int? ExecutionDuration { get; set; }

		[JsonProperty("execution_time")]
		public DateTimeOffset? ExecutionTime { get; set; }

		[JsonProperty("input")]
		public ExecutionResultInput Input { get; set; }
	}

	[JsonObject]
	public class ExecutionResultInput
	{
		[JsonProperty("payload")]
		public IReadOnlyDictionary<string, object> Payload { get; set; }

		[JsonProperty("status")]
		public Status Status { get; set; }

		[JsonProperty("type")]
		public InputType Type { get; set; }
	}

	[JsonObject]
	public class ExecutionResultCondition
	{
		[JsonProperty("met")]
		public bool? Met { get; set; }

		[JsonProperty("status")]
		public Status Status { get; set; }

		[JsonProperty("type")]
		public ConditionType Type { get; set; }
	}

	[JsonObject]
	public class ExecutionResultAction
	{
		[JsonProperty("email")]
		public EmailActionResult Email { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("index")]
		public IndexActionResult Index { get; set; }

		[JsonProperty("logging")]
		public LoggingActionResult Logging { get; set; }

		[JsonProperty("pagerduty")]
		public PagerDutyActionResult PagerDuty { get; set; }

		[JsonProperty("reason")]
		public string Reason { get; set; }

		[JsonProperty("slack")]
		public SlackActionResult Slack { get; set; }

		[JsonProperty("status")]
		public Status Status { get; set; }

		[JsonProperty("type")]
		public ActionType Type { get; set; }

		[JsonProperty("webhook")]
		public WebhookActionResult Webhook { get; set; }
	}
}
