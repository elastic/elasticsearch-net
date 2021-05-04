// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[MapsApi("update.json")]
	public partial interface IUpdateRequest<TDocument, TPartialDocument>
		where TDocument : class
		where TPartialDocument : class
	{
		[DataMember(Name = "detect_noop")]
		bool? DetectNoop { get; set; }

		[DataMember(Name = "doc")]
		[JsonFormatter(typeof(SourceFormatter<>))]
		TPartialDocument Doc { get; set; }

		[DataMember(Name = "doc_as_upsert")]
		bool? DocAsUpsert { get; set; }

		[DataMember(Name = "script")]
		IScript Script { get; set; }

		/// <summary>
		/// If you would like your script to run regardless of whether the document exists or not — i.e. the script handles
		/// initializing the document instead of the upsert element — then set scripted_upsert to true
		/// </summary>
		[DataMember(Name = "scripted_upsert")]
		bool? ScriptedUpsert { get; set; }

		[DataMember(Name = "_source")]
		Union<bool, ISourceFilter> Source { get; set; }

		[DataMember(Name = "upsert")]
		[JsonFormatter(typeof(SourceFormatter<>))]
		TDocument Upsert { get; set; }
	}

	public partial class UpdateRequest<TDocument, TPartialDocument>
		where TDocument : class
		where TPartialDocument : class
	{
		/// <inheritdoc />
		public bool? DetectNoop { get; set; }

		/// <inheritdoc />
		public TPartialDocument Doc { get; set; }

		/// <inheritdoc />
		public bool? DocAsUpsert { get; set; }

		/// <inheritdoc />
		public IScript Script { get; set; }

		/// <inheritdoc />
		public bool? ScriptedUpsert { get; set; }

		/// <inheritdoc />
		public Union<bool, ISourceFilter> Source { get; set; }

		/// <inheritdoc />
		public TDocument Upsert { get; set; }
	}

	public partial class UpdateDescriptor<TDocument, TPartialDocument>
		where TDocument : class
		where TPartialDocument : class
	{
		bool? IUpdateRequest<TDocument, TPartialDocument>.DetectNoop { get; set; }

		TPartialDocument IUpdateRequest<TDocument, TPartialDocument>.Doc { get; set; }

		bool? IUpdateRequest<TDocument, TPartialDocument>.DocAsUpsert { get; set; }

		IScript IUpdateRequest<TDocument, TPartialDocument>.Script { get; set; }

		bool? IUpdateRequest<TDocument, TPartialDocument>.ScriptedUpsert { get; set; }

		Union<bool, ISourceFilter> IUpdateRequest<TDocument, TPartialDocument>.Source { get; set; }

		TDocument IUpdateRequest<TDocument, TPartialDocument>.Upsert { get; set; }

		/// <summary>
		/// The full document to be created if an existing document does not exist for a partial merge.
		/// </summary>
		public UpdateDescriptor<TDocument, TPartialDocument> Upsert(TDocument upsertObject) => Assign(upsertObject, (a, v) => a.Upsert = v);

		/// <summary>
		/// The partial update document to be merged on to the existing object.
		/// </summary>
		public UpdateDescriptor<TDocument, TPartialDocument> Doc(TPartialDocument @object) => Assign(@object, (a, v) => a.Doc = v);

		public UpdateDescriptor<TDocument, TPartialDocument> DocAsUpsert(bool? docAsUpsert = true) => Assign(docAsUpsert, (a, v) => a.DocAsUpsert = v);

		public UpdateDescriptor<TDocument, TPartialDocument> DetectNoop(bool? detectNoop = true) => Assign(detectNoop, (a, v) => a.DetectNoop = v);

		public UpdateDescriptor<TDocument, TPartialDocument> ScriptedUpsert(bool? scriptedUpsert = true) =>
			Assign(scriptedUpsert, (a, v) => a.ScriptedUpsert = v);

		public UpdateDescriptor<TDocument, TPartialDocument> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));

		public UpdateDescriptor<TDocument, TPartialDocument> Source(bool? enabled = true) => Assign(enabled, (a, v) => a.Source = v);

		public UpdateDescriptor<TDocument, TPartialDocument> Source(Func<SourceFilterDescriptor<TDocument>, ISourceFilter> selector) =>
			Assign(selector, (a, v) => a.Source = new Union<bool, ISourceFilter>(v?.Invoke(new SourceFilterDescriptor<TDocument>())));
	}
}
