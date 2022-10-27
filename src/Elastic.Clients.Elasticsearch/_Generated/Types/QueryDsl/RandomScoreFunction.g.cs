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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.QueryDsl;
public sealed partial class RandomScoreFunction
{
	[JsonInclude]
	[JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Field? Field { get; set; }

	[JsonInclude]
	[JsonPropertyName("seed")]
	public Union<long?, string?>? Seed { get; set; }

	public static implicit operator FunctionScoreContainer(RandomScoreFunction randomScoreFunction) => FunctionScoreContainer.RandomScore(randomScoreFunction);
}

public sealed partial class RandomScoreFunctionDescriptor<TDocument> : SerializableDescriptor<RandomScoreFunctionDescriptor<TDocument>>
{
	internal RandomScoreFunctionDescriptor(Action<RandomScoreFunctionDescriptor<TDocument>> configure) => configure.Invoke(this);
	public RandomScoreFunctionDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }

	private Union<long?, string?>? SeedValue { get; set; }

	public RandomScoreFunctionDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field? field)
	{
		FieldValue = field;
		return Self;
	}

	public RandomScoreFunctionDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public RandomScoreFunctionDescriptor<TDocument> Seed(Union<long?, string?>? seed)
	{
		SeedValue = seed;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (FieldValue is not null)
		{
			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
		}

		if (SeedValue is not null)
		{
			writer.WritePropertyName("seed");
			JsonSerializer.Serialize(writer, SeedValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class RandomScoreFunctionDescriptor : SerializableDescriptor<RandomScoreFunctionDescriptor>
{
	internal RandomScoreFunctionDescriptor(Action<RandomScoreFunctionDescriptor> configure) => configure.Invoke(this);
	public RandomScoreFunctionDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }

	private Union<long?, string?>? SeedValue { get; set; }

	public RandomScoreFunctionDescriptor Field(Elastic.Clients.Elasticsearch.Field? field)
	{
		FieldValue = field;
		return Self;
	}

	public RandomScoreFunctionDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public RandomScoreFunctionDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	public RandomScoreFunctionDescriptor Seed(Union<long?, string?>? seed)
	{
		SeedValue = seed;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (FieldValue is not null)
		{
			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
		}

		if (SeedValue is not null)
		{
			writer.WritePropertyName("seed");
			JsonSerializer.Serialize(writer, SeedValue, options);
		}

		writer.WriteEndObject();
	}
}