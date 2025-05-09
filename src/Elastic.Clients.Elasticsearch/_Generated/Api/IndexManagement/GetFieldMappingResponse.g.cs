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

#nullable restore

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.IndexManagement;

internal sealed partial class GetFieldMappingResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.GetFieldMappingResponse>
{
	public override Elastic.Clients.Elasticsearch.IndexManagement.GetFieldMappingResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return new Elastic.Clients.Elasticsearch.IndexManagement.GetFieldMappingResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance) { FieldMappings = reader.ReadValue<System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.TypeFieldMappings>>(options, static System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.TypeFieldMappings> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.IndexManagement.TypeFieldMappings>(o, null, null)!) };
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.GetFieldMappingResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteValue(options, value.FieldMappings, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.TypeFieldMappings> v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.IndexManagement.TypeFieldMappings>(o, v, null, null));
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.GetFieldMappingResponseConverter))]
public partial class GetFieldMappingResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetFieldMappingResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetFieldMappingResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
required
#endif
System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.TypeFieldMappings> FieldMappings { get; set; }
}