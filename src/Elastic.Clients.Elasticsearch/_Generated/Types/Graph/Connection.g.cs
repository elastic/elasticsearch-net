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

namespace Elastic.Clients.Elasticsearch.Graph;

internal sealed partial class ConnectionConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Graph.Connection>
{
	private static readonly System.Text.Json.JsonEncodedText PropDocCount = System.Text.Json.JsonEncodedText.Encode("doc_count");
	private static readonly System.Text.Json.JsonEncodedText PropSource = System.Text.Json.JsonEncodedText.Encode("source");
	private static readonly System.Text.Json.JsonEncodedText PropTarget = System.Text.Json.JsonEncodedText.Encode("target");
	private static readonly System.Text.Json.JsonEncodedText PropWeight = System.Text.Json.JsonEncodedText.Encode("weight");

	public override Elastic.Clients.Elasticsearch.Graph.Connection Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long> propDocCount = default;
		LocalJsonValue<long> propSource = default;
		LocalJsonValue<long> propTarget = default;
		LocalJsonValue<double> propWeight = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDocCount.TryReadProperty(ref reader, options, PropDocCount, null))
			{
				continue;
			}

			if (propSource.TryReadProperty(ref reader, options, PropSource, null))
			{
				continue;
			}

			if (propTarget.TryReadProperty(ref reader, options, PropTarget, null))
			{
				continue;
			}

			if (propWeight.TryReadProperty(ref reader, options, PropWeight, null))
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
		return new Elastic.Clients.Elasticsearch.Graph.Connection(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			DocCount = propDocCount.Value,
			Source = propSource.Value,
			Target = propTarget.Value,
			Weight = propWeight.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Graph.Connection value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDocCount, value.DocCount, null, null);
		writer.WriteProperty(options, PropSource, value.Source, null, null);
		writer.WriteProperty(options, PropTarget, value.Target, null, null);
		writer.WriteProperty(options, PropWeight, value.Weight, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Graph.ConnectionConverter))]
public sealed partial class Connection
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public Connection(long docCount, long source, long target, double weight)
	{
		DocCount = docCount;
		Source = source;
		Target = target;
		Weight = weight;
	}
#if NET7_0_OR_GREATER
	public Connection()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public Connection()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Connection(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	long DocCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long Source { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long Target { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	double Weight { get; set; }
}