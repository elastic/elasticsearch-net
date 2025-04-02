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

internal sealed partial class TrainedModelAssignmentRoutingStateAndReasonConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelAssignmentRoutingStateAndReason>
{
	private static readonly System.Text.Json.JsonEncodedText PropReason = System.Text.Json.JsonEncodedText.Encode("reason");
	private static readonly System.Text.Json.JsonEncodedText PropRoutingState = System.Text.Json.JsonEncodedText.Encode("routing_state");

	public override Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelAssignmentRoutingStateAndReason Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propReason = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.RoutingState> propRoutingState = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propReason.TryReadProperty(ref reader, options, PropReason, null))
			{
				continue;
			}

			if (propRoutingState.TryReadProperty(ref reader, options, PropRoutingState, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelAssignmentRoutingStateAndReason(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Reason = propReason.Value,
			RoutingState = propRoutingState.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelAssignmentRoutingStateAndReason value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropReason, value.Reason, null, null);
		writer.WriteProperty(options, PropRoutingState, value.RoutingState, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelAssignmentRoutingStateAndReasonConverter))]
public sealed partial class TrainedModelAssignmentRoutingStateAndReason
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TrainedModelAssignmentRoutingStateAndReason(Elastic.Clients.Elasticsearch.MachineLearning.RoutingState routingState)
	{
		RoutingState = routingState;
	}
#if NET7_0_OR_GREATER
	public TrainedModelAssignmentRoutingStateAndReason()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public TrainedModelAssignmentRoutingStateAndReason()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal TrainedModelAssignmentRoutingStateAndReason(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The reason for the current state. It is usually populated only when the
	/// <c>routing_state</c> is <c>failed</c>.
	/// </para>
	/// </summary>
	public string? Reason { get; set; }

	/// <summary>
	/// <para>
	/// The current routing state.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.MachineLearning.RoutingState RoutingState { get; set; }
}