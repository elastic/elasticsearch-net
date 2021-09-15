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
using Elastic.Transport;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.QueryDsl
{
	[ConvertAs(typeof(MatchPhraseQuery))]
	public partial interface IMatchPhraseQuery : IFieldNameQuery
	{
		string? Analyzer { get; set; }

		string Query { get; set; }

		int? Slop { get; set; }

		Elastic.Clients.Elasticsearch.QueryDsl.ZeroTermsQuery? ZeroTermsQuery { get; set; }
	}

	public partial class MatchPhraseQuery : FieldNameQueryBase, IMatchPhraseQuery
	{
		[JsonInclude]
		[JsonPropertyName("analyzer")]
		public string? Analyzer { get; set; }

		[JsonInclude]
		[JsonPropertyName("query")]
		public string Query { get; set; }

		[JsonInclude]
		[JsonPropertyName("slop")]
		public int? Slop { get; set; }

		[JsonInclude]
		[JsonPropertyName("zero_terms_query")]
		public Elastic.Clients.Elasticsearch.QueryDsl.ZeroTermsQuery? ZeroTermsQuery { get; set; }
	}

	public partial class MatchPhraseQueryDescriptor : FieldNameQueryDescriptorBase<MatchPhraseQueryDescriptor, IMatchPhraseQuery>, IMatchPhraseQuery
	{
		string? IMatchPhraseQuery.Analyzer { get; set; }

		string IMatchPhraseQuery.Query { get; set; }

		int? IMatchPhraseQuery.Slop { get; set; }

		Elastic.Clients.Elasticsearch.QueryDsl.ZeroTermsQuery? IMatchPhraseQuery.ZeroTermsQuery { get; set; }
	}
}