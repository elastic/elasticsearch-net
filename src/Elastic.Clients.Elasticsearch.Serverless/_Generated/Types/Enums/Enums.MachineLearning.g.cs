// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

#nullable restore

using Elastic.Clients.Elasticsearch.Serverless.Core;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using Elastic.Transport;
using System;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.MachineLearning;

[JsonConverter(typeof(AppliesToConverter))]
public enum AppliesTo
{
	[EnumMember(Value = "typical")]
	Typical,
	[EnumMember(Value = "time")]
	Time,
	[EnumMember(Value = "diff_from_typical")]
	DiffFromTypical,
	[EnumMember(Value = "actual")]
	Actual
}

internal sealed class AppliesToConverter : JsonConverter<AppliesTo>
{
	public override AppliesTo Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "typical":
				return AppliesTo.Typical;
			case "time":
				return AppliesTo.Time;
			case "diff_from_typical":
				return AppliesTo.DiffFromTypical;
			case "actual":
				return AppliesTo.Actual;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, AppliesTo value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case AppliesTo.Typical:
				writer.WriteStringValue("typical");
				return;
			case AppliesTo.Time:
				writer.WriteStringValue("time");
				return;
			case AppliesTo.DiffFromTypical:
				writer.WriteStringValue("diff_from_typical");
				return;
			case AppliesTo.Actual:
				writer.WriteStringValue("actual");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(CategorizationStatusConverter))]
public enum CategorizationStatus
{
	[EnumMember(Value = "warn")]
	Warn,
	[EnumMember(Value = "ok")]
	Ok
}

internal sealed class CategorizationStatusConverter : JsonConverter<CategorizationStatus>
{
	public override CategorizationStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "warn":
				return CategorizationStatus.Warn;
			case "ok":
				return CategorizationStatus.Ok;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, CategorizationStatus value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case CategorizationStatus.Warn:
				writer.WriteStringValue("warn");
				return;
			case CategorizationStatus.Ok:
				writer.WriteStringValue("ok");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(ChunkingModeConverter))]
public enum ChunkingMode
{
	[EnumMember(Value = "off")]
	Off,
	[EnumMember(Value = "manual")]
	Manual,
	[EnumMember(Value = "auto")]
	Auto
}

internal sealed class ChunkingModeConverter : JsonConverter<ChunkingMode>
{
	public override ChunkingMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "off":
				return ChunkingMode.Off;
			case "manual":
				return ChunkingMode.Manual;
			case "auto":
				return ChunkingMode.Auto;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, ChunkingMode value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case ChunkingMode.Off:
				writer.WriteStringValue("off");
				return;
			case ChunkingMode.Manual:
				writer.WriteStringValue("manual");
				return;
			case ChunkingMode.Auto:
				writer.WriteStringValue("auto");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(ConditionOperatorConverter))]
public enum ConditionOperator
{
	[EnumMember(Value = "lte")]
	Lte,
	[EnumMember(Value = "lt")]
	Lt,
	[EnumMember(Value = "gte")]
	Gte,
	[EnumMember(Value = "gt")]
	Gt
}

internal sealed class ConditionOperatorConverter : JsonConverter<ConditionOperator>
{
	public override ConditionOperator Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "lte":
				return ConditionOperator.Lte;
			case "lt":
				return ConditionOperator.Lt;
			case "gte":
				return ConditionOperator.Gte;
			case "gt":
				return ConditionOperator.Gt;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, ConditionOperator value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case ConditionOperator.Lte:
				writer.WriteStringValue("lte");
				return;
			case ConditionOperator.Lt:
				writer.WriteStringValue("lt");
				return;
			case ConditionOperator.Gte:
				writer.WriteStringValue("gte");
				return;
			case ConditionOperator.Gt:
				writer.WriteStringValue("gt");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(DatafeedStateConverter))]
public enum DatafeedState
{
	[EnumMember(Value = "stopping")]
	Stopping,
	[EnumMember(Value = "stopped")]
	Stopped,
	[EnumMember(Value = "starting")]
	Starting,
	[EnumMember(Value = "started")]
	Started
}

