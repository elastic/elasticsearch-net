using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IUpdateRequest<TDocument, TPartialDocument>
		where TDocument : class
		where TPartialDocument : class
	{
		[JsonProperty(PropertyName = "script")]
		IScript Script { get; set; }

		[JsonProperty(PropertyName = "upsert")]
		TDocument Upsert { get; set; }

		[JsonProperty(PropertyName = "doc_as_upsert")]
		bool? DocAsUpsert { get; set; }

		[JsonProperty(PropertyName = "doc")]
		TPartialDocument Doc { get; set; }

		[JsonProperty(PropertyName = "detect_noop")]
		bool? DetectNoop { get; set; }
	}

	public partial class UpdateRequest<TDocument, TPartialDocument>
		where TDocument : class
		where TPartialDocument : class
	{
		public IScript Script { get; set; }
		public TDocument Upsert { get; set; }
		public bool? DocAsUpsert { get; set; }
		public TPartialDocument Doc { get; set; }
		public bool? DetectNoop { get; set; }

		public Fields Fields
		{
			get { return Self.RequestParameters.GetQueryStringValue<Fields>("fields"); }
			set { Self.RequestParameters.AddQueryString("fields", value); }
		}
	}

	public partial class UpdateDescriptor<TDocument, TPartialDocument>
		where TDocument : class
		where TPartialDocument : class
	{
		IScript IUpdateRequest<TDocument, TPartialDocument>.Script { get; set; }

		TDocument IUpdateRequest<TDocument, TPartialDocument>.Upsert { get; set; }

		bool? IUpdateRequest<TDocument, TPartialDocument>.DocAsUpsert { get; set; }

		TPartialDocument IUpdateRequest<TDocument, TPartialDocument>.Doc { get; set; }

		bool? IUpdateRequest<TDocument, TPartialDocument>.DetectNoop { get; set; }

		/// <summary>
		/// The full document to be created if an existing document does not exist for a partial merge.
		/// </summary>
		public UpdateDescriptor<TDocument, TPartialDocument> Upsert(TDocument upsertObject) => Assign(a => a.Upsert = upsertObject);

		/// <summary>
		/// The partial update document to be merged on to the existing object.
		/// </summary>
		public UpdateDescriptor<TDocument, TPartialDocument> Doc(TPartialDocument @object) => Assign(a => a.Doc = @object);

		public UpdateDescriptor<TDocument, TPartialDocument> DocAsUpsert(bool docAsUpsert = true) => Assign(a => a.DocAsUpsert = docAsUpsert);

		public UpdateDescriptor<TDocument, TPartialDocument> DetectNoop(bool detectNoop = true) => Assign(a => a.DetectNoop = detectNoop);

		public UpdateDescriptor<TDocument, TPartialDocument> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptDescriptor()));

		public UpdateDescriptor<TDocument, TPartialDocument> Fields(Fields fields) =>
			Assign(a => a.RequestParameters.AddQueryString("fields", fields));

		public UpdateDescriptor<TDocument, TPartialDocument> Fields(params Expression<Func<TPartialDocument, object>>[] typedPathLookups) =>
			Assign(a => a.RequestParameters.AddQueryString("fields", typedPathLookups));

		public UpdateDescriptor<TDocument, TPartialDocument> Fields(params string[] fields) =>
			Assign(a => a.RequestParameters.AddQueryString("fields", fields));
	}
}
