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

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Requests;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.Security;

public sealed partial class AuthenticateRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Authenticate a user.
/// Authenticates a user and returns information about the authenticated user.
/// Include the user information in a <a href="https://en.wikipedia.org/wiki/Basic_access_authentication">basic auth header</a>.
/// A successful call returns a JSON structure that shows user information such as their username, the roles that are assigned to the user, any assigned metadata, and information about the realms that authenticated and authorized the user.
/// If the user cannot be authenticated, this API returns a 401 status code.
/// </para>
/// </summary>
public sealed partial class AuthenticateRequest : PlainRequest<AuthenticateRequestParameters>
{
	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityAuthenticate;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.authenticate";
}

/// <summary>
/// <para>
/// Authenticate a user.
/// Authenticates a user and returns information about the authenticated user.
/// Include the user information in a <a href="https://en.wikipedia.org/wiki/Basic_access_authentication">basic auth header</a>.
/// A successful call returns a JSON structure that shows user information such as their username, the roles that are assigned to the user, any assigned metadata, and information about the realms that authenticated and authorized the user.
/// If the user cannot be authenticated, this API returns a 401 status code.
/// </para>
/// </summary>
public sealed partial class AuthenticateRequestDescriptor : RequestDescriptor<AuthenticateRequestDescriptor, AuthenticateRequestParameters>
{
	internal AuthenticateRequestDescriptor(Action<AuthenticateRequestDescriptor> configure) => configure.Invoke(this);

	public AuthenticateRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityAuthenticate;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.authenticate";

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}