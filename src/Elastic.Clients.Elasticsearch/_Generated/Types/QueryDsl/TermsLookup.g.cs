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
namespace Elastic.Clients.Elasticsearch.QueryDsl
{
	public sealed partial class TermsLookup
	{
		[JsonInclude]
		[JsonPropertyName("id")]
		public Elastic.Clients.Elasticsearch.Id Id { get; set; }

		[JsonInclude]
		[JsonPropertyName("index")]
		public Elastic.Clients.Elasticsearch.IndexName Index { get; set; }

		[JsonInclude]
		[JsonPropertyName("path")]
		public Elastic.Clients.Elasticsearch.Field Path { get; set; }

		[JsonInclude]
		[JsonPropertyName("routing")]
		public Elastic.Clients.Elasticsearch.Routing? Routing { get; set; }
	}

	public sealed partial class TermsLookupDescriptor<TDocument> : SerializableDescriptorBase<TermsLookupDescriptor<TDocument>>
	{
		internal TermsLookupDescriptor(Action<TermsLookupDescriptor<TDocument>> configure) => configure.Invoke(this);
		public TermsLookupDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.Id IdValue { get; set; }

		private Elastic.Clients.Elasticsearch.IndexName IndexValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field PathValue { get; set; }

		private Elastic.Clients.Elasticsearch.Routing? RoutingValue { get; set; }

		public TermsLookupDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Id id)
		{
			IdValue = id;
			return Self;
		}

		public TermsLookupDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.IndexName index)
		{
			IndexValue = index;
			return Self;
		}

		public TermsLookupDescriptor<TDocument> Path(Elastic.Clients.Elasticsearch.Field path)
		{
			PathValue = path;
			return Self;
		}

		public TermsLookupDescriptor<TDocument> Path<TValue>(Expression<Func<TDocument, TValue>> path)
		{
			PathValue = path;
			return Self;
		}

		public TermsLookupDescriptor<TDocument> Routing(Elastic.Clients.Elasticsearch.Routing? routing)
		{
			RoutingValue = routing;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("id");
			JsonSerializer.Serialize(writer, IdValue, options);
			writer.WritePropertyName("index");
			JsonSerializer.Serialize(writer, IndexValue, options);
			writer.WritePropertyName("path");
			JsonSerializer.Serialize(writer, PathValue, options);
			if (RoutingValue is not null)
			{
				writer.WritePropertyName("routing");
				JsonSerializer.Serialize(writer, RoutingValue, options);
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class TermsLookupDescriptor : SerializableDescriptorBase<TermsLookupDescriptor>
	{
		internal TermsLookupDescriptor(Action<TermsLookupDescriptor> configure) => configure.Invoke(this);
		public TermsLookupDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.Id IdValue { get; set; }

		private Elastic.Clients.Elasticsearch.IndexName IndexValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field PathValue { get; set; }

		private Elastic.Clients.Elasticsearch.Routing? RoutingValue { get; set; }

		public TermsLookupDescriptor Id(Elastic.Clients.Elasticsearch.Id id)
		{
			IdValue = id;
			return Self;
		}

		public TermsLookupDescriptor Index(Elastic.Clients.Elasticsearch.IndexName index)
		{
			IndexValue = index;
			return Self;
		}

		public TermsLookupDescriptor Path(Elastic.Clients.Elasticsearch.Field path)
		{
			PathValue = path;
			return Self;
		}

		public TermsLookupDescriptor Path<TDocument, TValue>(Expression<Func<TDocument, TValue>> path)
		{
			PathValue = path;
			return Self;
		}

		public TermsLookupDescriptor Path<TDocument>(Expression<Func<TDocument, object>> path)
		{
			PathValue = path;
			return Self;
		}

		public TermsLookupDescriptor Routing(Elastic.Clients.Elasticsearch.Routing? routing)
		{
			RoutingValue = routing;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("id");
			JsonSerializer.Serialize(writer, IdValue, options);
			writer.WritePropertyName("index");
			JsonSerializer.Serialize(writer, IndexValue, options);
			writer.WritePropertyName("path");
			JsonSerializer.Serialize(writer, PathValue, options);
			if (RoutingValue is not null)
			{
				writer.WritePropertyName("routing");
				JsonSerializer.Serialize(writer, RoutingValue, options);
			}

			writer.WriteEndObject();
		}
	}
}