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

public sealed partial class ChangePasswordRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// If <c>true</c> (the default) then refresh the affected shards to make this operation visible to search, if <c>wait_for</c> then wait for a refresh to make this operation visible to search, if <c>false</c> then do nothing with refreshes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }
}

internal sealed partial class ChangePasswordRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.ChangePasswordRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropPassword = System.Text.Json.JsonEncodedText.Encode("password");
	private static readonly System.Text.Json.JsonEncodedText PropPasswordHash = System.Text.Json.JsonEncodedText.Encode("password_hash");

	public override Elastic.Clients.Elasticsearch.Security.ChangePasswordRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propPassword = default;
		LocalJsonValue<string?> propPasswordHash = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propPassword.TryReadProperty(ref reader, options, PropPassword, null))
			{
				continue;
			}

			if (propPasswordHash.TryReadProperty(ref reader, options, PropPasswordHash, null))
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
		return new Elastic.Clients.Elasticsearch.Security.ChangePasswordRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Password = propPassword.Value,
			PasswordHash = propPasswordHash.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.ChangePasswordRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropPassword, value.Password, null, null);
		writer.WriteProperty(options, PropPasswordHash, value.PasswordHash, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Change passwords.
/// </para>
/// <para>
/// Change the passwords of users in the native realm and built-in users.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.ChangePasswordRequestConverter))]
public sealed partial class ChangePasswordRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Security.ChangePasswordRequestParameters>
{
	public ChangePasswordRequest(Elastic.Clients.Elasticsearch.Username? username) : base(r => r.Optional("username", username))
	{
	}
#if NET7_0_OR_GREATER
	public ChangePasswordRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public ChangePasswordRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ChangePasswordRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SecurityChangePassword;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.change_password";

	/// <summary>
	/// <para>
	/// The user whose password you want to change. If you do not specify this
	/// parameter, the password is changed for the current user.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Username? Username { get => P<Elastic.Clients.Elasticsearch.Username?>("username"); set => PO("username", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c> (the default) then refresh the affected shards to make this operation visible to search, if <c>wait_for</c> then wait for a refresh to make this operation visible to search, if <c>false</c> then do nothing with refreshes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }

	/// <summary>
	/// <para>
	/// The new password value. Passwords must be at least 6 characters long.
	/// </para>
	/// </summary>
	public string? Password { get; set; }

	/// <summary>
	/// <para>
	/// A hash of the new password value. This must be produced using the same
	/// hashing algorithm as has been configured for password storage. For more details,
	/// see the explanation of the <c>xpack.security.authc.password_hashing.algorithm</c>
	/// setting.
	/// </para>
	/// </summary>
	public string? PasswordHash { get; set; }
}

/// <summary>
/// <para>
/// Change passwords.
/// </para>
/// <para>
/// Change the passwords of users in the native realm and built-in users.
/// </para>
/// </summary>
public readonly partial struct ChangePasswordRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Security.ChangePasswordRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ChangePasswordRequestDescriptor(Elastic.Clients.Elasticsearch.Security.ChangePasswordRequest instance)
	{
		Instance = instance;
	}

	public ChangePasswordRequestDescriptor(Elastic.Clients.Elasticsearch.Username? username)
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.ChangePasswordRequest(username);
	}

	public ChangePasswordRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.ChangePasswordRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Security.ChangePasswordRequestDescriptor(Elastic.Clients.Elasticsearch.Security.ChangePasswordRequest instance) => new Elastic.Clients.Elasticsearch.Security.ChangePasswordRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.ChangePasswordRequest(Elastic.Clients.Elasticsearch.Security.ChangePasswordRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The user whose password you want to change. If you do not specify this
	/// parameter, the password is changed for the current user.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.ChangePasswordRequestDescriptor Username(Elastic.Clients.Elasticsearch.Username? value)
	{
		Instance.Username = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> (the default) then refresh the affected shards to make this operation visible to search, if <c>wait_for</c> then wait for a refresh to make this operation visible to search, if <c>false</c> then do nothing with refreshes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.ChangePasswordRequestDescriptor Refresh(Elastic.Clients.Elasticsearch.Refresh? value)
	{
		Instance.Refresh = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The new password value. Passwords must be at least 6 characters long.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.ChangePasswordRequestDescriptor Password(string? value)
	{
		Instance.Password = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A hash of the new password value. This must be produced using the same
	/// hashing algorithm as has been configured for password storage. For more details,
	/// see the explanation of the <c>xpack.security.authc.password_hashing.algorithm</c>
	/// setting.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.ChangePasswordRequestDescriptor PasswordHash(string? value)
	{
		Instance.PasswordHash = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Security.ChangePasswordRequest Build(System.Action<Elastic.Clients.Elasticsearch.Security.ChangePasswordRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Security.ChangePasswordRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Security.ChangePasswordRequestDescriptor(new Elastic.Clients.Elasticsearch.Security.ChangePasswordRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Security.ChangePasswordRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.ChangePasswordRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.ChangePasswordRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.ChangePasswordRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.ChangePasswordRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.ChangePasswordRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.ChangePasswordRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}