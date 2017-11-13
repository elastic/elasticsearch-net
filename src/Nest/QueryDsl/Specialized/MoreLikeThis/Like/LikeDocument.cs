using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<LikeDocument<object>>))]
	public interface ILikeDocument
	{
		[JsonProperty("_index")]
		IndexName Index { get; set; }

		[JsonProperty("_type")]
		TypeName Type { get; set; }

		[JsonProperty("_id")]
		Id Id { get; set; }

		[JsonProperty("fields")]
		Fields Fields { get; set; }

		[JsonProperty("_routing")]
		string Routing { get; set; }

		[JsonProperty("doc")]
		[JsonConverter(typeof(SourceConverter))]
		object Document { get; set; }

		[JsonProperty("per_field_analyzer")]
		IPerFieldAnalyzer PerFieldAnalyzer { get; set; }
	}

	public abstract class LikeDocumentBase : ILikeDocument
	{
		public IndexName Index { get; set; }

		public TypeName Type { get; set; }

		public Id Id { get; set; }

		public Fields Fields { get; set; }

		public string Routing { get; set; }

		public object Document { get; set; }

		public IPerFieldAnalyzer PerFieldAnalyzer { get; set; }
	}

	public class LikeDocument<TDocument> : LikeDocumentBase
		where TDocument : class
	{
		internal LikeDocument() { }

		public LikeDocument(Id id)
		{
			this.Id = id;
			this.Index = typeof(TDocument);
			this.Type = typeof(TDocument);
		}
		public LikeDocument(TDocument document)
		{
			this.Id = Id.From(document);
			this.Document = document;
			this.Index = typeof(TDocument);
			this.Type = typeof(TDocument);
		}
	}

	public class LikeDocumentDescriptor<TDocument> : DescriptorBase<LikeDocumentDescriptor<TDocument>, ILikeDocument>, ILikeDocument
		where TDocument : class
	{
		IndexName ILikeDocument.Index { get; set; }
		TypeName ILikeDocument.Type { get; set; }
		Id ILikeDocument.Id { get; set; }
		string ILikeDocument.Routing { get; set; }
		Fields ILikeDocument.Fields { get; set; }
		object ILikeDocument.Document { get; set; }
		IPerFieldAnalyzer ILikeDocument.PerFieldAnalyzer { get; set; }

		public LikeDocumentDescriptor()
		{
			Self.Index = typeof(TDocument);
			Self.Type = typeof(TDocument);
		}

		public LikeDocumentDescriptor<TDocument> Index(IndexName index) => Assign(a => a.Index = index);

		public LikeDocumentDescriptor<TDocument> Type(TypeName type) => Assign(a => a.Type = type);

		public LikeDocumentDescriptor<TDocument> Id(Id id) => Assign(a => a.Id = id);

		public LikeDocumentDescriptor<TDocument> Routing(string routing) => Assign(a => a.Routing = routing);

		public LikeDocumentDescriptor<TDocument> Fields(Func<FieldsDescriptor<TDocument>, IPromise<Fields>> fields) =>
			Assign(a => a.Fields = fields?.Invoke(new FieldsDescriptor<TDocument>())?.Value);

		public LikeDocumentDescriptor<TDocument> Fields(Fields fields) => Assign(a => a.Fields = fields);

		public LikeDocumentDescriptor<TDocument> Document(TDocument document) => Assign(a =>
		{
			a.Id = Nest.Infer.Id(document);
			a.Document = document;
		});

		public LikeDocumentDescriptor<TDocument> PerFieldAnalyzer(Func<PerFieldAnalyzerDescriptor<TDocument>, IPromise<IPerFieldAnalyzer>> analyzerSelector) =>
			Assign(a => a.PerFieldAnalyzer = analyzerSelector?.Invoke(new PerFieldAnalyzerDescriptor<TDocument>())?.Value);

	}
}
