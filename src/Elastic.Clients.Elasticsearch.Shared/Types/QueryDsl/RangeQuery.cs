// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

#if ELASTICSEARCH_SERVERLESS
using Elastic.Clients.Elasticsearch.Serverless.Fluent;
#else
using Elastic.Clients.Elasticsearch.Fluent;
#endif

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.QueryDsl;
#else
namespace Elastic.Clients.Elasticsearch.QueryDsl;
#endif

// TODO: This should be removed after implementing descriptor generation for union types

public sealed partial class QueryDescriptor<TDocument>
{
	public QueryDescriptor<TDocument> Range(Action<RangeQueryDescriptor<TDocument>> configure) => ProxiedSet(configure, "range");

	private QueryDescriptor<TDocument> ProxiedSet<T>(Action<T> descriptorAction, string variantName) where T : ProxiedDescriptor<T>
	{
		var descriptor = (T)Activator.CreateInstance(typeof(T), true);
		descriptorAction?.Invoke(descriptor);

		return Set(descriptor.Result, variantName);
	}
}

public sealed partial class QueryDescriptor
{
	public QueryDescriptor Range(Action<RangeQueryDescriptor> configure) => ProxiedSet(configure, "range");
	public QueryDescriptor Range<TDocument>(Action<RangeQueryDescriptor<TDocument>> configure) => ProxiedSet(configure, "range");

	private QueryDescriptor ProxiedSet<T>(Action<T> descriptorAction, string variantName) where T : ProxiedDescriptor<T>
	{
		var descriptor = (T)Activator.CreateInstance(typeof(T), true);
		descriptorAction?.Invoke(descriptor);

		return Set(descriptor.Result, variantName);
	}
}

public abstract class ProxiedDescriptor<T> : Descriptor<T>
	where T : Descriptor<T>
{
	internal Descriptor Result { get; set; }

	protected T SetResult<TD>(Action<TD> descriptorAction) where TD : Descriptor
	{
		var descriptor = (TD)Activator.CreateInstance(typeof(TD), true);
		descriptorAction?.Invoke(descriptor);
		Result = descriptor;
		return Self;
	}
}

public sealed class RangeQueryDescriptor<TDocument> : ProxiedDescriptor<RangeQueryDescriptor<TDocument>>
{
	public RangeQueryDescriptor<TDocument> NumberRange(Action<NumberRangeQueryDescriptor<TDocument>> configure) =>
		SetResult(configure);

	public RangeQueryDescriptor<TDocument> DateRange(Action<DateRangeQueryDescriptor<TDocument>> configure) =>
		SetResult(configure);
}

public sealed class RangeQueryDescriptor : ProxiedDescriptor<RangeQueryDescriptor>
{
	public RangeQueryDescriptor NumberRange(Action<NumberRangeQueryDescriptor> configure) => SetResult(configure);

	public RangeQueryDescriptor NumberRange<TDocument>(Action<NumberRangeQueryDescriptor<TDocument>> configure) => SetResult(configure);

	public RangeQueryDescriptor DateRange(Action<DateRangeQueryDescriptor> configure) => SetResult(configure);

	public RangeQueryDescriptor DateRange<TDocument>(Action<DateRangeQueryDescriptor<TDocument>> configure) => SetResult(configure);
}

public sealed partial class NumberRangeQuery
{
	public static implicit operator Query(NumberRangeQuery numberRangeQuery) => Query.Range(new RangeQuery(numberRangeQuery));
}

public sealed partial class DateRangeQuery
{
	public static implicit operator Query(DateRangeQuery dateRangeQuery) => Query.Range(new RangeQuery(dateRangeQuery));
}

[JsonConverter(typeof(RangeQueryConverter))]
public sealed class RangeQuery
{
	public enum RangeQueryKind
	{
		Date,
		Number,
		Terms
	}

	public RangeQueryKind Kind { get; }
	public object Value { get; }

	private RangeQuery(RangeQueryKind kind, object value)
	{
		Kind = kind;
		Value = value;
	}

