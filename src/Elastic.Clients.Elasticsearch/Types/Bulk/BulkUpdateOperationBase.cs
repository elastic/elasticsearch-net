// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	public abstract class BulkUpdateOperationBase : BulkOperationBase
	{
		private static byte _newline => (byte)'\n';

		[JsonPropertyName("retry_on_conflict")]
		public bool? RetryOnConflict { get; set; }

		[JsonPropertyName("require_alias")]
		public bool? RequireAlias { get; set; }

		protected override string Operation => "update";

		protected abstract void BeforeSerialize(IElasticsearchClientSettings settings);

		protected abstract void WriteOperation(Utf8JsonWriter writer, JsonSerializerOptions options = null);

		protected override void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			BeforeSerialize(settings);

			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				WriteOperation(internalWriter, dhls.Options);
			}
			else
			{
				WriteOperation(internalWriter);
			}

			internalWriter.WriteEndObject();
			internalWriter.Flush();

			stream.WriteByte(_newline);

			var body = GetBody();

			settings.RequestResponseSerializer.Serialize(body, stream, formatting);
		}

		protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			BeforeSerialize(settings);

			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				WriteOperation(internalWriter, dhls.Options);
			}
			else
			{
				WriteOperation(internalWriter);
			}

			internalWriter.WriteEndObject();
			internalWriter.Flush();

			stream.WriteByte(_newline);

			var body = GetBody();

			await settings.SourceSerializer.SerializeAsync(body, stream, formatting).ConfigureAwait(false);
		}
	}
}
