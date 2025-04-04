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

internal sealed partial class AutoDateHistogramAggregationConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregation>
{
	private static readonly System.Text.Json.JsonEncodedText PropBuckets = System.Text.Json.JsonEncodedText.Encode("buckets");
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropFormat = System.Text.Json.JsonEncodedText.Encode("format");
	private static readonly System.Text.Json.JsonEncodedText PropMinimumInterval = System.Text.Json.JsonEncodedText.Encode("minimum_interval");
	private static readonly System.Text.Json.JsonEncodedText PropMissing = System.Text.Json.JsonEncodedText.Encode("missing");
	private static readonly System.Text.Json.JsonEncodedText PropOffset = System.Text.Json.JsonEncodedText.Encode("offset");
	private static readonly System.Text.Json.JsonEncodedText PropParams = System.Text.Json.JsonEncodedText.Encode("params");
	private static readonly System.Text.Json.JsonEncodedText PropScript = System.Text.Json.JsonEncodedText.Encode("script");
	private static readonly System.Text.Json.JsonEncodedText PropTimeZone = System.Text.Json.JsonEncodedText.Encode("time_zone");

	public override Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregation Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int?> propBuckets = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field?> propField = default;
		LocalJsonValue<string?> propFormat = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Aggregations.MinimumInterval?> propMinimumInterval = default;
		LocalJsonValue<System.DateTime?> propMissing = default;
		LocalJsonValue<string?> propOffset = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<string, object>?> propParams = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Script?> propScript = default;
		LocalJsonValue<string?> propTimeZone = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBuckets.TryReadProperty(ref reader, options, PropBuckets, null))
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

			if (propMinimumInterval.TryReadProperty(ref reader, options, PropMinimumInterval, null))
			{
				continue;
			}

			if (propMissing.TryReadProperty(ref reader, options, PropMissing, static System.DateTime? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTime?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker))))
			{
				continue;
			}

			if (propOffset.TryReadProperty(ref reader, options, PropOffset, null))
			{
				continue;
			}

			if (propParams.TryReadProperty(ref reader, options, PropParams, static System.Collections.Generic.IDictionary<string, object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)))
			{
				continue;
			}

			if (propScript.TryReadProperty(ref reader, options, PropScript, null))
			{
				continue;
			}

			if (propTimeZone.TryReadProperty(ref reader, options, PropTimeZone, null))
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
		return new Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Buckets = propBuckets.Value,
			Field = propField.Value,
			Format = propFormat.Value,
			MinimumInterval = propMinimumInterval.Value,
			Missing = propMissing.Value,
			Offset = propOffset.Value,
			Params = propParams.Value,
			Script = propScript.Value,
			TimeZone = propTimeZone.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregation value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBuckets, value.Buckets, null, null);
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropFormat, value.Format, null, null);
		writer.WriteProperty(options, PropMinimumInterval, value.MinimumInterval, null, null);
		writer.WriteProperty(options, PropMissing, value.Missing, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTime? v) => w.WriteValueEx<System.DateTime?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker)));
		writer.WriteProperty(options, PropOffset, value.Offset, null, null);
		writer.WriteProperty(options, PropParams, value.Params, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, object>? v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteProperty(options, PropScript, value.Script, null, null);
		writer.WriteProperty(options, PropTimeZone, value.TimeZone, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationConverter))]
public sealed partial class AutoDateHistogramAggregation
{
#if NET7_0_OR_GREATER
	public AutoDateHistogramAggregation()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public AutoDateHistogramAggregation()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal AutoDateHistogramAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The target number of buckets.
	/// </para>
	/// </summary>
	public int? Buckets { get; set; }

	/// <summary>
	/// <para>
	/// The field on which to run the aggregation.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Field? Field { get; set; }

	/// <summary>
	/// <para>
	/// The date format used to format <c>key_as_string</c> in the response.
	/// If no <c>format</c> is specified, the first date format specified in the field mapping is used.
	/// </para>
	/// </summary>
	public string? Format { get; set; }

	/// <summary>
	/// <para>
	/// The minimum rounding interval.
	/// This can make the collection process more efficient, as the aggregation will not attempt to round at any interval lower than <c>minimum_interval</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.MinimumInterval? MinimumInterval { get; set; }

