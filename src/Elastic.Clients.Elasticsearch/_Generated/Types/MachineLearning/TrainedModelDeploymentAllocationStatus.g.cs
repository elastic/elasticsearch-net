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

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.MachineLearning;

internal sealed partial class TrainedModelDeploymentAllocationStatusConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelDeploymentAllocationStatus>
{
	private static readonly System.Text.Json.JsonEncodedText PropAllocationCount = System.Text.Json.JsonEncodedText.Encode("allocation_count");
	private static readonly System.Text.Json.JsonEncodedText PropState = System.Text.Json.JsonEncodedText.Encode("state");
	private static readonly System.Text.Json.JsonEncodedText PropTargetAllocationCount = System.Text.Json.JsonEncodedText.Encode("target_allocation_count");

	public override Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelDeploymentAllocationStatus Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int> propAllocationCount = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.DeploymentAllocationState> propState = default;
		LocalJsonValue<int> propTargetAllocationCount = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAllocationCount.TryReadProperty(ref reader, options, PropAllocationCount, null))
			{
				continue;
			}

			if (propState.TryReadProperty(ref reader, options, PropState, null))
			{
				continue;
			}

			if (propTargetAllocationCount.TryReadProperty(ref reader, options, PropTargetAllocationCount, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelDeploymentAllocationStatus(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AllocationCount = propAllocationCount.Value,
			State = propState.Value,
			TargetAllocationCount = propTargetAllocationCount.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelDeploymentAllocationStatus value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAllocationCount, value.AllocationCount, null, null);
		writer.WriteProperty(options, PropState, value.State, null, null);
		writer.WriteProperty(options, PropTargetAllocationCount, value.TargetAllocationCount, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelDeploymentAllocationStatusConverter))]
public sealed partial class TrainedModelDeploymentAllocationStatus
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TrainedModelDeploymentAllocationStatus(int allocationCount, Elastic.Clients.Elasticsearch.MachineLearning.DeploymentAllocationState state, int targetAllocationCount)
	{
		AllocationCount = allocationCount;
		State = state;
		TargetAllocationCount = targetAllocationCount;
	}
#if NET7_0_OR_GREATER
	public TrainedModelDeploymentAllocationStatus()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public TrainedModelDeploymentAllocationStatus()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal TrainedModelDeploymentAllocationStatus(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The current number of nodes where the model is allocated.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int AllocationCount { get; set; }

	/// <summary>
	/// <para>
	/// The detailed allocation state related to the nodes.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.MachineLearning.DeploymentAllocationState State { get; set; }

	/// <summary>
	/// <para>
	/// The desired number of nodes for model allocation.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int TargetAllocationCount { get; set; }
}