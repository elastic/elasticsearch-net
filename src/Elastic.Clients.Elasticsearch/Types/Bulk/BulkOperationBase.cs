// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	public abstract class BulkOperationBase : IBulkOperation, IStreamSerializable
	{
		[JsonPropertyName("_id")]
		public Id Id { get; set; }

		[JsonPropertyName("if_primary_term")]
		public long? IfPrimaryTerm { get; set; }

		[JsonPropertyName("if_seq_no")]
		public long? IfSequenceNumber { get; set; }

		[JsonPropertyName("_index")]
		public IndexName Index { get; set; }

		[JsonPropertyName("routing")]
		public Routing Routing { get; set; }

		[JsonPropertyName("version")]
		public long? Version { get; set; }

		[JsonPropertyName("version_type")]
		public VersionType? VersionType { get; set; }

		protected abstract void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None);

		protected abstract Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None);

		void IStreamSerializable.Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting)
		{
			Id = GetIdForOperation(settings.Inferrer);
			Routing = GetRoutingForOperation(settings.Inferrer);

			Serialize(stream, settings, formatting);
		}

		Task IStreamSerializable.SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting)
		{
			Id = GetIdForOperation(settings.Inferrer);
			Routing = GetRoutingForOperation(settings.Inferrer);

			return SerializeAsync(stream, settings, formatting);
		}

		protected abstract string Operation { get; }

		protected abstract object GetBody();

		protected virtual Id GetIdForOperation(Inferrer inferrer) => Id ?? new Id(GetBody());

		protected virtual Routing GetRoutingForOperation(Inferrer inferrer) => Routing ?? new Routing(GetBody());
	}
}
