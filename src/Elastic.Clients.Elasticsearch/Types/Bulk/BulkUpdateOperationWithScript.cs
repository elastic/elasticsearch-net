// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch
{
	public sealed class BulkUpdateOperationWithScript : BulkUpdateOperationBase
	{
		public BulkUpdateOperationWithScript(Id id, IndexName index, ScriptBase script)
		{
			Id = id;
			Index = index;
			Script = script;
		}

		[JsonIgnore]
		public ScriptBase Script { get; set; }

		//protected override Type ClrType => typeof(TPartialDocument);

		protected override string Operation => "update";

		protected override void BeforeSerialize(IElasticsearchClientSettings settings)
		{
		}

		protected override void WriteOperation(Utf8JsonWriter writer, JsonSerializerOptions options = null) => JsonSerializer.Serialize<BulkUpdateOperationWithScript>(writer, this, options);

		internal override BulkUpdateBodyBase GetBody() => new ScriptedBulkUpdateBody { Script = Script };
	}
}
