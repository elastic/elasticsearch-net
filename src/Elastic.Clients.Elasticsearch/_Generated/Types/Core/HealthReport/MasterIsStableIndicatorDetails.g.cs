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

namespace Elastic.Clients.Elasticsearch.Core.HealthReport;

internal sealed partial class MasterIsStableIndicatorDetailsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.HealthReport.MasterIsStableIndicatorDetails>
{
	private static readonly System.Text.Json.JsonEncodedText PropClusterFormation = System.Text.Json.JsonEncodedText.Encode("cluster_formation");
	private static readonly System.Text.Json.JsonEncodedText PropCurrentMaster = System.Text.Json.JsonEncodedText.Encode("current_master");
	private static readonly System.Text.Json.JsonEncodedText PropExceptionFetchingHistory = System.Text.Json.JsonEncodedText.Encode("exception_fetching_history");
	private static readonly System.Text.Json.JsonEncodedText PropRecentMasters = System.Text.Json.JsonEncodedText.Encode("recent_masters");

	public override Elastic.Clients.Elasticsearch.Core.HealthReport.MasterIsStableIndicatorDetails Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.HealthReport.MasterIsStableIndicatorClusterFormationNode>?> propClusterFormation = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Core.HealthReport.IndicatorNode> propCurrentMaster = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Core.HealthReport.MasterIsStableIndicatorExceptionFetchingHistory?> propExceptionFetchingHistory = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.HealthReport.IndicatorNode>> propRecentMasters = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propClusterFormation.TryReadProperty(ref reader, options, PropClusterFormation, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.HealthReport.MasterIsStableIndicatorClusterFormationNode>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Core.HealthReport.MasterIsStableIndicatorClusterFormationNode>(o, null)))
			{
				continue;
			}

			if (propCurrentMaster.TryReadProperty(ref reader, options, PropCurrentMaster, null))
			{
				continue;
			}

			if (propExceptionFetchingHistory.TryReadProperty(ref reader, options, PropExceptionFetchingHistory, null))
			{
				continue;
			}

			if (propRecentMasters.TryReadProperty(ref reader, options, PropRecentMasters, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.HealthReport.IndicatorNode> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Core.HealthReport.IndicatorNode>(o, null)!))
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
		return new Elastic.Clients.Elasticsearch.Core.HealthReport.MasterIsStableIndicatorDetails(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ClusterFormation = propClusterFormation.Value,
			CurrentMaster = propCurrentMaster.Value,
			ExceptionFetchingHistory = propExceptionFetchingHistory.Value,
			RecentMasters = propRecentMasters.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.HealthReport.MasterIsStableIndicatorDetails value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropClusterFormation, value.ClusterFormation, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.HealthReport.MasterIsStableIndicatorClusterFormationNode>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Core.HealthReport.MasterIsStableIndicatorClusterFormationNode>(o, v, null));
		writer.WriteProperty(options, PropCurrentMaster, value.CurrentMaster, null, null);
		writer.WriteProperty(options, PropExceptionFetchingHistory, value.ExceptionFetchingHistory, null, null);
		writer.WriteProperty(options, PropRecentMasters, value.RecentMasters, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.HealthReport.IndicatorNode> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Core.HealthReport.IndicatorNode>(o, v, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.HealthReport.MasterIsStableIndicatorDetailsConverter))]
public sealed partial class MasterIsStableIndicatorDetails
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MasterIsStableIndicatorDetails(Elastic.Clients.Elasticsearch.Core.HealthReport.IndicatorNode currentMaster, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.HealthReport.IndicatorNode> recentMasters)
	{
		CurrentMaster = currentMaster;
		RecentMasters = recentMasters;
	}
#if NET7_0_OR_GREATER
	public MasterIsStableIndicatorDetails()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public MasterIsStableIndicatorDetails()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal MasterIsStableIndicatorDetails(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.HealthReport.MasterIsStableIndicatorClusterFormationNode>? ClusterFormation { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Core.HealthReport.IndicatorNode CurrentMaster { get; set; }
	public Elastic.Clients.Elasticsearch.Core.HealthReport.MasterIsStableIndicatorExceptionFetchingHistory? ExceptionFetchingHistory { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.HealthReport.IndicatorNode> RecentMasters { get; set; }
}