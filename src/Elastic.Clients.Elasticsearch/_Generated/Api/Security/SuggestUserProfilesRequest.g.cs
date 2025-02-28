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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Security;

public sealed partial class SuggestUserProfilesRequestParameters : RequestParameters
{
}

internal sealed partial class SuggestUserProfilesRequestConverter : System.Text.Json.Serialization.JsonConverter<SuggestUserProfilesRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropData = System.Text.Json.JsonEncodedText.Encode("data");
	private static readonly System.Text.Json.JsonEncodedText PropHint = System.Text.Json.JsonEncodedText.Encode("hint");
	private static readonly System.Text.Json.JsonEncodedText PropName = System.Text.Json.JsonEncodedText.Encode("name");
	private static readonly System.Text.Json.JsonEncodedText PropSize = System.Text.Json.JsonEncodedText.Encode("size");

	public override SuggestUserProfilesRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<ICollection<string>?> propData = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Security.Hint?> propHint = default;
		LocalJsonValue<string?> propName = default;
		LocalJsonValue<long?> propSize = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propData.TryReadProperty(ref reader, options, PropData, static ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadSingleOrManyCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propHint.TryReadProperty(ref reader, options, PropHint, null))
			{
				continue;
			}

			if (propName.TryReadProperty(ref reader, options, PropName, null))
			{
				continue;
			}

			if (propSize.TryReadProperty(ref reader, options, PropSize, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new SuggestUserProfilesRequest
		{
			Data = propData.Value
,
			Hint = propHint.Value
,
			Name = propName.Value
,
			Size = propSize.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, SuggestUserProfilesRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropData, value.Data, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, ICollection<string>? v) => w.WriteSingleOrManyCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropHint, value.Hint, null, null);
		writer.WriteProperty(options, PropName, value.Name, null, null);
		writer.WriteProperty(options, PropSize, value.Size, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Suggest a user profile.
/// </para>
/// <para>
/// Get suggestions for user profiles that match specified search criteria.
/// </para>
/// </summary>
[JsonConverter(typeof(SuggestUserProfilesRequestConverter))]
public sealed partial class SuggestUserProfilesRequest : PlainRequest<SuggestUserProfilesRequestParameters>
{
	[JsonConstructor]
	internal SuggestUserProfilesRequest()
	{
	}

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
	public ICollection<string>? Data { get; set; }

	/// <summary>
	/// <para>
	/// Extra search criteria to improve relevance of the suggestion result.
	/// Profiles matching the spcified hint are ranked higher in the response.
	/// Profiles not matching the hint don't exclude the profile from the response
	/// as long as the profile matches the <c>name</c> field query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.Hint? Hint { get; set; }

	/// <summary>
	/// <para>
	/// Query string used to match name-related fields in user profile documents.
	/// Name-related fields are the user's <c>username</c>, <c>full_name</c>, and <c>email</c>.
	/// </para>
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// <para>
	/// Number of profiles to return.
	/// </para>
	/// </summary>
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
	private Elastic.Clients.Elasticsearch.Security.Hint? HintValue { get; set; }
	private Elastic.Clients.Elasticsearch.Security.HintDescriptor HintDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Security.HintDescriptor> HintDescriptorAction { get; set; }
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
	public SuggestUserProfilesRequestDescriptor Hint(Elastic.Clients.Elasticsearch.Security.Hint? hint)
	{
		HintDescriptor = null;
		HintDescriptorAction = null;
		HintValue = hint;
		return Self;
	}

	public SuggestUserProfilesRequestDescriptor Hint(Elastic.Clients.Elasticsearch.Security.HintDescriptor descriptor)
	{
		HintValue = null;
		HintDescriptorAction = null;
		HintDescriptor = descriptor;
		return Self;
	}

	public SuggestUserProfilesRequestDescriptor Hint(Action<Elastic.Clients.Elasticsearch.Security.HintDescriptor> configure)
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Security.HintDescriptor(HintDescriptorAction), options);
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