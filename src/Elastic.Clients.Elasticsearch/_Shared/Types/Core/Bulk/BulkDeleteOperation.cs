// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Elastic.Transport.Extensions;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

public class BulkDeleteOperation : BulkOperation
{
	public BulkDeleteOperation(Id id) => Id = id;

	protected override void Serialize(Stream stream, IElasticsearchClientSettings settings)
	{
		SetValues(settings);
		using var writer = new Utf8JsonWriter(stream);
		SerializeInternal(settings, writer);
		writer.Flush();
	}

	protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings)
	{
		SetValues(settings);
		var writer = new Utf8JsonWriter(stream);
		await using (writer.ConfigureAwait(false))
		{
			SerializeInternal(settings, writer);
			await writer.FlushAsync().ConfigureAwait(false);
		}
	}

	protected virtual void SetValues(IElasticsearchClientSettings settings)
	{
	}

	private void SerializeInternal(IElasticsearchClientSettings settings, Utf8JsonWriter writer)
	{
		var requestResponseSerializer = settings.RequestResponseSerializer;
		writer.WriteStartObject();
		writer.WritePropertyName(Operation);
		requestResponseSerializer.Serialize(this, writer, settings.MemoryStreamFactory);
		writer.WriteEndObject();
	}

	protected override string Operation => "delete";

	protected override Type ClrType => null;
}

public sealed class BulkDeleteOperation<T> : BulkDeleteOperation
{
	public BulkDeleteOperation(T document) : base (new Id(document))
		=> Document = document;

	public BulkDeleteOperation(Id id) : base(id) { }

	[JsonIgnore]
	public T Document { get; set; }

	protected override void SetValues(IElasticsearchClientSettings settings)
	{
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

		Index ??= typeof(T);
	}

	protected override Type ClrType => typeof(T);
}
