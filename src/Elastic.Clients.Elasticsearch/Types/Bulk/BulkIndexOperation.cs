// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Elastic.Clients.Elasticsearch
{
	public sealed class BulkIndexOperation<T> : BulkOperationBase
	{
		private readonly bool _skipInference;
		private readonly bool _skipIndexNameInference;

		/// <summary>
		/// Creates an instance of <see cref="BulkCreateOperation{T}"/> with the provided <typeparamref name="T"/> document serialized
		/// as source data.
		/// </summary>
		/// <param name="document">The <typeparamref name="T"/> document to index.</param>
		public BulkIndexOperation(T document) => Document = document;

		/// <summary>
		/// Creates an instance of <see cref="BulkCreateOperation{T}"/> with the provided <typeparamref name="T"/> document serialized
		/// as source data and optional inference.
		/// </summary>
		/// <param name="document">The <typeparamref name="T"/> document to index.</param>
		/// <param name="skipInference">Whether to skip inference of the index, id and routing values from the <typeparamref name="T"/> document type.</param>
		public BulkIndexOperation(T document, bool skipInference) : this(document) => _skipInference = skipInference;

		/// <summary>
		/// Creates an instance of <see cref="BulkCreateOperation{T}"/> with the provided <typeparamref name="T"/> document serialized
		/// as source data.
		/// </summary>
		/// <remarks>When an <see cref="IndexName"/> is provided, even if the value is <c>null</c>, no further inference will occur.
		/// <para>Setting null using this overload is considered explicit intent not to set an index for the operation. In this case
		/// it is expected that the bulk request will have defined an index to operate on.</para>
		/// </remarks>
		/// <param name="document">The <typeparamref name="T"/> document to index.</param>
		/// <param name="index">The <see cref="IndexName"/> which can represent an index, alias or data stream.</param>
		public BulkIndexOperation(T document, IndexName index) : this(document)
		{
			Index = index;
			_skipIndexNameInference = true;
		}

		[JsonPropertyName("pipeline")]
		public string? Pipeline { get; set; }

		[JsonPropertyName("dynamic_templates")]
		public Dictionary<string, string>? DynamicTemplates { get; set; }

		[JsonIgnore]
		public T Document { get; set; }

		protected override string Operation => "index";

		private void SetValues(IElasticsearchClientSettings settings)
		{
			// This allocates but avoids serialising "routing":null etc. into the operation action
			// Unfortunately, the alternative is we always set to new Routing(Document) which is then
			// never null even if the inferrer will be unable to return a value for the Routing during serialization

			if (_skipInference)
				return;

			if (settings.ExperimentalEnableSerializeNullInferredValues)
			{
				Routing ??= new Routing(Document);
			}
			else if (Routing is null)
			{
				var routing = new Routing(Document);
				if (!string.IsNullOrEmpty(routing.GetString(settings)))
					Routing = routing;
			}

			if (settings.ExperimentalEnableSerializeNullInferredValues)
			{
				Id ??= new Id(Document);
			}
			else if (Id is null)
			{
				var id = new Id(Document);
				if (!string.IsNullOrEmpty(id.GetString(settings)))
					Id = id;
			}

			if (!_skipIndexNameInference)
				Index ??= typeof(T);
		}

		protected override void Serialize(Stream stream, IElasticsearchClientSettings settings)
		{
			SetValues(settings);
			var writer = new Utf8JsonWriter(stream);
			SerializeOperationAction(writer, settings);
			writer.Flush();
			stream.WriteByte(SerializationConstants.Newline);
			settings.SourceSerializer.Serialize(Document, stream);
		}

		protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings)
		{
			SetValues(settings);
			var internalWriter = new Utf8JsonWriter(stream);
			SerializeOperationAction(internalWriter, settings);
			await internalWriter.FlushAsync().ConfigureAwait(false);
			stream.WriteByte(SerializationConstants.Newline);
			await settings.SourceSerializer.SerializeAsync(Document, stream).ConfigureAwait(false);
		}

		private void SerializeOperationAction(Utf8JsonWriter writer, IElasticsearchClientSettings settings)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			writer.WriteStartObject();
			writer.WritePropertyName(Operation);

			switch (requestResponseSerializer)
			{
				case DefaultHighLevelSerializer dhls:
					JsonSerializer.Serialize<BulkIndexOperation<T>>(writer, this, dhls.Options);
					break;
				default:
					JsonSerializer.Serialize<BulkIndexOperation<T>>(writer, this); // Unable to handle options if this were to ever be the case
					break;
			}

			writer.WriteEndObject();
		}
	}
}
