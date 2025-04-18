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

namespace Elastic.Clients.Elasticsearch.SearchableSnapshots;

internal sealed partial class ClearCacheResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.SearchableSnapshots.ClearCacheResponse>
{
	public override Elastic.Clients.Elasticsearch.SearchableSnapshots.ClearCacheResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return new Elastic.Clients.Elasticsearch.SearchableSnapshots.ClearCacheResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance) { Result = reader.ReadValue<object>(options, null) };
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.SearchableSnapshots.ClearCacheResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteValue(options, value.Result, null);
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.SearchableSnapshots.ClearCacheResponseConverter))]
public sealed partial class ClearCacheResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ClearCacheResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ClearCacheResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
required
#endif
object Result { get; set; }
}