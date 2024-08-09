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

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.Aggregations;

internal sealed partial class StringRareTermsBucketConverter : JsonConverter<StringRareTermsBucket>
{
	public override StringRareTermsBucket Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.StartObject)
			throw new JsonException("Unexpected JSON detected.");
		long docCount = default;
		string key = default;
		Dictionary<string, Elastic.Clients.Elasticsearch.Serverless.Aggregations.IAggregate> additionalProperties = null;
		while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
		{
			if (reader.TokenType == JsonTokenType.PropertyName)
			{
				var property = reader.GetString();
				if (property == "doc_count")
				{
					docCount = JsonSerializer.Deserialize<long>(ref reader, options);
					continue;
				}

				if (property == "key")
				{
					key = JsonSerializer.Deserialize<string>(ref reader, options);
					continue;
				}

				if (property.Contains("#"))
				{
					additionalProperties ??= new Dictionary<string, Elastic.Clients.Elasticsearch.Serverless.Aggregations.IAggregate>();
					AggregateDictionaryConverter.ReadItem(ref reader, options, additionalProperties, property);
					continue;
				}

				throw new JsonException("Unknown property read from JSON.");
			}
		}

		return new StringRareTermsBucket { Aggregations = new Elastic.Clients.Elasticsearch.Serverless.Aggregations.AggregateDictionary(additionalProperties), DocCount = docCount, Key = key };
	}

	public override void Write(Utf8JsonWriter writer, StringRareTermsBucket value, JsonSerializerOptions options)
	{
		throw new NotImplementedException("'StringRareTermsBucket' is a readonly type, used only on responses and does not support being written to JSON.");
	}
}

[JsonConverter(typeof(StringRareTermsBucketConverter))]
public sealed partial class StringRareTermsBucket
{
	/// <summary>
	/// <para>
	/// Nested aggregations
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Aggregations.AggregateDictionary Aggregations { get; init; }
	public long DocCount { get; init; }
	public string Key { get; init; }
}