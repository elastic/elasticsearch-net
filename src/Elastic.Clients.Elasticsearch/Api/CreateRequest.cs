// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Transport;
using System.Text.Json;

namespace Elastic.Clients.Elasticsearch
{
	public sealed partial class CreateRequest<TDocument> : ICustomJsonWriter
	{

		public CreateRequest(Id id) : this(typeof(TDocument), id)
		{
		}

		public CreateRequest(TDocument documentWithId, IndexName index = null, Id id = null)
			: this(index ?? typeof(TDocument), id ?? Id.From(documentWithId)) =>
				Document = documentWithId;

		public void WriteJson(Utf8JsonWriter writer, Serializer sourceSerializer) => SourceSerialisation.Serialize(Document, writer, sourceSerializer);
	}
}
