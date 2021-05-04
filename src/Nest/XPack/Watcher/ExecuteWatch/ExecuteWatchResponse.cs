// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class ExecuteWatchResponse : ResponseBase
	{
		[DataMember(Name = "_id")]
		public string Id { get; set; }

		[DataMember(Name = "watch_record")]
		public WatchRecord WatchRecord { get; set; }
	}

	[StringEnum]
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

	[DataContract]
	public class WatchRecord
	{
		[DataMember(Name = "condition")]
		public ConditionContainer Condition { get; set; }

		[DataMember(Name = "input")]
		public InputContainer Input { get; set; }

		[DataMember(Name = "messages")]
		public IReadOnlyCollection<string> Messages { get; set; }

		[DataMember(Name = "metadata")]
		public IReadOnlyDictionary<string, object> Metadata { get; set; }

		[DataMember(Name = "result")]
		public ExecutionResult Result { get; set; }

		[DataMember(Name = "state")]
		public ActionExecutionState? State { get; set; }

		[DataMember(Name = "trigger_event")]
		public TriggerEventResult TriggerEvent { get; set; }

		[DataMember(Name = "user")]
		public string User { get; set; }

		[DataMember(Name = "node")]
		public string Node { get; set; }

		[DataMember(Name = "watch_id")]
		public string WatchId { get; set; }
	}

	[DataContract]
	public class TriggerEventResult
	{
		[DataMember(Name = "manual")]
		public ITriggerEventContainer Manual { get; set; }

		[DataMember(Name = "triggered_time")]
		public DateTimeOffset? TriggeredTime { get; set; }

		[DataMember(Name = "type")]
		public string Type { get; set; }
	}

	public class ExecutionResult
	{
		[DataMember(Name = "actions")]
		public IReadOnlyCollection<ExecutionResultAction> Actions { get; set; }

		[DataMember(Name = "condition")]
		public ExecutionResultCondition Condition { get; set; }

		[DataMember(Name = "execution_duration")]
		public int? ExecutionDuration { get; set; }

		[DataMember(Name = "execution_time")]
		public DateTimeOffset? ExecutionTime { get; set; }

		[DataMember(Name = "input")]
		public ExecutionResultInput Input { get; set; }
	}

	[DataContract]
	public class ExecutionResultInput
	{
		[DataMember(Name = "payload")]
		public IReadOnlyDictionary<string, object> Payload { get; set; }

		[DataMember(Name = "status")]
		public Status Status { get; set; }

		[DataMember(Name = "type")]
		public InputType Type { get; set; }
	}

	[DataContract]
	public class ExecutionResultCondition
	{
		[DataMember(Name = "met")]
		public bool? Met { get; set; }

		[DataMember(Name = "status")]
		public Status Status { get; set; }

		[DataMember(Name = "type")]
		public ConditionType Type { get; set; }
	}

	[DataContract]
	public class ExecutionResultAction
	{
		[DataMember(Name = "email")]
		public EmailActionResult Email { get; set; }

		[DataMember(Name = "id")]
		public string Id { get; set; }

		[DataMember(Name = "index")]
		public IndexActionResult Index { get; set; }

		[DataMember(Name = "logging")]
		public LoggingActionResult Logging { get; set; }

		[DataMember(Name = "pagerduty")]
		public PagerDutyActionResult PagerDuty { get; set; }

		[DataMember(Name = "reason")]
		public string Reason { get; set; }

		[DataMember(Name = "slack")]
		public SlackActionResult Slack { get; set; }

		[DataMember(Name = "status")]
		public Status Status { get; set; }

		[DataMember(Name = "type")]
		public ActionType Type { get; set; }

		[DataMember(Name = "webhook")]
		public WebhookActionResult Webhook { get; set; }
	}
}
