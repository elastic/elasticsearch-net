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

internal sealed partial class MedianAbsoluteDeviationAggregationConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregation>
{
	private static readonly System.Text.Json.JsonEncodedText PropCompression = System.Text.Json.JsonEncodedText.Encode("compression");
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropFormat = System.Text.Json.JsonEncodedText.Encode("format");
	private static readonly System.Text.Json.JsonEncodedText PropMissing = System.Text.Json.JsonEncodedText.Encode("missing");
	private static readonly System.Text.Json.JsonEncodedText PropScript = System.Text.Json.JsonEncodedText.Encode("script");

	public override Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregation Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<double?> propCompression = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field?> propField = default;
		LocalJsonValue<string?> propFormat = default;
		LocalJsonValue<object?> propMissing = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Script?> propScript = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCompression.TryReadProperty(ref reader, options, PropCompression, null))
			{
				continue;
			}

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
		return new Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Compression = propCompression.Value,
			Field = propField.Value,
			Format = propFormat.Value,
			Missing = propMissing.Value,
			Script = propScript.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregation value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCompression, value.Compression, null, null);
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropFormat, value.Format, null, null);
		writer.WriteProperty(options, PropMissing, value.Missing, null, null);
		writer.WriteProperty(options, PropScript, value.Script, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationConverter))]
public sealed partial class MedianAbsoluteDeviationAggregation
{
#if NET7_0_OR_GREATER
	public MedianAbsoluteDeviationAggregation()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public MedianAbsoluteDeviationAggregation()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal MedianAbsoluteDeviationAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Limits the maximum number of nodes used by the underlying TDigest algorithm to <c>20 * compression</c>, enabling control of memory usage and approximation error.
	/// </para>
	/// </summary>
	public double? Compression { get; set; }

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

public readonly partial struct MedianAbsoluteDeviationAggregationDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MedianAbsoluteDeviationAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MedianAbsoluteDeviationAggregationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregation instance) => new Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregation(Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Limits the maximum number of nodes used by the underlying TDigest algorithm to <c>20 * compression</c>, enabling control of memory usage and approximation error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationDescriptor<TDocument> Compression(double? value)
	{
		Instance.Compression = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field on which to run the aggregation.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field on which to run the aggregation.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationDescriptor<TDocument> Format(string? value)
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
	public Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationDescriptor<TDocument> Missing(object? value)
	{
		Instance.Missing = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.Script = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationDescriptor<TDocument> Script()
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationDescriptor<TDocument> Script(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregation Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct MedianAbsoluteDeviationAggregationDescriptor
{
	internal Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MedianAbsoluteDeviationAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MedianAbsoluteDeviationAggregationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregation instance) => new Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregation(Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Limits the maximum number of nodes used by the underlying TDigest algorithm to <c>20 * compression</c>, enabling control of memory usage and approximation error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationDescriptor Compression(double? value)
	{
		Instance.Compression = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field on which to run the aggregation.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationDescriptor Field(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field on which to run the aggregation.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationDescriptor Format(string? value)
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
	public Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationDescriptor Missing(object? value)
	{
		Instance.Missing = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationDescriptor Script(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.Script = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationDescriptor Script()
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationDescriptor Script(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregation Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregationDescriptor(new Elastic.Clients.Elasticsearch.Aggregations.MedianAbsoluteDeviationAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}