	/// <summary>
	/// <para>
	/// The value to apply to documents that do not have a value.
	/// By default, documents without a value are ignored.
	/// </para>
	/// </summary>
	public System.DateTime? Missing { get; set; }

	/// <summary>
	/// <para>
	/// Time zone specified as a ISO 8601 UTC offset.
	/// </para>
	/// </summary>
	public string? Offset { get; set; }
	public System.Collections.Generic.IDictionary<string, object>? Params { get; set; }
	public Elastic.Clients.Elasticsearch.Script? Script { get; set; }

	/// <summary>
	/// <para>
	/// Time zone ID.
	/// </para>
	/// </summary>
	public string? TimeZone { get; set; }
}

public readonly partial struct AutoDateHistogramAggregationDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AutoDateHistogramAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AutoDateHistogramAggregationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregation instance) => new Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregation(Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The target number of buckets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor<TDocument> Buckets(int? value)
	{
		Instance.Buckets = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field on which to run the aggregation.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field on which to run the aggregation.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The date format used to format <c>key_as_string</c> in the response.
	/// If no <c>format</c> is specified, the first date format specified in the field mapping is used.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor<TDocument> Format(string? value)
	{
		Instance.Format = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The minimum rounding interval.
	/// This can make the collection process more efficient, as the aggregation will not attempt to round at any interval lower than <c>minimum_interval</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor<TDocument> MinimumInterval(Elastic.Clients.Elasticsearch.Aggregations.MinimumInterval? value)
	{
		Instance.MinimumInterval = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The value to apply to documents that do not have a value.
	/// By default, documents without a value are ignored.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor<TDocument> Missing(System.DateTime? value)
	{
		Instance.Missing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Time zone specified as a ISO 8601 UTC offset.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor<TDocument> Offset(string? value)
	{
		Instance.Offset = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor<TDocument> Params(System.Collections.Generic.IDictionary<string, object>? value)
	{
		Instance.Params = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor<TDocument> Params()
	{
		Instance.Params = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor<TDocument> Params(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject>? action)
	{
		Instance.Params = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor<TDocument> AddParam(string key, object value)
	{
		Instance.Params ??= new System.Collections.Generic.Dictionary<string, object>();
		Instance.Params.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.Script = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor<TDocument> Script()
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor<TDocument> Script(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Time zone ID.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor<TDocument> TimeZone(string? value)
	{
		Instance.TimeZone = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregation Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct AutoDateHistogramAggregationDescriptor
{
	internal Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AutoDateHistogramAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AutoDateHistogramAggregationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregation instance) => new Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregation(Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The target number of buckets.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor Buckets(int? value)
	{
		Instance.Buckets = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field on which to run the aggregation.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor Field(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field on which to run the aggregation.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The date format used to format <c>key_as_string</c> in the response.
	/// If no <c>format</c> is specified, the first date format specified in the field mapping is used.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor Format(string? value)
	{
		Instance.Format = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The minimum rounding interval.
	/// This can make the collection process more efficient, as the aggregation will not attempt to round at any interval lower than <c>minimum_interval</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor MinimumInterval(Elastic.Clients.Elasticsearch.Aggregations.MinimumInterval? value)
	{
		Instance.MinimumInterval = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The value to apply to documents that do not have a value.
	/// By default, documents without a value are ignored.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor Missing(System.DateTime? value)
	{
		Instance.Missing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Time zone specified as a ISO 8601 UTC offset.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor Offset(string? value)
	{
		Instance.Offset = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor Params(System.Collections.Generic.IDictionary<string, object>? value)
	{
		Instance.Params = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor Params()
	{
		Instance.Params = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor Params(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject>? action)
	{
		Instance.Params = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor AddParam(string key, object value)
	{
		Instance.Params ??= new System.Collections.Generic.Dictionary<string, object>();
		Instance.Params.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor Script(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.Script = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor Script()
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor Script(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Time zone ID.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor TimeZone(string? value)
	{
		Instance.TimeZone = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregation Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregationDescriptor(new Elastic.Clients.Elasticsearch.Aggregations.AutoDateHistogramAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}