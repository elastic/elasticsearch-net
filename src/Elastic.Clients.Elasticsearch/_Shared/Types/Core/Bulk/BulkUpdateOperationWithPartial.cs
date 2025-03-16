// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

public sealed class BulkUpdateOperationWithPartial<TPartialDocument> : BulkUpdateOperation
{
	public BulkUpdateOperationWithPartial(Id id, TPartialDocument partialDocument)
	{
		Id = id;
		PartialDocument = partialDocument;
	}

	public BulkUpdateOperationWithPartial(Id id, IndexName index, TPartialDocument partialDocument)
	{
		Id = id;
		Index = index;
		PartialDocument = partialDocument;
	}

	[JsonIgnore]
	public TPartialDocument PartialDocument { get; set; }

	protected override Type ClrType => null;

	protected override string Operation => "update";

	protected override void BeforeSerialize(IElasticsearchClientSettings settings)
	{
		if (Index is null)
			Index = settings.Inferrer.IndexName<TPartialDocument>();
	}

	protected override void WriteOperation(Utf8JsonWriter writer, JsonSerializerOptions options = null) =>
		JsonSerializer.Serialize<BulkUpdateOperationWithPartial<TPartialDocument>>(writer, this, options);

	protected override object GetBody() => new PartialBulkUpdateBody<TPartialDocument> { PartialUpdate = PartialDocument };
}
