using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IUpdateRequest<TDocument, TPartialDocument> : IUpdateRequest
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
		public UpdateDescriptor<TDocument, TPartialDocument> Upsert(TDocument upsertObject) => Assign(a => a.Upsert = upsertObject);

		/// <summary>
		/// The partial update document to be merged on to the existing object.
		/// </summary>
		public UpdateDescriptor<TDocument, TPartialDocument> Doc(TPartialDocument @object) => Assign(a => a.Doc = @object);

		public UpdateDescriptor<TDocument, TPartialDocument> DocAsUpsert(bool? docAsUpsert = true) => Assign(a => a.DocAsUpsert = docAsUpsert);

		public UpdateDescriptor<TDocument, TPartialDocument> DetectNoop(bool? detectNoop = true) => Assign(a => a.DetectNoop = detectNoop);

		public UpdateDescriptor<TDocument, TPartialDocument> ScriptedUpsert(bool? scriptedUpsert = true) =>
			Assign(a => a.ScriptedUpsert = scriptedUpsert);

		public UpdateDescriptor<TDocument, TPartialDocument> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptDescriptor()));

		public UpdateDescriptor<TDocument, TPartialDocument> Fields(Fields fields) =>
			Assign(a => a.RequestParameters.SetQueryString("fields", fields));

		public UpdateDescriptor<TDocument, TPartialDocument> Source(bool? enabled = true) => Assign(a => a.Source = enabled);

		public UpdateDescriptor<TDocument, TPartialDocument> Source(Func<SourceFilterDescriptor<TDocument>, ISourceFilter> selector) =>
			Assign(a => a.Source = new Union<bool, ISourceFilter>(selector?.Invoke(new SourceFilterDescriptor<TDocument>())));

		[Obsolete("Removed in Elasticsearch 7.x, use source filtering instead")]
		public UpdateDescriptor<TDocument, TPartialDocument> Fields(params Expression<Func<TPartialDocument, object>>[] typedPathLookups) =>
			Assign(a => a.RequestParameters.SetQueryString("fields", typedPathLookups));

		[Obsolete("Removed in Elasticsearch 7.x, use source filtering instead")]
		public UpdateDescriptor<TDocument, TPartialDocument> Fields(params string[] fields) =>
			Assign(a => a.RequestParameters.SetQueryString("fields", fields));
	}
}
