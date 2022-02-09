// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json;

namespace Elastic.Clients.Elasticsearch
{
	internal abstract class BulkUpdateBodyBase : ISelfSerializable
	{
		public long? IfSequenceNumber { get; set; }

		public long? IfPrimaryTerm { get; set; }

		protected abstract void SerializeProperties(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings);

		void ISelfSerializable.Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();

			if (IfSequenceNumber.HasValue)
			{
				writer.WritePropertyName("if_seq_no");
				writer.WriteNumberValue(IfSequenceNumber.Value);
			}

			if (IfPrimaryTerm.HasValue)
			{
				writer.WritePropertyName("if_primary_term");
				writer.WriteNumberValue(IfPrimaryTerm.Value);
			}

			SerializeProperties(writer, options, settings);

			writer.WriteEndObject();
		}
	}
}
