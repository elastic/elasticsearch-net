// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class ExecuteEnrichPolicyResponse : ResponseBase
	{
		[DataMember(Name = "task_id")]
		public TaskId TaskId { get; internal set; }

		[DataMember(Name = "status")]
		public ExecuteEnrichPolicyStatus Status { get; internal set; }
	}

	public class ExecuteEnrichPolicyStatus
	{
		[DataMember(Name = "phase")]
		public EnrichPolicyPhase Phase { get; internal set; }
	}

	[StringEnum]
	public enum EnrichPolicyPhase
	{
		[EnumMember(Value = "SCHEDULED")]
		Scheduled,
		[EnumMember(Value = "RUNNING")]
		Running,
		[EnumMember(Value = "COMPLETE")]
		Complete,
		[EnumMember(Value = "FAILED")]
		Failed
	}
}
