// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;

using Elastic.Clients.Elasticsearch.Core.Search;

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

public sealed class BulkUpdateOperation<TDocument, TPartialDocument> :
	BulkUpdateOperation
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

	protected override Type ClrType => typeof(TDocument);

	public TPartialDocument Doc { get; set; }

	public TDocument IdFrom { get; set; }

	public Script Script { get; set; }

	public bool? ScriptedUpsert { get; set; }

	public bool? DocAsUpsert { get; set; }

	public TDocument Upsert { get; set; }

	public Union<bool, SourceFilter> Source { get; set; }

	protected override string Operation => "update";

	protected override void BeforeSerialize(IElasticsearchClientSettings settings)
	{
		Id ??= new Id(new[] { IdFrom, Upsert }.FirstOrDefault(o => o != null));

		if (Routing is null)
		{
			if (IdFrom != null)
				Routing ??= new Routing(IdFrom);

			if (Upsert != null)
				Routing ??= new Routing(Upsert);
		}
	}

	private protected override BulkUpdateBody GetBody() => new BulkUpdateBody<TDocument, TPartialDocument>
	{
		PartialUpdate = Doc,
		Script = Script,
		Upsert = Upsert,
		DocAsUpsert = DocAsUpsert,
		ScriptedUpsert = ScriptedUpsert,
		IfPrimaryTerm = IfPrimaryTerm,
		IfSequenceNumber = IfSequenceNumber,
		Source = Source
	};
}

public static class BulkUpdateOperationFactory
{
	public static BulkUpdateOperationWithPartial<TPartial> WithPartial<TPartial>(Id id, TPartial partialDocument) => new(id, partialDocument);

	public static BulkUpdateOperationWithPartial<TPartial> WithPartial<TPartial>(Id id, IndexName index, TPartial partialDocument) => new(id, index, partialDocument);

	public static BulkUpdateOperationWithScript WithScript(Id id, IndexName index, Script script) => new(id, index, script);

	public static BulkUpdateOperationWithScript<TDocument> WithScript<TDocument>(Id id, IndexName index, Script script, TDocument upsert) => new(upsert, id, index, script);

	public static BulkUpdateOperationWithScript<TDocument> WithScript<TDocument>(Id id, Script script, TDocument upsert) => new(upsert, id, script);

	public static BulkUpdateOperationWithScript<TDocument> WithScript<TDocument>(Script script, TDocument upsert) => new(upsert, new Id(upsert), script);

	public static BulkUpdateOperationWithScript WithScript(Id id, Script script) => new(id, script);
}
