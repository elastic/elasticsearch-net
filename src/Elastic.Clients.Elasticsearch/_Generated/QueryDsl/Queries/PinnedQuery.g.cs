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

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.QueryDsl
{
	[ConvertAs(typeof(PinnedQuery))]
	public partial interface IPinnedQuery : IQuery
	{
		IEnumerable<Elastic.Clients.Elasticsearch.Id> Ids { get; set; }

		Elastic.Clients.Elasticsearch.QueryDsl.IQueryContainer Organic { get; set; }
	}

	public partial class PinnedQuery : QueryBase, IPinnedQuery
	{
		[JsonInclude]
		[JsonPropertyName("ids")]
		public IEnumerable<Elastic.Clients.Elasticsearch.Id> Ids { get; set; }

		[JsonInclude]
		[JsonPropertyName("organic")]
		public Elastic.Clients.Elasticsearch.QueryDsl.IQueryContainer Organic { get; set; }
	}

	public partial class PinnedQueryDescriptor : QueryDescriptorBase<PinnedQueryDescriptor, IPinnedQuery>, IPinnedQuery
	{
		IEnumerable<Elastic.Clients.Elasticsearch.Id> IPinnedQuery.Ids { get; set; }

		Elastic.Clients.Elasticsearch.QueryDsl.IQueryContainer IPinnedQuery.Organic { get; set; }
	}
}