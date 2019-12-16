using System;
using System.Runtime.Serialization;
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
