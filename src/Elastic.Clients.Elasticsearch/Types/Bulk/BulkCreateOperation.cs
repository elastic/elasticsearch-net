// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Elastic.Clients.Elasticsearch
{
	/// <summary>
	/// Represents a bulk operation to create a document.
	/// </summary>
	/// <typeparam name="T">The type representing the document being created.</typeparam>
	public sealed class BulkCreateOperation<T> : BulkOperationBase
	{
		/// <summary>
		/// Creates an instance of <see cref="BulkCreateOperation{T}"/> with the provided <typeparamref name="T"/> document serialized
		/// as source data.
		/// </summary>
		/// <param name="document">The <typeparamref name="T"/> document to index.</param>
		public BulkCreateOperation(T document) => Document = document;

		/// <summary>
		/// ID of the pipeline to use to preprocess incoming documents.
		/// </summary>
		[JsonPropertyName("pipeline")]
		public string? Pipeline { get; set; }

		/// <summary>
		/// A map from the full name of fields to the name of dynamic templates. Defaults to an empty map.
		/// If a name matches a dynamic template, then that template will be applied regardless of other match predicates
		/// defined in the template. And if a field is already defined in the mapping, then this parameter won’t be used.
		/// </summary>
		[JsonPropertyName("dynamic_templates")]
		public Dictionary<string, string>? DynamicTemplates { get; set; }

		/// <summary>
		/// The source document.
		/// </summary>
		[JsonIgnore]
		public T Document { get; set; }

		/// <inheritdoc />
		protected override void Serialize(Stream stream, IElasticsearchClientSettings settings)
		{
			SetValues(settings);

			using var writer = new Utf8JsonWriter(stream);
			SerializeOperationAction(settings, writer);
			writer.Flush();
			stream.WriteByte(SerializationConstants.Newline);
			settings.SourceSerializer.Serialize(Document, stream);
		}

		/// <inheritdoc />
		protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings)
		{
			SetValues(settings);

			await using var writer = new Utf8JsonWriter(stream);
			SerializeOperationAction(settings, writer);
			await writer.FlushAsync().ConfigureAwait(false);
			stream.WriteByte(SerializationConstants.Newline);
			await settings.SourceSerializer.SerializeAsync(Document, stream).ConfigureAwait(false);
		}

		private void SetValues(IElasticsearchClientSettings settings)
		{
			if (Routing is null)
			{
				var routing = new Routing(Document);
				if (!string.IsNullOrEmpty(routing.GetString(settings)))
					Routing = routing;
			}

			Id ??= new Id(Document);
			Index ??= typeof(T);
		}

		private void SerializeOperationAction(IElasticsearchClientSettings settings, Utf8JsonWriter writer)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			writer.WriteStartObject();
			writer.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkCreateOperation<T>>(writer, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkCreateOperation<T>>(writer, this); // Unable to handle options if this were to ever be the case
			}

			writer.WriteEndObject();
		}

		/// <inheritdoc />
		protected override string Operation => "create";
	}
}
