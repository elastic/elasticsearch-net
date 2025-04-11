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

namespace Elastic.Clients.Elasticsearch.Cluster;

internal sealed partial class AllocationDecisionConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Cluster.AllocationDecision>
{
	private static readonly System.Text.Json.JsonEncodedText PropDecider = System.Text.Json.JsonEncodedText.Encode("decider");
	private static readonly System.Text.Json.JsonEncodedText PropDecision = System.Text.Json.JsonEncodedText.Encode("decision");
	private static readonly System.Text.Json.JsonEncodedText PropExplanation = System.Text.Json.JsonEncodedText.Encode("explanation");

	public override Elastic.Clients.Elasticsearch.Cluster.AllocationDecision Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propDecider = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Cluster.AllocationExplainDecision> propDecision = default;
		LocalJsonValue<string> propExplanation = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDecider.TryReadProperty(ref reader, options, PropDecider, null))
			{
				continue;
			}

			if (propDecision.TryReadProperty(ref reader, options, PropDecision, null))
			{
				continue;
			}

			if (propExplanation.TryReadProperty(ref reader, options, PropExplanation, null))
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
		return new Elastic.Clients.Elasticsearch.Cluster.AllocationDecision(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Decider = propDecider.Value,
			Decision = propDecision.Value,
			Explanation = propExplanation.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Cluster.AllocationDecision value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDecider, value.Decider, null, null);
		writer.WriteProperty(options, PropDecision, value.Decision, null, null);
		writer.WriteProperty(options, PropExplanation, value.Explanation, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Cluster.AllocationDecisionConverter))]
public sealed partial class AllocationDecision
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AllocationDecision(string decider, Elastic.Clients.Elasticsearch.Cluster.AllocationExplainDecision decision, string explanation)
	{
		Decider = decider;
		Decision = decision;
		Explanation = explanation;
	}
#if NET7_0_OR_GREATER
	public AllocationDecision()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public AllocationDecision()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal AllocationDecision(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	string Decider { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Cluster.AllocationExplainDecision Decision { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Explanation { get; set; }
}