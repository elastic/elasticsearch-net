// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

public sealed class BulkUpdateOperation<TDocument, TPartialDocument> : BulkUpdateOperationBase
{
	public BulkUpdateOperation(Id id) => Id = id;

	public BulkUpdateOperation(TDocument idFrom, bool useIdFromAsUpsert = false)
	{
		IdFrom = idFrom;
		if (useIdFromAsUpsert)
			Upsert = idFrom;
	}

	public BulkUpdateOperation(TDocument idFrom, TPartialDocument update, bool useIdFromAsUpsert = false)
	{
		IdFrom = idFrom;
		if (useIdFromAsUpsert)
			Upsert = idFrom;
		Doc = update;
	}

	[JsonPropertyName("pipeline")]
	public string? Pipeline { get; set; }

	[JsonPropertyName("dynamic_templates")]
	public Dictionary<string, string>? DynamicTemplates { get; set; }

	[JsonIgnore]
	public TPartialDocument Doc { get; set; }

	[JsonIgnore]
	public TDocument IdFrom { get; set; }

	[JsonIgnore]
	public ScriptBase Script { get; set; }

	[JsonIgnore]
	public bool? ScriptedUpsert { get; set; }

	[JsonIgnore]
	public bool? DocAsUpsert { get; set; }

	[JsonIgnore]
	public TDocument Upsert { get; set; }

	protected override string Operation => "update";

	protected override void BeforeSerialize(IElasticsearchClientSettings settings)
	{
		//if (Id is null && IdFrom is not null)
		//	Id = settings.Inferrer.Id<TDocument>(IdFrom);

		//if (Index is null)
		//	Index = settings.Inferrer.IndexName<TDocument>();
	}

	protected override void WriteOperation(Utf8JsonWriter writer, JsonSerializerOptions options = null) => JsonSerializer.Serialize(writer, this, options);

	protected override object GetBody() => new BulkUpdateBody<TDocument, TPartialDocument> {
		PartialUpdate = Doc,
		Script = Script,
		Upsert = Upsert,
		DocAsUpsert = DocAsUpsert,
		ScriptedUpsert = ScriptedUpsert,
		IfPrimaryTerm = IfPrimaryTerm,
		IfSequenceNumber = IfSequenceNumber,
		//Source = Source
	};

	protected override Id GetIdForOperation(Inferrer inferrer) =>
		Id ?? new Id(new[] { IdFrom, Upsert }.FirstOrDefault(o => o != null));

	protected override Routing GetRoutingForOperation(Inferrer inferrer)
	{
		if (Routing != null)
			return Routing;

		if (IdFrom != null)
			return new Routing(IdFrom);

		if (Upsert != null)
			return new Routing(Upsert);

		return null;
	}
}

public static class BulkUpdateOperation
{
	public static BulkUpdateOperationWithPartial<TPartial> WithPartial<TPartial>(Id id, TPartial partialDocument) => new(id, partialDocument);

	public static BulkUpdateOperationWithPartial<TPartial> WithPartial<TPartial>(Id id, IndexName index, TPartial partialDocument) => new(id, index, partialDocument);

	public static BulkUpdateOperationWithScript WithScript(Id id, IndexName index, ScriptBase script) => new(id, index, script);
}

public sealed class BulkUpdateOperationDescriptor<TDocument, TPartialDocument> : BulkOperationDescriptorBase<BulkUpdateOperationDescriptor<TDocument, TPartialDocument>>
{
	private static byte _newline => (byte)'\n';

	private TPartialDocument _document;
	private Action<InlineScriptDescriptor> _scriptAction;

	protected override string Operation => "update";

	public BulkUpdateOperationDescriptor<TDocument, TPartialDocument> Doc(TPartialDocument document) => Assign(document, (a, v) => a._document = v);

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
