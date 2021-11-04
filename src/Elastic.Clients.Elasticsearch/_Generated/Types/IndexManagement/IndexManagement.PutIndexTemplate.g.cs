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
namespace Elastic.Clients.Elasticsearch.IndexManagement.PutIndexTemplate
{
	public partial class IndexTemplateMapping
	{
		[JsonInclude]
		[JsonPropertyName("aliases")]
		public Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>? Aliases { get; set; }

		[JsonInclude]
		[JsonPropertyName("mappings")]
		public Elastic.Clients.Elasticsearch.Mapping.TypeMapping? Mappings { get; set; }

		[JsonInclude]
		[JsonPropertyName("settings")]
		public Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings? Settings { get; set; }
	}

	[JsonConverter(typeof(IndexTemplateMappingDescriptorConverter))]
	public sealed partial class IndexTemplateMappingDescriptor : DescriptorBase<IndexTemplateMappingDescriptor>
	{
		internal Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>? _aliases;
		internal Elastic.Clients.Elasticsearch.Mapping.TypeMapping? _mappings;
		internal Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings? _settings;
	}

	internal sealed class IndexTemplateMappingDescriptorConverter : JsonConverter<IndexTemplateMappingDescriptor>
	{
		public override IndexTemplateMappingDescriptor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
		public override void Write(Utf8JsonWriter writer, IndexTemplateMappingDescriptor value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			if (value._aliases is not null)
			{
				writer.WritePropertyName("aliases");
				JsonSerializer.Serialize(writer, value._aliases, options);
			}

			if (value._mappings is not null)
			{
				writer.WritePropertyName("mappings");
				JsonSerializer.Serialize(writer, value._mappings, options);
			}

			if (value._settings is not null)
			{
				writer.WritePropertyName("settings");
				JsonSerializer.Serialize(writer, value._settings, options);
			}

			writer.WriteEndObject();
		}
	}
}