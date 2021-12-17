// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

public sealed class BulkUpdateOperationDescriptor<TDocument, TPartialDocument> : BulkOperationDescriptorBase<BulkUpdateOperationDescriptor<TDocument, TPartialDocument>>
{
	private static byte _newline => (byte)'\n';

	public BulkUpdateOperationDescriptor() { }

	public BulkUpdateOperationDescriptor(Id id) => Id(id);

	private TPartialDocument _document;
	private TDocument _idFrom;

	private Action<InlineScriptDescriptor> _scriptAction;

	protected override string Operation => "update";

	public BulkUpdateOperationDescriptor<TDocument, TPartialDocument> Doc(TPartialDocument document) => Assign(document, (a, v) => a._document = v);

	public BulkUpdateOperationDescriptor<TDocument, TPartialDocument> IdFrom(TDocument idFrom) => Assign(idFrom, (a, v) => a._idFrom = v);

	public BulkUpdateOperationDescriptor<TDocument, TPartialDocument> Script(Action<InlineScriptDescriptor> configure) => Assign(configure, (a, v) => a._scriptAction = v);

	protected override object GetBody()
	{
		if (_scriptAction is not null)
			return null;

		return new BulkUpdateBody<TDocument, TPartialDocument>
		{
			PartialUpdate = _document,
			//Script = Script,
			//Upsert = Upsert,
			//DocAsUpsert = DocAsUpsert,
			//ScriptedUpsert = ScriptedUpsert,
			//IfPrimaryTerm = IfPrimaryTerm,
			//IfSequenceNumber = IfSequenceNumber,
			//Source = Source
		};
	}

	protected override void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting)
	{
		var requestResponseSerializer = settings.RequestResponseSerializer;

		var writer = new Utf8JsonWriter(stream);

		writer.WriteStartObject();
		writer.WritePropertyName(Operation);

		JsonSerializerOptions options = null;

		if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			options = dhls.Options;

		JsonSerializer.Serialize<BulkUpdateOperationDescriptor<TDocument, TPartialDocument>>(writer, this, options);

		writer.WriteEndObject();
		writer.Flush();

		stream.WriteByte(_newline);

		var body = GetBody();

		if (body is not null)
		{
			settings.RequestResponseSerializer.Serialize(body, stream, formatting);
		}
		else
		{
			writer = new Utf8JsonWriter(stream);
			writer.WriteStartObject();

			if (_scriptAction is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, new InlineScriptDescriptor(_scriptAction), options);
			}

			writer.WriteEndObject();
			writer.Flush();
		}
	}

	protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting, CancellationToken cancellationToken = default)
	{
		var requestResponseSerializer = settings.RequestResponseSerializer;

		var writer = new Utf8JsonWriter(stream);

		writer.WriteStartObject();
		writer.WritePropertyName(Operation);

		JsonSerializerOptions options = null;

		if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			options = dhls.Options;

		JsonSerializer.Serialize<BulkUpdateOperationDescriptor<TDocument, TPartialDocument>>(writer, this, options);

		writer.WriteEndObject();
		await writer.FlushAsync(cancellationToken).ConfigureAwait(false);

		stream.WriteByte(_newline);

		var body = GetBody();
		
		if (body is not null)
		{
			await settings.RequestResponseSerializer.SerializeAsync(body, stream, formatting, cancellationToken).ConfigureAwait(false);
		}
		else
		{
			writer = new Utf8JsonWriter(stream);
			writer.WriteStartObject();

			if (_scriptAction is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, new InlineScriptDescriptor(_scriptAction), options);
			}

			writer.WriteEndObject();
			await writer.FlushAsync(cancellationToken).ConfigureAwait(false);
		}
	}

	protected override void SerializeInternal(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		// TODO
	}

	//protected override Id GetIdForOperation(Inferrer inferrer) =>
	//	Id ?? new Id(new[] { _document, Upsert }.FirstOrDefault(o => o != null));

	//protected override Routing GetRoutingForOperation(Inferrer inferrer)
	//{
	//	if (Routing != null)
	//		return Routing;

	//	if (IdFrom != null)
	//		return new Routing(IdFrom);

	//	if (Upsert != null)
	//		return new Routing(Upsert);

	//	return null;
	//}
}
