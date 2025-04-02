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

public sealed partial class DelegatePkiRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class DelegatePkiRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.DelegatePkiRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropX509CertificateChain = System.Text.Json.JsonEncodedText.Encode("x509_certificate_chain");

	public override Elastic.Clients.Elasticsearch.Security.DelegatePkiRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.ICollection<string>> propX509CertificateChain = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propX509CertificateChain.TryReadProperty(ref reader, options, PropX509CertificateChain, static System.Collections.Generic.ICollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)!))
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
		return new Elastic.Clients.Elasticsearch.Security.DelegatePkiRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			X509CertificateChain = propX509CertificateChain.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.DelegatePkiRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropX509CertificateChain, value.X509CertificateChain, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string> v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Delegate PKI authentication.
/// </para>
/// <para>
/// This API implements the exchange of an X509Certificate chain for an Elasticsearch access token.
/// The certificate chain is validated, according to RFC 5280, by sequentially considering the trust configuration of every installed PKI realm that has <c>delegation.enabled</c> set to <c>true</c>.
/// A successfully trusted client certificate is also subject to the validation of the subject distinguished name according to thw <c>username_pattern</c> of the respective realm.
/// </para>
/// <para>
/// This API is called by smart and trusted proxies, such as Kibana, which terminate the user's TLS session but still want to authenticate the user by using a PKI realm—-​as if the user connected directly to Elasticsearch.
/// </para>
/// <para>
/// IMPORTANT: The association between the subject public key in the target certificate and the corresponding private key is not validated.
/// This is part of the TLS authentication process and it is delegated to the proxy that calls this API.
/// The proxy is trusted to have performed the TLS authentication and this API translates that authentication into an Elasticsearch access token.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.DelegatePkiRequestConverter))]
public sealed partial class DelegatePkiRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Security.DelegatePkiRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DelegatePkiRequest(System.Collections.Generic.ICollection<string> x509CertificateChain)
	{
		X509CertificateChain = x509CertificateChain;
	}
#if NET7_0_OR_GREATER
	public DelegatePkiRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The request contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public DelegatePkiRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DelegatePkiRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SecurityDelegatePki;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.delegate_pki";

	/// <summary>
	/// <para>
	/// The X509Certificate chain, which is represented as an ordered string array.
	/// Each string in the array is a base64-encoded (Section 4 of RFC4648 - not base64url-encoded) of the certificate's DER encoding.
	/// </para>
	/// <para>
	/// The first element is the target certificate that contains the subject distinguished name that is requesting access.
	/// This may be followed by additional certificates; each subsequent certificate is used to certify the previous one.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.ICollection<string> X509CertificateChain { get; set; }
}

/// <summary>
/// <para>
/// Delegate PKI authentication.
/// </para>
/// <para>
/// This API implements the exchange of an X509Certificate chain for an Elasticsearch access token.
/// The certificate chain is validated, according to RFC 5280, by sequentially considering the trust configuration of every installed PKI realm that has <c>delegation.enabled</c> set to <c>true</c>.
/// A successfully trusted client certificate is also subject to the validation of the subject distinguished name according to thw <c>username_pattern</c> of the respective realm.
/// </para>
/// <para>
/// This API is called by smart and trusted proxies, such as Kibana, which terminate the user's TLS session but still want to authenticate the user by using a PKI realm—-​as if the user connected directly to Elasticsearch.
/// </para>
/// <para>
/// IMPORTANT: The association between the subject public key in the target certificate and the corresponding private key is not validated.
/// This is part of the TLS authentication process and it is delegated to the proxy that calls this API.
/// The proxy is trusted to have performed the TLS authentication and this API translates that authentication into an Elasticsearch access token.
/// </para>
/// </summary>
public readonly partial struct DelegatePkiRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Security.DelegatePkiRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DelegatePkiRequestDescriptor(Elastic.Clients.Elasticsearch.Security.DelegatePkiRequest instance)
	{
		Instance = instance;
	}

	public DelegatePkiRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.DelegatePkiRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Security.DelegatePkiRequestDescriptor(Elastic.Clients.Elasticsearch.Security.DelegatePkiRequest instance) => new Elastic.Clients.Elasticsearch.Security.DelegatePkiRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.DelegatePkiRequest(Elastic.Clients.Elasticsearch.Security.DelegatePkiRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The X509Certificate chain, which is represented as an ordered string array.
	/// Each string in the array is a base64-encoded (Section 4 of RFC4648 - not base64url-encoded) of the certificate's DER encoding.
	/// </para>
	/// <para>
	/// The first element is the target certificate that contains the subject distinguished name that is requesting access.
	/// This may be followed by additional certificates; each subsequent certificate is used to certify the previous one.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.DelegatePkiRequestDescriptor X509CertificateChain(System.Collections.Generic.ICollection<string> value)
	{
		Instance.X509CertificateChain = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The X509Certificate chain, which is represented as an ordered string array.
	/// Each string in the array is a base64-encoded (Section 4 of RFC4648 - not base64url-encoded) of the certificate's DER encoding.
	/// </para>
	/// <para>
	/// The first element is the target certificate that contains the subject distinguished name that is requesting access.
	/// This may be followed by additional certificates; each subsequent certificate is used to certify the previous one.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.DelegatePkiRequestDescriptor X509CertificateChain()
	{
		Instance.X509CertificateChain = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The X509Certificate chain, which is represented as an ordered string array.
	/// Each string in the array is a base64-encoded (Section 4 of RFC4648 - not base64url-encoded) of the certificate's DER encoding.
	/// </para>
	/// <para>
	/// The first element is the target certificate that contains the subject distinguished name that is requesting access.
	/// This may be followed by additional certificates; each subsequent certificate is used to certify the previous one.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.DelegatePkiRequestDescriptor X509CertificateChain(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString>? action)
	{
		Instance.X509CertificateChain = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The X509Certificate chain, which is represented as an ordered string array.
	/// Each string in the array is a base64-encoded (Section 4 of RFC4648 - not base64url-encoded) of the certificate's DER encoding.
	/// </para>
	/// <para>
	/// The first element is the target certificate that contains the subject distinguished name that is requesting access.
	/// This may be followed by additional certificates; each subsequent certificate is used to certify the previous one.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.DelegatePkiRequestDescriptor X509CertificateChain(params string[] values)
	{
		Instance.X509CertificateChain = [.. values];
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Security.DelegatePkiRequest Build(System.Action<Elastic.Clients.Elasticsearch.Security.DelegatePkiRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Security.DelegatePkiRequestDescriptor(new Elastic.Clients.Elasticsearch.Security.DelegatePkiRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Security.DelegatePkiRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.DelegatePkiRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.DelegatePkiRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.DelegatePkiRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.DelegatePkiRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.DelegatePkiRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.DelegatePkiRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}