// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

[JsonConverter(typeof(JsonIncompatibleConverter))]
public class BulkUpdateOperationWithScript :
	BulkUpdateOperation
{
	public BulkUpdateOperationWithScript(Id id, Script script)
	{
		Id = id;
		Script = script;
	}

	public BulkUpdateOperationWithScript(Id id, IndexName index, Script script)
	{
		Id = id;
		Index = index;
		Script = script;
	}

	public Script Script { get; set; }

	protected override string Operation => "update";

	protected override Type ClrType => null;

	protected override void BeforeSerialize(IElasticsearchClientSettings settings)
	{
	}

	private protected override BulkUpdateBody GetBody() => new ScriptedBulkUpdateBody
	{
		Script = Script
	};
}

public sealed class BulkUpdateOperationWithScript<TDocument> : BulkUpdateOperationWithScript
{
	public BulkUpdateOperationWithScript(TDocument upsert, Id id, Script script) : base(id, script) => Upsert = upsert;

	public BulkUpdateOperationWithScript(TDocument upsert, Id id, IndexName index, Script script) : base(id, index, script) => Upsert = upsert;

	public TDocument Upsert { get; set; }

	private protected override BulkUpdateBody GetBody() => new ScriptedBulkUpdateBody<TDocument> { Script = Script, Upsert = Upsert };
}
