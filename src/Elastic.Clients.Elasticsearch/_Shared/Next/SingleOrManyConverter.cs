// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal sealed class SingleOrManyMarker<TCollection, TElement>
	where TCollection : class, IEnumerable<TElement>
{
	static SingleOrManyMarker()
	{
		DynamicallyAccessed.PublicConstructors(typeof(SingleOrManyMarkerConverter<TCollection, TElement>));
	}
}

internal sealed class SingleOrManyMarkerConverter<TCollection, TElement> :
	JsonConverter<SingleOrManyMarker<TCollection, TElement>>,
	IMarkerTypeConverter
	where TCollection : class, IEnumerable<TElement>
{
	public JsonConverter WrappedConverter { get; }

	public SingleOrManyMarkerConverter()
	{
		WrappedConverter = new SingleOrManyConverter<TCollection, TElement>();
	}

	public override SingleOrManyMarker<TCollection, TElement>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		throw new NotImplementedException();
	}

	public override void Write(Utf8JsonWriter writer, SingleOrManyMarker<TCollection, TElement> value, JsonSerializerOptions options)
	{
		throw new NotImplementedException();
	}
}

internal sealed class SingleOrManyMarkerConverterFactory :
	JsonConverterFactory
{
	public override bool CanConvert(Type typeToConvert)
	{
		return typeToConvert.IsGenericType &&
			   typeToConvert.GetGenericTypeDefinition() == typeof(SingleOrManyMarker<,>);
	}

	public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		var args = typeToConvert.GetGenericArguments();

#pragma warning disable IL3050 // SingleOrManyMarker<,> static constructor roots SingleOrManyMarkerConverter<,>.

		var converter = (JsonConverter)Activator.CreateInstance(
			typeof(SingleOrManyMarkerConverter<,>).MakeGenericType(args[0], args[1]),
			BindingFlags.Instance | BindingFlags.Public,
			binder: null,
			args: null,
			culture: null)!;

#pragma warning restore IL3050

		return converter;
	}
}

// TODO: For this code to work well with AOT, we have to make sure that `JsonSerializable` attributes
//       for `TCollection`, `TElement` (and potentially `List<TElement>`) are generated.

internal sealed class SingleOrManyConverter<TCollection, TElement> :
	JsonConverter<TCollection>
	where TCollection : class, IEnumerable<TElement>
{
	public override TCollection? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType is JsonTokenType.StartArray)
		{
			return reader.ReadValue<TCollection>(options); // TODO: List<TElement>
		}

		return new[] { reader.ReadValue<TElement>(options)! } as TCollection;
	}

	public override void Write(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case ICollection<TElement> { Count: 1 }:
				writer.WriteValue(options, value.First());
				return;

			case ICollection<TElement>:
				writer.WriteValue(options, value);
				return;

			case IReadOnlyCollection<TElement> { Count: 1 }:
				writer.WriteValue(options, value.First());
				return;

			case IReadOnlyCollection<TElement>:
				writer.WriteValue(options, value);
				return;
		}

		var collection = value.ToArray();
		if (collection.Length is 1)
		{
			writer.WriteValue(options, value.First());
			return;
		}

		writer.WriteValue(options, value);
	}
}
