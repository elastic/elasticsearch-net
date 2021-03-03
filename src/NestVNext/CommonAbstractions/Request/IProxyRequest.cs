// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using Elastic.Transport;

namespace Nest
{
	/// <summary> A request that that does not necessarily (de)serializes itself </summary>
	public interface IProxyRequest : IRequest
	{
		void WriteJson(ITransportSerializer sourceSerializer, Stream s, SerializationFormatting serializationFormatting);
	}


	/// <summary>
	/// Describes a request that serializes the document passed to <see cref="DocumentPath{T}"/> when calling the fluent API.
	/// </summary>
	public interface IDocumentRequest { }
}
