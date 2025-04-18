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

namespace Elastic.Clients.Elasticsearch.Enrich;

internal sealed partial class EnrichSummaryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Enrich.EnrichSummary>
{
	private static readonly System.Text.Json.JsonEncodedText PropConfig = System.Text.Json.JsonEncodedText.Encode("config");

	public override Elastic.Clients.Elasticsearch.Enrich.EnrichSummary Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Enrich.PolicyType, Elastic.Clients.Elasticsearch.Enrich.EnrichPolicy>> propConfig = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propConfig.TryReadProperty(ref reader, options, PropConfig, static System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Enrich.PolicyType, Elastic.Clients.Elasticsearch.Enrich.EnrichPolicy> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadKeyValuePairValue<Elastic.Clients.Elasticsearch.Enrich.PolicyType, Elastic.Clients.Elasticsearch.Enrich.EnrichPolicy>(o, null, null)))
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
		return new Elastic.Clients.Elasticsearch.Enrich.EnrichSummary(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Config = propConfig.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Enrich.EnrichSummary value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropConfig, value.Config, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Enrich.PolicyType, Elastic.Clients.Elasticsearch.Enrich.EnrichPolicy> v) => w.WriteKeyValuePairValue<Elastic.Clients.Elasticsearch.Enrich.PolicyType, Elastic.Clients.Elasticsearch.Enrich.EnrichPolicy>(o, v, null, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Enrich.EnrichSummaryConverter))]
public sealed partial class EnrichSummary
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public EnrichSummary(System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Enrich.PolicyType, Elastic.Clients.Elasticsearch.Enrich.EnrichPolicy> config)
	{
		Config = config;
	}
#if NET7_0_OR_GREATER
	public EnrichSummary()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public EnrichSummary()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal EnrichSummary(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.KeyValuePair<Elastic.Clients.Elasticsearch.Enrich.PolicyType, Elastic.Clients.Elasticsearch.Enrich.EnrichPolicy> Config { get; set; }
}