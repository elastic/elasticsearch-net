using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Elastic.Transport;
using Elastic.Transport.Extensions;

namespace Nest
{
	public partial interface IIndexRequest<TDocument> : IProxyRequest
	{
	}

	public partial class IndexRequest<TDocument>
	{
		// TODO: Old approach using ProxyPostData - Leaving it here while we decide on the preferred mechanism
		//public void WriteJson(Stream stream, ITransportSerializer sourceSerializer, SerializationFormatting formatting) => sourceSerializer.Serialize(Document, stream, formatting);
		
		void IProxyRequest.WriteJson(Utf8JsonWriter writer, ITransportSerializer sourceSerializer)
		{
			// TODO: Review perf
			using var ms = new MemoryStream();
			sourceSerializer.Serialize(Document, ms);
			ms.Position = 0;
			using var document = JsonDocument.Parse(ms); // This is not super efficient but a variant on the suggestion at https://github.com/dotnet/runtime/issues/1784#issuecomment-608331125
			document.RootElement.WriteTo(writer);

			// Perhaps can be improved in .NET 6/ updated system.text.json - https://github.com/dotnet/runtime/pull/53212
		}
	}
}
