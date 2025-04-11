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

internal sealed partial class ApplicationGlobalUserPrivilegesConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivileges>
{
	private static readonly System.Text.Json.JsonEncodedText PropManage = System.Text.Json.JsonEncodedText.Encode("manage");

	public override Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivileges Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Security.ManageUserPrivileges> propManage = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propManage.TryReadProperty(ref reader, options, PropManage, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivileges(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Manage = propManage.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivileges value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropManage, value.Manage, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivilegesConverter))]
public sealed partial class ApplicationGlobalUserPrivileges
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ApplicationGlobalUserPrivileges(Elastic.Clients.Elasticsearch.Security.ManageUserPrivileges manage)
	{
		Manage = manage;
	}
#if NET7_0_OR_GREATER
	public ApplicationGlobalUserPrivileges()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ApplicationGlobalUserPrivileges()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ApplicationGlobalUserPrivileges(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Security.ManageUserPrivileges Manage { get; set; }
}

public readonly partial struct ApplicationGlobalUserPrivilegesDescriptor
{
	internal Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivileges Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ApplicationGlobalUserPrivilegesDescriptor(Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivileges instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ApplicationGlobalUserPrivilegesDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivileges(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivilegesDescriptor(Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivileges instance) => new Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivilegesDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivileges(Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivilegesDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivilegesDescriptor Manage(Elastic.Clients.Elasticsearch.Security.ManageUserPrivileges value)
	{
		Instance.Manage = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivilegesDescriptor Manage(System.Action<Elastic.Clients.Elasticsearch.Security.ManageUserPrivilegesDescriptor> action)
	{
		Instance.Manage = Elastic.Clients.Elasticsearch.Security.ManageUserPrivilegesDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivileges Build(System.Action<Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivilegesDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivilegesDescriptor(new Elastic.Clients.Elasticsearch.Security.ApplicationGlobalUserPrivileges(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}