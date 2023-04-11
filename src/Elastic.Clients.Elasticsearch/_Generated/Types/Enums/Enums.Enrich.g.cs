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
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;
using Elastic.Transport;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Enrich;

[JsonConverter(typeof(EnrichPolicyPhaseConverter))]
public enum EnrichPolicyPhase
{
	[EnumMember(Value = "SCHEDULED")]
	Scheduled,
	[EnumMember(Value = "RUNNING")]
	Running,
	[EnumMember(Value = "FAILED")]
	Failed,
	[EnumMember(Value = "COMPLETE")]
	Complete
}

internal sealed class EnrichPolicyPhaseConverter : JsonConverter<EnrichPolicyPhase>
{
	public override EnrichPolicyPhase Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "SCHEDULED":
				return EnrichPolicyPhase.Scheduled;
			case "RUNNING":
				return EnrichPolicyPhase.Running;
			case "FAILED":
				return EnrichPolicyPhase.Failed;
			case "COMPLETE":
				return EnrichPolicyPhase.Complete;
		}

		ThrowHelper.ThrowJsonException(); return default;
	}

	public override void Write(Utf8JsonWriter writer, EnrichPolicyPhase value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case EnrichPolicyPhase.Scheduled:
				writer.WriteStringValue("SCHEDULED");
				return;
			case EnrichPolicyPhase.Running:
				writer.WriteStringValue("RUNNING");
				return;
			case EnrichPolicyPhase.Failed:
				writer.WriteStringValue("FAILED");
				return;
			case EnrichPolicyPhase.Complete:
				writer.WriteStringValue("COMPLETE");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(PolicyTypeConverter))]
public enum PolicyType
{
	[EnumMember(Value = "range")]
	Range,
	[EnumMember(Value = "match")]
	Match,
	[EnumMember(Value = "geo_match")]
	GeoMatch
}

internal sealed class PolicyTypeConverter : JsonConverter<PolicyType>
{
	public override PolicyType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "range":
				return PolicyType.Range;
			case "match":
				return PolicyType.Match;
			case "geo_match":
				return PolicyType.GeoMatch;
		}

		ThrowHelper.ThrowJsonException(); return default;
	}

	public override void Write(Utf8JsonWriter writer, PolicyType value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case PolicyType.Range:
				writer.WriteStringValue("range");
				return;
			case PolicyType.Match:
				writer.WriteStringValue("match");
				return;
			case PolicyType.GeoMatch:
				writer.WriteStringValue("geo_match");
				return;
		}

		writer.WriteNullValue();
	}
}