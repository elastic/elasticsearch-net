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
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport.Products.Elasticsearch;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.SearchApplication;

public sealed partial class GetBehavioralAnalyticsResponse : DictionaryResponse<Elastic.Clients.Elasticsearch.Name, Elastic.Clients.Elasticsearch.SearchApplication.AnalyticsCollection>
{
	public GetBehavioralAnalyticsResponse(IReadOnlyDictionary<Elastic.Clients.Elasticsearch.Name, Elastic.Clients.Elasticsearch.SearchApplication.AnalyticsCollection> dictionary) : base(dictionary)
	{
	}

	public GetBehavioralAnalyticsResponse() : base()
	{
	}
}