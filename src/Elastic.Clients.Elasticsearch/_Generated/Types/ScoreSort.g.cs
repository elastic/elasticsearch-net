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
namespace Elastic.Clients.Elasticsearch
{
	public sealed partial class ScoreSort : ISortOptionsVariant
	{
		[JsonInclude]
		[JsonPropertyName("order")]
		public Elastic.Clients.Elasticsearch.SortOrder? Order { get; set; }
	}

	public sealed partial class ScoreSortDescriptor : SerializableDescriptorBase<ScoreSortDescriptor>
	{
		internal ScoreSortDescriptor(Action<ScoreSortDescriptor> configure) => configure.Invoke(this);
		public ScoreSortDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.SortOrder? OrderValue { get; set; }

		public ScoreSortDescriptor Order(Elastic.Clients.Elasticsearch.SortOrder? order)
		{
			OrderValue = order;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (OrderValue is not null)
			{
				writer.WritePropertyName("order");
				JsonSerializer.Serialize(writer, OrderValue, options);
			}

			writer.WriteEndObject();
		}
	}
}