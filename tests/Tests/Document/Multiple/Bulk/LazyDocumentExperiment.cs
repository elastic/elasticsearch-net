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

		var readJson = Encoding.UTF8.GetString(get.Source.Bytes);

		get.Found.Should().BeTrue();
		get.SequenceNumber.Should().Be(3);
		readJson.Should().Be(@"{""field1"":""value1"",""field2"":""value2"",""field3"":[""item1"",""item2"",""item3""],""field4"":{""subitemfield1"":""subitemvalue1""}}");
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
	public class LazyDocument
	{
		public LazyDocument(byte[] bytes) => Bytes = bytes;

		public byte[] Bytes { get; }
	}

	public class LazyDocumentConverter : JsonConverter<LazyDocument>
	{
		public override LazyDocument Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var bytes = reader.GetBytesUntilEndOfObject();
			return new LazyDocument(bytes);
		}

		public override void Write(Utf8JsonWriter writer, LazyDocument value, JsonSerializerOptions options) => throw new NotImplementedException();
	}
}
