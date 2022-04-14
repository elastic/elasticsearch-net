// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.IndexManagement
{
	public partial class IndexAndDataStreamAction : IActionVariant
	{
		[JsonIgnore]
		string IActionVariant.ActionVariantName => "add_backing_index";
		[JsonInclude]
		[JsonPropertyName("data_stream")]
		public Elastic.Clients.Elasticsearch.DataStreamName DataStream { get; set; }

		[JsonInclude]
		[JsonPropertyName("index")]
		public Elastic.Clients.Elasticsearch.IndexName Index { get; set; }
	}

	public sealed partial class IndexAndDataStreamActionDescriptor : DescriptorBase<IndexAndDataStreamActionDescriptor>
	{
		internal IndexAndDataStreamActionDescriptor(Action<IndexAndDataStreamActionDescriptor> configure) => configure.Invoke(this);
		public IndexAndDataStreamActionDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.DataStreamName DataStreamValue { get; set; }

		private Elastic.Clients.Elasticsearch.IndexName IndexValue { get; set; }

		public IndexAndDataStreamActionDescriptor DataStream(Elastic.Clients.Elasticsearch.DataStreamName dataStream)
		{
			DataStreamValue = dataStream;
			return Self;
		}

		public IndexAndDataStreamActionDescriptor Index(Elastic.Clients.Elasticsearch.IndexName index)
		{
			IndexValue = index;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("data_stream");
			JsonSerializer.Serialize(writer, DataStreamValue, options);
			writer.WritePropertyName("index");
			JsonSerializer.Serialize(writer, IndexValue, options);
			writer.WriteEndObject();
		}
	}
}