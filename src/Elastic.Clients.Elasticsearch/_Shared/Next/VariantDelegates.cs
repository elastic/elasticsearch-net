// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal delegate T? VariantReader<out T>(ref Utf8JsonReader reader, JsonSerializerOptions options);

internal delegate void VariantWriter<in T>(Utf8JsonWriter writer, T value, JsonSerializerOptions options);

internal readonly struct VariantWriterRegistration<T>
{
	public VariantWriterRegistration(string discriminator, VariantWriter<T> writer)
	{
		Discriminator = discriminator;
		_writer = writer;
	}

	private readonly VariantWriter<T> _writer;

	public string Discriminator { get; }

	public void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) =>
		_writer(writer, value, options);
}