	public RangeQuery(DateRangeQuery date) : this(RangeQueryKind.Date, date)
	{
	}

	public static RangeQuery Date(DateRangeQuery date) => new(date);

	public bool IsDate => Kind is RangeQueryKind.Date;

	public bool TryGetDate([NotNullWhen(true)] out DateRangeQuery? date)
	{
		date = null;
		if (Kind is RangeQueryKind.Date)
		{
			date = (DateRangeQuery)Value;
			return true;
		}

		return false;
	}

	public static implicit operator RangeQuery(DateRangeQuery date) => Date(date);

	public RangeQuery(NumberRangeQuery number) : this(RangeQueryKind.Number, number)
	{
	}

	public static RangeQuery Number(NumberRangeQuery number) => new(number);

	public bool IsNumber => Kind is RangeQueryKind.Number;

	public bool TryGetNumber([NotNullWhen(true)] out NumberRangeQuery? number)
	{
		number = null;
		if (Kind is RangeQueryKind.Number)
		{
			number = (NumberRangeQuery)Value;
			return true;
		}

		return false;
	}

	public static implicit operator RangeQuery(NumberRangeQuery number) => Number(number);

	public RangeQuery(TermsRangeQuery terms) : this(RangeQueryKind.Terms, terms)
	{
	}

	public static RangeQuery Terms(TermsRangeQuery terms) => new(terms);

	public bool IsTerms => Kind is RangeQueryKind.Terms;

	public bool TryGetTerms([NotNullWhen(true)] out TermsRangeQuery? terms)
	{
		terms = null;
		if (Kind is RangeQueryKind.Terms)
		{
			terms = (TermsRangeQuery)Value;
			return true;
		}

		return false;
	}

	public static implicit operator RangeQuery(TermsRangeQuery terms) => Terms(terms);
}

internal sealed class RangeQueryConverter : JsonConverter<RangeQuery>
{
	public override RangeQuery? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var readerCopy = reader;

		if (readerCopy.TokenType != JsonTokenType.StartObject)
			throw new JsonException($"Unexpected token.");

		readerCopy.Read(); // Read past the opening token
		readerCopy.Read(); // Read past the field name

		using var jsonDoc = JsonDocument.ParseValue(ref readerCopy);

		// When either of these properties are present, we know we have a date range query
		if (jsonDoc.RootElement.TryGetProperty("format", out _) || jsonDoc.RootElement.TryGetProperty("time_zone", out _))
		{
			return JsonSerializer.Deserialize<DateRangeQuery>(ref reader, options);
		}

		JsonElement? rangeElement = null;

		if (jsonDoc.RootElement.TryGetProperty("gte", out var gte))
		{
			rangeElement = gte;
		}
		else if (jsonDoc.RootElement.TryGetProperty("gt", out var gt))
		{
			rangeElement = gt;
		}
		else if (jsonDoc.RootElement.TryGetProperty("lte", out var lte))
		{
			rangeElement = lte;
		}
		else if (jsonDoc.RootElement.TryGetProperty("lt", out var lt))
		{
			rangeElement = lt;
		}

		if (!rangeElement.HasValue)
		{
			throw new JsonException("Unable to determine type of range query.");
		}

		return rangeElement.Value.ValueKind switch
		{
			JsonValueKind.String when DateMath.IsValidDateMathString(rangeElement.Value.GetString()!) =>
				JsonSerializer.Deserialize<DateRangeQuery>(ref reader, options),
			JsonValueKind.String => JsonSerializer.Deserialize<TermsRangeQuery>(ref reader, options),
			JsonValueKind.Number => JsonSerializer.Deserialize<NumberRangeQuery>(ref reader, options),
			_ => throw new JsonException("Unsupported range query type.")
		};
	}

	public override void Write(Utf8JsonWriter writer, RangeQuery value, JsonSerializerOptions options) =>
		JsonSerializer.Serialize(writer, value.Value, value.Value.GetType(), options);
}
