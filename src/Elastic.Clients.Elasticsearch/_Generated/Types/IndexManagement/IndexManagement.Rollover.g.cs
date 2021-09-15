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

using Elastic.Transport.Products.Elasticsearch.Failures;
using OneOf;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.IndexManagement.Rollover
{
	[ConvertAs(typeof(RolloverConditions))]
	public partial interface IRolloverConditions
	{
		Elastic.Clients.Elasticsearch.Time? MaxAge { get; set; }

		long? MaxDocs { get; set; }

		Elastic.Clients.Elasticsearch.ByteSize? MaxPrimaryShardSize { get; set; }

		string? MaxSize { get; set; }
	}

	public partial class RolloverConditionsDescriptor : DescriptorBase<RolloverConditionsDescriptor, IRolloverConditions>, IRolloverConditions
	{
		Elastic.Clients.Elasticsearch.Time? IRolloverConditions.MaxAge { get; set; }

		long? IRolloverConditions.MaxDocs { get; set; }

		string? IRolloverConditions.MaxSize { get; set; }

		Elastic.Clients.Elasticsearch.ByteSize? IRolloverConditions.MaxPrimaryShardSize { get; set; }
	}

	public partial class RolloverConditions : IRolloverConditions
	{
		[JsonInclude]
		[JsonPropertyName("max_age")]
		public Elastic.Clients.Elasticsearch.Time? MaxAge { get; set; }

		[JsonInclude]
		[JsonPropertyName("max_docs")]
		public long? MaxDocs { get; set; }

		[JsonInclude]
		[JsonPropertyName("max_primary_shard_size")]
		public Elastic.Clients.Elasticsearch.ByteSize? MaxPrimaryShardSize { get; set; }

		[JsonInclude]
		[JsonPropertyName("max_size")]
		public string? MaxSize { get; set; }
	}
}