internal sealed class DatafeedStateConverter : JsonConverter<DatafeedState>
{
	public override DatafeedState Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "stopping":
				return DatafeedState.Stopping;
			case "stopped":
				return DatafeedState.Stopped;
			case "starting":
				return DatafeedState.Starting;
			case "started":
				return DatafeedState.Started;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, DatafeedState value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case DatafeedState.Stopping:
				writer.WriteStringValue("stopping");
				return;
			case DatafeedState.Stopped:
				writer.WriteStringValue("stopped");
				return;
			case DatafeedState.Starting:
				writer.WriteStringValue("starting");
				return;
			case DatafeedState.Started:
				writer.WriteStringValue("started");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(DataframeStateConverter))]
public enum DataframeState
{
	[EnumMember(Value = "stopping")]
	Stopping,
	[EnumMember(Value = "stopped")]
	Stopped,
	[EnumMember(Value = "starting")]
	Starting,
	[EnumMember(Value = "started")]
	Started,
	[EnumMember(Value = "failed")]
	Failed
}

internal sealed class DataframeStateConverter : JsonConverter<DataframeState>
{
	public override DataframeState Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "stopping":
				return DataframeState.Stopping;
			case "stopped":
				return DataframeState.Stopped;
			case "starting":
				return DataframeState.Starting;
			case "started":
				return DataframeState.Started;
			case "failed":
				return DataframeState.Failed;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, DataframeState value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case DataframeState.Stopping:
				writer.WriteStringValue("stopping");
				return;
			case DataframeState.Stopped:
				writer.WriteStringValue("stopped");
				return;
			case DataframeState.Starting:
				writer.WriteStringValue("starting");
				return;
			case DataframeState.Started:
				writer.WriteStringValue("started");
				return;
			case DataframeState.Failed:
				writer.WriteStringValue("failed");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(DeploymentAllocationStateConverter))]
public enum DeploymentAllocationState
{
	/// <summary>
	/// <para>
	/// Trained model deployment is starting but it is not yet deployed on any nodes.
	/// </para>
	/// </summary>
	[EnumMember(Value = "starting")]
	Starting,
	/// <summary>
	/// <para>
	/// The trained model is started on at least one node.
	/// </para>
	/// </summary>
	[EnumMember(Value = "started")]
	Started,
	/// <summary>
	/// <para>
	/// Trained model deployment has started on all valid nodes.
	/// </para>
	/// </summary>
	[EnumMember(Value = "fully_allocated")]
	FullyAllocated
}

internal sealed class DeploymentAllocationStateConverter : JsonConverter<DeploymentAllocationState>
{
	public override DeploymentAllocationState Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "starting":
				return DeploymentAllocationState.Starting;
			case "started":
				return DeploymentAllocationState.Started;
			case "fully_allocated":
				return DeploymentAllocationState.FullyAllocated;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, DeploymentAllocationState value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case DeploymentAllocationState.Starting:
				writer.WriteStringValue("starting");
				return;
			case DeploymentAllocationState.Started:
				writer.WriteStringValue("started");
				return;
			case DeploymentAllocationState.FullyAllocated:
				writer.WriteStringValue("fully_allocated");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(DeploymentAssignmentStateConverter))]
public enum DeploymentAssignmentState
{
	[EnumMember(Value = "stopping")]
	Stopping,
	[EnumMember(Value = "starting")]
	Starting,
	[EnumMember(Value = "started")]
	Started,
	[EnumMember(Value = "failed")]
	Failed
}

internal sealed class DeploymentAssignmentStateConverter : JsonConverter<DeploymentAssignmentState>
{
	public override DeploymentAssignmentState Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "stopping":
				return DeploymentAssignmentState.Stopping;
			case "starting":
				return DeploymentAssignmentState.Starting;
			case "started":
				return DeploymentAssignmentState.Started;
			case "failed":
				return DeploymentAssignmentState.Failed;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, DeploymentAssignmentState value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case DeploymentAssignmentState.Stopping:
				writer.WriteStringValue("stopping");
				return;
			case DeploymentAssignmentState.Starting:
				writer.WriteStringValue("starting");
				return;
			case DeploymentAssignmentState.Started:
				writer.WriteStringValue("started");
				return;
			case DeploymentAssignmentState.Failed:
				writer.WriteStringValue("failed");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(DeploymentStateConverter))]
