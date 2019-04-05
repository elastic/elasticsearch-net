using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IUpdateRequest<TDocument, TPartialDocument>
		where TDocument : class
		where TPartialDocument : class
	{
		[JsonProperty("detect_noop")]
		bool? DetectNoop { get; set; }

		[JsonProperty("doc")]
		[JsonConverter(typeof(SourceConverter))]
		TPartialDocument Doc { get; set; }

		[JsonProperty("doc_as_upsert")]
		bool? DocAsUpsert { get; set; }

		[JsonProperty("script")]
		IScript Script { get; set; }

		/// <summary>
		/// If you would like your script to run regardless of whether the document exists or not — i.e. the script handles
		/// initializing the document instead of the upsert element — then set scripted_upsert to true
		/// </summary>
		[JsonProperty("scripted_upsert")]
		bool? ScriptedUpsert { get; set; }

		[JsonProperty("_source")]
		Union<bool, ISourceFilter> Source { get; set; }

		[JsonProperty("upsert")]
		[JsonConverter(typeof(SourceConverter))]
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
		[Obsolete("Removed in Elasticsearch 7.x, use source filtering instead")]
		public Fields Fields
		{
			get => Self.RequestParameters.GetQueryStringValue<Fields>("fields");
			set => Self.RequestParameters.SetQueryString("fields", value);
		}

		/// <inheritdoc />
		public IScript Script { get; set; }

		/// <inheritdoc />
		public bool? ScriptedUpsert { get; set; }

		/// <inheritdoc />
		public Union<bool, ISourceFilter> Source { get; set; }

		/// <inheritdoc />
		public TDocument Upsert { get; set; }

		private object AutoRouteDocument() => (object)Self.Upsert ?? Self.Doc;
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

		private object AutoRouteDocument() => (object)Self.Upsert ?? Self.Doc;

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

		public UpdateDescriptor<TDocument, TPartialDocument> Fields(Fields fields) =>
			Assign(fields, (a, v) => a.RequestParameters.SetQueryString("fields", v));

		public UpdateDescriptor<TDocument, TPartialDocument> Source(bool? enabled = true) => Assign(enabled, (a, v) => a.Source = v);

		public UpdateDescriptor<TDocument, TPartialDocument> Source(Func<SourceFilterDescriptor<TDocument>, ISourceFilter> selector) =>
			Assign(selector, (a, v) => a.Source = new Union<bool, ISourceFilter>(v?.Invoke(new SourceFilterDescriptor<TDocument>())));

		[Obsolete("Removed in Elasticsearch 7.x, use source filtering instead")]
		public UpdateDescriptor<TDocument, TPartialDocument> Fields(params Expression<Func<TPartialDocument, object>>[] typedPathLookups) =>
			Assign(typedPathLookups,(a, v) => a.RequestParameters.SetQueryString("fields", v));

		[Obsolete("Removed in Elasticsearch 7.x, use source filtering instead")]
		public UpdateDescriptor<TDocument, TPartialDocument> Fields(params string[] fields) =>
			Assign(fields,(a, v) => a.RequestParameters.SetQueryString("fields", v));
	}
}
