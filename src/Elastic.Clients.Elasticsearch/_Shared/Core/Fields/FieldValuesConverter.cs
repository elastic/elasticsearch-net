// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

using System.Text.Json.Serialization;
using System.Text.Json;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch;

internal sealed class FieldValuesConverter :
	JsonConverter<FieldValues>
{
	public override FieldValues Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var values = reader.ReadCollectionValue<FieldValue>(options, null)!;

		return new(values);
	}

	public override void Write(Utf8JsonWriter writer, FieldValues value, JsonSerializerOptions options)
	{
		writer.WriteCollectionValue(options, value.Values, null);
	}
}

internal sealed class SingleOrManyFieldValuesMarker;

internal sealed class SingleOrManyFieldValuesMarkerConverter :
	JsonConverter<SingleOrManyFieldValuesMarker>,
	IMarkerTypeConverter
{
	public JsonConverter WrappedConverter { get; }

	public SingleOrManyFieldValuesMarkerConverter()
	{
		WrappedConverter = new SingleOrManyFieldValuesConverter();
	}

	public override SingleOrManyFieldValuesMarker Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}

	public override void Write(Utf8JsonWriter writer, SingleOrManyFieldValuesMarker value, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}
}

internal sealed class SingleOrManyFieldValuesConverter :
	JsonConverter<FieldValues>
{
	public override FieldValues Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var values = reader.ReadSingleOrManyCollectionValue<FieldValue>(options, null)!;

		return new(values);
	}

	public override void Write(Utf8JsonWriter writer, FieldValues value, JsonSerializerOptions options)
	{
		writer.WriteSingleOrManyCollectionValue(options, value.Values, null);
	}
}
