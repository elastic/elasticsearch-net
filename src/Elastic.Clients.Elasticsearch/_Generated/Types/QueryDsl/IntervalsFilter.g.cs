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
[JsonConverter(typeof(IntervalsFilterConverter))]
public sealed partial class IntervalsFilter
{
	internal IntervalsFilter(string variantName, object variant)
	{
		if (variantName is null)
			throw new ArgumentNullException(nameof(variantName));
		if (variant is null)
			throw new ArgumentNullException(nameof(variant));
		if (string.IsNullOrWhiteSpace(variantName))
			throw new ArgumentException("Variant name must not be empty or whitespace.");
		VariantName = variantName;
		Variant = variant;
	}

	internal object Variant { get; }

	internal string VariantName { get; }
}

internal sealed class IntervalsFilterConverter : JsonConverter<IntervalsFilter>
{
	public override IntervalsFilter Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.StartObject)
		{
			throw new JsonException("Expected start token.");
		}

		reader.Read();
		if (reader.TokenType != JsonTokenType.PropertyName)
		{
			throw new JsonException("Expected a property name token representing the variant held within this container.");
		}

		var propertyName = reader.GetString();
		reader.Read();
		throw new JsonException();
	}

	public override void Write(Utf8JsonWriter writer, IntervalsFilter value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WritePropertyName(value.VariantName);
		writer.WriteEndObject();
	}
}

public sealed partial class IntervalsFilterDescriptor<TDocument> : SerializableDescriptor<IntervalsFilterDescriptor<TDocument>>
{
	internal IntervalsFilterDescriptor(Action<IntervalsFilterDescriptor<TDocument>> configure) => configure.Invoke(this);
	public IntervalsFilterDescriptor() : base()
	{
	}

	private bool ContainsVariant { get; set; }

	private string ContainedVariantName { get; set; }

	private object Variant { get; set; }

	private Descriptor Descriptor { get; set; }

	private IntervalsFilterDescriptor<TDocument> Set<T>(Action<T> descriptorAction, string variantName)
		where T : Descriptor
	{
		ContainedVariantName = variantName;
		ContainsVariant = true;
		var descriptor = (T)Activator.CreateInstance(typeof(T), true);
		descriptorAction?.Invoke(descriptor);
		Descriptor = descriptor;
		return Self;
	}

	private IntervalsFilterDescriptor<TDocument> Set(object variant, string variantName)
	{
		Variant = variant;
		ContainedVariantName = variantName;
		ContainsVariant = true;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		if (!ContainsVariant)
		{
			writer.WriteNullValue();
			return;
		}

		writer.WriteStartObject();
		writer.WritePropertyName(ContainedVariantName);
		if (Variant is not null)
		{
			JsonSerializer.Serialize(writer, Variant, Variant.GetType(), options);
			writer.WriteEndObject();
			return;
		}

		JsonSerializer.Serialize(writer, Descriptor, Descriptor.GetType(), options);
		writer.WriteEndObject();
	}
}

public sealed partial class IntervalsFilterDescriptor : SerializableDescriptor<IntervalsFilterDescriptor>
{
	internal IntervalsFilterDescriptor(Action<IntervalsFilterDescriptor> configure) => configure.Invoke(this);
	public IntervalsFilterDescriptor() : base()
	{
	}

	private bool ContainsVariant { get; set; }

	private string ContainedVariantName { get; set; }

	private object Variant { get; set; }

	private Descriptor Descriptor { get; set; }

	private IntervalsFilterDescriptor Set<T>(Action<T> descriptorAction, string variantName)
		where T : Descriptor
	{
		ContainedVariantName = variantName;
		ContainsVariant = true;
		var descriptor = (T)Activator.CreateInstance(typeof(T), true);
		descriptorAction?.Invoke(descriptor);
		Descriptor = descriptor;
		return Self;
	}

	private IntervalsFilterDescriptor Set(object variant, string variantName)
	{
		Variant = variant;
		ContainedVariantName = variantName;
		ContainsVariant = true;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		if (!ContainsVariant)
		{
			writer.WriteNullValue();
			return;
		}

		writer.WriteStartObject();
		writer.WritePropertyName(ContainedVariantName);
		if (Variant is not null)
		{
			JsonSerializer.Serialize(writer, Variant, Variant.GetType(), options);
			writer.WriteEndObject();
			return;
		}

		JsonSerializer.Serialize(writer, Descriptor, Descriptor.GetType(), options);
		writer.WriteEndObject();
	}
}