public enum DeploymentState
{
	/// <summary>
	/// <para>
	/// The deployment is preparing to stop and deallocate the model from the relevant nodes.
	/// </para>
	/// </summary>
	[EnumMember(Value = "stopping")]
	Stopping,
	/// <summary>
	/// <para>
	/// The deployment has recently started but is not yet usable; the model is not allocated on any nodes.
	/// </para>
	/// </summary>
	[EnumMember(Value = "starting")]
	Starting,
	/// <summary>
	/// <para>
	/// The deployment is usable; at least one node has the model allocated.
	/// </para>
	/// </summary>
	[EnumMember(Value = "started")]
	Started
}

internal sealed class DeploymentStateConverter : JsonConverter<DeploymentState>
{
	public override DeploymentState Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "stopping":
				return DeploymentState.Stopping;
			case "starting":
				return DeploymentState.Starting;
			case "started":
				return DeploymentState.Started;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, DeploymentState value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case DeploymentState.Stopping:
				writer.WriteStringValue("stopping");
				return;
			case DeploymentState.Starting:
				writer.WriteStringValue("starting");
				return;
			case DeploymentState.Started:
				writer.WriteStringValue("started");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(ExcludeFrequentConverter))]
public enum ExcludeFrequent
{
	[EnumMember(Value = "over")]
	Over,
	[EnumMember(Value = "none")]
	None,
	[EnumMember(Value = "by")]
	By,
	[EnumMember(Value = "all")]
	All
}

internal sealed class ExcludeFrequentConverter : JsonConverter<ExcludeFrequent>
{
	public override ExcludeFrequent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "over":
				return ExcludeFrequent.Over;
			case "none":
				return ExcludeFrequent.None;
			case "by":
				return ExcludeFrequent.By;
			case "all":
				return ExcludeFrequent.All;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, ExcludeFrequent value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case ExcludeFrequent.Over:
				writer.WriteStringValue("over");
				return;
			case ExcludeFrequent.None:
				writer.WriteStringValue("none");
				return;
			case ExcludeFrequent.By:
				writer.WriteStringValue("by");
				return;
			case ExcludeFrequent.All:
				writer.WriteStringValue("all");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(FilterTypeConverter))]
public enum FilterType
{
	[EnumMember(Value = "include")]
	Include,
	[EnumMember(Value = "exclude")]
	Exclude
}

internal sealed class FilterTypeConverter : JsonConverter<FilterType>
{
	public override FilterType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "include":
				return FilterType.Include;
			case "exclude":
				return FilterType.Exclude;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, FilterType value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case FilterType.Include:
				writer.WriteStringValue("include");
				return;
			case FilterType.Exclude:
				writer.WriteStringValue("exclude");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(IncludeConverter))]
public enum Include
{
	/// <summary>
	/// <para>
	/// Includes the total feature importance for the training data set. The
	/// baseline and total feature importance values are returned in the metadata
	/// field in the response body.
	/// </para>
	/// </summary>
	[EnumMember(Value = "total_feature_importance")]
	TotalFeatureImportance,
	/// <summary>
	/// <para>
	/// Includes the information about hyperparameters used to train the model.
	/// This information consists of the value, the absolute and relative
	/// importance of the hyperparameter as well as an indicator of whether it was
	/// specified by the user or tuned during hyperparameter optimization.
	/// </para>
	/// </summary>
	[EnumMember(Value = "hyperparameters")]
	Hyperparameters,
	/// <summary>
	/// <para>
	/// Includes the baseline for feature importance values.
	/// </para>
	/// </summary>
	[EnumMember(Value = "feature_importance_baseline")]
	FeatureImportanceBaseline,
	/// <summary>
	/// <para>
	/// Includes the model definition status.
	/// </para>
	/// </summary>
	[EnumMember(Value = "definition_status")]
	DefinitionStatus,
	/// <summary>
	/// <para>
	/// Includes the model definition.
	/// </para>
	/// </summary>
	[EnumMember(Value = "definition")]
	Definition
}

