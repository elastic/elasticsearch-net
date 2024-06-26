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
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.Xpack;

public sealed partial class EqlFeatures
{
	[JsonInclude, JsonPropertyName("event")]
	public int Event { get; init; }
	[JsonInclude, JsonPropertyName("join")]
	public int Join { get; init; }
	[JsonInclude, JsonPropertyName("joins")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.EqlFeaturesJoin Joins { get; init; }
	[JsonInclude, JsonPropertyName("keys")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.EqlFeaturesKeys Keys { get; init; }
	[JsonInclude, JsonPropertyName("pipes")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.EqlFeaturesPipes Pipes { get; init; }
	[JsonInclude, JsonPropertyName("sequence")]
	public int Sequence { get; init; }
	[JsonInclude, JsonPropertyName("sequences")]
	public Elastic.Clients.Elasticsearch.Serverless.Xpack.EqlFeaturesSequences Sequences { get; init; }
}