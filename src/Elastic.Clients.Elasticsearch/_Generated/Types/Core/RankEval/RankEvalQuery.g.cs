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

namespace Elastic.Clients.Elasticsearch.Core.RankEval;

internal sealed partial class RankEvalQueryConverter : System.Text.Json.Serialization.JsonConverter<RankEvalQuery>
{
	private static readonly System.Text.Json.JsonEncodedText PropQuery = System.Text.Json.JsonEncodedText.Encode("query");
	private static readonly System.Text.Json.JsonEncodedText PropSize = System.Text.Json.JsonEncodedText.Encode("size");

	public override RankEvalQuery Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		var readerSnapshot = reader;
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.Query> propQuery = default;
		LocalJsonValue<int?> propSize = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propQuery.TryRead(ref reader, options, PropQuery))
			{
				continue;
			}

			if (propSize.TryRead(ref reader, options, PropSize))
			{
				continue;
			}

			try
			{
				reader = readerSnapshot;
				var result = reader.ReadValue<Elastic.Clients.Elasticsearch.QueryDsl.Query>(options);
				return new RankEvalQuery { Query = result };
			}
			catch (System.Text.Json.JsonException)
			{
				throw;
			}
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new RankEvalQuery
		{
			Query = propQuery.Value
,
			Size = propSize.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, RankEvalQuery value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropQuery, value.Query);
		writer.WriteProperty(options, PropSize, value.Size);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(RankEvalQueryConverter))]
public sealed partial class RankEvalQuery
{
	public Elastic.Clients.Elasticsearch.QueryDsl.Query Query { get; set; }
	public int? Size { get; set; }
}

public sealed partial class RankEvalQueryDescriptor<TDocument> : SerializableDescriptor<RankEvalQueryDescriptor<TDocument>>
{
	internal RankEvalQueryDescriptor(Action<RankEvalQueryDescriptor<TDocument>> configure) => configure.Invoke(this);

	public RankEvalQueryDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.QueryDsl.Query QueryValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument> QueryDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>> QueryDescriptorAction { get; set; }
	private int? SizeValue { get; set; }

	public RankEvalQueryDescriptor<TDocument> Query(Elastic.Clients.Elasticsearch.QueryDsl.Query query)
	{
		QueryDescriptor = null;
		QueryDescriptorAction = null;
		QueryValue = query;
		return Self;
	}

	public RankEvalQueryDescriptor<TDocument> Query(Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument> descriptor)
	{
		QueryValue = null;
		QueryDescriptorAction = null;
		QueryDescriptor = descriptor;
		return Self;
	}

	public RankEvalQueryDescriptor<TDocument> Query(Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>> configure)
	{
		QueryValue = null;
		QueryDescriptor = null;
		QueryDescriptorAction = configure;
		return Self;
	}

	public RankEvalQueryDescriptor<TDocument> Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (QueryDescriptor is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryDescriptor, options);
		}
		else if (QueryDescriptorAction is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>(QueryDescriptorAction), options);
		}
		else
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryValue, options);
		}

		if (SizeValue.HasValue)
		{
			writer.WritePropertyName("size");
			writer.WriteNumberValue(SizeValue.Value);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class RankEvalQueryDescriptor : SerializableDescriptor<RankEvalQueryDescriptor>
{
	internal RankEvalQueryDescriptor(Action<RankEvalQueryDescriptor> configure) => configure.Invoke(this);

	public RankEvalQueryDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.QueryDsl.Query QueryValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor QueryDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor> QueryDescriptorAction { get; set; }
	private int? SizeValue { get; set; }

	public RankEvalQueryDescriptor Query(Elastic.Clients.Elasticsearch.QueryDsl.Query query)
	{
		QueryDescriptor = null;
		QueryDescriptorAction = null;
		QueryValue = query;
		return Self;
	}

	public RankEvalQueryDescriptor Query(Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor descriptor)
	{
		QueryValue = null;
		QueryDescriptorAction = null;
		QueryDescriptor = descriptor;
		return Self;
	}

	public RankEvalQueryDescriptor Query(Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor> configure)
	{
		QueryValue = null;
		QueryDescriptor = null;
		QueryDescriptorAction = configure;
		return Self;
	}

	public RankEvalQueryDescriptor Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (QueryDescriptor is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryDescriptor, options);
		}
		else if (QueryDescriptorAction is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor(QueryDescriptorAction), options);
		}
		else
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryValue, options);
		}

		if (SizeValue.HasValue)
		{
			writer.WritePropertyName("size");
			writer.WriteNumberValue(SizeValue.Value);
		}

		writer.WriteEndObject();
	}
}