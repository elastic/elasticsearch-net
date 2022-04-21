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

using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Ml
{
	public sealed class MlPutFilterRequestParameters : RequestParameters<MlPutFilterRequestParameters>
	{
	}

	public partial class MlPutFilterRequest : PlainRequestBase<MlPutFilterRequestParameters>
	{
		public MlPutFilterRequest(Elastic.Clients.Elasticsearch.Id filter_id) : base(r => r.Required("filter_id", filter_id))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.MachineLearningPutFilter;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => true;
		[JsonInclude]
		[JsonPropertyName("description")]
		public string? Description { get; set; }

		[JsonInclude]
		[JsonPropertyName("items")]
		public IEnumerable<string>? Items { get; set; }
	}

	public sealed partial class MlPutFilterRequestDescriptor : RequestDescriptorBase<MlPutFilterRequestDescriptor, MlPutFilterRequestParameters>
	{
		internal MlPutFilterRequestDescriptor(Action<MlPutFilterRequestDescriptor> configure) => configure.Invoke(this);
		public MlPutFilterRequestDescriptor(Elastic.Clients.Elasticsearch.Id filter_id) : base(r => r.Required("filter_id", filter_id))
		{
		}

		internal MlPutFilterRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.MachineLearningPutFilter;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => true;
		public MlPutFilterRequestDescriptor FilterId(Elastic.Clients.Elasticsearch.Id filter_id)
		{
			RouteValues.Required("filter_id", filter_id);
			return Self;
		}

		private string? DescriptionValue { get; set; }

		private IEnumerable<string>? ItemsValue { get; set; }

		public MlPutFilterRequestDescriptor Description(string? description)
		{
			DescriptionValue = description;
			return Self;
		}

		public MlPutFilterRequestDescriptor Items(IEnumerable<string>? items)
		{
			ItemsValue = items;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (!string.IsNullOrEmpty(DescriptionValue))
			{
				writer.WritePropertyName("description");
				writer.WriteStringValue(DescriptionValue);
			}

			if (ItemsValue is not null)
			{
				writer.WritePropertyName("items");
				JsonSerializer.Serialize(writer, ItemsValue, options);
			}

			writer.WriteEndObject();
		}
	}
}