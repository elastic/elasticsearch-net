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
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch
{
	public class UpdateRequestParameters : RequestParameters<UpdateRequestParameters>
	{
		[JsonIgnore]
		public long? IfPrimaryTerm { get => Q<long?>("if_primary_term"); set => Q("if_primary_term", value); }

		[JsonIgnore]
		public long? IfSeqNo { get => Q<long?>("if_seq_no"); set => Q("if_seq_no", value); }

		[JsonIgnore]
		public string? Lang { get => Q<string?>("lang"); set => Q("lang", value); }

		[JsonIgnore]
		public Refresh? Refresh { get => Q<Refresh?>("refresh"); set => Q("refresh", value); }

		[JsonIgnore]
		public bool? RequireAlias { get => Q<bool?>("require_alias"); set => Q("require_alias", value); }

		[JsonIgnore]
		public int? RetryOnConflict { get => Q<int?>("retry_on_conflict"); set => Q("retry_on_conflict", value); }

		[JsonIgnore]
		public string? Routing { get => Q<string?>("routing"); set => Q("routing", value); }

		[JsonIgnore]
		public Time? Timeout { get => Q<Time?>("timeout"); set => Q("timeout", value); }

		[JsonIgnore]
		public WaitForActiveShards? WaitForActiveShards { get => Q<WaitForActiveShards?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }

		[JsonIgnore]
		public Union<bool?, Elastic.Clients.Elasticsearch.Fields?>? Source { get => Q<Union<bool?, Elastic.Clients.Elasticsearch.Fields?>?>("_source"); set => Q("_source", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Fields? SourceExcludes { get => Q<Elastic.Clients.Elasticsearch.Fields?>("_source_excludes"); set => Q("_source_excludes", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Fields? SourceIncludes { get => Q<Elastic.Clients.Elasticsearch.Fields?>("_source_includes"); set => Q("_source_includes", value); }
	}

	public partial class UpdateRequest<TDocument, TPartialDocument> : PlainRequestBase<UpdateRequestParameters>
	{
		public UpdateRequest(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("index", index).Required("id", id))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.NoNamespaceUpdate;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		[JsonIgnore]
		public TDocument Document { get; set; }

		[JsonIgnore]
		public TPartialDocument PartialDocument { get; set; }

		[JsonIgnore]
		public long? IfPrimaryTerm { get => Q<long?>("if_primary_term"); set => Q("if_primary_term", value); }

		[JsonIgnore]
		public long? IfSeqNo { get => Q<long?>("if_seq_no"); set => Q("if_seq_no", value); }

		[JsonIgnore]
		public string? Lang { get => Q<string?>("lang"); set => Q("lang", value); }

		[JsonIgnore]
		public Refresh? Refresh { get => Q<Refresh?>("refresh"); set => Q("refresh", value); }

		[JsonIgnore]
		public bool? RequireAlias { get => Q<bool?>("require_alias"); set => Q("require_alias", value); }

		[JsonIgnore]
		public int? RetryOnConflict { get => Q<int?>("retry_on_conflict"); set => Q("retry_on_conflict", value); }

		[JsonIgnore]
		public string? Routing { get => Q<string?>("routing"); set => Q("routing", value); }

		[JsonIgnore]
		public Time? Timeout { get => Q<Time?>("timeout"); set => Q("timeout", value); }

		[JsonIgnore]
		public WaitForActiveShards? WaitForActiveShards { get => Q<WaitForActiveShards?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }

		[JsonIgnore]
		public Union<bool?, Elastic.Clients.Elasticsearch.Fields?>? Source { get => Q<Union<bool?, Elastic.Clients.Elasticsearch.Fields?>?>("_source"); set => Q("_source", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Fields? SourceExcludes { get => Q<Elastic.Clients.Elasticsearch.Fields?>("_source_excludes"); set => Q("_source_excludes", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Fields? SourceIncludes { get => Q<Elastic.Clients.Elasticsearch.Fields?>("_source_includes"); set => Q("_source_includes", value); }

		[JsonInclude]
		[JsonPropertyName("detect_noop")]
		public bool? DetectNoop { get; set; }

		[JsonInclude]
		[JsonPropertyName("doc")]
		public TPartialDocument? Doc { get; set; }

		[JsonInclude]
		[JsonPropertyName("doc_as_upsert")]
		public bool? DocAsUpsert { get; set; }

		[JsonInclude]
		[JsonPropertyName("script")]
		public Elastic.Clients.Elasticsearch.Script? Script { get; set; }

		[JsonInclude]
		[JsonPropertyName("scripted_upsert")]
		public bool? ScriptedUpsert { get; set; }

		[JsonInclude]
		[JsonPropertyName("upsert")]
		public TDocument? Upsert { get; set; }
	}

	public sealed partial class UpdateRequestDescriptor<TDocument, TPartialDocument> : RequestDescriptorBase<UpdateRequestDescriptor<TDocument, TPartialDocument>, UpdateRequestParameters>
	{
		public UpdateRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("index", index).Required("id", id))
		{
		}

		internal TDocument _document;
		internal TPartialDocument _partialDocument;
		internal bool? _detectNoop;
		internal TPartialDocument? _doc;
		internal bool? _docAsUpsert;
		internal Elastic.Clients.Elasticsearch.Script? _script;
		internal bool? _scriptedUpsert;
		internal TDocument? _upsert;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.NoNamespaceUpdate;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		public UpdateRequestDescriptor<TDocument, TPartialDocument> IfPrimaryTerm(long? ifPrimaryTerm) => Qs("if_primary_term", ifPrimaryTerm);
		public UpdateRequestDescriptor<TDocument, TPartialDocument> IfSeqNo(long? ifSeqNo) => Qs("if_seq_no", ifSeqNo);
		public UpdateRequestDescriptor<TDocument, TPartialDocument> Lang(string? lang) => Qs("lang", lang);
		public UpdateRequestDescriptor<TDocument, TPartialDocument> Refresh(Refresh? refresh) => Qs("refresh", refresh);
		public UpdateRequestDescriptor<TDocument, TPartialDocument> RequireAlias(bool? requireAlias) => Qs("require_alias", requireAlias);
		public UpdateRequestDescriptor<TDocument, TPartialDocument> RetryOnConflict(int? retryOnConflict) => Qs("retry_on_conflict", retryOnConflict);
		public UpdateRequestDescriptor<TDocument, TPartialDocument> Routing(string? routing) => Qs("routing", routing);
		public UpdateRequestDescriptor<TDocument, TPartialDocument> Timeout(Time? timeout) => Qs("timeout", timeout);
		public UpdateRequestDescriptor<TDocument, TPartialDocument> WaitForActiveShards(WaitForActiveShards? waitForActiveShards) => Qs("wait_for_active_shards", waitForActiveShards);
		public UpdateRequestDescriptor<TDocument, TPartialDocument> Source(Union<bool?, Elastic.Clients.Elasticsearch.Fields?>? source) => Qs("_source", source);
		public UpdateRequestDescriptor<TDocument, TPartialDocument> SourceExcludes(Elastic.Clients.Elasticsearch.Fields? sourceExcludes) => Qs("_source_excludes", sourceExcludes);
		public UpdateRequestDescriptor<TDocument, TPartialDocument> SourceIncludes(Elastic.Clients.Elasticsearch.Fields? sourceIncludes) => Qs("_source_includes", sourceIncludes);
		public UpdateRequestDescriptor<TDocument, TPartialDocument> Document(TDocument document) => Assign(document, (a, v) => a._document = v);
		public UpdateRequestDescriptor<TDocument, TPartialDocument> PartialDocument(TPartialDocument partialDocument) => Assign(partialDocument, (a, v) => a._partialDocument = v);
		public UpdateRequestDescriptor<TDocument, TPartialDocument> DetectNoop(bool? detectNoop = true) => Assign(detectNoop, (a, v) => a._detectNoop = v);
		public UpdateRequestDescriptor<TDocument, TPartialDocument> Doc(TPartialDocument? doc) => Assign(doc, (a, v) => a._doc = v);
		public UpdateRequestDescriptor<TDocument, TPartialDocument> DocAsUpsert(bool? docAsUpsert = true) => Assign(docAsUpsert, (a, v) => a._docAsUpsert = v);
		public UpdateRequestDescriptor<TDocument, TPartialDocument> Script(Elastic.Clients.Elasticsearch.Script? script) => Assign(script, (a, v) => a._script = v);
		public UpdateRequestDescriptor<TDocument, TPartialDocument> ScriptedUpsert(bool? scriptedUpsert = true) => Assign(scriptedUpsert, (a, v) => a._scriptedUpsert = v);
		public UpdateRequestDescriptor<TDocument, TPartialDocument> Upsert(TDocument? upsert) => Assign(upsert, (a, v) => a._upsert = v);
	}
}