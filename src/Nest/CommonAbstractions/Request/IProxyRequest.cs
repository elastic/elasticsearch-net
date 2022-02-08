// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using System.Text.Json;
using Elastic.Transport;

namespace Nest
{
	/// <summary> A request that that does not necessarily (de)serializes itself </summary>
	public interface IProxyRequest
	{
		// TODO: Temp
		//void WriteJson(Stream stream, ITransportSerializer sourceSerializer, SerializationFormatting formatting);
		
		// TODO: SerializationFormatting
		void WriteJson(Utf8JsonWriter writer, ITransportSerializer sourceSerializer);
	}


	/// <summary>
	/// Describes a request that serializes the document passed to <see cref="DocumentPath{T}"/> when calling the fluent API.
	/// </summary>
	public interface IDocumentRequest { }
}
