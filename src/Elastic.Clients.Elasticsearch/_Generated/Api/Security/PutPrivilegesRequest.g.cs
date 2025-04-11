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

public sealed partial class PutPrivilegesRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// If <c>true</c> (the default) then refresh the affected shards to make this operation visible to search, if <c>wait_for</c> then wait for a refresh to make this operation visible to search, if <c>false</c> then do nothing with refreshes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }
}

internal sealed partial class PutPrivilegesRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequest>
{
	public override Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return new Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance) { Privileges = reader.ReadValue<System.Collections.Generic.IDictionary<string, System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions>>>(options, static System.Collections.Generic.IDictionary<string, System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions>> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions>>(o, null, static System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions>(o, null, null)!)!) };
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteValue(options, value.Privileges, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions>> v) => w.WriteDictionaryValue<string, System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions>>(o, v, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions> v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions>(o, v, null, null)));
	}
}

/// <summary>
/// <para>
/// Create or update application privileges.
/// </para>
/// <para>
/// To use this API, you must have one of the following privileges:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// The <c>manage_security</c> cluster privilege (or a greater privilege such as <c>all</c>).
/// </para>
/// </item>
/// <item>
/// <para>
/// The "Manage Application Privileges" global privilege for the application being referenced in the request.
/// </para>
/// </item>
/// </list>
/// <para>
/// Application names are formed from a prefix, with an optional suffix that conform to the following rules:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// The prefix must begin with a lowercase ASCII letter.
/// </para>
/// </item>
/// <item>
/// <para>
/// The prefix must contain only ASCII letters or digits.
/// </para>
/// </item>
/// <item>
/// <para>
/// The prefix must be at least 3 characters long.
/// </para>
/// </item>
/// <item>
/// <para>
/// If the suffix exists, it must begin with either a dash <c>-</c> or <c>_</c>.
/// </para>
/// </item>
/// <item>
/// <para>
/// The suffix cannot contain any of the following characters: <c>\</c>, <c>/</c>, <c>*</c>, <c>?</c>, <c>"</c>, <c>&lt;</c>, <c>></c>, <c>|</c>, <c>,</c>, <c>*</c>.
/// </para>
/// </item>
/// <item>
/// <para>
/// No part of the name can contain whitespace.
/// </para>
/// </item>
/// </list>
/// <para>
/// Privilege names must begin with a lowercase ASCII letter and must contain only ASCII letters and digits along with the characters <c>_</c>, <c>-</c>, and <c>.</c>.
/// </para>
/// <para>
/// Action names can contain any number of printable ASCII characters and must contain at least one of the following characters: <c>/</c>, <c>*</c>, <c>:</c>.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequestConverter))]
public sealed partial class PutPrivilegesRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutPrivilegesRequest(System.Collections.Generic.IDictionary<string, System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions>> privileges)
	{
		Privileges = privileges;
	}
#if NET7_0_OR_GREATER
	public PutPrivilegesRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The request contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public PutPrivilegesRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PutPrivilegesRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SecurityPutPrivileges;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.put_privileges";

	/// <summary>
	/// <para>
	/// If <c>true</c> (the default) then refresh the affected shards to make this operation visible to search, if <c>wait_for</c> then wait for a refresh to make this operation visible to search, if <c>false</c> then do nothing with refreshes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IDictionary<string, System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions>> Privileges { get; set; }
}

/// <summary>
/// <para>
/// Create or update application privileges.
/// </para>
/// <para>
/// To use this API, you must have one of the following privileges:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// The <c>manage_security</c> cluster privilege (or a greater privilege such as <c>all</c>).
/// </para>
/// </item>
/// <item>
/// <para>
/// The "Manage Application Privileges" global privilege for the application being referenced in the request.
/// </para>
/// </item>
/// </list>
/// <para>
/// Application names are formed from a prefix, with an optional suffix that conform to the following rules:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// The prefix must begin with a lowercase ASCII letter.
/// </para>
/// </item>
/// <item>
/// <para>
/// The prefix must contain only ASCII letters or digits.
/// </para>
/// </item>
/// <item>
/// <para>
/// The prefix must be at least 3 characters long.
/// </para>
/// </item>
/// <item>
/// <para>
/// If the suffix exists, it must begin with either a dash <c>-</c> or <c>_</c>.
/// </para>
/// </item>
/// <item>
/// <para>
/// The suffix cannot contain any of the following characters: <c>\</c>, <c>/</c>, <c>*</c>, <c>?</c>, <c>"</c>, <c>&lt;</c>, <c>></c>, <c>|</c>, <c>,</c>, <c>*</c>.
/// </para>
/// </item>
/// <item>
/// <para>
/// No part of the name can contain whitespace.
/// </para>
/// </item>
/// </list>
/// <para>
/// Privilege names must begin with a lowercase ASCII letter and must contain only ASCII letters and digits along with the characters <c>_</c>, <c>-</c>, and <c>.</c>.
/// </para>
/// <para>
/// Action names can contain any number of printable ASCII characters and must contain at least one of the following characters: <c>/</c>, <c>*</c>, <c>:</c>.
/// </para>
/// </summary>
public readonly partial struct PutPrivilegesRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutPrivilegesRequestDescriptor(Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequest instance)
	{
		Instance = instance;
	}

	public PutPrivilegesRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequestDescriptor(Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequest instance) => new Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequest(Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// If <c>true</c> (the default) then refresh the affected shards to make this operation visible to search, if <c>wait_for</c> then wait for a refresh to make this operation visible to search, if <c>false</c> then do nothing with refreshes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequestDescriptor Refresh(Elastic.Clients.Elasticsearch.Refresh? value)
	{
		Instance.Refresh = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequestDescriptor Privileges(System.Collections.Generic.IDictionary<string, System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions>> value)
	{
		Instance.Privileges = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequestDescriptor Privileges()
	{
		Instance.Privileges = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringDictionaryOfStringPrivilegeActions.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequestDescriptor Privileges(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringDictionaryOfStringPrivilegeActions>? action)
	{
		Instance.Privileges = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringDictionaryOfStringPrivilegeActions.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequestDescriptor AddPrivilege(string key, System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions> value)
	{
		Instance.Privileges ??= new System.Collections.Generic.Dictionary<string, System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions>>();
		Instance.Privileges.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequestDescriptor AddPrivilege(string key)
	{
		Instance.Privileges ??= new System.Collections.Generic.Dictionary<string, System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions>>();
		Instance.Privileges.Add(key, Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringPrivilegeActions.Build(null));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequestDescriptor AddPrivilege(string key, System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringPrivilegeActions>? action)
	{
		Instance.Privileges ??= new System.Collections.Generic.Dictionary<string, System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Security.PrivilegeActions>>();
		Instance.Privileges.Add(key, Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringPrivilegeActions.Build(action));
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequest Build(System.Action<Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequestDescriptor(new Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.PutPrivilegesRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}