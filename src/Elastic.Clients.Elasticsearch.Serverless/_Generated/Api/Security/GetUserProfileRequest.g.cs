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

public sealed partial class GetUserProfileRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// List of filters for the <c>data</c> field of the profile document.
	/// To return all content use <c>data=*</c>. To return a subset of content
	/// use <c>data=&lt;key></c> to retrieve content nested under the specified <c>&lt;key></c>.
	/// By default returns no <c>data</c> content.
	/// </para>
	/// </summary>
	public ICollection<string>? Data { get => Q<ICollection<string>?>("data"); set => Q("data", value); }
}

/// <summary>
/// <para>
/// Retrieves a user's profile using the unique profile ID.
/// </para>
/// </summary>
public sealed partial class GetUserProfileRequest : PlainRequest<GetUserProfileRequestParameters>
{
	public GetUserProfileRequest(IReadOnlyCollection<string> uid) : base(r => r.Required("uid", uid))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityGetUserProfile;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.get_user_profile";

	/// <summary>
	/// <para>
	/// List of filters for the <c>data</c> field of the profile document.
	/// To return all content use <c>data=*</c>. To return a subset of content
	/// use <c>data=&lt;key></c> to retrieve content nested under the specified <c>&lt;key></c>.
	/// By default returns no <c>data</c> content.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public ICollection<string>? Data { get => Q<ICollection<string>?>("data"); set => Q("data", value); }
}

/// <summary>
/// <para>
/// Retrieves a user's profile using the unique profile ID.
/// </para>
/// </summary>
public sealed partial class GetUserProfileRequestDescriptor : RequestDescriptor<GetUserProfileRequestDescriptor, GetUserProfileRequestParameters>
{
	internal GetUserProfileRequestDescriptor(Action<GetUserProfileRequestDescriptor> configure) => configure.Invoke(this);

	public GetUserProfileRequestDescriptor(IReadOnlyCollection<string> uid) : base(r => r.Required("uid", uid))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityGetUserProfile;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.get_user_profile";

	public GetUserProfileRequestDescriptor Data(ICollection<string>? data) => Qs("data", data);

	public GetUserProfileRequestDescriptor Uid(IReadOnlyCollection<string> uid)
	{
		RouteValues.Required("uid", uid);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}