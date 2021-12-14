// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Text.Json;

namespace Elastic.Clients.Elasticsearch
{
	internal class ScriptedBulkUpdateBody : BulkUpdateBodyBase
	{
		public ScriptBase Script { get; set; }

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			if (Script is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, Script, options);
			}
		}
	}
}
