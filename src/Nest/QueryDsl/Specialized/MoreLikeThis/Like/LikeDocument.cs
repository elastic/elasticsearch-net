using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<LikeDocument<object>>))]
	public interface ILikeDocument
	{
		[JsonProperty("doc")]
		[JsonConverter(typeof(SourceConverter))]
		object Document { get; set; }

		[JsonProperty("fields")]
		Fields Fields { get; set; }

		[JsonProperty("_id")]
		Id Id { get; set; }

		[JsonProperty("_index")]
		IndexName Index { get; set; }

		[JsonProperty("per_field_analyzer")]
		IPerFieldAnalyzer PerFieldAnalyzer { get; set; }

		[JsonProperty("_routing")]
		Routing Routing { get; set; }
	}

	public abstract class LikeDocumentBase : ILikeDocument
	{
		private Routing _routing;

		public object Document { get; set; }

		public Fields Fields { get; set; }

		public Id Id { get; set; }
		public IndexName Index { get; set; }

		public IPerFieldAnalyzer PerFieldAnalyzer { get; set; }

		public Routing Routing
		{
			get => _routing ?? (Document == null ? null : new Routing(Document));
			set => _routing = value;
		}
	}

	public class LikeDocument<TDocument> : LikeDocumentBase
		where TDocument : class
	{
		internal LikeDocument() { }

		public LikeDocument(Id id)
		{
			Id = id;
			Index = typeof(TDocument);
		}

		public LikeDocument(TDocument document)
		{
			Id = Id.From(document);
			Document = document;
			Index = typeof(TDocument);
		}
	}

	public class LikeDocumentDescriptor<TDocument> : DescriptorBase<LikeDocumentDescriptor<TDocument>, ILikeDocument>, ILikeDocument
		where TDocument : class
	{
		private Routing _routing;

		public LikeDocumentDescriptor() => Self.Index = typeof(TDocument);

		object ILikeDocument.Document { get; set; }

		Fields ILikeDocument.Fields { get; set; }
		Id ILikeDocument.Id { get; set; }
		IndexName ILikeDocument.Index { get; set; }
		IPerFieldAnalyzer ILikeDocument.PerFieldAnalyzer { get; set; }

		Routing ILikeDocument.Routing
		{
			get => _routing ?? (Self.Document == null ? null : new Routing(Self.Document));
			set => _routing = value;
		}

		public LikeDocumentDescriptor<TDocument> Index(IndexName index) => Assign(a => a.Index = index);

		public LikeDocumentDescriptor<TDocument> Id(Id id) => Assign(a => a.Id = id);

		public LikeDocumentDescriptor<TDocument> Routing(Routing routing) => Assign(a => a.Routing = routing);

		public LikeDocumentDescriptor<TDocument> Fields(Func<FieldsDescriptor<TDocument>, IPromise<Fields>> fields) =>
			Assign(a => a.Fields = fields?.Invoke(new FieldsDescriptor<TDocument>())?.Value);

		public LikeDocumentDescriptor<TDocument> Fields(Fields fields) => Assign(a => a.Fields = fields);

		public LikeDocumentDescriptor<TDocument> Document(TDocument document) => Assign(a =>
		{
			a.Id = Infer.Id(document);
			a.Document = document;
		});

		public LikeDocumentDescriptor<TDocument> PerFieldAnalyzer(
			Func<PerFieldAnalyzerDescriptor<TDocument>, IPromise<IPerFieldAnalyzer>> analyzerSelector
		) =>
			Assign(a => a.PerFieldAnalyzer = analyzerSelector?.Invoke(new PerFieldAnalyzerDescriptor<TDocument>())?.Value);
	}
}
