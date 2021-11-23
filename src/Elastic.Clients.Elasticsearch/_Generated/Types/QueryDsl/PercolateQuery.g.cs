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
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.QueryDsl
{
	public partial class PercolateQuery : QueryDsl.QueryBase, IQueryContainerVariant
	{
		[JsonIgnore]
		string QueryDsl.IQueryContainerVariant.QueryContainerVariantName => "percolate";
		[JsonInclude]
		[JsonPropertyName("document")]
		public object? Document { get; set; }

		[JsonInclude]
		[JsonPropertyName("documents")]
		public IEnumerable<object>? Documents { get; set; }

		[JsonInclude]
		[JsonPropertyName("field")]
		public string Field { get; set; }

		[JsonInclude]
		[JsonPropertyName("id")]
		public Elastic.Clients.Elasticsearch.Id? Id { get; set; }

		[JsonInclude]
		[JsonPropertyName("index")]
		public Elastic.Clients.Elasticsearch.IndexName? Index { get; set; }

		[JsonInclude]
		[JsonPropertyName("name")]
		public string? Name { get; set; }

		[JsonInclude]
		[JsonPropertyName("preference")]
		public string? Preference { get; set; }

		[JsonInclude]
		[JsonPropertyName("routing")]
		public string? Routing { get; set; }

		[JsonInclude]
		[JsonPropertyName("version")]
		public long? Version { get; set; }
	}

	public sealed partial class PercolateQueryDescriptor<T> : DescriptorBase<PercolateQueryDescriptor<T>>
	{
		public PercolateQueryDescriptor()
		{
		}

		internal PercolateQueryDescriptor(Action<PercolateQueryDescriptor<T>> configure) => configure.Invoke(this);
		internal object? DocumentValue { get; private set; }

		internal IEnumerable<object>? DocumentsValue { get; private set; }

		internal string FieldValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Id? IdValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.IndexName? IndexValue { get; private set; }

		internal string? NameValue { get; private set; }

		internal string? PreferenceValue { get; private set; }

		internal string? RoutingValue { get; private set; }

		internal long? VersionValue { get; private set; }

		public PercolateQueryDescriptor<T> Document(object? document) => Assign(document, (a, v) => a.DocumentValue = v);
		public PercolateQueryDescriptor<T> Documents(IEnumerable<object>? documents) => Assign(documents, (a, v) => a.DocumentsValue = v);
		public PercolateQueryDescriptor<T> Field(string field) => Assign(field, (a, v) => a.FieldValue = v);
		public PercolateQueryDescriptor<T> Id(Elastic.Clients.Elasticsearch.Id? id) => Assign(id, (a, v) => a.IdValue = v);
		public PercolateQueryDescriptor<T> Index(Elastic.Clients.Elasticsearch.IndexName? index) => Assign(index, (a, v) => a.IndexValue = v);
		public PercolateQueryDescriptor<T> Name(string? name) => Assign(name, (a, v) => a.NameValue = v);
		public PercolateQueryDescriptor<T> Preference(string? preference) => Assign(preference, (a, v) => a.PreferenceValue = v);
		public PercolateQueryDescriptor<T> Routing(string? routing) => Assign(routing, (a, v) => a.RoutingValue = v);
		public PercolateQueryDescriptor<T> Version(long? version) => Assign(version, (a, v) => a.VersionValue = v);
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (DocumentsValue is not null)
			{
				writer.WritePropertyName("documents");
				JsonSerializer.Serialize(writer, DocumentsValue, options);
			}

			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
			if (IdValue is not null)
			{
				writer.WritePropertyName("id");
				JsonSerializer.Serialize(writer, IdValue, options);
			}

			if (IndexValue is not null)
			{
				writer.WritePropertyName("index");
				JsonSerializer.Serialize(writer, IndexValue, options);
			}

			if (!string.IsNullOrEmpty(NameValue))
			{
				writer.WritePropertyName("name");
				writer.WriteStringValue(NameValue);
			}

			if (!string.IsNullOrEmpty(PreferenceValue))
			{
				writer.WritePropertyName("preference");
				writer.WriteStringValue(PreferenceValue);
			}

			if (RoutingValue is not null)
			{
				writer.WritePropertyName("routing");
				JsonSerializer.Serialize(writer, RoutingValue, options);
			}

			if (VersionValue is not null)
			{
				writer.WritePropertyName("version");
				JsonSerializer.Serialize(writer, VersionValue, options);
			}

			writer.WriteEndObject();
		}
	}
}