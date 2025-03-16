// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch;

[JsonConverter(typeof(FieldValuesConverter))]
public sealed class FieldValues : IsADictionary<string, LazyJson>
{
	public static readonly FieldValues Empty = new();

	private static readonly HashSet<Type> NumericTypes = new()
	{
		typeof(int), typeof(double), typeof(decimal),
		typeof(long), typeof(short), typeof(sbyte),
		typeof(byte), typeof(ulong), typeof(ushort),
		typeof(uint), typeof(float)
	};

	private readonly Inferrer _inferrer;

	private FieldValues() { }

	internal FieldValues(Inferrer inferrer, IDictionary<string, LazyJson> container)
		: base(container) => _inferrer = inferrer;

	public TValue Value<TValue>(Field field)
	{
		var values = Values<TValue>(field);
		return values != null
			? values.FirstOrDefault()
			: default;
	}

	public TValue ValueOf<T, TValue>(Expression<Func<T, TValue>> objectPath)
	{
		var values = ValuesOf(objectPath);
		return values != null
			? values.FirstOrDefault()
			: default;
	}

	public TValue[] Values<TValue>(Field field)
	{
		if (_inferrer == null)
			return Array.Empty<TValue>();

		var path = _inferrer.Field(field);
		return FieldArray<TValue>(path);
	}

	public TValue[] ValuesOf<T, TValue>(Expression<Func<T, TValue>> objectPath)
	{
		if (_inferrer == null)
			return Array.Empty<TValue>();

		var field = _inferrer.Field(objectPath);
		return FieldArray<TValue>(field);
	}

	public static bool IsNumeric(Type myType) => NumericTypes.Contains(Nullable.GetUnderlyingType(myType) ?? myType);

	public static bool IsNullable(Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

	private TValue[] FieldArray<TValue>(string field)
	{
		//unknown field
		if (BackingDictionary == null || !BackingDictionary.TryGetValue(field, out var o))
			return null;

		//numerics are always returned as doubles by Elasticsearch.
		if (!IsNumeric(typeof(TValue)))
			return o.As<TValue[]>();

		//here we support casting to the desired numeric type whether its nullable or not.
		if (!IsNullable(typeof(TValue)))
			return o.As<double[]>().Select(d => (TValue)Convert.ChangeType(d, typeof(TValue))).ToArray();

		var underlyingType = Nullable.GetUnderlyingType(typeof(TValue));
		return o.As<double?[]>()
			.Select(d =>
			{
				if (d == null)
					return default;

				return (TValue)Convert.ChangeType(d, underlyingType);
			})
			.ToArray();
	}
}

internal sealed class FieldValuesConverter : JsonConverter<FieldValues>
{
	private IElasticsearchClientSettings _settings;

	public override FieldValues? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		InitializeSettings(options);

		if (reader.TokenType != JsonTokenType.StartObject)
		{
			return null;
		}

		var fields = new Dictionary<string, LazyJson>();

		while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
		{
			if (reader.TokenType != JsonTokenType.PropertyName)
				throw new JsonException($"Unexpected token. Expected {JsonTokenType.PropertyName}, but read {reader.TokenType}");

			var propertyName = reader.GetString();
			reader.Read();
			var lazyDocument = JsonSerializer.Deserialize<LazyJson>(ref reader, options);
			fields[propertyName] = lazyDocument;
		}

		return new FieldValues(_settings.Inferrer, fields);
	}

	public override void Write(Utf8JsonWriter writer, FieldValues value, JsonSerializerOptions options) => throw new NotImplementedException();

	private void InitializeSettings(JsonSerializerOptions options)
	{
		if (_settings is null)
		{
			if (!options.TryGetClientSettings(out var settings))
				ThrowHelper.ThrowJsonExceptionForMissingSettings();

			_settings = settings;
		}
	}
}
