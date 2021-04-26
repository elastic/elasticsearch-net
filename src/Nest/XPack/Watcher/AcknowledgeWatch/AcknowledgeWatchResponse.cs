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
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	public class AcknowledgeWatchResponse : ResponseBase
	{
		[DataMember(Name ="status")]
		public WatchStatus Status { get; internal set; }
	}

	[DataContract]
	public class WatchStatus
	{
		[DataMember(Name ="actions")]
		public IReadOnlyDictionary<string, ActionStatus> Actions { get; set; }

		[DataMember(Name ="last_checked")]
		public DateTimeOffset? LastChecked { get; set; }

		[DataMember(Name ="last_met_condition")]
		public DateTimeOffset? LastMetCondition { get; set; }

		[DataMember(Name ="state")]
		public ActivationState State { get; set; }

		[DataMember(Name ="version")]
		public int? Version { get; set; }
	}

	public class ActionStatus
	{
		[DataMember(Name ="ack")]
		public AcknowledgeState Acknowledgement { get; set; }

		[DataMember(Name ="last_execution")]
		public ExecutionState LastExecution { get; set; }

		[DataMember(Name ="last_successful_execution")]
		public ExecutionState LastSuccessfulExecution { get; set; }

		[DataMember(Name ="last_throttle")]
		public ThrottleState LastThrottle { get; set; }
	}

	[DataContract]
	public class ActivationState
	{
		[DataMember(Name ="active")]
		public bool Active { get; set; }

		[DataMember(Name ="timestamp")]
		public DateTimeOffset Timestamp { get; set; }
	}

	[DataContract]
	public class AcknowledgeState
	{
		[DataMember(Name ="state")]
		public AcknowledgementState State { get; set; }

		[DataMember(Name ="timestamp")]
		public DateTimeOffset Timestamp { get; set; }
	}

	[DataContract]
	public class ExecutionState
	{
		[DataMember(Name ="successful")]
		public bool Successful { get; set; }

		[DataMember(Name ="timestamp")]
		public DateTimeOffset Timestamp { get; set; }
	}

	[DataContract]
	public class ThrottleState
	{
		[DataMember(Name ="reason")]
		public string Reason { get; set; }

		[DataMember(Name ="timestamp")]
		public DateTimeOffset Timestamp { get; set; }
	}

	[StringEnum]
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
