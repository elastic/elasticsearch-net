// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal sealed class NumericAliasConverter<T> : JsonConverter<T>
{
	public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var value = reader.GetInt64();

		var instance = (T)Activator.CreateInstance(
			typeof(T),
			BindingFlags.Instance | BindingFlags.Public,
			args: new object[] {value},
			binder: null,
			culture: null)!;

		return instance;
	}

	public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) =>
		throw new NotImplementedException();
}
