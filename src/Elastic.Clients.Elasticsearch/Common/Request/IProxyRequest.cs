// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using System.Text.Json;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	public interface ICustomJsonWriter
	{
		// TODO: Temp
		//void WriteJson(Stream stream, ITransportSerializer sourceSerializer, SerializationFormatting formatting);
		
		void WriteJson(Utf8JsonWriter writer, Serializer sourceSerializer);
	}


	///// <summary>
	///// Describes a request that serializes the document passed to <see cref="DocumentPath{T}"/> when calling the fluent API.
	///// </summary>
	//public interface IDocumentRequest { }
}
