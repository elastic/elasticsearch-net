// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.Fluent;

namespace Elastic.Clients.Elasticsearch.QueryDsl;

public partial class Query
{
	public bool TryGet<T>([NotNullWhen(true)]out T? query)
	{
		query = default(T);

		if (Variant is T variant)
		{
			query = variant;
			return true;
		}

		return false;
	}
}

[JsonConverter(typeof(RangeQueryConverter))]
public class RangeQuery : SearchQuery
{
	internal RangeQuery() { }
}

internal sealed class RangeQueryConverter : JsonConverter<RangeQuery>
{
	public override RangeQuery? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var readerCopy = reader;

		if (readerCopy.TokenType != JsonTokenType.StartObject)
			ThrowHelper.ThrowJsonException($"Unexpected JSON detected. Expected {JsonTokenType.StartObject} but read {readerCopy.TokenType}.");

		readerCopy.Read(); // Read past the opening token
		readerCopy.Read(); // Read past the field name

		using var jsonDoc = JsonDocument.ParseValue(ref readerCopy);

		if (jsonDoc is null)
			return null;

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
		else if(jsonDoc.RootElement.TryGetProperty("lte", out var lte))
		{
			rangeElement = lte;
		}
		else if(jsonDoc.RootElement.TryGetProperty("lt", out var lt))
		{
			rangeElement = lt;
		}

		if (!rangeElement.HasValue)
		{
			ThrowHelper.ThrowJsonException("Unable to determine type of range query.");
		}

		switch (rangeElement.Value.ValueKind)
		{
			case JsonValueKind.String:
				return JsonSerializer.Deserialize<DateRangeQuery>(ref reader, options);
			case JsonValueKind.Number:
				return JsonSerializer.Deserialize<NumberRangeQuery>(ref reader, options);
		}

		ThrowHelper.ThrowJsonException("Unable to deserialize range query.");

		// We never reach here. I wish the flow analysis could infer that this isn't needed with the help of the DoesNotReturn attributes.
		return null;
	}

	public override void Write(Utf8JsonWriter writer, RangeQuery value, JsonSerializerOptions options) =>
		JsonSerializer.Serialize(writer, value, value.GetType(), options);
}


public sealed class RangeQueryDescriptor<TDocument> : SerializableDescriptor<RangeQueryDescriptor<TDocument>>
{
	private NumberRangeQueryDescriptor<TDocument> _numberRangeQueryDescriptor;
	private DateRangeQueryDescriptor<TDocument> _dateRangeQueryDescriptor;

	private Action<NumberRangeQueryDescriptor<TDocument>> _numberRangeQueryDescriptorAction;
	private Action<DateRangeQueryDescriptor<TDocument>> _dateRangeQueryDescriptorAction;

	public RangeQueryDescriptor<TDocument> DateRange(Action<DateRangeQueryDescriptor<TDocument>> configure)
	{
		_dateRangeQueryDescriptor = null;
		_dateRangeQueryDescriptorAction = configure;
		_numberRangeQueryDescriptor = null;
		_numberRangeQueryDescriptorAction = null;

		return Self;
	}

	public RangeQueryDescriptor<TDocument> NumberRange(Action<NumberRangeQueryDescriptor<TDocument>> configure)
	{
		_dateRangeQueryDescriptor = null;
		_dateRangeQueryDescriptorAction = null;
		_numberRangeQueryDescriptor = null;
		_numberRangeQueryDescriptorAction = configure;

		return Self;
	}

	public RangeQueryDescriptor<TDocument> DateRange(DateRangeQueryDescriptor<TDocument> descriptor)
	{
		_dateRangeQueryDescriptor = descriptor;
		_dateRangeQueryDescriptorAction = null;
		_numberRangeQueryDescriptor = null;
		_numberRangeQueryDescriptorAction = null;

		return Self;
	}

	public RangeQueryDescriptor<TDocument> NumberRange(NumberRangeQueryDescriptor<TDocument> descriptor)
	{
		_dateRangeQueryDescriptor = null;
		_dateRangeQueryDescriptorAction = null;
		_numberRangeQueryDescriptor = descriptor;
		_numberRangeQueryDescriptorAction = null;

		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		if (_dateRangeQueryDescriptor is not null)
		{
			JsonSerializer.Serialize(writer, _dateRangeQueryDescriptor, options);
		}
		else if (_dateRangeQueryDescriptorAction is not null)
		{
			JsonSerializer.Serialize(writer, new DateRangeQueryDescriptor<TDocument>(_dateRangeQueryDescriptorAction), options);
		}
		else if (_numberRangeQueryDescriptor is not null)
		{
			JsonSerializer.Serialize(writer, _numberRangeQueryDescriptor, options);
		}
		else if (_numberRangeQueryDescriptorAction is not null)
		{
			JsonSerializer.Serialize(writer, new NumberRangeQueryDescriptor<TDocument>(_numberRangeQueryDescriptorAction), options);
		}
	}
}

public sealed class RangeQueryDescriptor : SerializableDescriptor<RangeQueryDescriptor>
{
	private NumberRangeQueryDescriptor _numberRangeQueryDescriptor;
	private DateRangeQueryDescriptor _dateRangeQueryDescriptor;

	private Action<NumberRangeQueryDescriptor> _numberRangeQueryDescriptorAction;
	private Action<DateRangeQueryDescriptor> _dateRangeQueryDescriptorAction;

	public RangeQueryDescriptor DateRange(Action<DateRangeQueryDescriptor> configure)
	{
		_dateRangeQueryDescriptor = null;
		_dateRangeQueryDescriptorAction = configure;
		_numberRangeQueryDescriptor = null;
		_numberRangeQueryDescriptorAction = null;

		return Self;
	}

	public RangeQueryDescriptor NumberRange(Action<NumberRangeQueryDescriptor> configure)
	{
		_dateRangeQueryDescriptor = null;
		_dateRangeQueryDescriptorAction = null;
		_numberRangeQueryDescriptor = null;
		_numberRangeQueryDescriptorAction = configure;

		return Self;
	}

	public RangeQueryDescriptor DateRange(DateRangeQueryDescriptor descriptor)
	{
		_dateRangeQueryDescriptor = descriptor;
		_dateRangeQueryDescriptorAction = null;
		_numberRangeQueryDescriptor = null;
		_numberRangeQueryDescriptorAction = null;

		return Self;
	}

	public RangeQueryDescriptor NumberRange(NumberRangeQueryDescriptor descriptor)
	{
		_dateRangeQueryDescriptor = null;
		_dateRangeQueryDescriptorAction = null;
		_numberRangeQueryDescriptor = descriptor;
		_numberRangeQueryDescriptorAction = null;

		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		if (_dateRangeQueryDescriptor is not null)
		{
			JsonSerializer.Serialize(writer, _dateRangeQueryDescriptor, options);
		}
		else if (_dateRangeQueryDescriptorAction is not null)
		{
			JsonSerializer.Serialize(writer, new DateRangeQueryDescriptor(_dateRangeQueryDescriptorAction), options);
		}
		else if (_numberRangeQueryDescriptor is not null)
		{
			JsonSerializer.Serialize(writer, _numberRangeQueryDescriptor, options);
		}
		else if (_numberRangeQueryDescriptorAction is not null)
		{
			JsonSerializer.Serialize(writer, new NumberRangeQueryDescriptor(_numberRangeQueryDescriptorAction), options);
		}
	}
}

