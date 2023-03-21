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

public sealed partial class DocumentRating
{
	[JsonInclude, JsonPropertyName("_id")]
	public Elastic.Clients.Elasticsearch.Id Id { get; set; }
	[JsonInclude, JsonPropertyName("_index")]
	public Elastic.Clients.Elasticsearch.IndexName Index { get; set; }
	[JsonInclude, JsonPropertyName("rating")]
	public int Rating { get; set; }
}

public sealed partial class DocumentRatingDescriptor : SerializableDescriptor<DocumentRatingDescriptor>
{
	internal DocumentRatingDescriptor(Action<DocumentRatingDescriptor> configure) => configure.Invoke(this);

	public DocumentRatingDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Id IdValue { get; set; }
	private Elastic.Clients.Elasticsearch.IndexName IndexValue { get; set; }
	private int RatingValue { get; set; }

	public DocumentRatingDescriptor Id(Elastic.Clients.Elasticsearch.Id id)
	{
		IdValue = id;
		return Self;
	}

	public DocumentRatingDescriptor Index(Elastic.Clients.Elasticsearch.IndexName index)
	{
		IndexValue = index;
		return Self;
	}

	public DocumentRatingDescriptor Rating(int rating)
	{
		RatingValue = rating;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("_id");
		JsonSerializer.Serialize(writer, IdValue, options);
		writer.WritePropertyName("_index");
		JsonSerializer.Serialize(writer, IndexValue, options);
		writer.WritePropertyName("rating");
		writer.WriteNumberValue(RatingValue);
		writer.WriteEndObject();
	}
}