internal sealed class IncludeConverter : JsonConverter<Include>
{
	public override Include Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "total_feature_importance":
				return Include.TotalFeatureImportance;
			case "hyperparameters":
				return Include.Hyperparameters;
			case "feature_importance_baseline":
				return Include.FeatureImportanceBaseline;
			case "definition_status":
				return Include.DefinitionStatus;
			case "definition":
				return Include.Definition;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, Include value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case Include.TotalFeatureImportance:
				writer.WriteStringValue("total_feature_importance");
				return;
			case Include.Hyperparameters:
				writer.WriteStringValue("hyperparameters");
				return;
			case Include.FeatureImportanceBaseline:
				writer.WriteStringValue("feature_importance_baseline");
				return;
			case Include.DefinitionStatus:
				writer.WriteStringValue("definition_status");
				return;
			case Include.Definition:
				writer.WriteStringValue("definition");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(JobBlockedReasonConverter))]
public enum JobBlockedReason
{
	[EnumMember(Value = "revert")]
	Revert,
	[EnumMember(Value = "reset")]
	Reset,
	[EnumMember(Value = "delete")]
	Delete
}

internal sealed class JobBlockedReasonConverter : JsonConverter<JobBlockedReason>
{
	public override JobBlockedReason Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "revert":
				return JobBlockedReason.Revert;
			case "reset":
				return JobBlockedReason.Reset;
			case "delete":
				return JobBlockedReason.Delete;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, JobBlockedReason value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case JobBlockedReason.Revert:
				writer.WriteStringValue("revert");
				return;
			case JobBlockedReason.Reset:
				writer.WriteStringValue("reset");
				return;
			case JobBlockedReason.Delete:
				writer.WriteStringValue("delete");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(JobStateConverter))]
public enum JobState
{
	/// <summary>
	/// <para>
	/// The job open action is in progress and has not yet completed.
	/// </para>
	/// </summary>
	[EnumMember(Value = "opening")]
	Opening,
	/// <summary>
	/// <para>
	/// The job is available to receive and process data.
	/// </para>
	/// </summary>
	[EnumMember(Value = "opened")]
	Opened,
	/// <summary>
	/// <para>
	/// The job did not finish successfully due to an error.
	/// This situation can occur due to invalid input data, a fatal error occurring during the analysis, or an external interaction such as the process being killed by the Linux out of memory (OOM) killer.
	/// If the job had irrevocably failed, it must be force closed and then deleted.
	/// If the datafeed can be corrected, the job can be closed and then re-opened.
	/// </para>
	/// </summary>
	[EnumMember(Value = "failed")]
	Failed,
	/// <summary>
	/// <para>
	/// The job close action is in progress and has not yet completed. A closing job cannot accept further data.
	/// </para>
	/// </summary>
	[EnumMember(Value = "closing")]
	Closing,
	/// <summary>
	/// <para>
	/// The job finished successfully with its model state persisted. The job must be opened before it can accept further data.
	/// </para>
	/// </summary>
	[EnumMember(Value = "closed")]
	Closed
}

internal sealed class JobStateConverter : JsonConverter<JobState>
{
	public override JobState Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "opening":
				return JobState.Opening;
			case "opened":
				return JobState.Opened;
			case "failed":
				return JobState.Failed;
			case "closing":
				return JobState.Closing;
			case "closed":
				return JobState.Closed;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, JobState value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case JobState.Opening:
				writer.WriteStringValue("opening");
				return;
			case JobState.Opened:
				writer.WriteStringValue("opened");
				return;
			case JobState.Failed:
				writer.WriteStringValue("failed");
				return;
			case JobState.Closing:
				writer.WriteStringValue("closing");
				return;
			case JobState.Closed:
				writer.WriteStringValue("closed");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(MemoryStatusConverter))]
