// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text;
using System.Text.Json;
using System.IO;

namespace Tests.Core.Serialization
{
	public static class JsonDocumentExtensions
	{
		public static string ToJsonString(this JsonDocument jdoc)
		{
			using (var stream = new MemoryStream())
			{
				var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true });
				jdoc.WriteTo(writer);
				writer.Flush();
				return Encoding.UTF8.GetString(stream.ToArray());
			}
		}
	}
}
