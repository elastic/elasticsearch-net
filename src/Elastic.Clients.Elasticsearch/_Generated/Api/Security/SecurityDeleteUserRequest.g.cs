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

using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Security
{
	public sealed class SecurityDeleteUserRequestParameters : RequestParameters<SecurityDeleteUserRequestParameters>
	{
		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }
	}

	public sealed partial class SecurityDeleteUserRequest : PlainRequestBase<SecurityDeleteUserRequestParameters>
	{
		public SecurityDeleteUserRequest(Elastic.Clients.Elasticsearch.Username username) : base(r => r.Required("username", username))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityDeleteUser;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override bool SupportsBody => false;
		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }
	}

	public sealed partial class SecurityDeleteUserRequestDescriptor : RequestDescriptorBase<SecurityDeleteUserRequestDescriptor, SecurityDeleteUserRequestParameters>
	{
		internal SecurityDeleteUserRequestDescriptor(Action<SecurityDeleteUserRequestDescriptor> configure) => configure.Invoke(this);
		public SecurityDeleteUserRequestDescriptor(Elastic.Clients.Elasticsearch.Username username) : base(r => r.Required("username", username))
		{
		}

		internal SecurityDeleteUserRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityDeleteUser;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override bool SupportsBody => false;
		public SecurityDeleteUserRequestDescriptor Refresh(Elastic.Clients.Elasticsearch.Refresh? refresh) => Qs("refresh", refresh);
		public SecurityDeleteUserRequestDescriptor Username(Elastic.Clients.Elasticsearch.Username username)
		{
			RouteValues.Required("username", username);
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
		}
	}
}