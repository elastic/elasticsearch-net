// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.Core.Get;
using Elastic.Clients.Elasticsearch.Core.MGet;

namespace Elastic.Clients.Elasticsearch.Serialization;

/// <summary>
/// A converter factory able to provide a converter to handle (de)serializing <see cref="MultiGetResponseItem{TDocument}"/>.
/// </summary>
internal sealed class ResponseItemConverterFactory : JsonConverterFactory
{
	public override bool CanConvert(Type typeToConvert) => typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(MultiGetResponseItem<>);

	public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		var documentType = typeToConvert.GetGenericArguments()[0];

		return (JsonConverter)Activator.CreateInstance(
			typeof(ResponseItemConverter<>).MakeGenericType(documentType));
	}

	private sealed class ResponseItemConverter<TDocument> : JsonConverter<MultiGetResponseItem<TDocument>>
	{
		public override MultiGetResponseItem<TDocument>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			const string exceptionMessage = "Unable to deserialize union.";
			var readerCopy = reader;

			Exception getResultException = null;
			Exception errorException = null;

			// TODO - Review and optimise performance, possibly read-ahead to check for the error property and then deserialise
			// accordingly is better?

			try
			{
				var result = JsonSerializer.Deserialize<MultiGetError>(ref reader, options);

				if (result is not null && result.Error is not null)
				{
					return new MultiGetResponseItem<TDocument>(result);
				}
			}
			catch (Exception ex)
			{
				errorException = ex;
			}

			try
			{
				var result = JsonSerializer.Deserialize<GetResult<TDocument>>(ref readerCopy, options);

				// If we have a version number, we can be sure this isn't an error
				if (result is not null)
				{
					reader = readerCopy; // Ensure we swap the reader to reflect the data we have consumed.
					return new MultiGetResponseItem<TDocument>(result);
				}
			}
			catch (Exception ex)
			{
				getResultException = ex;
			}

			Exception innerException = null;

			if (errorException is not null && getResultException is not null)
			{
				innerException = new AggregateException(errorException, getResultException);
			}
			else if (errorException is not null)
			{
				innerException = errorException;
			}
			else if (getResultException is not null)
			{
				innerException = getResultException;
			}

			if (innerException is not null)
			{
				throw new JsonException(exceptionMessage, innerException);
			}

			throw new JsonException(exceptionMessage);
		}

		// Not implemented as this type is read-only on responses.
		public override void Write(Utf8JsonWriter writer, MultiGetResponseItem<TDocument> value, JsonSerializerOptions options) =>
			throw new NotImplementedException("We never expect to serialize an instance of MultiGetResponseItem<TDocument> as its a read-only response type.");
	}
}
