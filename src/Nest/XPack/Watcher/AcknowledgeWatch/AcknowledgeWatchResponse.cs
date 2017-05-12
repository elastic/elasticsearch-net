using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	public interface IAcknowledgeWatchResponse : IResponse
	{
		[JsonProperty("status")]
		WatchStatus Status { get; }
	}

	public class AcknowledgeWatchResponse : ResponseBase, IAcknowledgeWatchResponse
	{
		public WatchStatus Status { get; internal set; }
	}

	[JsonObject]
	public class WatchStatus
	{
		[JsonProperty("version")]
		public int? Version { get; set; }

		[JsonProperty("state")]
		public ActivationState State { get; set; }

		[JsonProperty("last_checked")]
		public DateTimeOffset? LastChecked { get; set; }

		[JsonProperty("last_met_condition")]
		public DateTimeOffset? LastMetCondition { get; set; }

		[JsonProperty("actions")]
		public IReadOnlyDictionary<string, ActionStatus> Actions { get; set; }
	}

	public class ActionStatus
	{
		[JsonProperty("ack")]
		public AcknowledgeState Acknowledgement { get; set; }

		[JsonProperty("last_execution")]
		public ExecutionState LastExecution { get; set; }

		[JsonProperty("last_successful_execution")]
		public ExecutionState LastSuccessfulExecution { get; set; }

		[JsonProperty("last_throttle")]
		public ThrottleState LastThrottle { get; set; }
	}

	[JsonObject]
	public class ActivationState
	{
		[JsonProperty("timestamp")]
		public DateTimeOffset Timestamp { get; set; }

		[JsonProperty("active")]
		public bool Active { get; set; }
	}

	[JsonObject]
	public class AcknowledgeState
	{
		[JsonProperty("timestamp")]
		public DateTimeOffset Timestamp { get; set; }

		[JsonProperty("state")]
		public AcknowledgementState State { get; set; }
	}

	[JsonObject]
	public class ExecutionState
	{
		[JsonProperty("timestamp")]
		public DateTimeOffset Timestamp { get; set; }

		[JsonProperty("successful")]
		public bool Successful { get; set; }
	}

	[JsonObject]
	public class ThrottleState
	{
		[JsonProperty("timestamp")]
		public DateTimeOffset Timestamp { get; set; }

		[JsonProperty("reason")]
		public string Reason { get; set; }
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum AcknowledgementState
	{
		[EnumMember(Value = "awaits_successful_execution")]
		AwaitsSuccessfulExecution,

		[EnumMember(Value = "ackable")]
		Acknowledgable,

		[EnumMember(Value = "acked")]
		Acknowledged
	}
}