public enum MemoryStatus
{
	[EnumMember(Value = "soft_limit")]
	SoftLimit,
	[EnumMember(Value = "ok")]
	Ok,
	[EnumMember(Value = "hard_limit")]
	HardLimit
}

internal sealed class MemoryStatusConverter : JsonConverter<MemoryStatus>
{
	public override MemoryStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "soft_limit":
				return MemoryStatus.SoftLimit;
			case "ok":
				return MemoryStatus.Ok;
			case "hard_limit":
				return MemoryStatus.HardLimit;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, MemoryStatus value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case MemoryStatus.SoftLimit:
				writer.WriteStringValue("soft_limit");
				return;
			case MemoryStatus.Ok:
				writer.WriteStringValue("ok");
				return;
			case MemoryStatus.HardLimit:
				writer.WriteStringValue("hard_limit");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(RoutingStateConverter))]
public enum RoutingState
{
	/// <summary>
	/// <para>
	/// The trained model is being deallocated from this node.
	/// </para>
	/// </summary>
	[EnumMember(Value = "stopping")]
	Stopping,
	/// <summary>
	/// <para>
	/// The trained model is fully deallocated from this node.
	/// </para>
	/// </summary>
	[EnumMember(Value = "stopped")]
	Stopped,
	/// <summary>
	/// <para>
	/// The trained model is attempting to allocate on this node; inference requests are not yet accepted.
	/// </para>
	/// </summary>
	[EnumMember(Value = "starting")]
	Starting,
	/// <summary>
	/// <para>
	/// The trained model is allocated and ready to accept inference requests.
	/// </para>
	/// </summary>
	[EnumMember(Value = "started")]
	Started,
	/// <summary>
	/// <para>
	/// The allocation attempt failed.
	/// </para>
	/// </summary>
	[EnumMember(Value = "failed")]
	Failed
}

internal sealed class RoutingStateConverter : JsonConverter<RoutingState>
{
	public override RoutingState Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "stopping":
				return RoutingState.Stopping;
			case "stopped":
				return RoutingState.Stopped;
			case "starting":
				return RoutingState.Starting;
			case "started":
				return RoutingState.Started;
			case "failed":
				return RoutingState.Failed;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, RoutingState value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case RoutingState.Stopping:
				writer.WriteStringValue("stopping");
				return;
			case RoutingState.Stopped:
				writer.WriteStringValue("stopped");
				return;
			case RoutingState.Starting:
				writer.WriteStringValue("starting");
				return;
			case RoutingState.Started:
				writer.WriteStringValue("started");
				return;
			case RoutingState.Failed:
				writer.WriteStringValue("failed");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(RuleActionConverter))]
public enum RuleAction
{
	/// <summary>
	/// <para>
	/// The result will not be created. Unless you also specify <c>skip_model_update</c>, the model will be updated as usual with the corresponding series value.
	/// </para>
	/// </summary>
	[EnumMember(Value = "skip_result")]
	SkipResult,
	/// <summary>
	/// <para>
	/// The value for that series will not be used to update the model. Unless you also specify <c>skip_result</c>, the results will be created as usual. This action is suitable when certain values are expected to be consistently anomalous and they affect the model in a way that negatively impacts the rest of the results.
	/// </para>
	/// </summary>
	[EnumMember(Value = "skip_model_update")]
	SkipModelUpdate
}

internal sealed class RuleActionConverter : JsonConverter<RuleAction>
{
	public override RuleAction Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "skip_result":
				return RuleAction.SkipResult;
			case "skip_model_update":
				return RuleAction.SkipModelUpdate;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, RuleAction value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case RuleAction.SkipResult:
				writer.WriteStringValue("skip_result");
				return;
			case RuleAction.SkipModelUpdate:
				writer.WriteStringValue("skip_model_update");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(SnapshotUpgradeStateConverter))]
