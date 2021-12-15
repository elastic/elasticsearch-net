// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch
{
	public sealed class BulkUpdateOperation<TDocument, TPartialDocument> : BulkUpdateOperationBase
	{
		[JsonPropertyName("pipeline")]
		public string? Pipeline { get; set; }

		[JsonPropertyName("dynamic_templates")]
		public Dictionary<string, string>? DynamicTemplates { get; set; }

		[JsonIgnore]
		public TDocument IdFrom { get; set; }

		protected override string Operation => "update";

		protected override void BeforeSerialize(IElasticsearchClientSettings settings)
		{
			if (Id is null && IdFrom is not null)
				Id = settings.Inferrer.Id<TDocument>(IdFrom);

			if (Index is null)
				Index = settings.Inferrer.IndexName<TDocument>();
		}
 
		protected override void WriteOperation(Utf8JsonWriter writer, JsonSerializerOptions options = null) => throw new NotImplementedException();

		protected override object GetBody() => new BulkUpdateBody<TDocument, TPartialDocument> { /** TODO **/  };
	}

	public static class BulkUpdateOperation
	{
		public static BulkUpdateOperationWithPartial<TPartial> WithPartial<TPartial>(Id id, TPartial partialDocument) => new(id, partialDocument);

		public static BulkUpdateOperationWithPartial<TPartial> WithPartial<TPartial>(Id id, IndexName index, TPartial partialDocument) => new(id, index, partialDocument);

		public static BulkUpdateOperationWithScript WithScript(Id id, IndexName index, ScriptBase script) => new(id, index, script);
	}
}
