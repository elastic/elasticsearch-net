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

namespace Elastic.Clients.Elasticsearch.Aggregations;

internal sealed partial class NestedAggregationConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Aggregations.NestedAggregation>
{
	private static readonly System.Text.Json.JsonEncodedText PropPath = System.Text.Json.JsonEncodedText.Encode("path");

	public override Elastic.Clients.Elasticsearch.Aggregations.NestedAggregation Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field?> propPath = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propPath.TryReadProperty(ref reader, options, PropPath, null))
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
		return new Elastic.Clients.Elasticsearch.Aggregations.NestedAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Path = propPath.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Aggregations.NestedAggregation value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropPath, value.Path, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Aggregations.NestedAggregationConverter))]
public sealed partial class NestedAggregation
{
#if NET7_0_OR_GREATER
	public NestedAggregation()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public NestedAggregation()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal NestedAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The path to the field of type <c>nested</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Field? Path { get; set; }
}

public readonly partial struct NestedAggregationDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Aggregations.NestedAggregation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public NestedAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.NestedAggregation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public NestedAggregationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.NestedAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.NestedAggregationDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Aggregations.NestedAggregation instance) => new Elastic.Clients.Elasticsearch.Aggregations.NestedAggregationDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.NestedAggregation(Elastic.Clients.Elasticsearch.Aggregations.NestedAggregationDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The path to the field of type <c>nested</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.NestedAggregationDescriptor<TDocument> Path(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.Path = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The path to the field of type <c>nested</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.NestedAggregationDescriptor<TDocument> Path(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Path = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.NestedAggregation Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.NestedAggregationDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Aggregations.NestedAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Aggregations.NestedAggregationDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Aggregations.NestedAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct NestedAggregationDescriptor
{
	internal Elastic.Clients.Elasticsearch.Aggregations.NestedAggregation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public NestedAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.NestedAggregation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public NestedAggregationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.NestedAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.NestedAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.NestedAggregation instance) => new Elastic.Clients.Elasticsearch.Aggregations.NestedAggregationDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.NestedAggregation(Elastic.Clients.Elasticsearch.Aggregations.NestedAggregationDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The path to the field of type <c>nested</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.NestedAggregationDescriptor Path(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.Path = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The path to the field of type <c>nested</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.NestedAggregationDescriptor Path<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Path = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.NestedAggregation Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.NestedAggregationDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Aggregations.NestedAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Aggregations.NestedAggregationDescriptor(new Elastic.Clients.Elasticsearch.Aggregations.NestedAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}