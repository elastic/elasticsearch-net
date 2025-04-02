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

namespace Elastic.Clients.Elasticsearch.Security;

internal sealed partial class GetPrivilegesResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.GetPrivilegesResponse>
{
	public override Elastic.Clients.Elasticsearch.Security.GetPrivilegesResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return new Elastic.Clients.Elasticsearch.Security.GetPrivilegesResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance) { Privileges = reader.ReadValue<System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions>>>(options, static System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions>> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions>>(o, null, static System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions>(o, null, null)!)!) };
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.GetPrivilegesResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteValue(options, value.Privileges, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions>> v) => w.WriteDictionaryValue<string, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions>>(o, v, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions> v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions>(o, v, null, null)));
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.GetPrivilegesResponseConverter))]
public sealed partial class GetPrivilegesResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetPrivilegesResponse(System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions>> privileges)
	{
		Privileges = privileges;
	}

	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetPrivilegesResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetPrivilegesResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
required
#endif
System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions>> Privileges { get; set; }
}