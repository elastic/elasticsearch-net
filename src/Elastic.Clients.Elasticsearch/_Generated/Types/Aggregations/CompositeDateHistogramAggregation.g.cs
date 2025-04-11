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

internal sealed partial class CompositeDateHistogramAggregationConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregation>
{
	private static readonly System.Text.Json.JsonEncodedText PropCalendarInterval = System.Text.Json.JsonEncodedText.Encode("calendar_interval");
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropFixedInterval = System.Text.Json.JsonEncodedText.Encode("fixed_interval");
	private static readonly System.Text.Json.JsonEncodedText PropFormat = System.Text.Json.JsonEncodedText.Encode("format");
	private static readonly System.Text.Json.JsonEncodedText PropMissingBucket = System.Text.Json.JsonEncodedText.Encode("missing_bucket");
	private static readonly System.Text.Json.JsonEncodedText PropMissingOrder = System.Text.Json.JsonEncodedText.Encode("missing_order");
	private static readonly System.Text.Json.JsonEncodedText PropOffset = System.Text.Json.JsonEncodedText.Encode("offset");
	private static readonly System.Text.Json.JsonEncodedText PropOrder = System.Text.Json.JsonEncodedText.Encode("order");
	private static readonly System.Text.Json.JsonEncodedText PropScript = System.Text.Json.JsonEncodedText.Encode("script");
	private static readonly System.Text.Json.JsonEncodedText PropTimeZone = System.Text.Json.JsonEncodedText.Encode("time_zone");
	private static readonly System.Text.Json.JsonEncodedText PropValueType = System.Text.Json.JsonEncodedText.Encode("value_type");

	public override Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregation Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propCalendarInterval = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field?> propField = default;
		LocalJsonValue<string?> propFixedInterval = default;
		LocalJsonValue<string?> propFormat = default;
		LocalJsonValue<bool?> propMissingBucket = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Aggregations.MissingOrder?> propMissingOrder = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propOffset = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.SortOrder?> propOrder = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Script?> propScript = default;
		LocalJsonValue<string?> propTimeZone = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Aggregations.ValueType?> propValueType = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCalendarInterval.TryReadProperty(ref reader, options, PropCalendarInterval, null))
			{
				continue;
			}

			if (propField.TryReadProperty(ref reader, options, PropField, null))
			{
				continue;
			}

			if (propFixedInterval.TryReadProperty(ref reader, options, PropFixedInterval, null))
			{
				continue;
			}

			if (propFormat.TryReadProperty(ref reader, options, PropFormat, null))
			{
				continue;
			}

			if (propMissingBucket.TryReadProperty(ref reader, options, PropMissingBucket, null))
			{
				continue;
			}

			if (propMissingOrder.TryReadProperty(ref reader, options, PropMissingOrder, null))
			{
				continue;
			}

			if (propOffset.TryReadProperty(ref reader, options, PropOffset, null))
			{
				continue;
			}

			if (propOrder.TryReadProperty(ref reader, options, PropOrder, null))
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

			if (propValueType.TryReadProperty(ref reader, options, PropValueType, null))
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
		return new Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			CalendarInterval = propCalendarInterval.Value,
			Field = propField.Value,
			FixedInterval = propFixedInterval.Value,
			Format = propFormat.Value,
			MissingBucket = propMissingBucket.Value,
			MissingOrder = propMissingOrder.Value,
			Offset = propOffset.Value,
			Order = propOrder.Value,
			Script = propScript.Value,
			TimeZone = propTimeZone.Value,
			ValueType = propValueType.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregation value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCalendarInterval, value.CalendarInterval, null, null);
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropFixedInterval, value.FixedInterval, null, null);
		writer.WriteProperty(options, PropFormat, value.Format, null, null);
		writer.WriteProperty(options, PropMissingBucket, value.MissingBucket, null, null);
		writer.WriteProperty(options, PropMissingOrder, value.MissingOrder, null, null);
		writer.WriteProperty(options, PropOffset, value.Offset, null, null);
		writer.WriteProperty(options, PropOrder, value.Order, null, null);
		writer.WriteProperty(options, PropScript, value.Script, null, null);
		writer.WriteProperty(options, PropTimeZone, value.TimeZone, null, null);
		writer.WriteProperty(options, PropValueType, value.ValueType, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationConverter))]
