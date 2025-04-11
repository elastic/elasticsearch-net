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

namespace Elastic.Clients.Elasticsearch.SnapshotLifecycleManagement;

internal sealed partial class GetLifecycleResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.SnapshotLifecycleManagement.GetLifecycleResponse>
{
	public override Elastic.Clients.Elasticsearch.SnapshotLifecycleManagement.GetLifecycleResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return new Elastic.Clients.Elasticsearch.SnapshotLifecycleManagement.GetLifecycleResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance) { Lifecycles = reader.ReadValue<System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.SnapshotLifecycleManagement.SnapshotLifecycle>>(options, static System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.SnapshotLifecycleManagement.SnapshotLifecycle> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.SnapshotLifecycleManagement.SnapshotLifecycle>(o, null, null)!) };
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.SnapshotLifecycleManagement.GetLifecycleResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteValue(options, value.Lifecycles, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.SnapshotLifecycleManagement.SnapshotLifecycle> v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.SnapshotLifecycleManagement.SnapshotLifecycle>(o, v, null, null));
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.SnapshotLifecycleManagement.GetLifecycleResponseConverter))]
public sealed partial class GetLifecycleResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetLifecycleResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetLifecycleResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
required
#endif
System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.SnapshotLifecycleManagement.SnapshotLifecycle> Lifecycles { get; set; }
}