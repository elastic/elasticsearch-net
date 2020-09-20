// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using Elastic.Transport;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.JsonNetSerializer
{
	internal static class JTokenExtensions
	{
		/// <summary>
		/// Writes a <see cref="JToken" /> to a <see cref="MemoryStream" /> using <see cref="ConnectionSettingsAwareSerializerBase.ExpectedEncoding" />
		/// </summary>
		public static MemoryStream ToStream(this JToken token, IMemoryStreamFactory memoryStreamFactory)
		{
			var ms = memoryStreamFactory.Create();
			using (var streamWriter = new StreamWriter(ms, ConnectionSettingsAwareSerializerBase.ExpectedEncoding,
				ConnectionSettingsAwareSerializerBase.DefaultBufferSize, true))
			using (var writer = new JsonTextWriter(streamWriter))
			{
				token.WriteTo(writer);
				writer.Flush();
				ms.Position = 0;
				return ms;
			}
		}
	}
}
