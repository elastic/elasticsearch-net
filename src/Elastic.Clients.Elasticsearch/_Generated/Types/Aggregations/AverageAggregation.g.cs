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

internal sealed partial class AverageAggregationConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Aggregations.AverageAggregation>
{
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropFormat = System.Text.Json.JsonEncodedText.Encode("format");
	private static readonly System.Text.Json.JsonEncodedText PropMissing = System.Text.Json.JsonEncodedText.Encode("missing");
	private static readonly System.Text.Json.JsonEncodedText PropScript = System.Text.Json.JsonEncodedText.Encode("script");

	public override Elastic.Clients.Elasticsearch.Aggregations.AverageAggregation Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field?> propField = default;
		LocalJsonValue<string?> propFormat = default;
		LocalJsonValue<object?> propMissing = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Script?> propScript = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propField.TryReadProperty(ref reader, options, PropField, null))
			{
				continue;
			}

			if (propFormat.TryReadProperty(ref reader, options, PropFormat, null))
			{
				continue;
			}

			if (propMissing.TryReadProperty(ref reader, options, PropMissing, null))
			{
				continue;
			}

			if (propScript.TryReadProperty(ref reader, options, PropScript, null))
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
		return new Elastic.Clients.Elasticsearch.Aggregations.AverageAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Field = propField.Value,
			Format = propFormat.Value,
			Missing = propMissing.Value,
			Script = propScript.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Aggregations.AverageAggregation value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropFormat, value.Format, null, null);
		writer.WriteProperty(options, PropMissing, value.Missing, null, null);
		writer.WriteProperty(options, PropScript, value.Script, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Aggregations.AverageAggregationConverter))]
public sealed partial class AverageAggregation
{
#if NET7_0_OR_GREATER
	public AverageAggregation()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public AverageAggregation()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal AverageAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The field on which to run the aggregation.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Field? Field { get; set; }
	public string? Format { get; set; }

	/// <summary>
	/// <para>
	/// The value to apply to documents that do not have a value.
	/// By default, documents without a value are ignored.
	/// </para>
	/// </summary>
	public object? Missing { get; set; }
	public Elastic.Clients.Elasticsearch.Script? Script { get; set; }
}

public readonly partial struct AverageAggregationDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Aggregations.AverageAggregation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AverageAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.AverageAggregation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AverageAggregationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.AverageAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.AverageAggregationDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Aggregations.AverageAggregation instance) => new Elastic.Clients.Elasticsearch.Aggregations.AverageAggregationDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.AverageAggregation(Elastic.Clients.Elasticsearch.Aggregations.AverageAggregationDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The field on which to run the aggregation.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AverageAggregationDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field on which to run the aggregation.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AverageAggregationDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AverageAggregationDescriptor<TDocument> Format(string? value)
	{
		Instance.Format = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The value to apply to documents that do not have a value.
	/// By default, documents without a value are ignored.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AverageAggregationDescriptor<TDocument> Missing(object? value)
	{
		Instance.Missing = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AverageAggregationDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.Script = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AverageAggregationDescriptor<TDocument> Script()
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AverageAggregationDescriptor<TDocument> Script(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.AverageAggregation Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.AverageAggregationDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Aggregations.AverageAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Aggregations.AverageAggregationDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Aggregations.AverageAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct AverageAggregationDescriptor
{
	internal Elastic.Clients.Elasticsearch.Aggregations.AverageAggregation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AverageAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.AverageAggregation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AverageAggregationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.AverageAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.AverageAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.AverageAggregation instance) => new Elastic.Clients.Elasticsearch.Aggregations.AverageAggregationDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.AverageAggregation(Elastic.Clients.Elasticsearch.Aggregations.AverageAggregationDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The field on which to run the aggregation.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AverageAggregationDescriptor Field(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field on which to run the aggregation.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AverageAggregationDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AverageAggregationDescriptor Format(string? value)
	{
		Instance.Format = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The value to apply to documents that do not have a value.
	/// By default, documents without a value are ignored.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AverageAggregationDescriptor Missing(object? value)
	{
		Instance.Missing = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AverageAggregationDescriptor Script(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.Script = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AverageAggregationDescriptor Script()
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AverageAggregationDescriptor Script(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.AverageAggregation Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.AverageAggregationDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Aggregations.AverageAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Aggregations.AverageAggregationDescriptor(new Elastic.Clients.Elasticsearch.Aggregations.AverageAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}