public enum SnapshotUpgradeState
{
	[EnumMember(Value = "stopped")]
	Stopped,
	[EnumMember(Value = "saving_new_state")]
	SavingNewState,
	[EnumMember(Value = "loading_old_state")]
	LoadingOldState,
	[EnumMember(Value = "failed")]
	Failed
}

internal sealed class SnapshotUpgradeStateConverter : JsonConverter<SnapshotUpgradeState>
{
	public override SnapshotUpgradeState Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "stopped":
				return SnapshotUpgradeState.Stopped;
			case "saving_new_state":
				return SnapshotUpgradeState.SavingNewState;
			case "loading_old_state":
				return SnapshotUpgradeState.LoadingOldState;
			case "failed":
				return SnapshotUpgradeState.Failed;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, SnapshotUpgradeState value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case SnapshotUpgradeState.Stopped:
				writer.WriteStringValue("stopped");
				return;
			case SnapshotUpgradeState.SavingNewState:
				writer.WriteStringValue("saving_new_state");
				return;
			case SnapshotUpgradeState.LoadingOldState:
				writer.WriteStringValue("loading_old_state");
				return;
			case SnapshotUpgradeState.Failed:
				writer.WriteStringValue("failed");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(TokenizationTruncateConverter))]
public enum TokenizationTruncate
{
	[EnumMember(Value = "second")]
	Second,
	[EnumMember(Value = "none")]
	None,
	[EnumMember(Value = "first")]
	First
}

internal sealed class TokenizationTruncateConverter : JsonConverter<TokenizationTruncate>
{
	public override TokenizationTruncate Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "second":
				return TokenizationTruncate.Second;
			case "none":
				return TokenizationTruncate.None;
			case "first":
				return TokenizationTruncate.First;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, TokenizationTruncate value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case TokenizationTruncate.Second:
				writer.WriteStringValue("second");
				return;
			case TokenizationTruncate.None:
				writer.WriteStringValue("none");
				return;
			case TokenizationTruncate.First:
				writer.WriteStringValue("first");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(TrainedModelTypeConverter))]
public enum TrainedModelType
{
	/// <summary>
	/// <para>
	/// The model definition is an ensemble model of decision trees.
	/// </para>
	/// </summary>
	[EnumMember(Value = "tree_ensemble")]
	TreeEnsemble,
	/// <summary>
	/// <para>
	/// The stored definition is a PyTorch (specifically a TorchScript) model.
	/// Currently only NLP models are supported.
	/// </para>
	/// </summary>
	[EnumMember(Value = "pytorch")]
	Pytorch,
	/// <summary>
	/// <para>
	/// A special type reserved for language identification models.
	/// </para>
	/// </summary>
	[EnumMember(Value = "lang_ident")]
	LangIdent
}

internal sealed class TrainedModelTypeConverter : JsonConverter<TrainedModelType>
{
	public override TrainedModelType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "tree_ensemble":
				return TrainedModelType.TreeEnsemble;
			case "pytorch":
				return TrainedModelType.Pytorch;
			case "lang_ident":
				return TrainedModelType.LangIdent;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, TrainedModelType value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case TrainedModelType.TreeEnsemble:
				writer.WriteStringValue("tree_ensemble");
				return;
			case TrainedModelType.Pytorch:
				writer.WriteStringValue("pytorch");
				return;
			case TrainedModelType.LangIdent:
				writer.WriteStringValue("lang_ident");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(TrainingPriorityConverter))]
public enum TrainingPriority
{
	[EnumMember(Value = "normal")]
	Normal,
	[EnumMember(Value = "low")]
	Low
}

internal sealed class TrainingPriorityConverter : JsonConverter<TrainingPriority>
{
	public override TrainingPriority Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "normal":
				return TrainingPriority.Normal;
			case "low":
				return TrainingPriority.Low;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, TrainingPriority value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case TrainingPriority.Normal:
				writer.WriteStringValue("normal");
				return;
			case TrainingPriority.Low:
				writer.WriteStringValue("low");
				return;
		}

		writer.WriteNullValue();
	}
}