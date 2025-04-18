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

internal sealed partial class ResolveClusterResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.ResolveClusterResponse>
{
	public override Elastic.Clients.Elasticsearch.IndexManagement.ResolveClusterResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return new Elastic.Clients.Elasticsearch.IndexManagement.ResolveClusterResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance) { Infos = reader.ReadValue<System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.ResolveClusterInfo>>(options, static System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.ResolveClusterInfo> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.IndexManagement.ResolveClusterInfo>(o, null, null)!) };
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.ResolveClusterResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteValue(options, value.Infos, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.ResolveClusterInfo> v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.IndexManagement.ResolveClusterInfo>(o, v, null, null));
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.ResolveClusterResponseConverter))]
public sealed partial class ResolveClusterResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ResolveClusterResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ResolveClusterResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
required
#endif
System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.ResolveClusterInfo> Infos { get; set; }
}