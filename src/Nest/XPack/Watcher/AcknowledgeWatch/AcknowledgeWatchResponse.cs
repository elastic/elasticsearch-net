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
		[JsonProperty("actions")]
		public IReadOnlyDictionary<string, ActionStatus> Actions { get; set; }

		[JsonProperty("last_checked")]
		public DateTimeOffset? LastChecked { get; set; }

		[JsonProperty("last_met_condition")]
		public DateTimeOffset? LastMetCondition { get; set; }

		[JsonProperty("state")]
		public ActivationState State { get; set; }

		[JsonProperty("version")]
		public int? Version { get; set; }
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
		[JsonProperty("active")]
		public bool Active { get; set; }

		[JsonProperty("timestamp")]
		public DateTimeOffset Timestamp { get; set; }
	}

	[JsonObject]
	public class AcknowledgeState
	{
		[JsonProperty("state")]
		public AcknowledgementState State { get; set; }

		[JsonProperty("timestamp")]
		public DateTimeOffset Timestamp { get; set; }
	}

	[JsonObject]
	public class ExecutionState
	{
		[JsonProperty("successful")]
		public bool Successful { get; set; }

		[JsonProperty("timestamp")]
		public DateTimeOffset Timestamp { get; set; }
	}

	[JsonObject]
	public class ThrottleState
	{
		[JsonProperty("reason")]
		public string Reason { get; set; }

		[JsonProperty("timestamp")]
		public DateTimeOffset Timestamp { get; set; }
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
