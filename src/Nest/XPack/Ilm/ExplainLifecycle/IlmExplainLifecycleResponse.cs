using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IIlmExplainLifecycleResponse : IResponse
	{
		[JsonProperty("indices")]
		IReadOnlyDictionary<string, LifecycleExplain> Indices { get; }
	}

	public class IlmExplainLifecycleResponse : ResponseBase, IIlmExplainLifecycleResponse
	{
		public IReadOnlyDictionary<string, LifecycleExplain> Indices { get; internal set; }
	}

	public class LifecycleExplain
	{
		[JsonProperty("index")]
		public IndexName Index { get; internal set; }

		/// <summary>
		/// Shows if the index is being managed by ILM. If the index is not managed by ILM the other fields will not be shown.
		/// </summary>
		[JsonProperty("managed")]
		public bool Managed { get; internal set; }

		/// <summary>
		/// The name of the policy which ILM is using for this index.
		/// </summary>
		[JsonProperty("policy")]
		public PolicyId Policy { get; internal set; }

		/// <summary>
		/// The timestamp used for minimum age.
		/// </summary>
		[JsonProperty("lifecycle_date_millis")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset LifecycleDate { get; internal set; }

		[JsonProperty("phase")]
		public string Phase { get; internal set; }

		/// <summary>
		/// When the index entered the current phase.
		/// </summary>
		[JsonProperty("phase_time_millis")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset PhaseTime { get; internal set; }

		[JsonProperty("action")]
		public string Action { get; internal set; }

		/// <summary>
		/// When the index entered the current action.
		/// </summary>
		[JsonProperty("action_time_millis")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset ActionTime { get; internal set; }

		[JsonProperty("step")]
		public string Step { get; internal set; }

		/// <summary>
		/// When the index entered the current step.
		/// </summary>
		[JsonProperty("step_time_millis")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset StepTime { get; internal set; }

		/// <summary>
		/// The step that caused the error, if applicable.
		/// </summary>
		[JsonProperty("failed_step")]
		public string FailedStep { get; internal set; }

		/// <summary>
		/// Step error details, if applicable.
		/// </summary>
		[JsonProperty("step_info")]
		public IReadOnlyDictionary<string, object> StepInfo { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;
	}

	public class PhaseExecution
	{
		[JsonProperty("policy")]
		public PolicyId Policy { get; internal set; }

		/// <summary>
		/// The phase definition loaded from the specified policy when the index entered this phase.
		/// </summary>
		[JsonProperty("phase_definition")]
		public IPhase PhaseDefinition { get; internal set; }

		/// <summary>
		/// The version of the policy that was loaded.
		/// </summary>
		[JsonProperty("version")]
		public int Version { get; internal set; }

		/// <summary>
		/// The date the loaded policy was last modified.
		/// </summary>
		[JsonProperty("modified_date_in_millis")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset ModifiedDate { get; internal set; }
	}
}
