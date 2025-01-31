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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Analysis;

internal sealed partial class StandardAnalyzerConverter : System.Text.Json.Serialization.JsonConverter<StandardAnalyzer>
{
	private static readonly System.Text.Json.JsonEncodedText PropMaxTokenLength = System.Text.Json.JsonEncodedText.Encode("max_token_length");
	private static readonly System.Text.Json.JsonEncodedText PropStopwords = System.Text.Json.JsonEncodedText.Encode("stopwords");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");

	public override StandardAnalyzer Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int?> propMaxTokenLength = default;
		LocalJsonValue<ICollection<string>?> propStopwords = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propMaxTokenLength.TryRead(ref reader, options, PropMaxTokenLength))
			{
				continue;
			}

			if (propStopwords.TryRead(ref reader, options, PropStopwords, typeof(SingleOrManyMarker<ICollection<string>?, string>)))
			{
				continue;
			}

			if (reader.ValueTextEquals(PropType))
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new StandardAnalyzer
		{
			MaxTokenLength = propMaxTokenLength.Value
,
			Stopwords = propStopwords.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, StandardAnalyzer value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropMaxTokenLength, value.MaxTokenLength);
		writer.WriteProperty(options, PropStopwords, value.Stopwords, null, typeof(SingleOrManyMarker<ICollection<string>?, string>));
		writer.WriteProperty(options, PropType, value.Type);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(StandardAnalyzerConverter))]
public sealed partial class StandardAnalyzer : IAnalyzer
{
	public int? MaxTokenLength { get; set; }
	public ICollection<string>? Stopwords { get; set; }

	public string Type => "standard";
}

public sealed partial class StandardAnalyzerDescriptor : SerializableDescriptor<StandardAnalyzerDescriptor>, IBuildableDescriptor<StandardAnalyzer>
{
	internal StandardAnalyzerDescriptor(Action<StandardAnalyzerDescriptor> configure) => configure.Invoke(this);

	public StandardAnalyzerDescriptor() : base()
	{
	}

	private int? MaxTokenLengthValue { get; set; }
	private ICollection<string>? StopwordsValue { get; set; }

	public StandardAnalyzerDescriptor MaxTokenLength(int? maxTokenLength)
	{
		MaxTokenLengthValue = maxTokenLength;
		return Self;
	}

	public StandardAnalyzerDescriptor Stopwords(ICollection<string>? stopwords)
	{
		StopwordsValue = stopwords;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (MaxTokenLengthValue.HasValue)
		{
			writer.WritePropertyName("max_token_length");
			writer.WriteNumberValue(MaxTokenLengthValue.Value);
		}

		if (StopwordsValue is not null)
		{
			writer.WritePropertyName("stopwords");
			SingleOrManySerializationHelper.Serialize<string>(StopwordsValue, writer, options);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("standard");
		writer.WriteEndObject();
	}

	StandardAnalyzer IBuildableDescriptor<StandardAnalyzer>.Build() => new()
	{
		MaxTokenLength = MaxTokenLengthValue,
		Stopwords = StopwordsValue
	};
}