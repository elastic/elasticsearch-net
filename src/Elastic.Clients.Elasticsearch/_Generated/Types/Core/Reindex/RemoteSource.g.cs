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

namespace Elastic.Clients.Elasticsearch.Core.Reindex;

internal sealed partial class RemoteSourceConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSource>
{
	private static readonly System.Text.Json.JsonEncodedText PropConnectTimeout = System.Text.Json.JsonEncodedText.Encode("connect_timeout");
	private static readonly System.Text.Json.JsonEncodedText PropHeaders = System.Text.Json.JsonEncodedText.Encode("headers");
	private static readonly System.Text.Json.JsonEncodedText PropHost = System.Text.Json.JsonEncodedText.Encode("host");
	private static readonly System.Text.Json.JsonEncodedText PropPassword = System.Text.Json.JsonEncodedText.Encode("password");
	private static readonly System.Text.Json.JsonEncodedText PropSocketTimeout = System.Text.Json.JsonEncodedText.Encode("socket_timeout");
	private static readonly System.Text.Json.JsonEncodedText PropUsername = System.Text.Json.JsonEncodedText.Encode("username");

	public override Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSource Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propConnectTimeout = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<string, string>?> propHeaders = default;
		LocalJsonValue<string> propHost = default;
		LocalJsonValue<string?> propPassword = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propSocketTimeout = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Username?> propUsername = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propConnectTimeout.TryReadProperty(ref reader, options, PropConnectTimeout, null))
			{
				continue;
			}

			if (propHeaders.TryReadProperty(ref reader, options, PropHeaders, static System.Collections.Generic.IDictionary<string, string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, string>(o, null, null)))
			{
				continue;
			}

			if (propHost.TryReadProperty(ref reader, options, PropHost, null))
			{
				continue;
			}

			if (propPassword.TryReadProperty(ref reader, options, PropPassword, null))
			{
				continue;
			}

			if (propSocketTimeout.TryReadProperty(ref reader, options, PropSocketTimeout, null))
			{
				continue;
			}

			if (propUsername.TryReadProperty(ref reader, options, PropUsername, null))
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
		return new Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSource(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ConnectTimeout = propConnectTimeout.Value,
			Headers = propHeaders.Value,
			Host = propHost.Value,
			Password = propPassword.Value,
			SocketTimeout = propSocketTimeout.Value,
			Username = propUsername.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSource value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropConnectTimeout, value.ConnectTimeout, null, null);
		writer.WriteProperty(options, PropHeaders, value.Headers, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, string>? v) => w.WriteDictionaryValue<string, string>(o, v, null, null));
		writer.WriteProperty(options, PropHost, value.Host, null, null);
		writer.WriteProperty(options, PropPassword, value.Password, null, null);
		writer.WriteProperty(options, PropSocketTimeout, value.SocketTimeout, null, null);
		writer.WriteProperty(options, PropUsername, value.Username, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceConverter))]
public sealed partial class RemoteSource
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RemoteSource(string host)
	{
		Host = host;
	}
#if NET7_0_OR_GREATER
	public RemoteSource()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public RemoteSource()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal RemoteSource(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The remote connection timeout.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? ConnectTimeout { get; set; }

	/// <summary>
	/// <para>
	/// An object containing the headers of the request.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IDictionary<string, string>? Headers { get; set; }

	/// <summary>
	/// <para>
	/// The URL for the remote instance of Elasticsearch that you want to index from.
	/// This information is required when you're indexing from remote.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Host { get; set; }

	/// <summary>
	/// <para>
	/// The password to use for authentication with the remote host.
	/// </para>
	/// </summary>
	public string? Password { get; set; }

	/// <summary>
	/// <para>
	/// The remote socket read timeout.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? SocketTimeout { get; set; }

	/// <summary>
	/// <para>
	/// The username to use for authentication with the remote host.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Username? Username { get; set; }
}

public readonly partial struct RemoteSourceDescriptor
{
	internal Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSource Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RemoteSourceDescriptor(Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSource instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RemoteSourceDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSource(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor(Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSource instance) => new Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSource(Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The remote connection timeout.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor ConnectTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.ConnectTimeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// An object containing the headers of the request.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor Headers(System.Collections.Generic.IDictionary<string, string>? value)
	{
		Instance.Headers = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// An object containing the headers of the request.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor Headers()
	{
		Instance.Headers = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// An object containing the headers of the request.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor Headers(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString>? action)
	{
		Instance.Headers = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor AddHeader(string key, string value)
	{
		Instance.Headers ??= new System.Collections.Generic.Dictionary<string, string>();
		Instance.Headers.Add(key, value);
		return this;
	}

	/// <summary>
	/// <para>
	/// The URL for the remote instance of Elasticsearch that you want to index from.
	/// This information is required when you're indexing from remote.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor Host(string value)
	{
		Instance.Host = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The password to use for authentication with the remote host.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor Password(string? value)
	{
		Instance.Password = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The remote socket read timeout.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor SocketTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.SocketTimeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The username to use for authentication with the remote host.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor Username(Elastic.Clients.Elasticsearch.Username? value)
	{
		Instance.Username = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSource Build(System.Action<Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSourceDescriptor(new Elastic.Clients.Elasticsearch.Core.Reindex.RemoteSource(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}