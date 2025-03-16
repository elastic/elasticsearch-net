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

public sealed partial class UpdateUserProfileDataRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// Only perform the operation if the document has this primary term.
	/// </para>
	/// </summary>
	public long? IfPrimaryTerm { get => Q<long?>("if_primary_term"); set => Q("if_primary_term", value); }

	/// <summary>
	/// <para>
	/// Only perform the operation if the document has this sequence number.
	/// </para>
	/// </summary>
	public long? IfSeqNo { get => Q<long?>("if_seq_no"); set => Q("if_seq_no", value); }

	/// <summary>
	/// <para>
	/// If 'true', Elasticsearch refreshes the affected shards to make this operation
	/// visible to search.
	/// If 'wait_for', it waits for a refresh to make this operation visible to search.
	/// If 'false', nothing is done with refreshes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }
}

/// <summary>
/// <para>
/// Update user profile data.
/// </para>
/// <para>
/// Update specific data for the user profile that is associated with a unique ID.
/// </para>
/// <para>
/// NOTE: The user profile feature is designed only for use by Kibana and Elastic's Observability, Enterprise Search, and Elastic Security solutions.
/// Individual users and external applications should not call this API directly.
/// Elastic reserves the right to change or remove this feature in future releases without prior notice.
/// </para>
/// <para>
/// To use this API, you must have one of the following privileges:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// The <c>manage_user_profile</c> cluster privilege.
/// </para>
/// </item>
/// <item>
/// <para>
/// The <c>update_profile_data</c> global privilege for the namespaces that are referenced in the request.
/// </para>
/// </item>
/// </list>
/// <para>
/// This API updates the <c>labels</c> and <c>data</c> fields of an existing user profile document with JSON objects.
/// New keys and their values are added to the profile document and conflicting keys are replaced by data that's included in the request.
/// </para>
/// <para>
/// For both labels and data, content is namespaced by the top-level fields.
/// The <c>update_profile_data</c> global privilege grants privileges for updating only the allowed namespaces.
/// </para>
/// </summary>
public sealed partial class UpdateUserProfileDataRequest : PlainRequest<UpdateUserProfileDataRequestParameters>
{
	public UpdateUserProfileDataRequest(string uid) : base(r => r.Required("uid", uid))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityUpdateUserProfileData;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.update_user_profile_data";

	/// <summary>
	/// <para>
	/// Only perform the operation if the document has this primary term.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public long? IfPrimaryTerm { get => Q<long?>("if_primary_term"); set => Q("if_primary_term", value); }

	/// <summary>
	/// <para>
	/// Only perform the operation if the document has this sequence number.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public long? IfSeqNo { get => Q<long?>("if_seq_no"); set => Q("if_seq_no", value); }

	/// <summary>
	/// <para>
	/// If 'true', Elasticsearch refreshes the affected shards to make this operation
	/// visible to search.
	/// If 'wait_for', it waits for a refresh to make this operation visible to search.
	/// If 'false', nothing is done with refreshes.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }

	/// <summary>
	/// <para>
	/// Non-searchable data that you want to associate with the user profile.
	/// This field supports a nested data structure.
	/// Within the <c>data</c> object, top-level keys cannot begin with an underscore (<c>_</c>) or contain a period (<c>.</c>).
	/// The data object is not searchable, but can be retrieved with the get user profile API.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("data")]
	public IDictionary<string, object>? Data { get; set; }

	/// <summary>
	/// <para>
	/// Searchable data that you want to associate with the user profile.
	/// This field supports a nested data structure.
	/// Within the labels object, top-level keys cannot begin with an underscore (<c>_</c>) or contain a period (<c>.</c>).
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("labels")]
	public IDictionary<string, object>? Labels { get; set; }
}

/// <summary>
/// <para>
/// Update user profile data.
/// </para>
/// <para>
/// Update specific data for the user profile that is associated with a unique ID.
/// </para>
/// <para>
/// NOTE: The user profile feature is designed only for use by Kibana and Elastic's Observability, Enterprise Search, and Elastic Security solutions.
/// Individual users and external applications should not call this API directly.
/// Elastic reserves the right to change or remove this feature in future releases without prior notice.
/// </para>
/// <para>
/// To use this API, you must have one of the following privileges:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// The <c>manage_user_profile</c> cluster privilege.
/// </para>
/// </item>
/// <item>
/// <para>
/// The <c>update_profile_data</c> global privilege for the namespaces that are referenced in the request.
/// </para>
/// </item>
/// </list>
/// <para>
/// This API updates the <c>labels</c> and <c>data</c> fields of an existing user profile document with JSON objects.
/// New keys and their values are added to the profile document and conflicting keys are replaced by data that's included in the request.
/// </para>
/// <para>
/// For both labels and data, content is namespaced by the top-level fields.
/// The <c>update_profile_data</c> global privilege grants privileges for updating only the allowed namespaces.
/// </para>
/// </summary>
public sealed partial class UpdateUserProfileDataRequestDescriptor : RequestDescriptor<UpdateUserProfileDataRequestDescriptor, UpdateUserProfileDataRequestParameters>
{
	internal UpdateUserProfileDataRequestDescriptor(Action<UpdateUserProfileDataRequestDescriptor> configure) => configure.Invoke(this);

	public UpdateUserProfileDataRequestDescriptor(string uid) : base(r => r.Required("uid", uid))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityUpdateUserProfileData;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.update_user_profile_data";

	public UpdateUserProfileDataRequestDescriptor IfPrimaryTerm(long? ifPrimaryTerm) => Qs("if_primary_term", ifPrimaryTerm);
	public UpdateUserProfileDataRequestDescriptor IfSeqNo(long? ifSeqNo) => Qs("if_seq_no", ifSeqNo);
	public UpdateUserProfileDataRequestDescriptor Refresh(Elastic.Clients.Elasticsearch.Refresh? refresh) => Qs("refresh", refresh);

	public UpdateUserProfileDataRequestDescriptor Uid(string uid)
	{
		RouteValues.Required("uid", uid);
		return Self;
	}

	private IDictionary<string, object>? DataValue { get; set; }
	private IDictionary<string, object>? LabelsValue { get; set; }

	/// <summary>
	/// <para>
	/// Non-searchable data that you want to associate with the user profile.
	/// This field supports a nested data structure.
	/// Within the <c>data</c> object, top-level keys cannot begin with an underscore (<c>_</c>) or contain a period (<c>.</c>).
	/// The data object is not searchable, but can be retrieved with the get user profile API.
	/// </para>
	/// </summary>
	public UpdateUserProfileDataRequestDescriptor Data(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		DataValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// Searchable data that you want to associate with the user profile.
	/// This field supports a nested data structure.
	/// Within the labels object, top-level keys cannot begin with an underscore (<c>_</c>) or contain a period (<c>.</c>).
	/// </para>
	/// </summary>
	public UpdateUserProfileDataRequestDescriptor Labels(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		LabelsValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (DataValue is not null)
		{
			writer.WritePropertyName("data");
			JsonSerializer.Serialize(writer, DataValue, options);
		}

		if (LabelsValue is not null)
		{
			writer.WritePropertyName("labels");
			JsonSerializer.Serialize(writer, LabelsValue, options);
		}

		writer.WriteEndObject();
	}
}