public sealed partial class CompositeDateHistogramAggregation
{
#if NET7_0_OR_GREATER
	public CompositeDateHistogramAggregation()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public CompositeDateHistogramAggregation()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal CompositeDateHistogramAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Either <c>calendar_interval</c> or <c>fixed_interval</c> must be present
	/// </para>
	/// </summary>
	public string? CalendarInterval { get; set; }

	/// <summary>
	/// <para>
	/// Either <c>field</c> or <c>script</c> must be present
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Field? Field { get; set; }

	/// <summary>
	/// <para>
	/// Either <c>calendar_interval</c> or <c>fixed_interval</c> must be present
	/// </para>
	/// </summary>
	public string? FixedInterval { get; set; }
	public string? Format { get; set; }
	public bool? MissingBucket { get; set; }
	public Elastic.Clients.Elasticsearch.Aggregations.MissingOrder? MissingOrder { get; set; }
	public Elastic.Clients.Elasticsearch.Duration? Offset { get; set; }
	public Elastic.Clients.Elasticsearch.SortOrder? Order { get; set; }

	/// <summary>
	/// <para>
	/// Either <c>field</c> or <c>script</c> must be present
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Script? Script { get; set; }
	public string? TimeZone { get; set; }
	public Elastic.Clients.Elasticsearch.Aggregations.ValueType? ValueType { get; set; }
}

public readonly partial struct CompositeDateHistogramAggregationDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CompositeDateHistogramAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CompositeDateHistogramAggregationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregation instance) => new Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregation(Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Either <c>calendar_interval</c> or <c>fixed_interval</c> must be present
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor<TDocument> CalendarInterval(string? value)
	{
		Instance.CalendarInterval = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Either <c>field</c> or <c>script</c> must be present
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Either <c>field</c> or <c>script</c> must be present
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Either <c>calendar_interval</c> or <c>fixed_interval</c> must be present
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor<TDocument> FixedInterval(string? value)
	{
		Instance.FixedInterval = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor<TDocument> Format(string? value)
	{
		Instance.Format = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor<TDocument> MissingBucket(bool? value = true)
	{
		Instance.MissingBucket = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor<TDocument> MissingOrder(Elastic.Clients.Elasticsearch.Aggregations.MissingOrder? value)
	{
		Instance.MissingOrder = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor<TDocument> Offset(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Offset = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor<TDocument> Order(Elastic.Clients.Elasticsearch.SortOrder? value)
	{
		Instance.Order = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Either <c>field</c> or <c>script</c> must be present
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.Script = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Either <c>field</c> or <c>script</c> must be present
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor<TDocument> Script()
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Either <c>field</c> or <c>script</c> must be present
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor<TDocument> Script(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor<TDocument> TimeZone(string? value)
	{
		Instance.TimeZone = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor<TDocument> ValueType(Elastic.Clients.Elasticsearch.Aggregations.ValueType? value)
	{
		Instance.ValueType = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregation Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct CompositeDateHistogramAggregationDescriptor
{
	internal Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregation Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CompositeDateHistogramAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregation instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CompositeDateHistogramAggregationDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor(Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregation instance) => new Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregation(Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Either <c>calendar_interval</c> or <c>fixed_interval</c> must be present
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor CalendarInterval(string? value)
	{
		Instance.CalendarInterval = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Either <c>field</c> or <c>script</c> must be present
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor Field(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Either <c>field</c> or <c>script</c> must be present
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Either <c>calendar_interval</c> or <c>fixed_interval</c> must be present
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor FixedInterval(string? value)
	{
		Instance.FixedInterval = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor Format(string? value)
	{
		Instance.Format = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor MissingBucket(bool? value = true)
	{
		Instance.MissingBucket = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor MissingOrder(Elastic.Clients.Elasticsearch.Aggregations.MissingOrder? value)
	{
		Instance.MissingOrder = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor Offset(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Offset = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor Order(Elastic.Clients.Elasticsearch.SortOrder? value)
	{
		Instance.Order = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Either <c>field</c> or <c>script</c> must be present
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor Script(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.Script = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Either <c>field</c> or <c>script</c> must be present
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor Script()
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Either <c>field</c> or <c>script</c> must be present
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor Script(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor TimeZone(string? value)
	{
		Instance.TimeZone = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor ValueType(Elastic.Clients.Elasticsearch.Aggregations.ValueType? value)
	{
		Instance.ValueType = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregation Build(System.Action<Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregationDescriptor(new Elastic.Clients.Elasticsearch.Aggregations.CompositeDateHistogramAggregation(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}