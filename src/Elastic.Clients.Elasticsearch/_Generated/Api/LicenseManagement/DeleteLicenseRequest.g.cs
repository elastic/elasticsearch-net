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

namespace Elastic.Clients.Elasticsearch.LicenseManagement;

public sealed partial class DeleteLicenseRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Deletes licensing information for the cluster
/// </para>
/// </summary>
public sealed partial class DeleteLicenseRequest : PlainRequest<DeleteLicenseRequestParameters>
{
	internal override ApiUrls ApiUrls => ApiUrlLookup.LicenseManagementDelete;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "license.delete";
}

/// <summary>
/// <para>
/// Deletes licensing information for the cluster
/// </para>
/// </summary>
public sealed partial class DeleteLicenseRequestDescriptor : RequestDescriptor<DeleteLicenseRequestDescriptor, DeleteLicenseRequestParameters>
{
	internal DeleteLicenseRequestDescriptor(Action<DeleteLicenseRequestDescriptor> configure) => configure.Invoke(this);

	public DeleteLicenseRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.LicenseManagementDelete;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "license.delete";

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}