using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Elastic.Transport;

namespace Nest
{
	public partial interface IIndexRequest<TDocument> : IProxyRequest
	{
	}

	public partial class IndexRequest<TDocument>
	{
		public void WriteJson(Stream stream, ITransportSerializer sourceSerializer, SerializationFormatting formatting) => sourceSerializer.Serialize(Document, stream, formatting);
	}
}
