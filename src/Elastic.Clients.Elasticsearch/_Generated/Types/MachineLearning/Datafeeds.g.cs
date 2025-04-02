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

internal sealed partial class DatafeedsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.Datafeeds>
{
	private static readonly System.Text.Json.JsonEncodedText PropScrollSize = System.Text.Json.JsonEncodedText.Encode("scroll_size");

	public override Elastic.Clients.Elasticsearch.MachineLearning.Datafeeds Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int> propScrollSize = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propScrollSize.TryReadProperty(ref reader, options, PropScrollSize, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.Datafeeds(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ScrollSize = propScrollSize.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.Datafeeds value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropScrollSize, value.ScrollSize, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.DatafeedsConverter))]
public sealed partial class Datafeeds
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public Datafeeds(int scrollSize)
	{
		ScrollSize = scrollSize;
	}
#if NET7_0_OR_GREATER
	public Datafeeds()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public Datafeeds()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Datafeeds(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	int ScrollSize { get; set; }
}