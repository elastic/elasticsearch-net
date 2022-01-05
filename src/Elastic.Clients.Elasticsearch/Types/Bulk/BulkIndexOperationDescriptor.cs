// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	public sealed class BulkIndexOperationDescriptor<TSource> : BulkOperationDescriptorBase<BulkIndexOperationDescriptor<TSource>>
	{
		private string _pipeline;
		
		private Dictionary<string, string> _dynamicTemplates;

		private static byte _newline => (byte)'\n';

		private readonly TSource _document;

		public BulkIndexOperationDescriptor(TSource source) => _document = source;

		public BulkIndexOperationDescriptor(TSource source, IndexName index) : this(source) => IndexNameValue = index;

		public BulkIndexOperationDescriptor<TSource> Pipeline(string pipeline) => Assign(pipeline, (a, v) => a._pipeline = v);


		public BulkIndexOperationDescriptor<TSource> DynamicTemplates(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector) =>
			Assign(selector, (a, v) => a._dynamicTemplates = v?.Invoke(new FluentDictionary<string, string>()));

		protected override string Operation => "index";

		protected override Type ClrType => typeof(TSource);

		protected override void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkIndexOperationDescriptor<TSource>>(internalWriter, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkIndexOperationDescriptor<TSource>>(internalWriter, this); // Unable to handle options if this were to ever be the case
			}

			internalWriter.WriteEndObject();
			internalWriter.Flush();

			stream.WriteByte(_newline);

			settings.SourceSerializer.Serialize(_document, stream);
		}

		protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting, CancellationToken cancellationToken = default)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkIndexOperationDescriptor<TSource>>(internalWriter, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkIndexOperationDescriptor<TSource>>(internalWriter, this); // Unable to handle options if this were to ever be the case
			}

			internalWriter.WriteEndObject();
			await internalWriter.FlushAsync().ConfigureAwait(false);

			stream.WriteByte(_newline);

			await settings.SourceSerializer.SerializeAsync(_document, stream).ConfigureAwait(false);
		}

		protected override void SerializeInternal(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			if (!string.IsNullOrEmpty(_pipeline))
			{
				writer.WritePropertyName("pipeline");
				JsonSerializer.Serialize(writer, _pipeline, options);
			}

			if (_dynamicTemplates is not null)
			{
				writer.WritePropertyName("dynamic_templates");
				JsonSerializer.Serialize(writer, _dynamicTemplates, options);
			}
		}

		protected override object GetBody() => _document;

		protected override Id GetIdForOperation(Inferrer inferrer) => IdValue ?? new Id(_document);

		protected override Routing GetRoutingForOperation(Inferrer inferrer) => RoutingValue ?? new Routing(_document);
	}
}
