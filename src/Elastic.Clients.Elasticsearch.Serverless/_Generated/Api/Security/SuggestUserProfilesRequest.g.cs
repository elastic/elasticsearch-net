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

public sealed partial class SuggestUserProfilesRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Suggest a user profile.
/// </para>
/// <para>
/// Get suggestions for user profiles that match specified search criteria.
/// </para>
/// </summary>
public sealed partial class SuggestUserProfilesRequest : PlainRequest<SuggestUserProfilesRequestParameters>
{
	internal override ApiUrls ApiUrls => ApiUrlLookup.SecuritySuggestUserProfiles;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.suggest_user_profiles";

	/// <summary>
	/// <para>
	/// List of filters for the <c>data</c> field of the profile document.
	/// To return all content use <c>data=*</c>. To return a subset of content
	/// use <c>data=&lt;key></c> to retrieve content nested under the specified <c>&lt;key></c>.
	/// By default returns no <c>data</c> content.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("data")]
	[SingleOrManyCollectionConverter(typeof(string))]
	public ICollection<string>? Data { get; set; }

	/// <summary>
	/// <para>
	/// Extra search criteria to improve relevance of the suggestion result.
	/// Profiles matching the spcified hint are ranked higher in the response.
	/// Profiles not matching the hint don't exclude the profile from the response
	/// as long as the profile matches the <c>name</c> field query.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("hint")]
	public Elastic.Clients.Elasticsearch.Serverless.Security.Hint? Hint { get; set; }

	/// <summary>
	/// <para>
	/// Query string used to match name-related fields in user profile documents.
	/// Name-related fields are the user's <c>username</c>, <c>full_name</c>, and <c>email</c>.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("name")]
	public string? Name { get; set; }

	/// <summary>
	/// <para>
	/// Number of profiles to return.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("size")]
	public long? Size { get; set; }
}

/// <summary>
/// <para>
/// Suggest a user profile.
/// </para>
/// <para>
/// Get suggestions for user profiles that match specified search criteria.
/// </para>
/// </summary>
public sealed partial class SuggestUserProfilesRequestDescriptor : RequestDescriptor<SuggestUserProfilesRequestDescriptor, SuggestUserProfilesRequestParameters>
{
	internal SuggestUserProfilesRequestDescriptor(Action<SuggestUserProfilesRequestDescriptor> configure) => configure.Invoke(this);

	public SuggestUserProfilesRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecuritySuggestUserProfiles;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.suggest_user_profiles";

	private ICollection<string>? DataValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Security.Hint? HintValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Security.HintDescriptor HintDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.Security.HintDescriptor> HintDescriptorAction { get; set; }
	private string? NameValue { get; set; }
	private long? SizeValue { get; set; }

	/// <summary>
	/// <para>
	/// List of filters for the <c>data</c> field of the profile document.
	/// To return all content use <c>data=*</c>. To return a subset of content
	/// use <c>data=&lt;key></c> to retrieve content nested under the specified <c>&lt;key></c>.
	/// By default returns no <c>data</c> content.
	/// </para>
	/// </summary>
	public SuggestUserProfilesRequestDescriptor Data(ICollection<string>? data)
	{
		DataValue = data;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Extra search criteria to improve relevance of the suggestion result.
	/// Profiles matching the spcified hint are ranked higher in the response.
	/// Profiles not matching the hint don't exclude the profile from the response
	/// as long as the profile matches the <c>name</c> field query.
	/// </para>
	/// </summary>
	public SuggestUserProfilesRequestDescriptor Hint(Elastic.Clients.Elasticsearch.Serverless.Security.Hint? hint)
	{
		HintDescriptor = null;
		HintDescriptorAction = null;
		HintValue = hint;
		return Self;
	}

	public SuggestUserProfilesRequestDescriptor Hint(Elastic.Clients.Elasticsearch.Serverless.Security.HintDescriptor descriptor)
	{
		HintValue = null;
		HintDescriptorAction = null;
		HintDescriptor = descriptor;
		return Self;
	}

	public SuggestUserProfilesRequestDescriptor Hint(Action<Elastic.Clients.Elasticsearch.Serverless.Security.HintDescriptor> configure)
	{
		HintValue = null;
		HintDescriptor = null;
		HintDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Query string used to match name-related fields in user profile documents.
	/// Name-related fields are the user's <c>username</c>, <c>full_name</c>, and <c>email</c>.
	/// </para>
	/// </summary>
	public SuggestUserProfilesRequestDescriptor Name(string? name)
	{
		NameValue = name;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Number of profiles to return.
	/// </para>
	/// </summary>
	public SuggestUserProfilesRequestDescriptor Size(long? size)
	{
		SizeValue = size;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (DataValue is not null)
		{
			writer.WritePropertyName("data");
			SingleOrManySerializationHelper.Serialize<string>(DataValue, writer, options);
		}

		if (HintDescriptor is not null)
		{
			writer.WritePropertyName("hint");
			JsonSerializer.Serialize(writer, HintDescriptor, options);
		}
		else if (HintDescriptorAction is not null)
		{
			writer.WritePropertyName("hint");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.Security.HintDescriptor(HintDescriptorAction), options);
		}
		else if (HintValue is not null)
		{
			writer.WritePropertyName("hint");
			JsonSerializer.Serialize(writer, HintValue, options);
		}

		if (!string.IsNullOrEmpty(NameValue))
		{
			writer.WritePropertyName("name");
			writer.WriteStringValue(NameValue);
		}

		if (SizeValue.HasValue)
		{
			writer.WritePropertyName("size");
			writer.WriteNumberValue(SizeValue.Value);
		}

		writer.WriteEndObject();
	}
}