// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Tests.Core.Extensions;

namespace Tests.Document.Multiple;

public class LazyDocumentExperiment
{
	[U]
	public void ReadBulkSourceAsLazyDocument()
	{
		var json = @" {
					""_seq_no"": 3,
					""found"": true,
					""_source"": {
						""field1"": ""value1"",
						""field2"": ""value2"",
						""field3"": [ ""item1"", ""item2"", ""item3"" ],
						""field4"": { ""subitemfield1"":""subitemvalue1"" }
					}
				}";

		var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
		var get = JsonSerializer.Deserialize<Get>(ms);

		get.Found.Should().BeTrue();
		get.SequenceNumber.Should().Be(3);

		var client = new ElasticClient();
		var stream = new MemoryStream(get.Source.Bytes);
		var myType = client.SourceSerializer.Deserialize<MyType>(stream);
		myType.Field1.Should().Be("value1");
	}

	private class MyType
	{
		[JsonPropertyName("field1")]
		public string Field1 { get; set; }
	}

	public class Get
	{
		[JsonPropertyName("_seq_no")]
		public int? SequenceNumber { get; set; }

		[JsonPropertyName("found")]
		public bool? Found { get; set; }

		[JsonPropertyName("_source")]
		public LazyDocument Source { get; set; }
	}

	[JsonConverter(typeof(LazyDocumentConverter))]
	public class LazyDocument /*: IDisposable*/
	{
		//private bool _disposedValue;

		//public LazyDocument(JsonDocument doc) => Doc = doc;

		//public JsonDocument Doc { get; }

		//protected virtual void Dispose(bool disposing)
		//{
		//	if (!_disposedValue)
		//	{
		//		if (disposing)
		//		{
		//			Doc.Dispose();
		//		}

		//		_disposedValue = true;
		//	}
		//}

		//public void Dispose()
		//{
		//	Dispose(disposing: true);
		//	GC.SuppressFinalize(this);
		//}

		public LazyDocument(byte[] bytes) => Bytes = bytes;

		public byte[] Bytes { get; }
	}

	public class LazyDocumentConverter : JsonConverter<LazyDocument>
	{
		public override LazyDocument Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			// At this stage we're just getting the bytes from valid JSON

			using var jsonDoc = JsonSerializer.Deserialize<JsonDocument>(ref reader);

			using var stream = new MemoryStream();

			var writer = new Utf8JsonWriter(stream);
			jsonDoc.WriteTo(writer);
			writer.Flush();

			return new LazyDocument(stream.ToArray());

			//var bytes = reader.GetBytesUntilEndOfObject();
			//return new LazyDocument(bytes);
		}

		public override void Write(Utf8JsonWriter writer, LazyDocument value, JsonSerializerOptions options) => throw new NotImplementedException();
	}
}
